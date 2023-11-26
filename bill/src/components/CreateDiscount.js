import "../styles/CreateDiscount.css"
import {useEffect, useState} from "react";
import {CreateDiscountRequest} from "../lib/controllers/DiscountController";
import {GetShortBillboardList} from "../lib/controllers/BillboardController";

let selectedBillboards = [];
function CreateDiscount({hide, handleNewDiscount})
{
    const [name, setName] = useState('');
    const [minRent, setMinRent] = useState(0);
    const [discount, setDiscount] = useState(0);
    const [endDate, setEndDate] = useState(new Date());
    const [selectAllBillboards, setSelectAllBillboards] = useState(false);
    const [searchText, setSearchText] = useState('');
    const [billboards, setBillboards] = useState([]);

    function handleCreateDiscount()
    {
        CreateDiscountRequest(name, minRent, discount, endDate, selectedBillboards)
            .then(e=>e.json())
            .then(e=>handleNewDiscount(e));
    }

    useEffect(()=>{
        GetShortBillboardList()
            .then(e=>e.json())
            .then(e=>setBillboards(e));
    }, []);

    function selectBillboard(ev, billboard)
    {
        if(ev.target.className === 'selected-billboard-name')
        {
            ev.target.className = '';
            selectedBillboards = [...selectedBillboards.filter(e=>e.id !== billboard.id)]
        }
        else
        {
            ev.target.className = 'selected-billboard-name'
            selectedBillboards.push(billboard);
        }
    }

    return (
        <div className="create-discount-block" hidden={hide}>
            <span className="create-discount-title">
                Новая акция
            </span>
            <div className="create-discount-general-information">
                <span className="create-discount-general-span">Общая информация</span>
                <input required type="text" className="discount-name-input" placeholder="Название" onChange={(e)=>setName(e.target.value)}/>
                <input required type="number" className="discount-name-input" placeholder="Минимальный срок аренды" onChange={(e)=>setMinRent(parseInt(e.target.value))}/>
                <input required type="number" className="discount-name-input" placeholder="Скидка %" onChange={(e)=>setDiscount(parseInt(e.target.value))}/>
                <input required type="date" className="discount-name-input" placeholder="Дата завершения акции" onChange={(e)=>setEndDate(new Date(e.target.value))}/>
            </div>
            <span className="create-discount-general-span">
                Выбрать эффект
            </span>
            <span className="select-billboards-block">
                <input type="checkbox" checked={selectAllBillboards} onChange={e=>setSelectAllBillboards(!selectAllBillboards)}/>
                <label>Действует на все билборды</label>
                <br/>
                <input type="checkbox" checked={!selectAllBillboards} onChange={e=>setSelectAllBillboards(!selectAllBillboards)}/>
                <label>Действует на выбранные билборды</label>
            </span>
            <input required type="text" className="discount-name-input" placeholder="Поиск билборда по названию" onChange={(e)=>setSearchText(e.target.value)}/>
            <ul className="billboards-names">
                {billboards.filter(e=>e.name.includes(searchText)).map(e=><li key={e.id} onClick={ev=>selectBillboard(ev, e)}>{e.name}</li>)}
            </ul>
            <button className="create-discount-button" onClick={()=>handleCreateDiscount()}>
                Создать
            </button>
        </div>
    );
}

export default CreateDiscount;