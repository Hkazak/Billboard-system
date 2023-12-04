import arrow from "../assets/arrow.png";
import {useState} from "react";
import "../styles/Discount.css"

function Discount({name, minRentCount, discount, endDate, billboards = []}) {
    const [hideBillboards, setHideBillboards] = useState(true)
    return (
        <div className="discount-item-block">
            <span className="discount-item-block-title">
                <span className="discount-item-block-name">
                    {name}
                </span>
                <span className="discount-item-block-general-information">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;от {minRentCount} дней&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-{discount}%&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Дата завершения: {endDate}
                </span>
                <button className="hide-billboards-list-button" onClick={() => setHideBillboards(!hideBillboards)}>
                    <img src={arrow} alt={"Hide"} width="15" height="15"/>
                </button>
                <div className="discount-billboards-list-content" hidden={hideBillboards}>
                    <hr/>
                    <ul className="discount-billboards-list">
                        {billboards.map(e => <li className="discount-billboard-list-item">{e.name}</li>)}
                    </ul>
                </div>
            </span>
        </div>
    );
}

export default Discount;