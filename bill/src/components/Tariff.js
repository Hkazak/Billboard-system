import '../styles/Tariff.css'

function Tariff({tariffTitle, tariffPrice, startTime, endTime, onClickCallback})
{
    return (
            <div className="tariff-block" onClick={onClickCallback}>
                <span className="tariff-title">
                    {tariffTitle}
                </span>
                <span className="tariff-time-period">
                    {startTime}-{endTime}
                </span>
                <span className="tariff-price">
                    {tariffPrice} тенге
                </span>
            </div>
        );
}

export default Tariff;