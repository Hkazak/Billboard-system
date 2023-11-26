// NEW START #3

import React from 'react'
import { useState } from 'react';
import Sidebar from '../components/SideBar'
import Table from 'react-bootstrap/Table';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import 'bootstrap/dist/css/bootstrap.min.css';
import { data } from '../data.js';
import ReactPaginate from "react-paginate";
import './page_styles/AllBillboards.css'
import Button from 'react-bootstrap/Button';
import TESTING from './TESTING.js';
import Success from './Success.js';
import AN from './AN.js';


function AllBillboards() {
    const [isPopup1Open, setPopup1Open] = useState(false);
    const [isPopup2Open, setPopup2Open] = useState(false);
    const [isPopup3Open, setPopup3Open] = useState(false);

  
    const openPopup1 = () => {
      setPopup1Open(true);
    };
  
    const closePopup1 = () => {
      setPopup1Open(false);
    };
  
    const openPopup2 = () => {
      setPopup2Open(true);
    };
  
    const closePopup2 = () => {
      setPopup2Open(false);
    };
  
    const openPopup3 = () => {
        setPopup2Open(true);
      };
    
      const closePopup3 = () => {
        setPopup2Open(false);
      };






    const [modalActive, setModalActive] = useState(false);
    const [modalActive1, setModalActive1] = useState(false);


    const [users, setUsers] = useState(data.slice(0, 50));
    const [pageNumber, setPageNumber] = useState(0);
  
    const usersPerPage = 3;
    const pagesVisited = pageNumber * usersPerPage;
  
    const displayUsers = users
      .slice(pagesVisited, pagesVisited + usersPerPage)
      .map((user) => {
        return (
          <>
          
          <div className="user">
            <div className='desc-main-wrapper'>
                <div className='pers-info'>
                    <p> 
                        Имя: {user.first_name} {user.last_name}
                    </p>
                    <p>
                        E-mail: {user.email}
                    </p>
                </div>

                <div className='arr-btn'>
                    <div >


                        {/* SUCCESS */}
                    {/* <div className='Apped'> 
                    

                    <header className="App-header">
                    <AN isOpen={isPopup1Open} closeModal={closePopup1} title="Form 1">
                       
                        <div className="conteiner">
                            <div className="form">
                            
                                <form>
                                    <p className="input"> <i className="gg-check">   </i> Менеджер успешно создан</p>
                                    <div className='pop-create'>
                                
                                    </div>
                                    
                                </form>
                            </div>
                        </div>
                    </AN>
                    </header>
                
                </div> */}




                        {/* UPDATE */}

                        <AN isOpen={isPopup1Open} closeModal={closePopup1} >
                        <div className="conteiner">
                        <h3>Редактирование менеджера</h3>
                        <div className="form">
                        
                            <form>
                                <input
                                type="text"
                                required
                                placeholder="Имя"
                                id="name1"
                                className="input"
                                />
                                <input
                                type="text"
                                required
                                placeholder="Фамилия"
                                id="name2"
                                className="input"
                                />
                                <input
                                type="text"
                                required
                                placeholder="Отчество"
                                id="name3"
                                className="input"
                                />
                                <input
                                type="email"
                                required
                                placeholder="Email"
                                id="email"
                                className="input"
                                />
                                <input
                                type="password"
                                required
                                placeholder="Мобильный телефон"
                                id="pass1"
                                className="input"
                                />
                                <div className='pop-create'>
                                    <Button variant="warning">Редактировать</Button>{' '}
                                </div>
                                
                            </form>
                        </div>
                    </div>
                        </AN>

                        <Button  variant="warning">Заморозить</Button>{' '}
                        <Button onClick={openPopup1} variant="warning">Редактирование</Button>{' '}
                    </div>
                </div>
            </div>    
          </div>
          </>
        );
      });
  
    const pageCount = Math.ceil(users.length / usersPerPage);
  
    const changePage = ({ selected }) => {
      setPageNumber(selected);
    };
  
    return (
        <>
            <Sidebar>

            <h1>React Distinct Pop-Up Forms</h1>

            <button onClick={openPopup1}>Open Form 1</button>
            <button onClick={openPopup2}>Open Form 2</button>

            <AN isOpen={isPopup1Open} closeModal={closePopup1} title="Form 1">
            {/* Customize Form 1 content here */}
            <form>
                <label>
                Name for Form 1:
                <input type="text" />
                </label>
                {/* Additional form fields for Form 1 */}
            </form>
            </AN>

            <AN isOpen={isPopup2Open} closeModal={closePopup2} title="Form 2">
            {/* Customize Form 2 content here */}
            <form>
                <label>
                Name for Form 2:
                <input type="text" />
                </label>
                {/* Additional form fields for Form 2 */}
            </form>
            </AN>





            <div className="Apps">
            
            {/* POP-UP  ADD MANAGER*/}
            <div className='Apped'> 
                

                <header className="App-header">


                <AN isOpen={isPopup2Open} closeModal={closePopup2} >
                    {/* Customize Form 2 content here */}
                    <div className="conteiner">
                        <h3>Добавление менеджера</h3>
                        <div className="form">
                        
                            <form>
                                <input
                                type="text"
                                required
                                placeholder="Имя"
                                id="name1"
                                className="input"
                                />
                                <input
                                type="text"
                                required
                                placeholder="Фамилия"
                                id="name2"
                                className="input"
                                />
                                <input
                                type="text"
                                required
                                placeholder="Отчество"
                                id="name3"
                                className="input"
                                />
                                <input
                                type="email"
                                required
                                placeholder="Email"
                                id="email"
                                className="input"
                                />
                                <input
                                type="password"
                                required
                                placeholder="Мобильный телефон"
                                id="pass1"
                                className="input"
                                />
                                <div className='pop-create'>
                                    <Button variant="warning">Создать</Button>{' '}
                                </div>
                                
                            </form>
                        </div>
                    </div>
                 </AN>
                </header>
            </div>


                <h1>Управление аккаунтами</h1>
                <div className='main-select-cont'>
                    <div className='searchbar-wrapper'>
                        {/* <div className='searchbarr'>

                            <input placeholder='SOME'/>
                        </div> */}

                        
                        <div className="search">
                            <input type="text" className="searchTerm" placeholder="What are you looking for?"/>
                            <button type="submit" className="searchButton">
                                <i className="fa fa-search">🛃</i>
                            </button>
                        </div>
                        




                        <div className='manager-create-btn'>
                            <Button onClick={openPopup2} variant="warning">Создать менеджера</Button>{' '}
                        </div>
                    </div>
                </div>
                {displayUsers}
                <ReactPaginate
                previousLabel={"Previous"}
                nextLabel={"Next"}
                pageCount={pageCount}
                onPageChange={changePage}
                containerClassName={"paginationBttns"}
                previousLinkClassName={"previousBttn"}
                nextLinkClassName={"nextBttn"}
                disabledClassName={"paginationDisabled"}
                activeClassName={"paginationActive"}
                />
            </div>
            </Sidebar>
      </>
    );
  

}


export default AllBillboards
