import {useEffect, useState} from "react";
import {GetTariffs} from "../lib/controllers/TariffController";

function CreateGroupOfTariff()
{
    const [selectedTariffs, setSelectedTariffs] = useState([]);
    const [allTariffs, setAllTariffs] = useState([]);
    useEffect(()=>{
        GetTariffs()
            .then(e=>e.json())
            .then(e=>setAllTariffs(e));
    }, []);

    // TODO continue
    return (
      <div className="create-new-group-of-tariffs-block">
          <span className="title">
              Новая группа тарифов
          </span>
          <div className="general-information">
              <input type="text" className="group-of-tariffs-name" placeholder="Название"/>
          </div>
          <div className="tariffs-list"></div>
      </div>
    );
}

export default CreateGroupOfTariff;