import "../styles/BillboardInformation.css";
import DiscountBlock from "./DiscountBlock";
import {useEffect, useState} from "react";
import Tariff from "./Tariff";
import {Calendar, DateObject} from "react-multi-date-picker";
import {GetBookedOrdersRequest} from "../lib/controllers/OrderController";

function BillboardInformation({billboard, hide, setHide, isClientView})
{
    const [selectedRanges, setSelectedRanges] = useState([]);
    const [tariffs, setTariffs] = useState([])
    function handleSelectDay(date)
    {
        if(date.length === 2)
        {
            const startDate = new Date(date[0].toString());
            const endDate = new Date(date[1].toString());

        }
    }

    function handleSelectTariff(e, id)
    {
        GetBookedOrdersRequest(billboard.id, id)
            .then(e=>e.json())
            .then(e=>selectRanges(e))
    }

    function toDate(dateString)
    {
        const dateParts = dateString.split('-');
        return {day: parseInt(dateParts[0]), month: parseInt(dateParts[1]), year: parseInt(dateParts[2])}
    }

    function selectRanges(ranges)
    {
        const bookedRanges = ranges?.map(range=>{
            return {start: toDate(range.startDate), end: toDate(range.endDate)}
        }).map(range=>[new DateObject().set(range.start), new DateObject().set(range.end)]);
        console.log(bookedRanges);
        setSelectedRanges(bookedRanges);
    }

    useEffect(()=>{
        if(billboard?.id)
        {
            GetBookedOrdersRequest(billboard.id)
                .then(e=>e.json())
                .then(e=>selectRanges(e))
        }
        setTariffs([...billboard.groupOfTariffs.tariffs]);
    }, []);
    return (
        <div className="billboard-information-block" hidden={hide}>
            <div className="billboard-information-pictures">
                {billboard.pictureSource?.map(picture=><img src={picture} alt={picture} className="billboard-picture"/>)}
            </div>
            <div className="billboard-information-text">
                <span className="billboard-information-title">{billboard.name}</span>
                <p className="billboard-information-data">{billboard.description}</p>
                <div className="billboard-information-general">
                    <div className="billboard-information-general-data-wrapper">
                        <span className="billboard-information-general-data-label">Вид поверхности</span>
                        <span className="billboard-information-general-data-dots"></span>
                        <span className="billboard-information-general-data">{billboard.billboardSurface}</span>
                    </div>
                    <div className="billboard-information-general-data-wrapper">
                        <span className="billboard-information-general-data-label">Размер</span>
                        <span className="billboard-information-general-data-dots"></span>
                        <span className="billboard-information-general-data">{`${billboard.width}м х ${billboard.height}м`}</span>
                    </div>
                    <div className="billboard-information-general-data-wrapper">
                        <span className="billboard-information-general-data-label">Тип экспанирования</span>
                        <span className="billboard-information-general-data-dots"></span>
                        <span className="billboard-information-general-data">{billboard.billboardType}</span>
                    </div>
                </div>
                <div className="billboard-information-discounts">
                    <span className="billboard-information-general-text">Акции</span>
                    <div className="billboard-information-discounts-list">
                        {billboard.discounts?.map(discount=><DiscountBlock key={discount.id} name={discount.name} minRentCount={discount.minRentCount} salesOf={discount.salesOf} />)}
                    </div>
                </div>
                <div className="billboard-information-tariffs">
                    <span className="billboard-information-general-text">{isClientView ? "Выбрать тариф" : "Доступные тарифы"}</span>
                    <div className="billboard-information-tariffs-list">
                        {billboard.groupOfTariffs?.tariffs?.map(tariff=><Tariff key={tariff.id} tariffTitle={tariff.title} tariffPrice={tariff.price} startTime={tariff.startTime} endTime={tariff.endTime} onClickCallback={e=>handleSelectTariff(e, tariff.id)} />)}
                    </div>
                </div>
                <div className="billboard-information-booked-days">
                    <span className="billboard-information-general-text">{isClientView ? "Выбрать период использования" : "Забронированные дни"}</span>
                    <div className="billboard-information-calendars">
                        <Calendar className="billboard-information-calendar" weekStartDayIndex={1} range onChange={handleSelectDay} />
                        <Calendar className="billboard-information-calendar" weekStartDayIndex={1} value={selectedRanges} range multiple readOnly/>
                    </div>
                </div>
                <div className="manage-buttons">
                    <button className="billboard-information-send">Оформить</button>
                    <button className="billboard-information-close">Закрыть</button>
                </div>
            </div>
        </div>
    );
}

export default BillboardInformation;