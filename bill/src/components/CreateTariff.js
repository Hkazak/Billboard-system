import '../styles/CreateTariff.css'
import {useState} from "react";
import {SendTariff} from "../lib/controllers/TariffController";

function CreateTariff({isEnabled})
{
    const [tariffName, setTariffName] = useState('');
    const [tariffPrice, setTariffPrice] = useState(0);
    const [timeStart, setTimeStart] = useState('00:00');
    const [timeEnd, setTimeEnd] = useState('23:59');

    function tariffNameChanged(ev)
    {
        setTariffName(ev.target.value);
    }

    function tariffPriceChanged(ev)
    {
        setTariffPrice(ev.target.value);
    }

    function isAllDayTariff(ev)
    {
        if(ev.target.checked)
        {
            setTimeStart('00:00');
            setTimeEnd('23:59');
        }
    }

    function tariffTimeStartChanged(ev)
    {
        setTimeStart(ev.target.value);
    }

    function tariffTimeEndChanged(ev)
    {
        setTimeEnd(ev.target.value);
    }

    async function createTariff()
    {
        const response = await SendTariff(tariffName, timeStart, timeEnd, tariffPrice);
        console.log(response);
    }

    return (
        <div className="new-tariff" hidden={!isEnabled}>
            <h2 className="title">Новый тариф</h2>
            <span className="general-text">Общая информация</span>
            <input type="text" placeholder="Название" name="tariffName" className="tariff-property" onChange={tariffNameChanged}/>
            <input type="number" placeholder="Цена" name="tariffPrice" className="tariff-property" onChange={tariffPriceChanged}/>
            <span className="general-text">Временные отрезки</span>
            <span className="all-day-block">
                <input type="checkbox" onChange={isAllDayTariff}/>
                <label>Целый день</label>
            </span>
            <input type="time" name="tariffTimeStart" className="time-period" value={timeStart} onChange={tariffTimeStartChanged}/>
            <input type="time" name="tariffTimeEnd" className="time-period" value={timeEnd} onChange={tariffTimeEndChanged}/>
            <button className="create-tariff" onClick={createTariff}>Создать</button>
        </div>
    );
}

export default CreateTariff;