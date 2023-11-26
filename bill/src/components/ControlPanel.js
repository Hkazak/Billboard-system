import React from "react";
import "../styles/ControlPanel.css";

function ControlPanel({handleSearch, placeholderSearchText, handleCreateItem, createButtonText})
{
    return (
        <div className="control-panel-block">
            <input type="text" placeholder={placeholderSearchText} className="control-panel-search" onChange={e=>handleSearch(e)}/>
            <button type="button" className="open-panel-create-new-item-button" data-toggle="modal"
                    data-target="#staticBackdrop" onClick={(e)=>handleCreateItem(e)}>
                <span className="new-tarif">{createButtonText}</span>
            </button>
        </div>
    );
}

export default ControlPanel;