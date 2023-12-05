import arrow from "../assets/arrow.png";
import "../styles/PaginationPanel.css";
import {useEffect, useState} from "react";

function PaginationPanel({onPageSizeChange, onPageChange})
{
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(25);

    function handlePageChanged(newPage)
    {
        const next = Math.max(1, newPage);
        setPage(next);
        onPageChange(next);
    }

    function handlePageSizeChanged(newPageSize)
    {
        const next = Math.max(1, newPageSize);
        setPageSize(next);
        onPageSizeChange(next);
    }

    useEffect(()=>
    {
        onPageChange(page);
        onPageSizeChange(pageSize);
    });
    return (
        <div className="pagination-control-panel">
            <div className="pagination-control-panel-block">
                <input type="number" className="pagination-control-panel-size-input" value={pageSize} onChange={e=>handlePageSizeChanged(parseInt(e.target.value))}/>
                <span className="pagination-control-panel-size-text">На одной странице</span>
            </div>
            <div className="pagination-control-panel-block">
                <img src={arrow} alt="" className="pagination-panel-arrow-icon" width={20} onClick={e=>handlePageChanged(page - 1)}/>
                <input type="number" className="pagination-control-panel-page-input" value={page} onChange={e=>handlePageChanged(parseInt(e.target.value))}/>
                <img src={arrow} alt="" className="pagination-panel-arrow-icon-reverse" width={20} onClick={e=>handlePageChanged(page + 1)}/>
            </div>
        </div>
    );
}

export default PaginationPanel;