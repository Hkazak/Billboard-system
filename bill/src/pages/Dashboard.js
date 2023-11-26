import React from 'react'
import {Link} from 'react-router-dom'
import Sidebar from '../components/SideBar'
import './page_styles/Dashboard.css'
import {useNavigate} from 'react-router-dom'
import Button from 'react-bootstrap/esm/Button'
import Header from "../components/Header";
import ControlPanel from "../components/ControlPanel";
import DashboardControlPanel from "../components/DashboardControlPanel";

function Dashboard() {
    const navigate = useNavigate();

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

    return (
        <div className="dashboard-content">
            <Header title={"Dashboard"}/>
            <Sidebar>
                <DashboardControlPanel handleSelectSurface={handleSetSurface} handleSelectExposure={handleSetBillboardType} handleSelectTariff={handleSetTariff} handleSelectStartDate={handleSetStartDate} handleSelectEndDate={handleSetEndDate} />
                <iframe className='map'
                        src="https://www.google.com/maps/d/embed?mid=1DayHk74XQHB1StkXz5_yeCdlzeo&hl=en&ehbc=2E312F"
                        width="1200" height="650"></iframe>
                <br></br>
                <div className='cr-btn'>
                    <Button variant='warning' onClick={() => {
                        navigate('/cr-bills')
                    }}> Create </Button>
                </div>
            </Sidebar>
        </div>
    )
}

export default Dashboard
