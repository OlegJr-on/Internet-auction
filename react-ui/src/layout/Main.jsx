import {Route,Routes} from 'react-router-dom'
import {Auction} from "../pages/Auction";
import {About} from "../pages/About";
import { Home } from "../pages/Home";
import {Registration} from "../pages/Registration";
import { SignIn } from "../pages/SignIn";
import {Lot} from '../pages/Lot';
import {Profile} from '../pages/UserProfile';
import {Basket} from '../pages/Basket'
import { NotFound } from '../pages/NotFound';
import React from 'react';
import {useSelector} from 'react-redux'

function Main(){

    const isAuth = useSelector(state => state.user.isAuth)

    return (
            <main className="container content">
               <Routes>
                    <Route path='/Auction' element={<Auction />}/>
                    <Route path='/Auction/:lotId' element={<Lot/>}/>
                    <Route path='/About' element={<About/>}/>
                    <Route path='/' element={<Home/>}/>
                    <Route path='/Home' element={<Home/>}/>
                    {!isAuth &&
                     <>
                    <Route path='/Registration' element={<Registration/>}/>
                    <Route path='/SignIn' element={<SignIn/>}/>  
                    </>
                    }
                    <Route path='/Profile' element={<Profile/>}/>   
                    <Route path='/Basket' element={<Basket/>}/>                  
                    <Route path="*" element={ <NotFound/>} />
                </Routes>
            </main>
    )
}

export {Main}