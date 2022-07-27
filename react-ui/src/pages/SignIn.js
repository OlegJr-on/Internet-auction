import React,{useState} from 'react';
import { login } from '../actions/user';
import {useDispatch} from "react-redux";
import { NavLink } from 'react-router-dom';

export function SignIn(){

        const [email,setEmail] = useState('')
        const [password,setPassword] = useState('')
        const dispatch = useDispatch()
        
        return (
            <div className="registration-cssave">
                <form>
                    <h3 className="text-center">Login</h3>

                    <div className="form-group">
                        <input value={email} onChange={(e) => setEmail(e.target.value) } 
                                className="form-control item" type="text" name="email" maxlength="50" minlength="4" pattern="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" id="email" placeholder="Email" required/>
                    </div>

                    <div className="form-group">
                        <input value={password}   onChange={(e) => setPassword   (e.target.value) } 
                                className="form-control item" type="password" name="password" minlength="6" id="password" placeholder="Password" required/>
                    </div>
                    <div className="form-group">
                    <NavLink to="/" >
                        <button className="btn btn-primary btn-block create-account"
                                 type="submit" 
                                 disabled={!(!!(email)) || !(!!(password))}
                                 onClick={() => dispatch(login(email, password))}  >Sign In</button>
                        </NavLink>
                    </div>
                </form>
            </div>

        )
}

