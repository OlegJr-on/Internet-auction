import {Link,NavLink} from 'react-router-dom'
import {useSelector,useDispatch} from 'react-redux'
import {logout} from '../reducers/userReducer'

function Header(){

    const isAuth = useSelector(state => state.user.isAuth)
    const dispatch = useDispatch();

    return ( 
        <div className="header">
            <nav className=" grey darken-1">
                <div className="nav-wrapper">
                    <Link to="/" className="brand-logo">Auction</Link>
                        <ul id="nav-mobile" className="right hide-on-med-and-down">
                            <NavLink className="btn grey darken-2" to="/Home" >
                                Home
                            </NavLink>
                            <NavLink className="btn grey darken-2" to="/Auction" >
                                Auction
                            </NavLink>
                            <NavLink className="btn grey darken-2" to="/About" >
                                About
                            </NavLink>

                           
                            {!isAuth &&
                            <>
                                <NavLink className="btn grey darken-2" to="/Registration"> 
                                    Registration
                                </NavLink>
                                </>
                            }
                            {!isAuth &&
                            <>
                                <NavLink className="btn grey darken-2" to="/SignIn" >
                                    Sign In
                                </NavLink>
                            </>
                            }
                            {isAuth &&
                            <>
                            
                            <NavLink className="btn grey darken-2" to="/Profile" >
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-person" viewBox="0 0 16 16">
                            <path d="M12 1a1 1 0 0 1 1 1v10.755S12 11 8 11s-5 1.755-5 1.755V2a1 1 0 0 1 1-1h8zM4 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H4z"/>
                            <path d="M8 10a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
                            </svg>
                            </NavLink>

                            <NavLink className="btn grey darken-2" to="/Basket" >
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-cart" viewBox="0 0 16 16">
                                <path d="M0 1.5A.5.5 0 0 1 .5 1H2a.5.5 0 0 1 .485.379L2.89 3H14.5a.5.5 0 0 1 .491.592l-1.5 8A.5.5 0 0 1 13 12H4a.5.5 0 0 1-.491-.408L2.01 3.607 1.61 2H.5a.5.5 0 0 1-.5-.5zM3.102 4l1.313 7h8.17l1.313-7H3.102zM5 12a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm7 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm-7 1a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm7 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2z"/>
                                </svg> 
                            </NavLink>

                            <NavLink className="btn grey darken-2" to="/" onClick={() => dispatch(logout()) }>
                                Sign Out
                            </NavLink>
                            </>
                            }
                        </ul>   
                </div>
            </nav>
      </div>
    )
}

export {Header}