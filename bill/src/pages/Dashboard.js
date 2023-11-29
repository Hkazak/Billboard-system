import React, {useState} from 'react'
import Sidebar from '../components/SideBar'
import './page_styles/Dashboard.css'
import {useNavigate} from 'react-router-dom'
import Header from "../components/Header";
import DashboardControlPanel from "../components/DashboardControlPanel";
import CreateBillboard from "../components/CreateBillboard";

function Dashboard() {
    const [hideCreatePanel, setHideCreatePanel] = useState(true);

    function handleSetSurface(surface)
    {

    }

    function handleSetTariff(tariff)
    {

    }

    function handleSetBillboardType(billboardType)
    {

    }

    function handleSetStartDate(startDate)
    {

    }

    function handleSetEndDate(endDate)
    {

    }

    function handleCreateBillboard(billboard)
    {
    }

    return (
        <div className="dashboard-content">
            <Header title={"Dashboard"}/>
            <CreateBillboard hide={hideCreatePanel} setHide={setHideCreatePanel} handleNewBillboard={handleCreateBillboard} />
            <Sidebar>
                <DashboardControlPanel handleSelectSurface={handleSetSurface} handleSelectExposure={handleSetBillboardType} handleSelectTariff={handleSetTariff} handleSelectStartDate={handleSetStartDate} handleSelectEndDate={handleSetEndDate} />
                <iframe className='map'
                        src="https://www.google.com/maps/d/embed?mid=1DayHk74XQHB1StkXz5_yeCdlzeo&hl=en&ehbc=2E312F"
                        width="1200" height="650"></iframe>
                <div className="create-billboard-panel-button-block">
                    <button className="create-billboard-panel-button" onClick={e=>setHideCreatePanel(!hideCreatePanel)}>
                        Создать билборд
                    </button>
                </div>
            </Sidebar>
        </div>
    )
}

export default Dashboard
