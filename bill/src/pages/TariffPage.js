import React, { useRef } from 'react'
import { useState } from 'react';
import Sidebar from '../components/SideBar'
import './page_styles/TariffPages.css'
import im from './logo.png'
import 'bootstrap/dist/css/bootstrap.min.css';
import { data } from '../data.js';
import ReactPaginate from "react-paginate";
import './page_styles/AllBillboards.css'

import { useNavigate } from 'react-router';
import CreateTariff from "../components/CreateTariff";
import { useEffect } from 'react';
import { GetTariffs } from '../lib/controllers/TariffController.js';

var isInitialized = false;
var lastSearch = '';

function TariffPage() {

    const [isCreation, setIsCreation] = useState(false);
    function openCreateTariff() {
        setIsCreation(!isCreation);
    }

    const navigate = useNavigate();

    const [users, setUsers] = useState([]);
    const [pageNumber, setPageNumber] = useState(0);

    const search = useRef(null);
    async function onSearch(ev) {
        lastSearch = search.current.value;
        console.log(search.current.value);
        let tariffsListResponse = await GetTariffs();
        if (tariffsListResponse.ok) {
            let json = await tariffsListResponse.json();

            if (lastSearch === search.current.value) {
                let searchResult = json.filter((tariff) => tariff.title.toLowerCase().includes(search.current.value.toLowerCase()));
                setUsers(searchResult);
            }
        }
    }

    async function initialize() {
        let tariffsListResponse = await GetTariffs();
        if (tariffsListResponse.ok) {
            let json = await tariffsListResponse.json();
            setUsers(json);
        }
    }

    useEffect(() => {
        if (isInitialized) {
            return;
        }

        initialize();
        isInitialized = true;
    });

    const usersPerPage = 8;
    const pagesVisited = pageNumber * usersPerPage;

    const pageCount = Math.ceil(users.length / usersPerPage);

    const changePage = ({ selected }) => {
        setPageNumber(selected);
    };

    const displayUsers1 = users
        .slice(pagesVisited, pagesVisited + usersPerPage / 2)
        .map((user) => {
            return (
                <>
                    <div className="tarif">
                        <p>{user.title} {user.last_name}</p>
                        <p>{user.startTime.slice(0, 5)}-{user.endTime.slice(0, 5)}</p>
                        <p>{user.price} тенге</p>
                    </div>

                </>
            );
        });

    const displayUsers2 = users
        .slice(pagesVisited + usersPerPage / 2, pagesVisited + usersPerPage)
        .map((user) => {
            return (
                <>
                    <div className="tarif">
                        <p>{user.title} {user.last_name}</p>
                        <p>{user.startTime.slice(0, 5)}-{user.endTime.slice(0, 5)}</p>
                        <p>{user.price} тенге</p>
                    </div>

                </>
            );
        });

    const tariffs =
        <>
            <div className='inner'>
                <div className='main-content'>
                    <div className='tarifs'>
                        {displayUsers1}
                    </div>
                </div>
            </div>


            <div className='inner'>
                <div className='main-content'>
                    <div className='tarifs'>
                        {displayUsers2}
                    </div>
                </div>
            </div>
        </>


    return (
        <div>

            <CreateTariff isEnabled={isCreation} setIsEnabled={setIsCreation} tariffs={users} setTariffs={setUsers} setPage={setPageNumber} />
            <Sidebar>
                <header>
                    <div className="logo"><img src={im} alt="" /></div>
                    <div className="header-top"><h1>Тарифы</h1></div>
                </header>


                <div className="search-add-tarif1">
                    <div className="search-container">
                        <button className="search-btn"><img src="./static/search-icon.png" alt="" /></button>
                        <input onChange={onSearch} ref={search} type="text" id="search-input" placeholder="Search" />
                    </div>
                    <button onClick={openCreateTariff} type="button" className="btn btn-primary">
                        <p className="new-tarif">Новый тариф</p>
                    </button>
                </div>


                {/* POP UP */}

                {/* <div className="modal-dialog modal-dialog-centered">
    <div className="modal-content">
      <div className="header-new-tarif">
       <h2>Новый тариф</h2> 
       <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <div className="new-tarif-info">
    	<p>Общая информация</p>
    	<input type="text" placeholder="Название" className="new-tarif-name"/>
    	<input type="text" placeholder="Цена" className="new-tarif-cost"/>
    	<p>Временные отрезки</p>
    	<div className="checkbox-allday">
    		<a href="">
    			<img src="./static/checkbox_icon.png" alt="" width="17px" height="17px"/>
    		</a>
    		<p>Целый день</p>
    	</div>
    	<div className="time-interval">
    		<input type="time" placeholder="00:00" className="start-time-input"/>
    		<img src="./static/line-time-icon.png" alt="" width="12px"/>
    		<input type="time" className= "end-time-input"/>

    	</div>
    	<button className="create-new-tarif">
    		Создать
    	</button>
    </div>
  </div>
</div> */}

                {/* END POP */}


                {tariffs}
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
                    forcePage={pageNumber}
                />

            </Sidebar>

        </div>
    )
}

export default TariffPage
