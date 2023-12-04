import React, {useEffect, useState} from "react";
import {GetOrdersStatusesRequest} from "../lib/controllers/OrderController";
import "../styles/ControlPanel.css";

function OrdersControlPanel({handleSearch, placeholderSearchText, onStatusSet}) {
    const [orderStatuses, setOrderStatuses] = useState([]);

    function getOrderStatus(status) {
        if (status === 'Submitted') {
            return 'На рассмотрении';
        }
        if (status === 'InProgress') {
            return 'Подтвержден';
        }
        if (status === 'Cancelled') {
            return 'Отменен'
        }
        return 'Завершен';
    }

    useEffect(() => {
        GetOrdersStatusesRequest()
            .then(e => e.json())
            .then(e => setOrderStatuses(e));
    }, []);
    return (
        <div className="control-panel-block">
            <input type="text" placeholder={placeholderSearchText} className="control-panel-search"
                   onChange={e => handleSearch(e)}/>
            <select className="open-panel-create-new-item-button" defaultValue={0} onChange={onStatusSet}>
                <option>Статус</option>
                {orderStatuses.map(e => <option id={e} key={e}>{getOrderStatus(e)}</option>)}
            </select>
        </div>
    );
}

export default OrdersControlPanel;