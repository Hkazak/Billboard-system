import React from 'react'
import './page_styles/BillDescr.css'
import prof from '../assets/subway.jpg'
import Button from 'react-bootstrap/esm/Button'

function BillDescr() {
    return (
        <div>

            <div className='decription-main-wrapper'>
                {/* RIGHT SIDE */}
                <div className='array-img-bills pad-img-move'>
                    <p>SOMOEWMOMO</p>
                    <img src={prof}/>

                </div>


                {/* LEFT SIDE */}
                <div className='array-img-bills pad-move'>
                    <div className='bill-text'>
                        <h3>Название билборда</h3>
                        <br/>
                        <div className='paragr-bill'>
                            <p>lolvplrplbpelrblerbplerblerplbpe reblerplbmperlmbpelrmbplmerberbdlmvdlvmldmvldmvlmvdmvd
                            </p>
                        </div>

                        <div className='charac-bill'>
                            <p>Вид поверхности.................Цифровой<br/><br/>
                                Размер...............................................3мХ3м
                            </p>
                            <br/>
                        </div>

                        <br/><br/>

                        <p className='discount-p'>Действующая акция</p>

                        <div className='tarif-bill'>
                            <p>Персональный<br/><br/>
                                <h4>10%</h4>
                                При заказе от
                            </p>
                            <br/>
                        </div>

                        <br/><br/>

                        <p className='discount-p'>Выбранный тариф</p>

                        <div className='tarif-bill1'>
                            <p>Название<br/><br/>
                                <h4>00:00-04:00</h4>
                                <h4>10:00-16:00</h4>
                                <div className='price-tagg'>
                                    <p>18000tg</p>
                                </div>
                            </p>
                            <br/>
                        </div>

                        <br/><br/>
                        <div>
                            <div>
                                <label htmlFor="dob">Начало:</label>
                                <input type="date" id="dob" name="dob" required/>
                            </div>
                            <br/>
                            <div>
                                <label for="dob">Конец:</label>
                                <input type="date" id="dob" name="dob" required/>
                            </div>
                        </div>

                        <div className='total-pricee'>
                            <h3>Итоговая цена:</h3>
                            <p>Изготовление..................</p>
                            <p>Аренда.....................................100000tg</p>
                            <br/><br/><br/>
                            <Button className='downloadd' variant='warning'>Скачать файлы</Button>

                            <br/><br/><br/><br/><br/><br/>
                            <div className='verify-btn'>
                                <Button className='childsss' variant='warning'>Подтвердить заказ</Button>
                                <Button className='childsss' variant='danger'>Отклонить заказ</Button>
                            </div>
                        </div>


                        {/* <br/><br/><br/><br/><br/><br/><br/>
          <div><Button variant='warning'>Скачать файлы</Button></div> */}


                    </div>

                </div>

            </div>


        </div>
    )
}

export default BillDescr
