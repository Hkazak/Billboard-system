import "../styles/OrderPrice.css";

function OrderPrice({price, isClientView}) {
    return (
        <div className="order-price-block" hidden={!isClientView}>
            <span className="order-price-title">Итоговая цена</span>
            <div className="order-price-list">
                <div className="order-price-item">
                    <span className="order-price-item-label">Изготовление</span>
                    <span className="order-price-item-dots"></span>
                    <span className="order-price-item-price">{price.productPrice} тенге</span>
                </div>
                <div className="order-price-item">
                    <span className="order-price-item-label">Аренда</span>
                    <span className="order-price-item-dots"></span>
                    <span className="order-price-item-price">{price.rentPrice} тенге</span>
                </div>
                <div className="order-price-item">
                    <span className="order-price-item-label">Штраф</span>
                    <span className="order-price-item-dots"></span>
                    <span className="order-price-item-price">{price.penaltyPrice} тенге</span>
                </div>
            </div>
        </div>
    );
}

export default OrderPrice;