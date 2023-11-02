import React from 'react'
import Sidebar from '../components/SideBar'
import './page_styles/UserAuthorization.css'
import { Link, json } from 'react-router-dom'
import { useRef } from 'react'
import { RegisterUser } from '../lib/UserController'
import { useNavigate } from 'react-router-dom'

function UserRegistration() {
  const navigate = useNavigate();

  const email = useRef(null);
  const name = useRef(null);
  const password = useRef(null)
  const confirmPassword = useRef(null);

  const form = useRef(null);

  function clearValidation(){
    email.current.setCustomValidity('');
    name.current.setCustomValidity('');
    password.current.setCustomValidity('');
    confirmPassword.current.setCustomValidity('');
  }

  async function handleRegistration(event){
    clearValidation();
    event.preventDefault();

    var strongRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$_%\^&\*])(?=.{8,})/m;

    if(!strongRegex.test(password.current.value)){
      password.current.setCustomValidity('Пароль должен содержать заглавные и строчные латинские буквы, цифру, символ и иметь длину не менее 8.');
    }

    if(password.current.value !== confirmPassword.current.value){
      confirmPassword.current.setCustomValidity('Пароли не совпадают');
    }

    const isValid = event.target.form.checkValidity();
    console.log(isValid);
    if(!isValid)
    {
      event.target.form.reportValidity();
      return;
    }

    const response = await RegisterUser(name.current.value, email.current.value, password.current.value, confirmPassword.current.value);
    const jsonResponse = await response.json();
    if(response.ok){
      console.log(jsonResponse);
      navigate('/');
      return;
    }

    const errorMessage = jsonResponse['errorMessage'];
    if(errorMessage === 'Email is already used'){
      email.current.setCustomValidity('Электронная почта уже используется.');
    }

    event.target.form.reportValidity();
    return;
  }

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
              <form ref={form}>
                <input ref={email} name='email' className='input-field' placeholder='Email' type='email' required/>
                <input ref={name} name='username' className='input-field' placeholder='Name' type='text' required/>
                <input ref={password} name='password' className='input-field' placeholder='Password' type='password' required/>
                <input ref={confirmPassword} name='password-valid' className='input-field' placeholder='Reenter Password' type='password' required/>
                <div className='policy-link'>
                  <Link to='/'>Принять пользовательское соглашение</Link>
                </div>
                <button onClick={handleRegistration} className='btn-auth'>Зарегистрироваться</button>
              </form>
            </div>

          </div>
      </div>

    </div>
  )
}

export default UserRegistration
