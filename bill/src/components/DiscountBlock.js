import "../styles/DiscountBlock.css";

function DiscountBlock({name, salesOf, minRentCount, isSelected})
{
    return (
        <div className={isSelected ? "billboard-discount-block-selected" : "billboard-discount-block"}>
            <span className="billboard-discount-title">{name}</span>
            <span className="billboard-discount-percentage">-{salesOf}%</span>
            <span className="billboard-discount-condition">При заказе от {minRentCount} дней</span>
        </div>
    );
}

export default DiscountBlock;