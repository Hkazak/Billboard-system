import React, { useState } from 'react'

import './page_styles/CreateBill.css'
import Sidebar from '../components/SideBar'
import Button from 'react-bootstrap/esm/Button'
import './page_styles/CreateBill.css'
import { useNavigate } from 'react-router'
import { useRef } from 'react'
import { useEffect } from 'react'
import { GetGroupOfTariffs, getBillboardSurfacesList } from '../lib/controllers/TarrifsController'
import { CreateBillboard } from '../lib/controllers/BillboardController'

let isInitialized = false;

function CreateBill() {

  var [surfaceList, setSurfaceList] = useState([]);
  var [groupOfTariffs, setGroupOfTariffs] = useState([]);
  const name = useRef(null);
  const address = useRef(null);
  const description = useRef(null);
  const tariff = useRef(null);
  const surface = useRef(null);
  const penalty = useRef(null);
  const height = useRef(null);
  const width = useRef(null);
  const image = useRef(null);

  async function initialize() {
    const surfaceListResponse = await getBillboardSurfacesList();
    const groupOfTariffsResponse = await GetGroupOfTariffs();

    if (surfaceListResponse.ok) {
      const temp = await surfaceListResponse.json();
      setSurfaceList(temp);
    }

    if (groupOfTariffsResponse.ok) {
      const temp = await groupOfTariffsResponse.json();
      setGroupOfTariffs(temp);
    }
  }

  useEffect(() => {
    if (isInitialized) {
      return;
    }

    initialize();
    isInitialized = true;
  });

  const navigate = useNavigate();

  async function handleClick(event) {
    event.preventDefault();

    const isValid = event.target.form.checkValidity();
    console.log(isValid);
    if (!isValid) {
      event.target.form.reportValidity();
      return;
    }
    console.log(tariff.current.value);
    var response = await CreateBillboard(name.current.value, address.current.value, description.current.value, tariff.current.value, "", surface.current.value, penalty.current.value, height.current.value, width.current.value, "");
    if(response.ok){
      navigate("/billboards");
      return;
    }
  }

  return (
    <div>
      <div className='cre-bill-wrapper'>

        <form className='b-form'>
          <h3>Создание билбордов</h3>
          <br /><br />
          <input ref={name} placeholder='Название' type="text" name="bill-name" required />

          <br /><br />
          <textarea ref={description} placeholder='Описание' name="comment" form="desc-usrform"></textarea>
          <br /><br />

          <select ref={tariff}>
            <option value="" disabled selected hidden>Тариф</option>

            {
              groupOfTariffs.map((el) => {
                console.log(el);
                return <>
                  <option value={el.id}>{el.name}</option>
                </>
              })
            }
          </select>

          <br /><br />

          <select ref={surface}>
            <option value="" disabled selected hidden>Тип поверхности</option>
            {
              surfaceList.map((el) => {
                console.log(el);
                return <>
                  <option value={el.id}>{el.surface}</option>
                </>
              })
            }
          </select>

          <br /><br />

          <input ref={address} placeholder='Адрес' type="text" name="quantity" required />

          <br /><br />

          <input ref={penalty} placeholder='Штраф за изменения' type="text" name="money" required />

          <br /><br />

          <div className='two-blocks'>
            <div><input ref={height} className='b-1' placeholder='Высота' type="text" name="money" required /></div>

            <div className='lee'><input ref={width} className='b-2' placeholder='Длина' type="text" name="money" required /></div>
          </div>

          <br /><br />


          {/* <label for="dob">Date of Birth:</label>
        <input type="date" id="dob" name="dob" required/> */}

          {/* <label for="file">Фотография</label>
          <input ref={image} type="file" id="file" name="file" /> */}

          <div className='cr-bills-btn'><Button onClick={handleClick} variant='warning' type="submit">Submit</Button></div>

        </form>
      </div>
    </div>

  )
}

export default CreateBill
