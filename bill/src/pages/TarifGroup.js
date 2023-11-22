import React from 'react'
import { useState } from 'react';
import Sidebar from '../components/SideBar'
import './page_styles/TarifPages.css'
import im from './logo.png'
import Table from 'react-bootstrap/Table';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import 'bootstrap/dist/css/bootstrap.min.css';
import { data } from '../data.js';
import ReactPaginate from "react-paginate";
import './page_styles/AllBillboards.css'
import Button from 'react-bootstrap/Button';
import AN from './AN.js';

import prof from '../assets/subway.jpg'
import { useNavigate } from 'react-router';

function TarifGroup() {

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

    const navigate = useNavigate();

    const [users, setUsers] = useState(data.slice(0, 50));
    const [pageNumber, setPageNumber] = useState(0);
  
    const usersPerPage = 2;
    const pagesVisited = pageNumber * usersPerPage;

    const displayUsers = users
      .slice(pagesVisited, pagesVisited + usersPerPage)
      .map((user) => {
        return (
          <>
            <div class="inner">
			
            <div className="main-content">
            
               
                <div class="accordion" id="accordionExample">
      <div class="accordion-item">
        <h2 class="accordion-header">
          <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
            Название
          </button>
        </h2>
        <div id="collapseOne" class="accordion-collapse collapse show" data-bs-parent="#accordionExample">
          <div class="accordion-body">
            <div class="tarif-in-group">
            <p>Имя: {user.first_name} {user.last_name}</p>
                    <p>10:00-14:00 <br/>20:00-23:59</p>
                    <p>18.000 тенге</p>
                </div>
            <div class="tarif-in-group">
                    <p>Название тарифа</p>
                    <p>10:00-14:00 <br/>20:00-23:59</p>
                    <p>18.000 тенге</p>
                </div>
            <div class="tarif-in-group">
                    <p>Название тарифа</p>
                    <p>10:00-14:00 <br/>20:00-23:59</p>
                    <p>18.000 тенге</p>
                </div>
          </div>
        </div>
      </div>


      
     
    </div>
            </div>
        </div>
    
    











                {/* <div className='arr-btn'>
                    <div >



                        <Button onClick={()=>{navigate('/bill-descr')}} variant="warning">Редактирование</Button>{' '}
                    </div>
                </div> */}
          </>
        );
      });



    const pageCount = Math.ceil(users.length / usersPerPage);
  
    const changePage = ({ selected }) => {
      setPageNumber(selected);
    };



  return (

    
    <div>
        <Sidebar>
        <header>
            <div class="logo"><img src={im} alt=""/></div>
            <div class="header-top"><h1>Тарифы</h1></div>
	    </header>

        <div className="main-content-forses">
        <div class="search-add-tarif">
				<div class="search-container">
					<button class="search-btn"><img src="./static/search-icon.png" alt=""/></button>
   					<input type="text" id="search-input" placeholder="Search"/>
				</div>
                <button onClick={openPopup1} type="button" class="btn btn-primary" data-toggle="modal" data-target="#staticBackdrop">
					<p class="new-tarif">Новая группа</p>
				</button>
			</div>
        </div>

  {/* POP UP */}
            <AN isOpen={isPopup1Open} closeModal={closePopup1} title="Form 1">
            <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content modal-content-2">
      <div class="header-new-tarif groups">
       <h2>Новая группа тарифов</h2> 
    </div>
    <div class="new-tarif-info groups">
    	<p>Общая информация</p>
    	<input type="text" placeholder="Название" class="new-group-name"/>
    	<p>Тарифы</p>
    	<div class="search-new-group">
    		<div class="search-container groups">
					<button class="search-btn"><img src="./static/search-icon.png" alt=""/></button>
   					<input type="text" id="search-input" placeholder="Search"/>
			</div>
			<div class="pagination-new-group">
				<div class="back-icon-tarifs">
					<a href=""><img src="./static/left_icon.png" alt=""/></a>
				</div>
				<div class="forward-icon-tarifs">
					<a href="#"><img src="./static/right_icon.png" alt=""/></a>
				</div>
			</div>
    	</div>
    	<div class="tarifs-new-group">
    		<div class="tarif-in-new-group">
				<p>Название тарифа</p>
				<p>10:00-14:00 <br/>20:00-23:59</p>
				<p>18.000 тенге</p>
			</div>
			<div class="tarif-in-new-group">
				<p>Название тарифа</p>
				<p>10:00-14:00 <br/>20:00-23:59</p>
				<p>18.000 тенге</p>
			</div>
    	</div>
    	<p>Выбранные Тарифы</p>
    	<div class="tarifs-new-group">
    		<div class="tarif-in-new-group">
				<p>Название тарифа</p>
				<p>10:00-14:00 <br/>20:00-23:59</p>
				<p>18.000 тенге</p>
			</div>
			<div class="tarif-in-new-group">
				<p>Название тарифа</p>
				<p>10:00-14:00 <br/>20:00-23:59</p>
				<p>18.000 тенге</p>
			</div>
    	</div>
    	<button class="create-new-group">
    		Создать
    	</button>
    </div>
  </div>
</div>
            </AN>





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

</Sidebar>


</div>
  )
}

export default TarifGroup
