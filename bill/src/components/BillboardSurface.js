import "../styles/BillboardSurface.css";

function BillboardSurface({name})
{
    return (
        <div className="billboard-surface-block">
            <span className="billboard-surface-data">{name}</span>
        </div>
    )
}

export default BillboardSurface;