import React, { useRef } from 'react'
import { useState } from 'react';
import Sidebar from '../components/SideBar'
import './page_styles/TariffPages.css'
import im from './logo.png'
import 'bootstrap/dist/css/bootstrap.min.css';
import ReactPaginate from "react-paginate";
import './page_styles/AllBillboards.css'

import { useNavigate } from 'react-router';
import CreateTariff from "../components/CreateTariff";
import { useEffect } from 'react';
import { GetTariffs } from '../lib/controllers/TariffController.js';
import Header from "../components/Header";

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

            <Header title={"Тарифы"} />
            <CreateTariff isEnabled={isCreation} setIsEnabled={setIsCreation} tariffs={users} setTariffs={setUsers} setPage={setPageNumber} />
            <Sidebar>
                <div className="search-add-tarif1">
                    <div className="search-container">
                        <button className="search-btn"><img src="./static/search-icon.png" alt="" /></button>
                        <input onChange={onSearch} ref={search} type="text" id="search-input" placeholder="Search" />
                    </div>
                    <button onClick={openCreateTariff} type="button" className="btn btn-primary">
                        <p className="new-tarif">Новый тариф</p>
                    </button>
                </div>


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
