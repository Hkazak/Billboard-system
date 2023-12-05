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
    const [selectedSurface, setSelectedSurface] = useState(null);
    const [selectedBillboardType, setSelectedBillboardType] = useState(null);
    const [selectedTariff, setSelectedTariff] = useState(null);

    function handleSetSurface(surface) {
        setSelectedSurface(surface);
    }

    function handleSetTariff(tariff) {
        setSelectedTariff(tariff);
    }

    function handleSetBillboardType(billboardType) {
        setSelectedBillboardType(billboardType);
    }

    function handleCreateBillboard(billboard) {
        setBillboards([...billboards, billboard]);
        setSelectedBillboards([...billboards, billboard]);
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
                setSelectedSurface([]);
            });
    }, []);

    useEffect(()=>{
        const allBillboards = [...billboards];
        const filteredBillboards = allBillboards
            .filter(e=>e?.billboardSurface?.toLowerCase()?.includes(selectedSurface?.surface?.toLowerCase() ?? ''))
            .filter(e=>e?.billboardType?.toLowerCase()?.includes(selectedBillboardType?.toLowerCase() ?? ''))
            .filter(e=>e?.groupOfTariffs?.tariffs?.map(e=>e.id)?.some(e=>e.includes(selectedTariff?.id ?? '')));
        setSelectedBillboards(filteredBillboards);
    }, [selectedTariff, selectedSurface, selectedBillboardType]);
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
                                       handleSelectTariff={handleSetTariff}
                />
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
