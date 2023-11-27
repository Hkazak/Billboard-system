import React, {useEffect, useState} from 'react';
import {
    FaTh,
}from "react-icons/fa";
import { NavLink } from 'react-router-dom';
import {
    AdminAuthRoute,
    BillboardsRoute,
    CreateManagerRoute,
    DashboardRoute, DiscountRoute,
    ManagerAuthRoute, PriceRuleRoute,
    TariffRoute,
    UserAuthorizationRoute
} from '../Paths';
import "../styles/Sidebar.css";

const pageHeight = window.innerHeight;
const Sidebar = ({children}) => {
    const menuItem=[
        {
            path:DashboardRoute,
            name:"Dashboard",
            icon:<FaTh/>
        },
        {
            path:UserAuthorizationRoute,
            name:"Авторизация",
            icon:<FaTh/>
        },
        {
            path: BillboardsRoute,
            name:"Билборды",
            icon:<FaTh/>
        },
        {
            path:CreateManagerRoute,
            name:"Менеджеры",
            icon:<FaTh/>
        },
        {
            path:ManagerAuthRoute,
            name:"Авторизация Менеджера",
            icon:<FaTh/>
        },
        {
            path:AdminAuthRoute,
            name:"Авторизация Администратора",
            icon:<FaTh/>
        },
        {
            path:TariffRoute,
            name:"Тарифы",
            icon:<FaTh/>
        },
        {
            path:DiscountRoute,
            name:"Акции",
            icon:<FaTh/>
        },
        {
            path:PriceRuleRoute,
            name:"Цена изготовление",
            icon:<FaTh/>
        }
    ]
    const [offsetTop, setOffsetTop] = useState(0);
    useEffect(()=>{
        setOffsetTop(document.getElementsByClassName('sidebar')[0].offsetTop);
    }, []);
    return (
        <div className="container">
           <div className="sidebar" style={{minHeight: `${pageHeight - offsetTop}px`}}>
               {
                   menuItem.map((item, index)=>(
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
