import React from 'react'
import './page_styles/UserAuthorization.css'
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom'
import { useRef } from 'react'
import { ResetPasswordSendEmail } from '../lib/controllers/UserController'
import { localStorageForgotPasswordEmail } from '../lib/Consts'
import { ManagerAuthRoute, ManagerResetPasswordRoute } from '../Paths'

function ManagerForgotPassword() {
  const navigate = useNavigate();
  const email = useRef(null);

  async function handleSubmit(event){
    event.preventDefault();
    email.current.setCustomValidity('');

    const isValid = event.target.form.checkValidity();
    console.log(isValid);
    if(!isValid)
    {
      event.target.form.reportValidity();
      return;
    }

    const response = await ResetPasswordSendEmail(email.current.value);

    if(response.ok){
      console.log('Manager forgot password feature');
      localStorage.setItem(localStorageForgotPasswordEmail, email.current.value);
      navigate(ManagerResetPasswordRoute);
      return;
    }

    email.current.setCustomValidity('Электронная почта не найдена');
    event.target.form.reportValidity();
  }

  return (
    <div>
      <div className='main-cont'>
          <div className='child left-cont'>
            <div className='greet-cont'>
              <h1 className='greet'>Welcome</h1><br/><br/><br/>
              <p className='desc-text'>Мы сожалеем что вы забыли пароль от своего аккаунта.</p><br/>
              <p className='desc-text'>Если у вас все еще есть доступ к своей почте, мы сможем отправить вам код подтверждения для того чтобы вы смогли создать новый пароль.</p><br/>
              <p className='desc-text'>Если вы потеряли доступ к своей электронной почте, пожалуйста свяжитесь со своим ответственным менеджером по заказу, либо по номеру +770012345678!</p>
            </div>
          </div>

          <div className='child right-cont'>
            <div className='reg-link'>
              <Link to={ManagerAuthRoute}>Авторизация</Link>
            </div>
            <h1 className='auth-text'>Восстановление пароля</h1>
            <div className='form-input'>
              <form>
                <input ref={email} name='email' className='input-field' placeholder='Email' type='email' required/>
                
                <button onClick={handleSubmit} className='btn-auth'>Отправить код</button>
              </form>
            </div>

          </div>
      </div>
    </div>
  )
}

export default ManagerForgotPassword
