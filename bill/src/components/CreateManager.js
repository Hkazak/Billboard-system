import "../styles/CreateManager.css";
import {useState} from "react";
import {CreateManagerRequest} from "../lib/controllers/ManagerController";

function CreateManager({hide, setHide, handleNewManager})
{
    const [firstName, setFirstName] = useState('');
    const [middleName, setMiddleName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [phone, setPhone] = useState('');
    function createManager()
    {
        CreateManagerRequest(firstName, middleName, lastName, email, phone)
            .then(e=>e.json())
            .then(e=>handleNewManager(e));
    }

    return (
        <form className="create-manager-block" hidden={hide}>
            <span className="create-manager-title">
                Добавление менеджера
            </span>
            <input required placeholder="Фамилия" type="text" className="manager-data-input" onChange={e=>setLastName(e.target.value)}/>
            <input required placeholder="Имя" type="text" className="manager-data-input" onChange={e=>setFirstName(e.target.value)}/>
            <input required placeholder="Отчество" type="text" className="manager-data-input" onChange={e=>setMiddleName(e.target.value)}/>
            <input required placeholder="E-mail" type="email" className="manager-data-input" onChange={e=>setEmail(e.target.value)}/>
            <input required placeholder="Моб. телефон" type="text" className="manager-data-input" onChange={e=>setPhone(e.target.value)}/>
            <div className="manage-buttons">
                <button className="create-manager-button" onClick={e=>createManager()}>
                    Создать
                </button>
                <button className="cancel-create-manager-button" onClick={()=>setHide(true)}>
                    Отмена
                </button>
            </div>
        </form>
    );
}

export default CreateManager;