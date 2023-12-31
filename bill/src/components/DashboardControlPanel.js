import React, {useEffect, useState} from "react";
import "../styles/ControlPanel.css";
import "../styles/DashboardControlPanel.css";
import {getBillboardSurfacesList} from "../lib/controllers/TarrifsController";
import {GetBillboardTypes} from "../lib/controllers/BillboardTypesController";
import {GetTariffs} from "../lib/controllers/TariffController";

function DashboardControlPanel({
                                   handleSelectSurface,
                                   handleSelectExposure,
                                   handleSelectTariff,
                               }) {
    const [surfacesList, setSurfacesList] = useState([]);
    const [exposureTypeList, setExposureTypeList] = useState([]);
    const [tariffsList, setTariffsList] = useState([]);
    useEffect(() => {
        getBillboardSurfacesList()
            .then(e => e.json())
            .then(e => setSurfacesList(e));
        GetBillboardTypes()
            .then(e => e.json())
            .then(e => setExposureTypeList(e));
        GetTariffs()
            .then(e => e.json())
            .then(e => setTariffsList(e));
    }, []);
    return (
        <div className="control-panel-block">
            <ul className="control-panel-options-list">
                <li className="control-panel-options-list-item">
                    <select name="surface-type" className="control-panel-input"
                            onChange={(ev) => handleSelectSurface(ev.target.options.selectedIndex === 0 ? null : surfacesList[ev.target.options.selectedIndex - 1])}>
                        <option className="control-panel-input-option">Вид поверхности</option>
                        {surfacesList.map(surface => <option key={surface.id}
                                                             className="control-panel-input-option">{surface.surface}</option>)}
                    </select>
                </li>
                <li className="control-panel-options-list-item">
                    <select name="exposure-type" className="control-panel-input"
                            onChange={(ev) => handleSelectExposure(ev.target.options.selectedIndex === 0 ? null : exposureTypeList[ev.target.options.selectedIndex - 1])}>
                        <option className="control-panel-input-option">Вид экспонирования</option>
                        {exposureTypeList.map(exposure => <option key={exposure}
                                                                  className="control-panel-input-option">{exposure}</option>)}
                    </select>
                </li>
                <li className="control-panel-options-list-item">
                    <select name="tariff" className="control-panel-input"
                            onChange={(ev) => handleSelectTariff(ev.target.options.selectedIndex === 0 ? null : tariffsList[ev.target.options.selectedIndex - 1])}>
                        <option className="control-panel-input-option">Тариф</option>
                        {tariffsList.map(tariff => <option key={tariff.id}
                                                           className="control-panel-input-option">{tariff.title}</option>)}
                    </select>
                </li>
            </ul>
        </div>
    );
}

export default DashboardControlPanel;