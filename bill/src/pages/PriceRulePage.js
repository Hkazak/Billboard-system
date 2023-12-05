import Header from "../components/Header";
import ControlPanel from "../components/ControlPanel";
import {useEffect, useState} from "react";
import Sidebar from "../components/SideBar";
import {GetPriceRulesList} from "../lib/controllers/PriceRuleController";
import PriceRule from "../components/PriceRule";
import CreatePriceRule from "../components/CreatePriceRule";
import PaginationPanel from "../components/PaginationPanel";

function PriceRulePage() {
    const [searchText, setSearchText] = useState('');
    const [priceRules, setPriceRules] = useState([]);
    const [hideCreateRulePanel, setHideCreateRulePanel] = useState(true);
    const [page, setPage] = useState(0);
    const [pageSize, setPageSize] = useState(0);

    function handleSearchText(e) {
        setSearchText(e.target.value);
    }

    function handleCreateItem() {
        setHideCreateRulePanel(!hideCreateRulePanel);
    }

    function handleNewRule(rule) {
        setPriceRules([...priceRules, rule]);
    }

    useEffect(() => {
        GetPriceRulesList()
            .then(e => e.json())
            .then(e => setPriceRules(e));
    }, []);

    return (
        <div className="price-rule-page-block">
            <Header title="Цена изготовления"/>
            <CreatePriceRule hide={hideCreateRulePanel} setHide={setHideCreateRulePanel}
                             handleNewPriceRule={handleNewRule}/>
            <Sidebar>
                <ControlPanel placeholderSearchText="Поиск" createButtonText="Новое правило"
                              handleSearch={handleSearchText} handleCreateItem={handleCreateItem}/>
                <PaginationPanel onPageChange={setPage} onPageSizeChange={setPageSize} />
                {priceRules
                    .filter(e => (e?.billboardSurface?.surface + e?.billboardType + e?.price?.toString())?.toLowerCase()?.includes(searchText?.toLowerCase()))
                    .slice((page - 1) * pageSize, page * pageSize)
                    .map(e => <PriceRule key={e.id} surface={e.billboardSurface.surface} type={e.billboardType}
                                         price={e.price}/>)}
            </Sidebar>
        </div>
    );
}

export default PriceRulePage;