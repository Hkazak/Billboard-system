import React from 'react'

import './page_styles/UserAuthorization.css'
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom'
import { useRef } from 'react'
import { ResetPasswordChangePassword } from '../lib/controllers/UserController'
import { localStorageForgotPasswordEmail } from '../lib/Consts'
import { UserAuthorizationRoute } from '../Paths'

function ChangePassword() {
  const navigate = useNavigate();

  const code = useRef(null);
  const newPassword = useRef(null);
  const newPasswordConfirm = useRef(null);

  function clearValidation(){
    code.current.setCustomValidity('');
    newPassword.current.setCustomValidity('');
    newPasswordConfirm.current.setCustomValidity('');
  }

  async function handleResetPassword(event){
    event.preventDefault();
    clearValidation();

    var strongRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$_%\^&\*])(?=.{8,})/m;

    if(!strongRegex.test(newPassword.current.value)){
      newPassword.current.setCustomValidity('Пароль должен содержать заглавные и строчные латинские буквы, цифру, символ и иметь длину не менее 8.');
    }

    if(newPassword.current.value !== newPasswordConfirm.current.value){
      newPasswordConfirm.current.setCustomValidity('Пароли не совпадают');
    }

    const isValid = event.target.form.checkValidity();
    console.log(isValid);
    if(!isValid)
    {
      event.target.form.reportValidity();
      return;
    }

    const email = localStorage.getItem(localStorageForgotPasswordEmail);
    const response = await ResetPasswordChangePassword(email, code.current.value, newPassword.current.value, newPasswordConfirm.current.value);

    if(response.ok){
      console.log('User reset password feature');
      localStorage.removeItem(localStorageForgotPasswordEmail);
      navigate(UserAuthorizationRoute);
      return;
    }

    code.current.setCustomValidity('Неправильный код');
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
              <Link to={UserAuthorizationRoute}>Authorization</Link>
            </div>
            <h1 className='auth-text'>Восстановление пароля</h1>
            <div className='form-input'>
              <form>
                <input ref={code} name='verification-code' className='input-field' placeholder='Код' type='text' required/>
                <input ref={newPassword} name='new_password' className='input-field' placeholder='Новый пароль' type='password' required/>
                <input ref={newPasswordConfirm} name='new_password_val' className='input-field' placeholder='Подтверждение пароля' type='password' required/>
                <button onClick={handleResetPassword} className='btn-auth'>Изменить пароль</button>
              </form>
            </div>

          </div>
      </div>

    </div>
  )
}

export default ChangePassword
