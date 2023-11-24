import React, {useEffect} from 'react'
import {useState} from 'react';
import Sidebar from '../components/SideBar';
import './page_styles/TariffPages.css';
import './page_styles/AllBillboards.css'
import './page_styles/TariffGroup.css'
import {useNavigate} from 'react-router';
import {GetGroupOfTariffsList} from "../lib/controllers/TariffGroupController";
import GroupOfTariffs from "../components/GroupOfTariffs";
import Header from "../components/Header";
import CreateGroupOfTariff from "../components/CreateGroupOfTariff";

function TariffGroup() {
    const [groups, setGroups] = useState([]);
    const [hideCreateGroupOfTariffsBlock, setHideCreateGroupOfTariffsBlock] = useState(true);
    const navigate = useNavigate();
    useEffect(() => {
        refreshGroupOfTariffsList();
    }, []);

    function refreshGroupOfTariffsList()
    {
        GetGroupOfTariffsList()
            .then(e => e.json())
            .then(e => setGroups(e));
    }

    function handleCreateGroupOfTariffsBlock()
    {
        setHideCreateGroupOfTariffsBlock(!hideCreateGroupOfTariffsBlock);
    }

    return (
        <div className="page-content">
            <CreateGroupOfTariff show={hideCreateGroupOfTariffsBlock} groupsOfTariffs={groups} setGroupsOfTariffs={setGroups} />
            <Header title={"Группа тарифов"}/>
            <Sidebar>
                <div className="control-panel">
                    <input type="text" placeholder="Search" className="search"/>
                    <button type="button" className="open-panel-create-new-group-of-tariffs-button" data-toggle="modal"
                            data-target="#staticBackdrop" onClick={(e)=>handleCreateGroupOfTariffsBlock()}>
                        <span className="new-tarif">Новая группа</span>
                    </button>
                </div>
                {groups.map(g => <GroupOfTariffs name={g.name} key={g.id} tariffs={g.tariffs}/>)}
            </Sidebar>
        </div>
    )
}

export default TariffGroup;