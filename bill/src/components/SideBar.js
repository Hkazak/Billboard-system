import React, {useEffect, useState} from 'react';
import {
    FaTh,
} from "react-icons/fa";
import {NavLink} from 'react-router-dom';
import {
    AdminAuthRoute,
    BillboardsRoute,
    CreateManagerRoute,
    DashboardRoute, DiscountRoute,
    ManagerAuthRoute, OrdersRoute, PriceRuleRoute, SurfaceRoute, TariffGroupRoute,
    TariffRoute,
    UserAuthorizationRoute
} from '../Paths';
import "../styles/Sidebar.css";
import {LS} from "../lib/Consts";


const Sidebar = ({children}) => {
    const userRole = localStorage.getItem(LS.userRole);
    const menuItem = [
        {
            path: DashboardRoute,
            name: "Dashboard",
            icon: <FaTh/>,
            allowed: ['client', 'manager']
        },
        {
            path: UserAuthorizationRoute,
            name: "Авторизация",
            icon: <FaTh/>,
            allowed: ['client', 'manager', 'admin']
        },
        {
            path: BillboardsRoute,
            name: "Билборды",
            icon: <FaTh/>,
            allowed: []
        },
        {
            path: CreateManagerRoute,
            name: "Менеджеры",
            icon: <FaTh/>,
            allowed: ['admin']
        },
        {
            path: ManagerAuthRoute,
            name: "Авторизация Менеджера",
            icon: <FaTh/>,
            allowed: ['client', 'manager', 'admin']
        },
        {
            path: AdminAuthRoute,
            name: "Авторизация Администратора",
            icon: <FaTh/>,
            allowed: ['client', 'manager', 'admin']
        },
        {
            path: TariffRoute,
            name: "Тарифы",
            icon: <FaTh/>,
            allowed: ['manager']
        },
        {
            path: DiscountRoute,
            name: "Акции",
            icon: <FaTh/>,
            allowed: ['manager']
        },
        {
            path: PriceRuleRoute,
            name: "Цена изготовление",
            icon: <FaTh/>,
            allowed: ['manager']
        },
        {
            path: TariffGroupRoute,
            name: "Группы тарифов",
            icon: <FaTh/>,
            allowed: ['manager']
        },
        {
            path: SurfaceRoute,
            name: "Виды поверхности",
            icon: <FaTh/>,
            allowed: ['manager']
        },
        {
            path: OrdersRoute,
            name: "Заказы",
            icon: <FaTh/>,
            allowed: ['manager', 'client']
        }
    ]
    const [offsetTop, setOffsetTop] = useState(0);
    const [minPageHeight, setMinPageHeight] = useState(0);

    useEffect(() => {
        setOffsetTop(document.getElementsByClassName('sidebar')[0].offsetTop);
        setMinPageHeight(window.innerHeight);
    }, []);
    return (
        <div className="container">
            <div className="sidebar" style={{minHeight: `${minPageHeight - offsetTop}px`}}>
                {
                    menuItem.filter(e=>e.allowed.includes(userRole)).map((item, index) => (
                        <NavLink to={item.path} key={index} className="link">
                            <div className="link-text">{item.name}</div>
                        </NavLink>
                    ))
                }
            </div>
            <main className="main-content">{children}</main>
        </div>
    );
};

export default Sidebar;
