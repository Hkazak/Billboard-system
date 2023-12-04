import "../styles/OrderInformation.css";
import {baseUrl} from "../lib/Consts";

function OrderInformation({id, name, userName, userEmail, uploadedFiles, status, onSelect}) {
    function getOrderStatus() {
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

    return (
        <div className="order-block" onClick={e=>onSelect(id)}>
            <img src={baseUrl + uploadedFiles[0]} alt="" className="order-image"/>
            <div className="order-block-information">
                <span className="order-block-title">{name}</span>
                <span className="user-info">
                    <span className="user-info-span">{userName}</span>
                    <span className="user-info-span">{userEmail}</span>
                </span>
            </div>
            <div className="order-status">
                <div className={`order-status-${status.toLowerCase()}`}></div>
                <span className="order-status-text">
                    {getOrderStatus()}
                </span>
            </div>
        </div>
    );
}

export default OrderInformation;