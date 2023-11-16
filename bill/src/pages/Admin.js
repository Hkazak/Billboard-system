// NEW START #3

import React from 'react'
import { useState } from 'react';
import Sidebar from '../components/SideBar'
import 'bootstrap/dist/css/bootstrap.min.css';
import ReactPaginate from "react-paginate";
import './page_styles/AllBillboards.css'
import Button from 'react-bootstrap/Button';
import AN from './AN.js';
import './page_styles/Admin.css'
import { useRef } from 'react';
import { CreateManager, GetManagersList } from '../lib/controllers/AdministratorController.js';
import { useEffect } from 'react';

const data = [];

function Admin() {
    const [users, setUsers] = useState(data.slice(0, 50));
    
    async function initialize(){
        const response = await GetManagersList();
        const jsonResponse = await response.json();
    
        if(response.ok){
            data.push(...jsonResponse);
        }
    }

    useEffect(() => {
        initialize();
    });

    const [isPopup1Open, setPopup1Open] = useState(false);
    const [isPopup2Open, setPopup2Open] = useState(false);
  
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

    const createManager = {
        email: useRef(null),
        firstName: useRef(null),
        lastName: useRef(null),
        middleName: useRef(null),
        phone: useRef(null),
    };

    const [pageNumber, setPageNumber] = useState(0);
  
    const usersPerPage = 2;
    const pagesVisited = pageNumber * usersPerPage;

    async function handleCreateManager(event){
        event.preventDefault();
    
        const isValid = event.target.form.checkValidity();
        console.log(isValid);
        if(!isValid)
        {
          event.target.form.reportValidity();
          return;
        }
    
        const response = await CreateManager(
            createManager.email.current.value,
            createManager.firstName.current.value,
            createManager.lastName.current.value,
            createManager.middleName.current.value,
            createManager.phone.current.value);
        
        const jsonResponse = await response.json();
        if(response.ok){
            data.push(jsonResponse);

            setUsers(data);
            closePopup2();
            return;
        }


    }
  
    const displayUsers = users
      .slice(pagesVisited, pagesVisited + usersPerPage)
      .map((user) => {
        return (
          <>
          
          <div className="user">
            <div className='desc-main-wrapper'>
                <div className='pers-info'>
                    <p> 
                        {user.firstName} {user.middleName} {user.lastName}
                    </p>
                    <pre>
                        {user.email} &#9;&#9;&#9; {user.phone}
                    </pre>
                </div>

                <div className='arr-btn'>
                    <div >


                        {/* SUCCESS */}
                    {/* <div className='Apped'> 
                    

                    <header className="App-header">
                    <AN isOpen={isPopup1Open} closeModal={closePopup1} title="Form 1">
                       
                        <div class="conteiner">
                            <div class="form">
                            
                                <form>
                                    <p class="input"> <i class="gg-check">   </i> Менеджер успешно создан</p>
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
                        <div class="conteiner">
                        <h3>Редактирование менеджера</h3>
                        <div class="form">
                        
                            <form>
                                <input
                                type="text"
                                required
                                placeholder="Имя"
                                id="name1"
                                class="input"
                                />
                                <input
                                type="text"
                                required
                                placeholder="Фамилия"
                                id="name2"
                                class="input"
                                />
                                <input
                                type="text"
                                required
                                placeholder="Отчество"
                                id="name3"
                                class="input"
                                />
                                <input
                                type="email"
                                required
                                placeholder="Email"
                                id="email"
                                class="input"
                                />
                                <input
                                type="text"
                                required
                                placeholder="Мобильный телефон"
                                id="pass1"
                                class="input"
                                />
                                <div className='pop-create'>
                                    <Button variant="warning">Редактировать</Button>{' '}
                                </div>
                                
                            </form>
                        </div>
                    </div>
                        </AN>

                        {/* <Button  variant="warning">Заморозить</Button>{' '} */}
                        {/* <Button onClick={openPopup1} variant="warning">Редактирование</Button>{' '} */}
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

              {/* Example */}

            {/* <h1>React Distinct Pop-Up Forms</h1>

            <button onClick={openPopup1}>Open Form 1</button>
            <button onClick={openPopup2}>Open Form 2</button>

            <AN isOpen={isPopup1Open} closeModal={closePopup1} title="Form 1">
            <form>
                <label>
                Name for Form 1:
                <input type="text" />
                </label>
            </form>
            </AN>

            <AN isOpen={isPopup2Open} closeModal={closePopup2} title="Form 2">
            <form>
                <label>
                Name for Form 2:
                <input type="text" />
                </label>
            </form>
            </AN> */}





            <div className="Apps">
            
            {/* POP-UP  ADD MANAGER*/}
            <div className='Apped'> 
                

                <header className="App-header">


                <AN isOpen={isPopup2Open} closeModal={closePopup2} >
                    {/* Customize Form 2 content here */}
                    <div class="conteiner">
                        <h3>Добавление менеджера</h3>
                        <div class="form">
                        
                            <form>
                                <input
                                ref={createManager.firstName}
                                type="text"
                                required
                                placeholder="Имя"
                                id="name1"
                                class="input"
                                />
                                <input
                                ref={createManager.lastName}
                                type="text"
                                required
                                placeholder="Фамилия"
                                id="name2"
                                class="input"
                                />
                                <input
                                ref={createManager.middleName}
                                type="text"
                                required
                                placeholder="Отчество"
                                id="name3"
                                class="input"
                                />
                                <input
                                ref={createManager.email}
                                type="email"
                                required
                                placeholder="Email"
                                id="email"
                                class="input"
                                />
                                <input
                                ref={createManager.phone}
                                type="text"
                                required
                                placeholder="Мобильный телефон"
                                id="pass1"
                                class="input"
                                />
                                <div className='pop-create'>
                                    <Button onClick={handleCreateManager} variant="warning">Создать</Button>{' '}
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

                        
                        <div class="search">
                            <input type="text" class="searchTerm" placeholder="What are you looking for?"/>
                            <button type="submit" class="searchButton">
                                <i class="fa fa-search">🛃</i>
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

export default Admin
