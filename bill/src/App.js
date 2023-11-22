import { useState } from 'react'
import {BrowserRouter, Routes, Route} from 'react-router-dom'
import './App.css';
import Dashboard from './pages/Dashboard';
import UserAuthorization from './pages/UserAuthorization'
import SideBar from './components/SideBar';
import UserRegistration from './pages/UserRegistration';
import ForgotPassword from './pages/ForgotPassword';
import ChangePassword from './pages/ChangePassword';
import Admin from './pages/Admin';
import ManagerAuthorization from './pages/ManagerAuthorization';
import {
  AdminAuthRoute, BillboardDescriptionRoute, BillboardsRoute, CreateBillboardRoute,
  CreateManagerRoute,
  DashboardRoute,
  ForgotPasswordRoute,
  ManagerAuthRoute,
  ManagerForgotPasswordRoute,
  ManagerResetPasswordRoute,
  ResetPasswordRoute, TariffGroupRoute,
  TariffRoute,
  UserAuthorizationRoute,
  UserRegistrationRoute
} from './Paths';
import { ResetPasswordChangePassword } from './lib/controllers/UserController';
import AdminAuthorization from './pages/AdminAuthorization';
import ManagerResetPassword from './pages/ManagerResetPassword';
import ManagerForgotPassword from './pages/ManagerForgotPassword';
import BillPages from './pages/BillPages';
import CreateBill from './pages/CreateBill';
import BillDescr from './pages/BillDescr';
import TarifPage from './pages/TarifPage';
import TarifGroup from './pages/TarifGroup';

function App(){

  return (
    <>
      <BrowserRouter>
        
            <Routes>
              <Route path={DashboardRoute} element={<Dashboard/>}/>
              <Route path={UserAuthorizationRoute} element={<UserAuthorization/>}/>
              <Route path={UserRegistrationRoute} element={<UserRegistration/>}/>
              <Route path={ForgotPasswordRoute} element={<ForgotPassword/>}></Route>
              <Route path={ResetPasswordRoute} element={<ChangePassword/>}></Route>
              <Route path={ManagerAuthRoute} element={<ManagerAuthorization/>}></Route>
              <Route path={ManagerResetPasswordRoute} element={<ManagerResetPassword/>}></Route>
              <Route path={ManagerForgotPasswordRoute} element={<ManagerForgotPassword/>}></Route>
              <Route path={CreateBillboardRoute} element={<CreateBill/>}></Route>
              <Route path={BillboardsRoute} element={<BillPages/>}></Route>
              <Route path={AdminAuthRoute} element={<AdminAuthorization/>}></Route>
              <Route path={CreateManagerRoute} element={<Admin/>}></Route>
              <Route path={BillboardDescriptionRoute} element={<BillDescr/>}></Route>
              <Route path={TariffRoute} element={<TarifPage/>}></Route>
              <Route path={TariffGroupRoute} element={<TarifGroup/>}></Route>
            </Routes>
        
      </BrowserRouter>
      
    </>
  )

}

export default App;
