import React, {useEffect} from 'react'
import {useState} from 'react';
import Sidebar from '../components/SideBar';
import './page_styles/TariffPages.css';
import './page_styles/AllBillboards.css'
import {useNavigate} from 'react-router';
import {GetGroupOfTariffsList} from "../lib/controllers/TariffGroupController";
import GroupOfTariffs from "../components/GroupOfTariffs";
import Header from "../components/Header";
import CreateGroupOfTariff from "../components/CreateGroupOfTariff";
import ControlPanel from "../components/ControlPanel";
import PaginationPanel from "../components/PaginationPanel";

function TariffGroup() {
    const [groups, setGroups] = useState([]);
    const [hideCreateGroupOfTariffsBlock, setHideCreateGroupOfTariffsBlock] = useState(true);
    const [searchText, setSearchText] = useState('');
    const [page, setPage] = useState(0);
    const [pageSize, setPageSize] = useState(0);
    const navigate = useNavigate();
    useEffect(() => {
        refreshGroupOfTariffsList();
    }, []);

    function refreshGroupOfTariffsList() {
        GetGroupOfTariffsList()
            .then(e => e.json())
            .then(e => setGroups(e));
    }

    function handleCreateGroupOfTariffsBlock(e) {
        setHideCreateGroupOfTariffsBlock(!hideCreateGroupOfTariffsBlock);
    }

    function handleSearch(e) {
        setSearchText(e.target.value);
    }

    function handleNewGroupOfTariffs(groupOfTariffs) {
        setGroups([...groups, groupOfTariffs]);
    }

    return (
        <div className="page-content">
            <CreateGroupOfTariff hide={hideCreateGroupOfTariffsBlock} setHide={setHideCreateGroupOfTariffsBlock}
                                 handleNewGroupOfTariffs={handleNewGroupOfTariffs}/>
            <Header title={"Группа тарифов"}/>
            <Sidebar>
                <ControlPanel handleCreateItem={handleCreateGroupOfTariffsBlock} handleSearch={handleSearch}
                              createButtonText={"Новая группа"} placeholderSearchText={"Название"}/>
                <PaginationPanel onPageChange={setPage} onPageSizeChange={setPageSize} />
                {groups.filter(e => e?.name?.toLowerCase()?.includes(searchText?.toLowerCase()))
                    .slice((page - 1) * pageSize, page * pageSize)
                    .map(g => <GroupOfTariffs name={g.name} key={g.id} tariffs={g.tariffs}/>)}
            </Sidebar>
        </div>
    )
}

export default TariffGroup;