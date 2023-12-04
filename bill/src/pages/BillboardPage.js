import React from 'react'
import Sidebar from '../components/SideBar'
import 'bootstrap/dist/css/bootstrap.min.css';
import './page_styles/BillboardPage.css'
import Header from "../components/Header";
import ControlPanel from "../components/ControlPanel";

// TODO add implementation if design will be defined
function BillboardPage() {
    return (
        <div className="billboard-block">
            <Header title="Управление билбордами"/>
            <Sidebar>
                <ControlPanel handleSearch={(e) => console.log(e)} placeholderSearchText={"Название билборда"}
                              createButtonText="Новый билборд" handleCreateItem={(e) => console.log(e)}
                              isClientView={true}/>
            </Sidebar>
        </div>
    );
}


export default BillboardPage
