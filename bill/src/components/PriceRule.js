import "../styles/PriceRule.css";

function PriceRule({surface, type, price})
{
    return (
        <div className="price-rule-item-block">
            <div className="price-rule-information">
                <div className="price-rule-information-item">{surface}</div>
                <div className="price-rule-information-item">{type}</div>
                <div className="price-rule-information-item">{price}</div>
            </div>
        </div>
    );
}

export default PriceRule;