import {Link,NavLink} from 'react-router-dom'
import {useSelector} from 'react-redux'

function Header(){

    const isAuth = useSelector(state => state.user.isAuth)

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
                                <NavLink className="btn grey darken-2" to="/SignOut" >
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