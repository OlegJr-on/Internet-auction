import React,{  useEffect, useState} from 'react';
import { useParams } from 'react-router-dom';
import {data} from '../actions/user'
import { variables } from '../ApiEndPoints/Variables';
import {useSelector} from 'react-redux'
import { setUser } from '../reducers/userReducer';

function updateClick(){
    fetch(variables.API_URL+ 'user/Update',{
        method: 'PUT',
        headers:{
            'Accept':'application/json',
            'Content-Type':'application/json'
        },
        body:JSON.stringify({
            Name: data.User.Name,
            Surname:data.User.Surname
        })
    })
    .then(response => response.json())
    .then(data => { setUser(data) 
    });
}


export function Profile()
{
    const [user,setUser] = useState({})
    const isAuth = useSelector(state => state.user.isAuth)
    
    function refreshList(){
        fetch(variables.API_URL+ 'user/GetById/'+ data.User.Id)
        .then(response => response.json())
        .then(data => { setUser(data) 
        });
    }

    useEffect(
        () => refreshList ,[]
    )

   
    return(
        <div className="user-description">
           <h2>Personal data</h2>
            <div className='photo'>
                <img height="250" width="250" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRNBNdcMDNS2r9df1IWFVc8AY0QNtfNhEJv7fGS5TdhUWrlBqfGu1PCCn9lKpL-FqF9dWc&usqp=CAU"/>
            </div>

                <div className='data'>
                    <table className='personal-data'>
                        <tr>
                            <td><h3>{user.Name} {user.Surname}</h3></td>
                        </tr>
                        <tr>
                            <td>Location</td>
                            <td> <b>{user.Location}</b></td>
                        </tr>
                        <tr>
                            <td>Number of phone</td>
                            <td> <b>{user.PhoneNumber}</b></td>
                        </tr>
                        <tr>
                            <td>Email </td>
                            <td> <b>{user.Email}</b></td>
                        </tr>
                    </table>
                </div>

                {/* <div className='Update-data-user'>
                    <button class="btn waves-effect waves-light" type="submit" name="action">Update
                        <i class="material-icons right"> 
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                            <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                            </svg>
                        </i>
                    </button>
                </div> */}

        </div>
        ) 
}