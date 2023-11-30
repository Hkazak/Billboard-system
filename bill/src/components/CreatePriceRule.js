import {useEffect, useState} from "react";
import "../styles/CreatePriceRule.css";
import {getBillboardSurfacesList} from "../lib/controllers/TarrifsController";
import {GetBillboardTypes} from "../lib/controllers/BillboardTypesController";
import {CreatePriceRuleRequest} from "../lib/controllers/PriceRuleController";

function CreatePriceRule({hide, setHide, handleNewPriceRule})
{
    const [surfaces, setSurfaces] = useState([]);
    const [billboardTypes, setBillboardTypes] = useState([]);
    const [surfaceId, setSurfaceId] = useState('');
    const [billboardType, setBillboardType] = useState('');
    const [price, setPrice] = useState(0);

    function createPriceRule(e)
    {
        e.preventDefault();
        CreatePriceRuleRequest(surfaceId, billboardType, price)
            .then(e=>e.json())
            .then(e=>{
                console.log(e);
                handleNewPriceRule(e);
            });
    }

    function resetPanel(e)
    {
        e.preventDefault();
        e.target.form.reset();
        setHide(true);
        setPrice(0);
        setBillboardType('');
        setSurfaceId('');
        refresh();
    }

    function refresh()
    {
        getBillboardSurfacesList()
            .then(e=>e.json())
            .then(e=>setSurfaces(e));
        GetBillboardTypes()
            .then(e=>e.json())
            .then(e=>setBillboardTypes(e));
    }

    useEffect(()=> {
        refresh();
    }, []);
    return (
        <form className="create-new-price-rule-block" hidden={hide}>
            <span className="create-price-rule-title">
                Новое правило расчета цены
            </span>
            <div className="create-price-rule-general-information">
                <span className="create-price-rule-general-span">Общая информация</span>
                <select className="price-rule-input" onChange={e=>setSurfaceId(surfaces[e.target.selectedIndex-1].id)}>
                    <option>Вид поверхности</option>
                    {surfaces.map(e=><option key={e.id}>{e.surface}</option>)}
                </select>
                <select className="price-rule-input" onChange={e=>setBillboardType(billboardTypes[e.target.selectedIndex-1])}>
                    <option>Тип экспонирования</option>
                    {billboardTypes.map(e=><option key={e}>{e}</option>)}
                </select>
                <input required type="number" className="price-rule-input" placeholder="Цена за 1м х 1м" onChange={(e)=>setPrice(parseInt(e.target.value))}/>
            </div>
            <div className="manage-buttons">
                <button className="create-price-rule-button" onClick={(e)=>createPriceRule(e)}>
                    Создать
                </button>
                <button className="cancel-create-price-rule-button" onClick={(e)=>resetPanel(e)}>
                    Отмена
                </button>
            </div>
        </form>
    );
}

export default CreatePriceRule;