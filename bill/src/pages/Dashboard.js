import React from 'react'
import { Link } from 'react-router-dom'
import Sidebar from '../components/SideBar'
import './page_styles/Dashboard.css'
import {useNavigate} from 'react-router-dom'
import Button from 'react-bootstrap/esm/Button'
 
function Dashboard() {
  const navigate = useNavigate();
 
  return (
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
        <iframe className='map' src="https://www.google.com/maps/d/embed?mid=1DayHk74XQHB1StkXz5_yeCdlzeo&hl=en&ehbc=2E312F" width="1200" height="650"></iframe>
        <br></br>
        <div className='cr-btn'>
          <Button variant='warning' onClick={() => {
            navigate('/cr-bills')
          }}> Create </Button>
        </div>
    </Sidebar>
  )
}

export default Dashboard
