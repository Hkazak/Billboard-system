import Header from "../components/Header";
import Sidebar from "../components/SideBar";
import ControlPanel from "../components/ControlPanel";
import {useEffect, useState} from "react";
import Discount from "../components/Discount";
import {GetDiscounts} from "../lib/controllers/DiscountController";
import CreateDiscount from "../components/CreateDiscount";
import {LS} from "../lib/Consts";
import PaginationPanel from "../components/PaginationPanel";

function DiscountPage() {
    const [searchText, setSearchText] = useState('');
    const [hideCreateDiscountBlock, setHideCreateDiscountBlock] = useState(true);
    const [discounts, setDiscounts] = useState([]);
    const [isClientView, setIsClientView] = useState(localStorage.getItem(LS.isClient) === 'true');
    const [page, setPage] = useState(0);
    const [pageSize, setPageSize] = useState(0);

    function handleSearch(e) {
        setSearchText(e.target.value);
    }

    function handleCreateItem(e) {
        setHideCreateDiscountBlock(!hideCreateDiscountBlock);
    }

    function handleNewDiscount(discount) {
        setDiscounts([...discounts, discount])
    }

    useEffect(() => {
        GetDiscounts()
            .then(e => e.json())
            .then(e => setDiscounts(e));
    }, []);

    return (
        <div className="discount-block">
            <Header title={"Акции"}/>
            <CreateDiscount hide={hideCreateDiscountBlock} setHide={setHideCreateDiscountBlock}
                            handleNewDiscount={handleNewDiscount}/>
            <Sidebar>
                <ControlPanel handleSearch={handleSearch} handleCreateItem={handleCreateItem}
                              placeholderSearchText={"Название"} createButtonText={"Новые акции"}
                              isClientView={isClientView}/>
                <PaginationPanel onPageChange={setPage} onPageSizeChange={setPageSize}/>
                {discounts?.filter(e => e?.name?.toLowerCase()?.includes(searchText?.toLowerCase()))
                    .slice((page - 1) * pageSize, page * pageSize)
                    .map(e => <Discount key={e?.id} name={e?.name}
                                        discount={e?.salesOf}
                                        minRentCount={e?.minRentCount}
                                        endDate={e?.endDate}
                                        billboards={e?.billboards}/>)}
            </Sidebar>
        </div>
    );
}

export default DiscountPage;