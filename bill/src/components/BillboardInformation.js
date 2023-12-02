import "../styles/BillboardInformation.css";
import DiscountBlock from "./DiscountBlock";
import {useEffect, useRef, useState} from "react";
import Tariff from "./Tariff";
import {Calendar, DateObject} from "react-multi-date-picker";
import {
    CalculatePriceEndpoint,
    CalculatePriceRequest,
    GetBookedOrdersRequest
} from "../lib/controllers/OrderController";
import OrderPrice from "./OrderPrice";

function BillboardInformation({billboard, hide, setHide, isClientView})
{
    const [bookedRanges, setBookedRanges] = useState([]);
    const [tariffs, setTariffs] = useState([])
    const selectedRanges = useRef([]);
    const [selectedTariffId, setSelectedTariffId] = useState('');
    const [selectedDiscountId, setSelectedDiscountId] = useState('');
    const [price, setPrice] = useState({rentPrice: 0, productPrice: 0});

    function handleSelectDay(date)
    {
        if(date.length === 2 && selectedTariffId !== '')
        {
            const startDate = new Date(date[0].toString());
            const endDate = new Date(date[1].toString());
            const start = {day: startDate.getDate(), month: startDate.getMonth(), year: startDate.getFullYear()};
            const end = {day: endDate.getDate(), month: endDate.getMonth(), year: endDate.getFullYear()};
            selectedRanges.current = [new DateObject().set(start), new DateObject().set(end)];
            console.log(start, end);
            CalculatePriceRequest(billboard.id, startDate, endDate, selectedTariffId)
                .then(e=>e.json())
                .then(e=>setPrice(e));
        }
    }

    function handleSelectTariff(e, id)
    {
        if(selectedTariffId === id)
        {
            setSelectedTariffId('');
            selectedRanges.current = [];
            selectRanges([]);
        }
        else
        {
            setSelectedTariffId(id);
            GetBookedOrdersRequest(billboard.id, id)
                .then(e=>e.json())
                .then(e=>selectRanges(e));
        }
    }

    function handleSelectDiscount(e, id)
    {
        if(selectedDiscountId === id)
        {
            setSelectedDiscountId('');
        }
        else
        {
            setSelectedDiscountId(id);
        }
    }

    function toDate(dateString)
    {
        const dateParts = dateString.split('-');
        return {day: parseInt(dateParts[0]), month: parseInt(dateParts[1]), year: parseInt(dateParts[2])};
    }

    function selectRanges(ranges)
    {
        const bookedRanges = ranges?.map(range=>{
            return {start: toDate(range.startDate), end: toDate(range.endDate)}
        }).map(range=>[new DateObject().set(range.start), new DateObject().set(range.end)]);
        setBookedRanges(bookedRanges);
    }

    useEffect(()=>{
        if(billboard?.id)
        {
            GetBookedOrdersRequest(billboard.id)
                .then(e=>e.json())
                .then(e=>selectRanges(e));
            setTariffs([...billboard.groupOfTariffs.tariffs]);
        }
    }, []);
    return (
        <div className="billboard-information-block" hidden={hide}>
            <div className="billboard-information-pictures">
                {billboard.pictureSource?.map(picture=><img key={picture.id} src={picture} alt={picture} className="billboard-picture"/>)}
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
                        {billboard.discounts?.map(discount=><DiscountBlock isSelected={selectedDiscountId === discount.id} onClick={e=>handleSelectDiscount(e, discount.id)} key={discount.id} name={discount.name} minRentCount={discount.minRentCount} salesOf={discount.salesOf} />)}
                    </div>
                </div>
                <div className="billboard-information-tariffs">
                    <span className="billboard-information-general-text">{isClientView ? "Выбрать тариф" : "Доступные тарифы"}</span>
                    <div className="billboard-information-tariffs-list">
                        {billboard?.groupOfTariffs?.tariffs?.map(tariff=><Tariff isSelected={selectedTariffId === tariff.id} key={tariff.id} tariffTitle={tariff.title} tariffPrice={tariff.price} startTime={tariff.startTime} endTime={tariff.endTime} onClickCallback={e=>handleSelectTariff(e, tariff.id)} />)}
                    </div>
                </div>
                <div className="billboard-information-booked-days">
                    <span className="billboard-information-general-text">{isClientView ? "Выбрать период использования" : "Забронированные дни"}</span>
                    <div className="billboard-information-calendars">
                        <Calendar className="billboard-information-calendar" rangeHover={true} readOnly={selectedTariffId === '' || !isClientView} value={selectedRanges} weekStartDayIndex={1} range onChange={handleSelectDay} />
                        <Calendar className="billboard-information-calendar" weekStartDayIndex={1} value={bookedRanges} range multiple readOnly/>
                    </div>
                </div>
                <OrderPrice price={price} isClientView={isClientView} />
                <div className="manage-buttons">
                    <button className="billboard-information-send" hidden={!isClientView}>Оформить</button>
                    <button className="billboard-information-close" onClick={e=>setHide(true)}>Закрыть</button>
                </div>
            </div>
        </div>
    );
}

export default BillboardInformation;