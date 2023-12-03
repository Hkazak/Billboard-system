import Header from "../components/Header";
import Sidebar from "../components/SideBar";
import ControlPanel from "../components/ControlPanel";
import {useEffect, useState} from "react";
import {LS} from "../lib/Consts";
import {getBillboardSurfacesList} from "../lib/controllers/TarrifsController";
import BillboardSurface from "../components/BillboardSurface";
import CreateBillboardSurface from "../components/CreateBillboardSurface";

function BillboardSurfacePage()
{
    const [isClientView, setIsClientView] = useState(localStorage.getItem(LS.isClient) === 'true');
    const [searchText, setSearchText] = useState('');
    const [hideCreatePanel, setHideCreatePanel] = useState(true);
    const [surfaces, setSurfaces] = useState([]);

    function handleSearch(e)
    {
        setSearchText(e.target.value);
    }

    function handleNewSurface(surface)
    {
        setSurfaces([...surfaces, surface]);
    }

    useEffect(()=>{
        getBillboardSurfacesList()
            .then(e=>e.json())
            .then(e=>setSurfaces(e));
    }, []);
    return (
        <div className="billboard-surfaces-page">
            <Header title={"Виды поверхности"} />
            <CreateBillboardSurface hide={hideCreatePanel} setHide={setHideCreatePanel} onCreated={handleNewSurface} />
            <Sidebar>
                <ControlPanel isClientView={isClientView} createButtonText={"Создать поверхность"} placeholderSearchText={"Название"} handleCreateItem={e=>setHideCreatePanel(!hideCreatePanel)} handleSearch={handleSearch} />
                {surfaces?.filter(e=>e?.surface?.includes(searchText)).map(e=><BillboardSurface key={e.id} name={e.surface} />)}
            </Sidebar>
        </div>
    );
}

export default BillboardSurfacePage;