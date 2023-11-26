import {BrowserRouter, Routes, Route} from 'react-router-dom'
import './App.css';
import Dashboard from './pages/Dashboard';
import UserAuthorization from './pages/UserAuthorization'
import UserRegistration from './pages/UserRegistration';
import ForgotPassword from './pages/ForgotPassword';
import ChangePassword from './pages/ChangePassword';
import ManagerPage from './pages/ManagerPage';
import ManagerAuthorization from './pages/ManagerAuthorization';
import {
  AdminAuthRoute, BillboardDescriptionRoute, BillboardsRoute, CreateBillboardRoute,
  CreateManagerRoute,
  DashboardRoute, DiscountRoute,
  ForgotPasswordRoute,
  ManagerAuthRoute,
  ManagerForgotPasswordRoute,
  ManagerResetPasswordRoute,
  ResetPasswordRoute, TariffGroupRoute,
  TariffRoute,
  UserAuthorizationRoute,
  UserRegistrationRoute
} from './Paths';
import AdminAuthorization from './pages/AdminAuthorization';
import ManagerResetPassword from './pages/ManagerResetPassword';
import ManagerForgotPassword from './pages/ManagerForgotPassword';
import BillboardPage from './pages/BillboardPage';
import CreateBill from './pages/CreateBill';
import BillDescr from './pages/BillDescr';
import TariffPage from './pages/TariffPage';
import TariffGroup from './pages/TariffGroup';
import DiscountPage from "./pages/DiscountPage";

function App(){

  return (
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
            <Route path={BillboardsRoute} element={<BillboardPage/>}></Route>
            <Route path={AdminAuthRoute} element={<AdminAuthorization/>}></Route>
            <Route path={CreateManagerRoute} element={<ManagerPage/>}></Route>
            <Route path={BillboardDescriptionRoute} element={<BillDescr/>}></Route>
            <Route path={TariffRoute} element={<TariffPage/>}></Route>
            <Route path={TariffGroupRoute} element={<TariffGroup/>}></Route>
            <Route path={DiscountRoute} element={<DiscountPage/>}></Route>
          </Routes>

    </BrowserRouter>
  )

}

export default App;
