import '../styles/CreateTariff.css'
import { useState } from "react";
import { SendTariff } from "../lib/controllers/TariffController";

function CreateTariff({ isEnabled, setIsEnabled, handleNewTariff, setPage }) {
    const [tariffName, setTariffName] = useState('');
    const [tariffPrice, setTariffPrice] = useState(0);
    const [timeStart, setTimeStart] = useState('00:00');
    const [timeEnd, setTimeEnd] = useState('23:59');

    function tariffNameChanged(ev) {
        setTariffName(ev.target.value);
    }

    function tariffPriceChanged(ev) {
        setTariffPrice(ev.target.value);
    }

    function isAllDayTariff(ev) {
        if (ev.target.checked) {
            setTimeStart('00:00');
            setTimeEnd('23:59');
        }
    }

    function tariffTimeStartChanged(ev) {
        setTimeStart(ev.target.value);
    }

    function tariffTimeEndChanged(ev) {
        setTimeEnd(ev.target.value);
    }

    function createTariff(ev) {
        ev.preventDefault();

        const isValid = ev.target.form.checkValidity();
        if (!isValid) {
            ev.target.form.reportValidity();
            return;
        }
        SendTariff(tariffName, timeStart, timeEnd, tariffPrice)
            .then(e=>e.json())
            .then(e=>handleNewTariff(e));
    }

    return (
        <form className="new-tariff" hidden={!isEnabled}>
            <h2 className="title">Новый тариф</h2>
            <span className="general-text">Общая информация</span>
            <input required type="text" placeholder="Название*" name="tariffName" className="tariff-property" onChange={tariffNameChanged} />
            <input required type="number" placeholder="Цена*" name="tariffPrice" className="tariff-property" onChange={tariffPriceChanged} />
            <span className="general-text">Временные отрезки</span>
            <span className="all-day-block">
                <input type="checkbox" onChange={isAllDayTariff} />
                <label>Целый день</label>
            </span>
            <div className="select-time-period">
                <input type="time" name="tariffTimeStart" className="time-period" value={timeStart} onChange={tariffTimeStartChanged} />
                <input type="time" name="tariffTimeEnd" className="time-period" value={timeEnd} onChange={tariffTimeEndChanged} />
            </div>
            <div className='two-buttons'>
                <button className="create-tariff" onClick={createTariff}>Создать</button>
                <button className="cancel" onClick={(ev) => { setIsEnabled(false) }}>Отмена</button>
            </div>
        </form>
    );
}

export default CreateTariff;