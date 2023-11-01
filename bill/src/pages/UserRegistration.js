import React from 'react'
import Sidebar from '../components/SideBar'
import './page_styles/UserAuthorization.css'
import { Link } from 'react-router-dom'

function UserRegistration() {
  return (
    <div>
      
      <div className='main-cont'>
          <div className='child left-cont'>
            <div className='greet-cont'>
              <h1 className='greet'>Welcome</h1><br/><br/>
              <p className='desc-text'>Мы рады что вы решили присоединиться к нашей платформе бронирования билбордов.</p><br/>
            </div>
          </div>

          <div className='child right-cont'>
            <div className='reg-link'>
              <Link to="/auth">Authorization</Link>
            </div>
            <h1 className='auth-text'>Регистрация</h1>
            <div className='form-input'>
              <form>
                <input name='email' className='input-field' placeholder='Email' type='email' required/>
                <input name='username' className='input-field' placeholder='Name' type='text' required/>
                <input name='password' className='input-field' placeholder='Password' type='password' required/>
                <input name='password-valid' className='input-field' placeholder='Reenter Password' type='password' required/>
                <div className='policy-link'>
                  <Link to='/'>Принять пользовательское соглашение</Link>
                </div>
                <button className='btn-auth'>Зарегистрироваться</button>
              </form>
            </div>

          </div>
      </div>

    </div>
  )
}

export default UserRegistration
