import '../styles/CreateTariff.css'
import { useState } from "react";
import { SendTariff } from "../lib/controllers/TariffController";

function CreateTariff({ isEnabled, setIsEnabled, tariffs, setTariffs, setPage }) {
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

    async function createTariff(ev) {
        ev.preventDefault();

        const isValid = ev.target.form.checkValidity();
        console.log(isValid);
        if (!isValid) {
            ev.target.form.reportValidity();
            return;
        }
        const response = await SendTariff(tariffName, timeStart, timeEnd, tariffPrice);
        console.log(response);

        if(response.ok){
            let json = await response.json();

            setTariffs([json, ...tariffs]);
            setIsEnabled(false);
            setPage(0);
        }
    }

    return (
        <div className="new-tariff" hidden={!isEnabled}>
            <form>
                <h2 className="title">Новый тариф</h2>
                <span className="general-text">Общая информация</span>
                <input required type="text" placeholder="Название*" name="tariffName" className="tariff-property" onChange={tariffNameChanged} />
                <input required type="number" placeholder="Цена*" name="tariffPrice" className="tariff-property" onChange={tariffPriceChanged} />
                <span className="general-text">Временные отрезки</span>
                <span className="all-day-block">
                    <input type="checkbox" onChange={isAllDayTariff} />
                    <label>Целый день</label>
                </span>
                <input type="time" name="tariffTimeStart" className="time-period" value={timeStart} onChange={tariffTimeStartChanged} />
                <input type="time" name="tariffTimeEnd" className="time-period" value={timeEnd} onChange={tariffTimeEndChanged} />
                <div className='two-buttons'>
                    <button className="create-tariff" onClick={createTariff}>Создать</button>
                    <button className="cancel" onClick={(ev) => { setIsEnabled(false) }}>Отмена</button>
                </div>
            </form>
        </div>
    );
}

export default CreateTariff;