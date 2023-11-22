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

function TarifPage() {

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

    const pageCount = Math.ceil(users.length / usersPerPage);
  
    const changePage = ({ selected }) => {
      setPageNumber(selected);
    };

    const displayUsers = users
      .slice(pagesVisited, pagesVisited + usersPerPage)
      .map((user) => {
        return (
          <>

<div class="inner">
		<div class="main-content">
			
			<div class="tarifs">
			<div class="tarif">
				<p>Имя: {user.first_name} {user.last_name}</p>
				<p>10:00-14:00 <br/>20:00-23:59</p>
				<p>18.000 тенге</p>
			</div>
			<div class="tarif">
				
			</div>
			<div class="tarif">
				
			</div>
			<div class="tarif">
				
			</div>
			</div>
            </div>
        </div>

          </>
        );
      });





  return (
    <div>
      
        
        <Sidebar>
        <header>
            <div class="logo"><img src={im} alt=""/></div>
            <div class="header-top"><h1>Тарифы</h1></div>
	    </header>


        <div class="search-add-tarif1">
				<div class="search-container">
					<button class="search-btn"><img src="./static/search-icon.png" alt=""/></button>
   					<input type="text" id="search-input" placeholder="Search"/>
				</div>
				<button onClick={openPopup1} type="button" class="btn btn-primary" >
					<p class="new-tarif">Новый тариф</p>
				</button>
			</div>

       


    {/* POP UP */}

    {/* <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="header-new-tarif">
       <h2>Новый тариф</h2> 
       <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <div class="new-tarif-info">
    	<p>Общая информация</p>
    	<input type="text" placeholder="Название" class="new-tarif-name"/>
    	<input type="text" placeholder="Цена" class="new-tarif-cost"/>
    	<p>Временные отрезки</p>
    	<div class="checkbox-allday">
    		<a href="">
    			<img src="./static/checkbox_icon.png" alt="" width="17px" height="17px"/>
    		</a>
    		<p>Целый день</p>
    	</div>
    	<div class="time-interval">
    		<input type="time" placeholder="00:00" class="start-time-input"/>
    		<img src="./static/line-time-icon.png" alt="" width="12px"/>
    		<input type="time" class= "end-time-input"/>

    	</div>
    	<button class="create-new-tarif">
    		Создать
    	</button>
    </div>
  </div>
</div> */}

{/* END POP */}


<AN isOpen={isPopup1Open} closeModal={closePopup1} title="">
<div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="header-new-tarif">
       <h2>Новый тариф</h2> 
    </div>
    <div class="new-tarif-info">
    	<p>Общая информация</p>
    	<input type="text" placeholder="Название" class="new-tarif-name"/>
    	<input type="text" placeholder="Цена" class="new-tarif-cost"/>
    	<p>Временные отрезки</p>
    	<div class="checkbox-allday">
    		<a href="">
    			<img src="./static/checkbox_icon.png" alt="" width="17px" height="17px"/>
    		</a>
    		<p>Целый день</p>
    	</div>
    	<div class="time-interval">
    		<input type="time" placeholder="00:00" class="start-time-input"/>
    		<img src="./static/line-time-icon.png" alt="" width="12px"/>
    		<input type="time" class= "end-time-input"/>

    	</div>
    	<button class="create-new-tarif">
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

export default TarifPage
