import React from 'react'
import { Link } from 'react-router-dom'
import Sidebar from '../components/SideBar'
 
function Dashboard() {
  return (
    <div>
        <Sidebar>
            <h1>Dashboard</h1>
            
            <iframe src="https://www.google.com/maps/d/embed?mid=1DayHk74XQHB1StkXz5_yeCdlzeo&hl=en&ehbc=2E312F" width="1000" height="680"></iframe>
        </Sidebar>
    </div>
  )
}

export default Dashboard
