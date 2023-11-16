import React from 'react'

import './page_styles/CreateBill.css'
import Sidebar from '../components/SideBar'
import Button from 'react-bootstrap/esm/Button'
import './page_styles/CreateBill.css'
import { useNavigate } from 'react-router'

function CreateBill() {

  const navigate = useNavigate();
  
  return (
    <div>
      <div className='cre-bill-wrapper'>
        
        <form className='b-form'>
        <h3>Создание билбордов</h3>
        <br/><br/>
        <input placeholder='Название' type="text" name="bill-name" required/>

        <br/><br/>
        <textarea placeholder='Описание' name="comment" form="desc-usrform"></textarea>
        <br/><br/>
        
        <select>
          <option value="" disabled selected hidden>Тариф</option>
          <option value="1">A</option>
          <option value="2">B</option>
          <option value="3">C</option>
        </select>

        <br/><br/>

        <select>
          <option value="" disabled selected hidden>Тип поверхности</option>
          <option value="4">A</option>
          <option value="5">B</option>
          <option value="6">C</option>
        </select>
        
        <br/><br/>

        <input placeholder='Количество объявлений за тариф' type="text" name="quantity" required/>

        <br/><br/>

        <input placeholder='Штраф за изменения' type="text" name="money" required/>

        <br/><br/>

        <div className='two-blocks'>
          <div><input className='b-1' placeholder='Высота' type="text" name="money" required/></div>
          
          <div className='lee'><input className='b-2' placeholder='Длина' type="text" name="money" required/></div>
        </div>

        <br/><br/>


        {/* <label for="dob">Date of Birth:</label>
        <input type="date" id="dob" name="dob" required/> */}

        <label for="file">Фотография</label>
        <input type="file" id="file" name="file" accept=".pdf, .doc, .docx"/>

        <div className='cr-bills-btn'><Button onClick={()=>{navigate('/billboards')}} variant='warning' type="submit">Submit</Button></div>

        

        </form>
      </div>
    </div>
 
  )
}

export default CreateBill
