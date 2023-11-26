import React, { useState } from 'react';
import {
    FaTh,
    FaBars,
    FaUserAlt,
    FaRegChartBar,
    FaCommentAlt,
    FaShoppingBag,
    FaThList
}from "react-icons/fa";
import { NavLink } from 'react-router-dom';
import { AdminAuthRoute, CreateManagerRoute, DashboardRoute, ManagerAuthRoute, TariffRoute, UserAuthorizationRoute } from '../Paths';
import AdminAuthorization from '../pages/AdminAuthorization';

const Sidebar = ({children}) => {
    const[isOpen ,setIsOpen] = useState(false);
    const toggle = () => setIsOpen (!isOpen);
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
            path:"/all-bills",
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
        }
    ]
    return (
        <div className="container">
           <div style={{width: isOpen ? "450px" : "50px"}} className="sidebar">
               <div className="top-section">
                   <div style={{marginLeft: isOpen ? "150px" : "0px"}} className="bars">
                       <FaBars onClick={toggle}/>
                   </div>
               </div>
               {
                   menuItem.map((item, index)=>(
                       <NavLink to={item.path} key={index} className="link">
                           <div className="icon">{item.icon}</div>
                           <div style={{display: isOpen ? "block" : "none"}} className="link-text">{item.name}</div>
                       </NavLink>
                   ))
               }
           </div>
           <main className="main-content">{children}</main>
        </div>
    );
};

export default Sidebar;
