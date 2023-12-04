import {useEffect, useState} from "react";
import {GetGroupOfTariffsList} from "../lib/controllers/TariffGroupController";
import {getBillboardSurfacesList} from "../lib/controllers/TarrifsController";
import {GetBillboardTypes} from "../lib/controllers/BillboardTypesController";
import "../styles/CreateBillboard.css";
import {CreateBillboardRequest} from "../lib/controllers/BillboardController";
import UploadPictures from "./UploadPictures";

function CreateBillboard({hide = true, setHide, handleNewBillboard, isClientView}) {
    const [groupOfTariffsList, setGroupOfTariffsList] = useState([]);
    const [surfacesList, setSurfacesList] = useState([]);
    const [billboardTypesList, setBillboardTypesList] = useState([]);
    const [billboardName, setBillboardName] = useState('');
    const [billboardDescription, setBillboardDescription] = useState('');
    const [address, setAddress] = useState('');
    const [groupOfTariffId, setGroupOfTariffId] = useState('');
    const [surfaceId, setSurfaceId] = useState('');
    const [billboardType, setBillboardType] = useState('');
    const [penalty, setPenalty] = useState(0);
    const [width, setWidth] = useState(0);
    const [height, setHeight] = useState(0);
    const [pictures, setPictures] = useState([]);

    function handleCreateBillboard(e) {
        e.preventDefault();
        CreateBillboardRequest(billboardName, address, billboardDescription, groupOfTariffId, billboardType, surfaceId, penalty, height, width, pictures)
            .then(e => e.json())
            .then(e => handleNewBillboard(e));
    }

    function resetPanel(e) {
        e.preventDefault();
        setHide(true);
    }

    useEffect(() => {
        GetGroupOfTariffsList()
            .then(e => e.json())
            .then(e => setGroupOfTariffsList(e));
        getBillboardSurfacesList()
            .then(e => e.json())
            .then(e => setSurfacesList(e));
        GetBillboardTypes()
            .then(e => e.json())
            .then(e => setBillboardTypesList(e));
    }, []);

    return (
        <form className="create-billboard-block" hidden={hide || isClientView}>
            <span className="create-billboard-title">
                Новая акция
            </span>
            <div className="create-billboard-general-information">
                <span className="create-billboard-general-span">Общая информация</span>
                <input required type="text" className="billboard-data-input" placeholder="Название"
                       onChange={e => setBillboardName(e.target.value)}/>
                <textarea required className="billboard-data-textarea" placeholder="Описание"
                          onChange={e => setBillboardDescription(e.target.value)}/>
                <input required type="text" className="billboard-data-input" placeholder="Адрес"
                       onChange={e => setAddress(e.target.value)}/>
                <select className="billboard-data-input" defaultValue={0}
                        onChange={e => setGroupOfTariffId(groupOfTariffsList[e.target.selectedIndex - 1].id)}>
                    <option>Группа тарифов</option>
                    {groupOfTariffsList.map(e => <option key={e.id}>{e.name}</option>)}
                </select>
                <select className="billboard-data-input" defaultValue={0}
                        onChange={e => setSurfaceId(surfacesList[e.target.selectedIndex - 1].id)}>
                    <option>Тип экспанирования</option>
                    {surfacesList.map(e => <option key={e.id}>{e.surface}</option>)}
                </select>
                <select className="billboard-data-input" defaultValue={0}
                        onChange={e => setBillboardType(billboardTypesList[e.target.selectedIndex - 1])}>
                    <option>Вид поверхности</option>
                    {billboardTypesList.map(e => <option key={e}>{e}</option>)}
                </select>
                <input required placeholder="Количество объявлений за тариф" type="number"
                       className="billboard-data-input"/>
                <input required placeholder="Штраф за изменение заказа" type="number" className="billboard-data-input"
                       onChange={e => setPenalty(parseInt(e.target.value))}/>
                <span className="create-billboard-general-span">Размер</span>
                <input required placeholder="Ширина" type="number" className="billboard-data-short-input"
                       onChange={e => setWidth(parseInt(e.target.value))}/>
                <span className="billboard-size-unit">M</span>
                <input required placeholder="Высота" type="number" className="billboard-data-short-input"
                       onChange={e => setHeight(parseInt(e.target.value))}/>
                <span className="billboard-size-unit">M</span>
                <span className="create-billboard-general-span">Фотографии</span>
                <UploadPictures onUpload={files => setPictures([...files])}/>
            </div>
            <div className="manage-buttons">
                <button className="create-billboard-button" onClick={(e) => handleCreateBillboard(e)}>
                    Создать
                </button>
                <button className="cancel-create-billboard-button" onClick={(e) => resetPanel(e)}>
                    Отмена
                </button>
            </div>
        </form>
    );
}

export default CreateBillboard;