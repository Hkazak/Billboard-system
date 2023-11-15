import React from 'react'
import { useNavigate } from 'react-router'
import Sidebar from '../components/SideBar'
import './page_styles/UserAuthorization.css'
import { Link } from 'react-router-dom'
import { useRef } from 'react'
import { AuthorizeUser } from '../lib/controllers/UserController'
import { LS } from '../lib/Consts'
import { DashboardRoute, ForgotPasswordRoute, UserRegistrationRoute } from '../Paths'

function UserAuthorization() {
  const navigate = useNavigate();

  const email = useRef(null);
  const password = useRef(null);

  function clearValidation(){
    email.current.setCustomValidity('');
    password.current.setCustomValidity('');
  }

  async function handleAuthorization(event){
    event.preventDefault();
    clearValidation();

    const response = await AuthorizeUser(email.current.value, password.current.value);
    const jsonResponse = await response.json();

    if(response.ok){
      console.log('User authorization feature');
      console.log(jsonResponse);
      localStorage.setItem(LS.accessToken, jsonResponse['accessToken']);
      navigate(DashboardRoute);
      return;
    }

    email.current.setCustomValidity('Электронная почта или пароль указаны неправильно');
    event.target.form.reportValidity();
  }

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
              <Link to={UserRegistrationRoute}>Registration</Link>
            </div>
            <h1 className='auth-text'>Авторизация</h1>
            <div className='form-input'>
              <form>
                <input ref={email} name='email' className='input-field' placeholder='Email' type='email' required/>
                <input ref={password} name='password' className='input-field' placeholder='Password' type='password' required/>
                <div className='reg-link'>
                  <Link to={ForgotPasswordRoute}>Забыли пароль???</Link>
                </div>
                <button onClick={handleAuthorization} className='btn-auth'>Авторизоваться</button>
              </form>
            </div>

          </div>
      </div>
    </div>
  )
}

export default UserAuthorization
