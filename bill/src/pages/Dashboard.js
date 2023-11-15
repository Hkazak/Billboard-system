import React from 'react'
import { Link } from 'react-router-dom'
import Sidebar from '../components/SideBar'
import './page_styles/Dashboard.css'
 
function Dashboard() {
  return (
    <div>
        <Sidebar>
            <h1>Dashboard</h1>

            <div className='main-select-cont'>
              <div className='list-wrapper'>
                  <ul className='list-cont'>
                    <li className='select-cont'>
                      <select>
                        <option value="hello"> Hello </option>
                        <option value="bye"> Bye </option>
                        <option value="what"> What </option>
                      </select>
                    </li>

                    <li className='select-cont'>
                      <select>
                        <option value="hello"> Hello </option>
                        <option value="bye"> Bye </option>
                        <option value="what"> What </option>
                      </select>
                    </li>


                    <li className='select-cont'>
                      <select>
                        <option value="hello"> Hello </option>
                        <option value="bye"> Bye </option>
                        <option value="what"> What </option>
                      </select>
                    </li>

                    <li className='select-cont'>
                      <select>
                        <option value="hello"> Hello </option>
                        <option value="bye"> Bye </option>
                        <option value="what"> What </option>
                      </select>
                    </li>

                    <li className='select-cont'>
                      <select>
                        <option value="hello"> Hello </option>
                        <option value="bye"> Bye </option>
                        <option value="what"> What </option>
                      </select>
                    </li>


                  </ul>
              </div>
            </div>
            <div className='sep'></div>
            <iframe src="https://www.google.com/maps/d/embed?mid=1DayHk74XQHB1StkXz5_yeCdlzeo&hl=en&ehbc=2E312F" width="1000" height="550"></iframe>
        </Sidebar>
    </div>
  )
}

export default Dashboard
