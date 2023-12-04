import UploadIcon from "../assets/upload.svg";
import {useState} from "react";
import "../styles/UploadPicture.css";
import PicturesContainer from "./PicturesContainer";

function UploadPictures({onUpload}) {
    const [uploadedPictures, setUploadedPictures] = useState([]);

    function handleUploadFiles(ev) {
        const files = ev.target.files;
        const pictures = [...uploadedPictures];
        for (let i = 0; i < files.length; ++i) {
            const file = files[i];
            console.log(file);
            const reader = new FileReader();
            reader.onload = (e) => {
                const picture = {
                    id: pictures.length,
                    name: file.name,
                    data: e.target.result,
                };
                pictures.push(picture);
                if (pictures.length === ev.target.files.length) {
                    setUploadedPictures([...pictures]);
                    onUpload([...pictures]);
                }
            }
            reader.readAsDataURL(file);
        }
    }

    return (
        <div className="upload-file-block">
            <label htmlFor="upload-file" className="upload-picture-block">
                <input type="file" name="upload-file" multiple className="upload-pictures-button"
                       onChange={handleUploadFiles} accept="image/jpeg,image/png"/>
                <img src={UploadIcon} alt="" className="upload-picture-block-icon" width="50" height="50"/>
                <span className="upload-picture-text">Нажмите или перетащите файлы</span>
            </label>
            <PicturesContainer pictures={uploadedPictures}/>
        </div>
    );
}

export default UploadPictures;