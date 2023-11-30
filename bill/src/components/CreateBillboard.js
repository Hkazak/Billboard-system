import {useEffect, useState} from "react";
import {GetGroupOfTariffsList} from "../lib/controllers/TariffGroupController";
import {getBillboardSurfacesList} from "../lib/controllers/TarrifsController";
import {GetBillboardTypes} from "../lib/controllers/BillboardTypesController";
import UploadIcon from "../assets/upload.svg";
import "../styles/CreateBillboard.css";
import {CreateBillboardRequest} from "../lib/controllers/BillboardController";

function CreateBillboard({hide = true, setHide, handleNewBillboard})
{
    const [groupOfTariffsList, setGroupOfTariffsList] = useState([]);
    const [surfacesList, setSurfacesList] = useState([]);
    const [billboardTypesList, setBillboardTypesList] = useState([]);
    const [uploadedPictures, setUploadedPictures] = useState([]);
    const [billboardName, setBillboardName] = useState('');
    const [billboardDescription, setBillboardDescription] = useState('');
    const [address, setAddress] = useState('');
    const [groupOfTariffId, setGroupOfTariffId] = useState('');
    const [surfaceId, setSurfaceId] = useState('');
    const [billboardType, setBillboardType] = useState('');
    const [penalty, setPenalty] = useState(0);
    const [width, setWidth] = useState(0);
    const [height, setHeight] = useState(0);

    function handleCreateBillboard(e)
    {
        e.preventDefault();
        CreateBillboardRequest(billboardName, billboardDescription, address, groupOfTariffId, billboardType, surfaceId, penalty, height, width, uploadedPictures)
            .then(e=> e.json())
            .then(e=>handleNewBillboard(e));
    }

    function handleUploadFiles(ev)
    {
        const files = ev.target.files;
        const pictures = [...uploadedPictures];
        for(let i = 0; i < files.length; ++i)
        {
            const file = files[i];
            console.log(file);
            const reader = new FileReader();
            reader.onload = (e  )=>
            {
                const picture = {
                    id: pictures.length,
                    name: file.name,
                    data: e.target.result,
                };
                pictures.push(picture);
                if(pictures.length === ev.target.files.length)
                {
                    setUploadedPictures([...pictures]);
                }
            }
            reader.readAsDataURL(file);
        }
    }

    function resetPanel(e)
    {
        e.preventDefault();
        setHide(true);
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
        <form className="create-billboard-block" hidden={hide}>
            <span className="create-billboard-title">
                Новая акция
            </span>
            <div className="create-billboard-general-information">
                <span className="create-billboard-general-span">Общая информация</span>
                <input required type="text" className="billboard-data-input" placeholder="Название" onChange={e=>setBillboardName(e.target.value)} />
                <textarea required className="billboard-data-textarea" placeholder="Описание" onChange={e=>setBillboardDescription(e.target.value)} />
                <input required type="text" className="billboard-data-input" placeholder="Адрес" onChange={e=>setAddress(e.target.value)} />
                <select className="billboard-data-input" defaultValue={0} onChange={e=>setGroupOfTariffId(groupOfTariffsList[e.target.selectedIndex-1].id)}>
                    <option>Группа тарифов</option>
                    {groupOfTariffsList.map(e=><option key={e.id}>{e.name}</option>)}
                </select>
                <select className="billboard-data-input" defaultValue={0} onChange={e=>setSurfaceId(surfacesList[e.target.selectedIndex-1].id)}>
                    <option>Тип экспанирования</option>
                    {surfacesList.map(e=><option key={e.id}>{e.surface}</option>)}
                </select>
                <select className="billboard-data-input" defaultValue={0} onChange={e=>setBillboardType(billboardTypesList[e.target.selectedIndex-1])}>
                    <option>Вид поверхности</option>
                    {billboardTypesList.map(e=><option key={e}>{e}</option>)}
                </select>
                <input required placeholder="Количество объявлений за тариф" type="number" className="billboard-data-input"/>
                <input required placeholder="Штраф за изменение заказа" type="number" className="billboard-data-input" onChange={e=>setPenalty(parseInt(e.target.value))}/>
                <span className="create-billboard-general-span">Размер</span>
                <input required placeholder="Ширина" type="number" className="billboard-data-short-input" onChange={e=>setWidth(parseInt(e.target.value))}/>
                <span className="billboard-size-unit">M</span>
                <input required placeholder="Высота" type="number" className="billboard-data-short-input" onChange={e=>setHeight(parseInt(e.target.value))}/>
                <span className="billboard-size-unit">M</span>
                <span className="create-billboard-general-span">Фотографии</span>
                <div className="upload-file-block">
                    <label htmlFor="upload-file" className="upload-picture-block">
                        <input type="file" name="upload-file" multiple className="upload-pictures-button" onChange={handleUploadFiles} accept="image/jpeg,image/png"/>
                        <img src={UploadIcon} alt="" className="upload-picture-block-icon" width="50" height="50"/>
                        <span className="upload-picture-text">Нажмите или перетащите файлы</span>
                    </label>
                    <div className="pictures-container">
                        {uploadedPictures.map(e=><img className="uploaded-image" src={e.data} key={e.id} alt={e.name} width="100" height="100"/>)}
                    </div>
                </div>
            </div>
            <div className="manage-buttons">
                <button className="create-billboard-button" onClick={(e)=>handleCreateBillboard(e)}>
                    Создать
                </button>
                <button className="cancel-create-billboard-button" onClick={(e)=>resetPanel(e)}>
                    Отмена
                </button>
            </div>
        </form>
    );
}

export default CreateBillboard;