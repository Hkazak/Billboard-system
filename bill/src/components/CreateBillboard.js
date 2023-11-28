import {useEffect, useId, useState} from "react";
import {GetGroupOfTariffsList} from "../lib/controllers/TariffGroupController";
import {getBillboardSurfacesList} from "../lib/controllers/TarrifsController";
import {GetBillboardTypes} from "../lib/controllers/BillboardTypesController";
import UploadIcon from "../assets/upload.svg";
import "../styles/CreateBillboard.css";

function CreateBillboard({hide = true, setHide, handleNewBillboard})
{
    const [groupOfTariffsList, setGroupOfTariffsList] = useState([]);
    const [surfacesList, setSurfacesList] = useState([]);
    const [billboardTypesList, setBillboardTypesList] = useState([]);
    const [uploadedPictures, setUploadedPictures] = useState([]);

    function handleCreateBillboard()
    {

    }

    function handleUploadFiles(ev)
    {
        const pictures = [...uploadedPictures];
        for(let i = 0; i < ev.target.files.length; ++i)
        {
            const file = ev.target.files[i];
            const picture = {
                id: pictures.length + i,
                name: file.name,
                data: URL.createObjectURL(file)
            };
            pictures.push(picture);
        }
        setUploadedPictures([...pictures]);
    }

    useEffect(()=>{
        GetGroupOfTariffsList()
            .then(e=>e.json())
            .then(e=>setGroupOfTariffsList(e));
        getBillboardSurfacesList()
            .then(e=>e.json())
            .then(e=>setSurfacesList(e));
        GetBillboardTypes()
            .then(e=>e.json())
            .then(e=>setBillboardTypesList(e));
    }, []);
    return (
        <div className="create-billboard-block" hidden={hide}>
            <span className="create-billboard-title">
                Новая акция
            </span>
            <div className="create-billboard-general-information">
                <span className="create-billboard-general-span">Общая информация</span>
                <input required type="text" className="billboard-data-input" placeholder="Название" />
                <textarea required className="billboard-data-textarea" placeholder="Описание" />
                <select className="billboard-data-input" defaultValue={0}>
                    <option>Группа тарифов</option>
                    {groupOfTariffsList.map(e=><option key={e.id}>{e.name}</option>)}
                </select>
                <select className="billboard-data-input" defaultValue={0}>
                    <option>Тип экспанирования</option>
                    {surfacesList.map(e=><option key={e.id}>{e.surface}</option>)}
                </select>
                <select className="billboard-data-input" defaultValue={0}>
                    <option>Вид поверхности</option>
                    {billboardTypesList.map(e=><option key={e}>{e}</option>)}
                </select>
                <input required type="number" className="billboard-data-input"/>
                <input required type="number" className="billboard-data-input"/>
                <span className="create-billboard-general-span">Размер</span>
                <input required type="number" className="billboard-data-short-input"/>
                <span className="billboard-size-unit">M</span>
                <input required type="number" className="billboard-data-short-input"/>
                <span className="billboard-size-unit">M</span>
                <span className="create-billboard-general-span">Фотографии</span>
                <div className="upload-file-block">
                    <label htmlFor="upload-file" className="upload-picture-block">
                        <input type="file" name="upload-file" multiple className="upload-pictures-button" onChange={handleUploadFiles} accept="image/jpeg,image/png"/>
                        <img src={UploadIcon} alt="" className="upload-picture-block-icon" width="50" height="50"/>
                        <span className="upload-picture-text">Нажмите или перетащите файлы</span>
                    </label>
                    <div className="pictures-container">
                        {uploadedPictures.map(e=><img src={e.data} key={e.id} alt={e.name} width="100" height="100"/>)}
                    </div>
                </div>
            </div>
            <div className="manage-buttons">
                <button className="create-billboard-button" onClick={()=>handleCreateBillboard()}>
                    Создать
                </button>
                <button className="cancel-create-billboard-button" onClick={()=>setHide(true)}>
                    Отмена
                </button>
            </div>
        </div>
    );
}

export default CreateBillboard;