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

import prof from '../assets/subway.jpg'
import { useNavigate } from 'react-router';


function BillPages() {

    const navigate = useNavigate();
    
   
    const [modalActive, setModalActive] = useState(false);
    const [modalActive1, setModalActive1] = useState(false);


    const [users, setUsers] = useState(data.slice(0, 50));
    const [pageNumber, setPageNumber] = useState(0);
  
    const usersPerPage = 2;
    const pagesVisited = pageNumber * usersPerPage;
  
    const displayUsers = users
      .slice(pagesVisited, pagesVisited + usersPerPage)
      .map((user) => {
        return (
          <>
          
          <div className="user">
            <div className='desc-main-wrapper3'>
                {/* <div className='pers-info'>

                    <p> 
                        Имя: {user.first_name} {user.last_name}
                    </p>
                    <p>
                        E-mail: {user.email}
                    </p>
                </div> */}

            <div className="profile-container">
                <div className="left-side">
                    <img src="" alt="Profile" />
                </div>
                <div className="right-side">
                    Имя: {user.first_name} {user.last_name}       <br/>E-mail: {user.email}
                </div>
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

                        <Button onClick={()=>{navigate('/bill-descr')}} variant="warning">Редактирование</Button>{' '}
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
            
            


                <h1>Управление билбордами</h1>
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


export default BillPages
