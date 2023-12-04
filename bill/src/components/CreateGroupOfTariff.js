import {useEffect, useState} from "react";
import {GetTariffs} from "../lib/controllers/TariffController";
import Tariff from "./Tariff";
import "../styles/CreateGroupOfTariffs.css";
import {CreateGroupOfTariffs} from "../lib/controllers/TariffGroupController";
import arrow from "../assets/arrow.png"

function CreateGroupOfTariff({hide, setHide, handleNewGroupOfTariffs}) {
    const [selectedTariffs, setSelectedTariffs] = useState([]);
    const [allTariffs, setAllTariffs] = useState([]);
    const [searchTariffName, setSearchTariffName] = useState('');
    const [name, setName] = useState('');
    const [pageNumber, setPageNumber] = useState(1);

    function selectTariff(tariff) {
        const tariffs = [
            ...selectedTariffs,
            tariff
        ];
        const distinctTariffs = [...new Set(tariffs)]
        setSelectedTariffs(distinctTariffs);
    }

    function unselectTariff(tariff) {
        setSelectedTariffs([
            ...selectedTariffs.filter(e => e.id !== tariff.id)
        ]);
    }

    function handleCreateNewGroupOfTariffs(e) {
        e.preventDefault();
        CreateGroupOfTariffs(name, selectedTariffs)
            .then(e => e.json())
            .then(e => handleNewGroupOfTariffs(e));
        setSelectedTariffs([]);
    }

    function changePage(e, page) {
        e.preventDefault();
        setPageNumber(Math.max(1, page));
    }

    function resetPanel(e) {
        e.preventDefault();
        e.target.form.reset();
        setHide(true);
        setSearchTariffName('');
        setName('');
        setPageNumber(1);
        setSelectedTariffs([]);
        refresh();
    }

    function refresh() {
        GetTariffs()
            .then(e => e.json())
            .then(e => setAllTariffs(e));
    }

    useEffect(() => {
        refresh();
    }, []);

    return (
        <form className="create-new-group-of-tariffs-block" hidden={hide}>
          <span className="create-new-group-of-tariffs-title">
              Новая группа тарифов
          </span>
            <div className="general-information">
                <span className="create-new-group-of-tariffs-general-span">Общая информация</span>
                <input type="text" className="group-of-tariffs-name-input" placeholder="Название" value={name}
                       onChange={(e) => setName(e.target.value)}/>
            </div>
            <span className="create-new-group-of-tariffs-general-span">Тарифы</span>
            <div className="search-controller-block">
                <input type="text" className="search-tariff-input" placeholder="Название тарифа"
                       onChange={(e) => setSearchTariffName(e.target.value)}/>
                <div className="pagination-controller-block">
                    <button className="current-page-change-button" onClick={(e) => changePage(e, pageNumber - 1)}>
                        <img src={arrow} alt="&lt;" className="reverse-arrow" width={10}/>
                    </button>
                    <input type="number" className="pagination-set-page" value={pageNumber}
                           onChange={(e) => changePage(e, parseInt(e.target.value))}/>
                    <button className="current-page-change-button" onClick={(e) => changePage(e, pageNumber + 1)}>
                        <img src={arrow} alt="&lt;" width={10}/>
                    </button>
                </div>
            </div>
            <div className="tariffs-list">
                {allTariffs.filter(e => e.title.includes(searchTariffName)).slice((pageNumber - 1) * 4, pageNumber * 4).map(t =>
                    <Tariff key={t.id} tariffTitle={t.title} startTime={t.startTime} endTime={t.endTime}
                            tariffPrice={t.price} onClickCallback={() => selectTariff(t)}/>)}
            </div>
            <span className="create-new-group-of-tariffs-general-span">Выбранные тарифы</span>
            <div className="selected-tariffs-list">
                {selectedTariffs.map(tariff => <Tariff key={tariff.id} tariffTitle={tariff.title}
                                                       startTime={tariff.startTime} endTime={tariff.endTime}
                                                       tariffPrice={tariff.price}
                                                       onClickCallback={() => unselectTariff(tariff)}/>)}
            </div>
            <div className="manage-buttons">
                <button className="create-new-group-of-tariffs-button"
                        onClick={(e) => handleCreateNewGroupOfTariffs(e)}>
                    Создать
                </button>
                <button className="cancel-create-group-of-tariffs-button" onClick={(e) => resetPanel(e)}>
                    Отмена
                </button>
            </div>
        </form>
    );
}

export default CreateGroupOfTariff;