import React, {useEffect, useState} from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import './page_styles/AllBillboards.css'
import './page_styles/Admin.css'
import Header from "../components/Header";
import ControlPanel from "../components/ControlPanel";
import Sidebar from "../components/SideBar";
import {GetManagersList} from "../lib/controllers/AdministratorController";
import ManagerInfo from "../components/ManagerInfo";
import CreateManager from "../components/CreateManager";

const data = [];

function ManagerPage() {
    const [searchText, setSearchText] = useState('');
    const [managers, setManagers] = useState([]);
    const [hideCreateManagerPanel, setHideCreateManagerPanel] = useState(true);

    function handleSearch(e)
    {
        setSearchText(e.target.value);
    }

    function handleCreateManagerPanelVisible(e)
    {
        setHideCreateManagerPanel(!hideCreateManagerPanel);
    }

    function handleNewManager(manager)
    {
        setManagers([...managers, manager]);
    }

    useEffect(()=>
    {
        GetManagersList()
            .then(e=>e.json())
            .then(e=>setManagers(e));
    }, []);
    return (
        <div className="manager-block">
            <Header title="Управление аккаунтами" />
            <CreateManager hide={hideCreateManagerPanel} setHide={setHideCreateManagerPanel} handleNewManager={handleNewManager} />
            <Sidebar>
                <ControlPanel isClientView={false} handleSearch={handleSearch} placeholderSearchText="Название" handleCreateItem={handleCreateManagerPanelVisible} createButtonText="Создать менеджера" />
                {
                    managers.filter(e=>(e.firstName + e.middleName + e.lastName + e.email + e.phone).includes(searchText))
                        .map(e=><ManagerInfo firstName={e.firstName} middleName={e.middleName} lastName={e.lastName} email={e.email} phone={e.phone} status={e.status} key={e.id} id={e.id}/>)
                }
            </Sidebar>
        </div>
    );
  

}

export default ManagerPage
