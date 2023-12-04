import "../styles/ShortBillboardInfo.css";

function ShortBillboardInfo({billboard, onSelect}) {
    return (
        <div className="short-billboard-info-block" onClick={e => onSelect(billboard.id)}>
            <div className="short-billboard-info">{billboard.name}</div>
        </div>
    );
}

export default ShortBillboardInfo;