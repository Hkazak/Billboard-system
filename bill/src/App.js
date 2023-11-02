import { useState } from 'react'
import {BrowserRouter, Routes, Route} from 'react-router-dom'
import './App.css';
import Dashboard from './pages/Dashboard';
import UserAuthorization from './pages/UserAuthorization'
import SideBar from './components/SideBar';
import UserRegistration from './pages/UserRegistration';
import ForgotPassword from './pages/ForgotPassword';
import ChangePassword from './pages/ChangePassword';

function App(){

  return (
    <>
      <BrowserRouter>
        
            <Routes>
              <Route path='/' element={<Dashboard/>}/>
              <Route path='/auth' element={<UserAuthorization/>}/>
              <Route path='/reg' element={<UserRegistration/>}/>
              <Route path='/recover' element={<ForgotPassword/>}></Route>
              <Route path='/alter' element={<ChangePassword/>}></Route>
            </Routes>
        
      </BrowserRouter>
      
    </>
  )

}

export default App;
