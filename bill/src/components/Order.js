import {baseUrl} from "../lib/Consts";
import DiscountBlock from "./DiscountBlock";
import Tariff from "./Tariff";
import {Calendar, DateObject} from "react-multi-date-picker";
import OrderPrice from "./OrderPrice";
import PicturesContainer from "./PicturesContainer";
import "../styles/Order.css";
import {
    ApproveOrderRequest,
    CancelOrderRequest, ChangePriceRequest,
    RecalculatePriceRequest
} from "../lib/controllers/OrderController";
import {useEffect, useRef, useState} from "react";

function Order({
                   id,
                   name,
                   billboardId,
                   billboardPictures,
                   billboardDescription,
                   billboardType,
                   discount,
                   uploadedFiles,
                   tariff,
                   startDate,
                   endDate,
                   price,
                   billboardSurface,
                   width,
                   height,
                   isClientView,
                   hide,
                   setHide,
                   onChange,
                   setPrice
               }) {

    const [orderStartDate, setOrderStartDate] = useState({day: 1, month: 1, year: 1970});
    const [orderEndDate, setOrderEndDate] = useState({day: 1, month: 1, year: 1970});
    const selectedRange = useRef([]);
    const [changed, setChanged] = useState(false);

    function handleChangeRange(range) {
        if (range.length === 2) {
            const start = range[0].toDate().toLocaleDateString('ru-RU')
                .replaceAll('.', '-')
                .replaceAll('/', '-');
            const end = range[1].toDate().toLocaleDateString('ru-RU')
                .replaceAll('.', '-')
                .replaceAll('/', '-');
            setChanged(true);
            setOrderStartDate(toDate(start));
            setOrderEndDate(toDate(end));
            selectedRange.current = [new DateObject().set(toDate(start)), new DateObject().set(toDate(end))];
            RecalculatePriceRequest(id, range[0].toDate(), range[1].toDate())
                .then(e => e.json())
                .then(e => setPrice(e));
        }
    }

    function toDate(dateString) {
        const dateParts = dateString.split('-');
        return {day: parseInt(dateParts[0]), month: parseInt(dateParts[1]), year: parseInt(dateParts[2])};
    }

    function handleHide(e) {
        e.preventDefault();
        e.target.form.reset();
        selectedRange.current = [new DateObject().set(orderStartDate), new DateObject().set(orderEndDate)];
        setHide(true);
    }

    function printDiscount() {
        if (discount) {
            return <DiscountBlock key={discount.id} name={discount.name} minRentCount={discount.minRentCount}
                                  salesOf={discount.salesOf}/>;
        }
    }

    function handleApprove(ev) {
        ev.preventDefault();
        ApproveOrderRequest(id)
            .then(e => {
                if (e.ok) {
                    onChange(id, 'InProgress');
                    setHide(true);
                }
            });
    }

    function handleCancel(ev) {
        ev.preventDefault();
        CancelOrderRequest(id)
            .then(e => {
                if (e.ok) {
                    onChange(id, 'Cancelled');
                    setHide(true);
                }
            })
            .catch(e => console.log(e));
    }

    function handleChange(ev) {
        ev.preventDefault();
        ChangePriceRequest(id, new DateObject().set(orderStartDate).toDate(), new DateObject().set(orderEndDate).toDate())
            .then(e => e.json())
            .then(e => window.open(e.checkoutUrl, '_blank'));
    }

    useEffect(() => {
        const start = toDate(startDate);
        const end = toDate(endDate);
        setOrderStartDate(start);
        setOrderEndDate(end);
        selectedRange.current = [new DateObject().set(start), new DateObject().set(end)];
    }, []);
    return (
        <form className="order-data-block" hidden={hide}>
            <div className="order-data-pictures">
                {billboardPictures?.map(picture => <img key={picture.id} src={baseUrl + picture} alt={picture}
                                                        className="billboard-picture"/>)}
            </div>
            <div className="order-data-text">
                <span className="order-data-title">{name}</span>
                <p className="order-data-data">{billboardDescription}</p>
                <div className="order-data-general">
                    <div className="order-data-general-data-wrapper">
                        <span className="order-data-general-data-label">Вид поверхности</span>
                        <span className="order-data-general-data-dots"></span>
                        <span className="order-data-general-data">{billboardSurface}</span>
                    </div>
                    <div className="order-data-general-data-wrapper">
                        <span className="order-data-general-data-label">Размер</span>
                        <span className="order-data-general-data-dots"></span>
                        <span className="order-data-general-data">{`${width}м х ${height}м`}</span>
                    </div>
                    <div className="order-data-general-data-wrapper">
                        <span className="order-data-general-data-label">Тип экспанирования</span>
                        <span className="order-data-general-data-dots"></span>
                        <span className="order-data-general-data">{billboardType}</span>
                    </div>
                </div>
                <div className="order-data-discounts">
                    <span className="order-data-general-text">Действующая акция</span>
                    <div className="order-data-discounts-list">
                        {printDiscount()}
                    </div>
                </div>
                <div className="order-data-tariffs">
                    <span className="order-data-general-text">Выбранный тариф</span>
                    <div className="order-data-tariffs-list">
                        {<Tariff key={tariff.id} tariffTitle={tariff.title} tariffPrice={tariff.price}
                                 startTime={tariff.startTime} endTime={tariff.endTime} isSelected={true}/>}
                    </div>
                </div>
                <div className="order-data-booked-days">
                    <span className="order-data-general-text">Выбранный период использования</span>
                    <div className="order-data-calendars">
                        <Calendar className="order-data-calendar" weekStartDayIndex={1}
                                  range readOnly={!isClientView}
                                  value={selectedRange.current}
                                  onChange={handleChangeRange}
                        />
                    </div>
                </div>
                <OrderPrice price={price} isClientView={true}/>
                <button className="download-uploaded-pictures">Скачать загруженные файлы</button>
                <PicturesContainer pictures={uploadedFiles.map((e, i) => {
                    return {id: i, name: i, data: (baseUrl + e)}
                })}/>
                <div className="manage-buttons">
                    <button className="order-data-send" hidden={isClientView} onClick={handleApprove}>
                        Подтвердить заказ
                    </button>
                    <button className="order-data-send" hidden={!changed} onClick={handleChange}>
                        Подтвердить изменения
                    </button>
                    <button className="order-data-deny" onClick={handleCancel}>
                        {isClientView ? 'Отменить заказ' : 'Отклонить заказ'}
                    </button>
                    <button className="order-data-close" onClick={handleHide}>Закрыть</button>
                </div>
            </div>
        </form>
    );
}

export default Order;