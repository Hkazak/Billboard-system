import {CreateBillboardSurfaceRequest} from "../lib/controllers/BillboardSurfaceController";
import {useState} from "react";
import "../styles/CreateBillboardSurface.css";

function CreateBillboardSurface({hide, setHide, onCreated})
{
    const [surface, setSurface] = useState('');
    function handleCreate(e)
    {
        e.preventDefault();
        CreateBillboardSurfaceRequest(surface)
            .then(e=>e.json())
            .then(e=>onCreated(e));
    }

    function handleCancel(e)
    {
        e.preventDefault();
        e.target.form.reset();
        setHide(true);
    }

    return (
        <form className="create-billboard-surface-block" hidden={hide}>
            <span className="create-surface-title">Новая поверхность</span>
            <span className="create-surface-general-span">Общая информация</span>
            <input type="text" placeholder="Название" className="create-surface-input" value={surface} onChange={e=>setSurface(e.target.value)}/>
            <div className="manage-buttons">
                <button className="create-surface-button" onClick={handleCreate}>Создать</button>
                <button className="cancel-create-surface-button" onClick={handleCancel}>Отмена</button>
            </div>
        </form>
    );
}

export default CreateBillboardSurface;