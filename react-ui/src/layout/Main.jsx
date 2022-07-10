import {Route,Routes} from 'react-router-dom'
import {Auction} from "../pages/Auction";
import {About} from "../pages/About";
import { Home } from "../pages/Home";
import {Registration} from "../pages/Registration";
import { SignIn } from "../pages/SignIn";
import {Lot} from '../pages/Lot';
import { NotFound } from '../pages/NotFound';
import React, {useEffect} from 'react';
import {useDispatch,useSelector} from 'react-redux'

function Main(){

    const isAuth = useSelector(state => state.user.isAuth)
    const dispatch = useDispatch()
  
    // useEffect(() => {
    //     dispatch(auth())
    // }, [])

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

                    <Route path="*" element={ <NotFound/>} />
                </Routes>
            </main>
    )
}

export {Main}