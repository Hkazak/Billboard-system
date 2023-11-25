import React from 'react'
import { useState } from 'react';
import Sidebar from '../components/SideBar'
import './page_styles/TariffPages.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import './page_styles/AllBillboards.css'
import { useNavigate } from 'react-router';
import CreateTariff from "../components/CreateTariff";
import { useEffect } from 'react';
import { GetTariffs } from '../lib/controllers/TariffController.js';
import Header from "../components/Header";
import ControlPanel from "../components/ControlPanel";
import Tariff from "../components/Tariff";

function TariffPage() {

    const [isCreation, setIsCreation] = useState(false);
    const [searchText, setSearchText] = useState('');

    function handleCreatePanel(e) {
        setIsCreation(!isCreation);
    }

    function handleSearch(e)
    {
        setSearchText(e.target.value);
    }

    const navigate = useNavigate();

    const [tariffs, setTariffs] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);

    useEffect(() => {
        GetTariffs()
            .then(e=>e.json())
            .then(e=>setTariffs(e));
    }, []);

    return (
        <div className="tariff-page-block">
            <Header title={"Тарифы"} />
            <CreateTariff isEnabled={isCreation} setIsEnabled={setIsCreation} tariffs={tariffs} setTariffs={setTariffs} setPage={setPageNumber} />
            <Sidebar>
                <ControlPanel handleCreateItem={handleCreatePanel} handleSearch={handleSearch} createButtonText={"Новый тариф"} placeholderSearchText={"Название"} />
                {tariffs.filter(e=>e.title.includes(searchText)).map(t=><Tariff tariffTitle={t.title} startTime={t.startTime} endTime={t.endTime} tariffPrice={t.price} />)}
            </Sidebar>
        </div>
    )
}

export default TariffPage
