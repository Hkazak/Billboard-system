function CreateDiscount({show, discounts, setDiscounts})
{
    // TODO add implementation
    return (
        <div className="create-discount-block" hidden={show}>
            <span className="create-discount-title">
                Новая акция
            </span>
        </div>
    );
}

export default CreateDiscount;