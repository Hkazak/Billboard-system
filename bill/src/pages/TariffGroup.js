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

function TariffGroup() {
    const [groups, setGroups] = useState([]);
    const navigate = useNavigate();
    useEffect(() => {
        GetGroupOfTariffsList()
            .then(e => e.json())
            .then(e => {
                console.log(e);
                setGroups(e.map(g => <GroupOfTariffs name={g.name} key={g.id} tariffs={g.tariffs}/>))
            });
    }, []);

    return (
        <div className="page-content">
            <Header title={"Группа тарифов"}/>
            <Sidebar>
                <div className="control-panel">
                    <input type="text" placeholder="Search" className="search"/>
                    // TODO add feature to create new group of tariffs
                    <button type="button" className="create-new-group-of-tariffs-button" data-toggle="modal"
                            data-target="#staticBackdrop">
                        <span className="new-tarif">Новая группа</span>
                    </button>
                </div>
                {groups}
            </Sidebar>
        </div>
    )
}

export default TariffGroup;