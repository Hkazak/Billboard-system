import UploadPictures from "./UploadPictures";
import {useState} from "react";
import OrderPrice from "./OrderPrice";
import {CreateOrderRequest} from "../lib/controllers/OrderController";
import {CreatePaymentRequest} from "../lib/controllers/PaymentController";
import "../styles/CreateOrder.css";

function CreateOrder({price, billboardId, tariffId, startDate, endDate, isClientView, hide, setHide}) {
    const [uploadedPictures, setUploadedPictures] = useState([]);

    async function handlePay(e) {
        e.preventDefault();
        e.target.form.reset();
        console.log(billboardId);
        const response = await CreateOrderRequest(billboardId, startDate, endDate, tariffId, uploadedPictures);
        if (response.ok) {
            const order = await response.json();
            const paymentResponse = await CreatePaymentRequest(order.id);
            const payment = await paymentResponse.json();
            if (paymentResponse.ok) {
                window.open(payment.checkoutUrl, '_blank');
            }
        }
    }

    function handleClose(e) {
        e.preventDefault();
        e.target.form.reset();
        setHide(true);
    }

    return (
        <form className="create-order-block" hidden={!isClientView || hide}>
            <div className="top-block-content">
                <span className="create-order-title">Оформление заказа</span>
                <button className="close-create-order-block" onClick={handleClose}>X</button>
            </div>
            <span className="upload-files-text">
                <span className="upload-files-text-title">Загрузить файлы</span>
                <span className="upload-files-text-description">Загрузите фотографию, видео и другие файлы которые помогут нам при исполнении вашего заказа</span>
            </span>
            <UploadPictures onUpload={files => setUploadedPictures([...files])}/>
            <div className="payment-block">
                <OrderPrice price={price} isClientView={isClientView}/>
                <button className="create-order-button" onClick={handlePay}>Оформить</button>
            </div>
        </form>
    );
}

export default CreateOrder;