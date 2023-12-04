import "../styles/ManagerInfo.css";
import {useState} from "react";
import {ActivateManager, DeactivateManager} from "../lib/controllers/ManagerController";

function ManagerInfo({firstName, middleName, lastName, email, phone, status, id}) {
    const [isActiveManager, setIsActiveManager] = useState(status.toLowerCase() === 'active');
    const [managerStatus, setManagerStatus] = useState(status.toLowerCase());

    function changeManagerStatus(id) {
        if (managerStatus !== 'active') {
            ActivateManager(id)
                .then(e => {
                    if (e.ok) {
                        setIsActiveManager(true);
                        setManagerStatus('active');
                    }
                });
        } else {
            DeactivateManager(id)
                .then(e => {
                    if (e.ok) {
                        setIsActiveManager(false);
                        setManagerStatus('inactive');
                    }
                });
        }
    }

    return (
        <div className="manager-information-block">
            <div className={isActiveManager ? "manager-active-status" : "manager-inactive-status"}></div>
            <div className="manager-information">
                <span className="manager-information-span">
                    {firstName}&nbsp;{middleName}&nbsp;{lastName}
                </span>
                <br/>
                <span className="manager-information-span">
                    {email}&nbsp;{phone}
                </span>
            </div>
            <div className="control-buttons">
                <button className="control-button" onClick={e => changeManagerStatus(id)}>
                    {isActiveManager ? 'Заморозить аккаунт' : 'Активировать аккаунт'}
                </button>
                <button className="control-button">
                    Редактирование
                </button>
            </div>
        </div>
    );
}

export default ManagerInfo;