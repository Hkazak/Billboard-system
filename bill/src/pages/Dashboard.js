import React, {useEffect, useState} from 'react'
import Sidebar from '../components/SideBar'
import './page_styles/Dashboard.css'
import Header from "../components/Header";
import DashboardControlPanel from "../components/DashboardControlPanel";
import CreateBillboard from "../components/CreateBillboard";
import {GetBillboardList} from "../lib/controllers/BillboardController";
import Map from "../components/Map";
import BillboardInformation from "../components/BillboardInformation";
import {LS} from "../lib/Consts";

function Dashboard() {
    const [hideCreatePanel, setHideCreatePanel] = useState(true);
    const [billboards, setBillboards] = useState([]);
    const [selectedBillboards, setSelectedBillboards] = useState([]);
    const [hideBillboardInfo, setHideBillboardInfo] = useState(true);
    const [selectedBillboard, setSelectedBillboard] = useState({});
    const [isClientView, setIsClientView] = useState(localStorage.getItem(LS.isClient) === 'true');

    function handleSetSurface(surface) {
    }

    function handleSetTariff(tariff) {

    }

    function handleSetBillboardType(billboardType) {

    }

    function handleSetStartDate(startDate) {

    }

    function handleSetEndDate(endDate) {

    }

    function handleCreateBillboard(billboard) {
    }

    function selectBillboard(billboardId) {
        const billboard = billboards.find(e => e.id === billboardId);
        setSelectedBillboard(billboard);
        setHideBillboardInfo(false);
    }

    useEffect(() => {
        GetBillboardList()
            .then(e => e.json())
            .then(e => {
                setBillboards(e);
                setSelectedBillboards(e);
            });
    }, []);
    return (
        <div className="dashboard-content">
            <Header title={"Dashboard"}/>
            <CreateBillboard hide={hideCreatePanel} setHide={setHideCreatePanel}
                             handleNewBillboard={handleCreateBillboard}/>
            <BillboardInformation
                billboardId={selectedBillboard.id}
                billboardType={selectedBillboard.billboardType}
                name={selectedBillboard.name}
                description={selectedBillboard.description}
                width={selectedBillboard.width}
                height={selectedBillboard.height}
                billboardSurface={selectedBillboard.billboardSurface}
                discounts={selectedBillboard.discounts}
                groupOfTariffs={selectedBillboard.groupOfTariffs}
                pictureSource={selectedBillboard.pictureSource}
                isClientView={isClientView}
                hide={hideBillboardInfo}
                setHide={setHideBillboardInfo}
            />
            <Sidebar>
                <DashboardControlPanel handleSelectSurface={handleSetSurface}
                                       handleSelectExposure={handleSetBillboardType}
                                       handleSelectTariff={handleSetTariff} handleSelectStartDate={handleSetStartDate}
                                       handleSelectEndDate={handleSetEndDate}/>
                <Map markBillboards={selectedBillboards} onSelectBillboard={selectBillboard}/>
                <div className="create-billboard-panel-button-block" hidden={isClientView}>
                    <button className="create-billboard-panel-button"
                            onClick={e => setHideCreatePanel(!hideCreatePanel)}>
                        Создать билборд
                    </button>
                </div>
            </Sidebar>
        </div>
    )
}

export default Dashboard
