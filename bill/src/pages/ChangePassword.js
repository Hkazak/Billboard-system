import React from 'react'

import './page_styles/UserAuthorization.css'
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom'

function ChangePassword() {
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
            <h1 className='auth-text'>Восстановление пароля</h1>
            <div className='form-input'>
              <form>
                <input name='verification-code' className='input-field' placeholder='Код' type='text' required/>
                <input name='new_password' className='input-field' placeholder='Новый пароль' type='password' required/>
                <input name='new_password_val' className='input-field' placeholder='Подтверждение пароля' type='password' required/>
                <button  className='btn-auth'>Изменить пароль</button>
              </form>
            </div>

          </div>
      </div>

    </div>
  )
}

export default ChangePassword
