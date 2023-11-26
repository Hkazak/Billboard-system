import Header from "../components/Header";
import Sidebar from "../components/SideBar";
import ControlPanel from "../components/ControlPanel";
import {useEffect, useState} from "react";
import Discount from "../components/Discount";
import {GetDiscounts} from "../lib/controllers/DiscountController";
import CreateDiscount from "../components/CreateDiscount";

function DiscountPage()
{
    const [searchText, setSearchText] = useState('');
    const [hideCreateDiscountBlock, setHideCreateDiscountBlock] = useState(true);
    const [discounts, setDiscounts] = useState([]);

    function handleSearch(e)
    {
        setSearchText(e.target.value);
    }

    function handleCreateItem(e)
    {
        setHideCreateDiscountBlock(!hideCreateDiscountBlock);
    }

    function handleNewDiscount(discount)
    {
        setDiscounts([...discounts, discount])
    }

    useEffect(()=>{
        GetDiscounts()
            .then(e=>e.json())
            .then(e=>setDiscounts(e));
    }, []);

    return (
        <div className="discount-block">
            <Header title={"Акции"} />
            <CreateDiscount show={hideCreateDiscountBlock} handleNewDiscount={handleNewDiscount} />
            <Sidebar>
                <ControlPanel handleSearch={handleSearch} handleCreateItem={handleCreateItem} placeholderSearchText={"Название"} createButtonText={"Новые акции"} />
                {discounts.filter(e=>e.name.includes(searchText)).map(e=><Discount key={e.id} name={e.name} discount={e.salesOf} minRentCount={e.minRentCount} endDate={e.endDate} billboards={e.billboards} />)}
            </Sidebar>
        </div>
    );
}

export default DiscountPage;