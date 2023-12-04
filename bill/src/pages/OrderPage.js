import Header from "../components/Header";
import Sidebar from "../components/SideBar";
import OrdersControlPanel from "../components/OrdersControlPanel";
import {useEffect, useRef, useState} from "react";
import OrderInformation from "../components/OrderInformation";
import {GetOrdersListRequest} from "../lib/controllers/OrderController";
import Order from "../components/Order";
import {LS} from "../lib/Consts";

function OrderPage() {
    const [orders, setOrders] = useState([]);
    const [searchText, setSearchText] = useState('');
    const [selectedStatus, setSelectedStatus] = useState('');
    const [orderId, setOrderId] = useState('');
    const [orderName, setOrderName] = useState('');
    const [tariff, setTariff] = useState({id: '', title: '', startTime: '', endTime: '', price: 0});
    const [billboardPictures, setBillboardPictures] = useState([]);
    const [billboardDescription, setBillboardDescription] = useState('');
    const [billboardType, setBillboardType] = useState('');
    const [discount, setDiscount] = useState({
        id: '',
        name: '',
        salesOf: 0,
        minRentCount: 0,
        endDate: '',
        billboards: []
    });
    const [uploadedFiles, setUploadedFiles] = useState([]);
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');
    const [billboardSurface, setBillboardSurface] = useState(0);
    const [width, setWidth] = useState(0);
    const [height, setHeight] = useState(0);
    const [price, setPrice] = useState({rentPrice: 0, penaltyPrice: 0, productPrice: 0});
    const [billboardId, setBillboardId] = useState('');
    const [hideOrderInformation, setHideOrderInformation] = useState(true);
    const [isClientView, setIsClientView] = useState(localStorage.getItem(LS.isClient) === 'true');

    function handleStatusChanged(e) {
        if (e.target.selectedIndex === -1) {
            setSelectedStatus('');
        } else {
            setSelectedStatus(e.target.options[e.target.selectedIndex].id);
        }
    }

    function handleSelectOrder(orderId) {
        const order = orders.find(e => e.id === orderId);
        console.log(order);
        setOrderId(orderId);
        setOrderName(order.name);
        setTariff(order.tariff);
        setBillboardPictures(order.billboardPictures);
        setBillboardDescription(order.billboardDescription);
        setBillboardType(order.billboardType);
        setDiscount(order.discount);
        setUploadedFiles(order.uploadedFiles);
        setStartDate(order.startDate);
        setEndDate(order.endDate);
        setBillboardSurface(order.billboardSurface);
        setWidth(order.width);
        setHeight(order.height);
        setBillboardId(order.billboardId);
        setPrice({rentPrice: order.rentPrice, penaltyPrice: order.penaltyPrice, productPrice: order.productPrice});
        setHideOrderInformation(false);
    }

    function handleChangeStatus(orderId, newStatus)
    {
        const order = orders.find(e=>e.id===orderId);
        order.status = newStatus;
    }

    useEffect(() => {
        GetOrdersListRequest()
            .then(e => e.json())
            .then(e => setOrders(e));
    }, []);
    return (
        <div className="manage-orders-page">
            <Header title={"Управление заказами"}/>
            <Order
                id={orderId}
                name={orderName}
                tariff={tariff}
                uploadedFiles={uploadedFiles}
                width={width}
                height={height}
                price={price}
                discount={discount}
                billboardType={billboardType}
                startDate={startDate}
                endDate={endDate}
                billboardId={billboardId}
                billboardSurface={billboardSurface}
                billboardDescription={billboardDescription}
                billboardPictures={billboardPictures}
                hide={hideOrderInformation}
                isClientView={isClientView}
                setHide={setHideOrderInformation}
                onChange={handleChangeStatus}
                setPrice={setPrice}
            />
            <Sidebar>
                <OrdersControlPanel placeholderSearchText={"Название"} handleSearch={e => setSearchText(e.target.value)}
                                    onStatusSet={handleStatusChanged}/>
                {orders.filter(e => e?.status?.includes(selectedStatus)).filter(e => e?.name?.toLowerCase()?.includes(searchText)).map(e =>
                    <OrderInformation
                        key={e.id}
                        id={e.id}
                        name={e.name}
                        status={e.status}
                        uploadedFiles={e.uploadedFiles}
                        userName={e.userName}
                        userEmail={e.userEmail}
                        onSelect={handleSelectOrder}
                    />)}
            </Sidebar>
        </div>
    );
}

export default OrderPage;