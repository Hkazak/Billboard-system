import React from 'react'
import './page_styles/UserAuthorization.css'
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom'

function ForgotPassword() {

    const navigate = useNavigate()

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
              <Link to="/reg">Registration</Link>
            </div>
            <h1 className='auth-text'>Восстановление пароля</h1>
            <div className='form-input'>
              <form>
                <input name='email' className='input-field' placeholder='Email' type='email' required/>
                
                <button onClick={() => {navigate("/alter")}} className='btn-auth'>Отправить код</button>
              </form>
            </div>

          </div>
      </div>
    </div>
  )
}

export default ForgotPassword
