import Tariff from "./Tariff";
import "../styles/GroupOfTariffs.css"
import arrow from '../assets/arrow.png'
import {useState} from "react";

function GroupOfTariffs({name, tariffs})
{
    const [hideTariffs, setHideTariffs] = useState(true);
    const tariffsList = tariffs.map(t=><Tariff key={t.id} tariffTitle={t.title} startTime={t.startTime} endTime={t.endTime} tariffPrice={t.price} />)
    return (
        <div className="group-of-tariffs-block">
            <span className="group-name">
                {name}
            </span>
            <button className="hide-group-of-tariffs-button" onClick={()=>setHideTariffs(!hideTariffs)}>
                <img src={arrow} alt={"Hide"} width="15" height="15"/>
            </button>
            <div className="tariffs-list-content" hidden={hideTariffs}>
            <hr/>
                {tariffsList}
            </div>
        </div>
    );
}

export default GroupOfTariffs;