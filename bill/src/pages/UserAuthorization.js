import React from 'react'
import { useNavigate } from 'react-router'
import Sidebar from '../components/SideBar'
import './page_styles/UserAuthorization.css'
import { Link } from 'react-router-dom'

function UserAuthorization() {
  const navigate = useNavigate()

  return (
    <div>
      <div className='main-cont'>
          <div className='child left-cont'>
            <div className='greet-cont'>
              <h1 className='greet'>Welcome</h1><br/><br/><br/>
              <p className='desc-text'>Рады приветствовать вас в самой удобной платформе бронирования билбордов!</p><br/>
              <p className='desc-text'>Для получения доступа к своим заказам пожалуйста авторизуйтесь.</p>
            </div>
          </div>

          <div className='child right-cont'>
            <div className='reg-link'>
              <Link to="/reg">Registration</Link>
            </div>
            <h1 className='auth-text'>Авторизация</h1>
            <div className='form-input'>
              <form>
                <input name='email' className='input-field' placeholder='Email' type='email' required/>
                <input name='password' className='input-field' placeholder='Password' type='password' required/>
                <div className='reg-link'>
                  <Link to='/recover'>Забыли пароль???</Link>
                </div>
                <button className='btn-auth'>Авторизоваться</button>
              </form>
            </div>

          </div>
      </div>
    </div>
  )
}

export default UserAuthorization
