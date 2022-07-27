import axios from 'axios'
import { variables } from '../ApiEndPoints/Variables'
import {setUser} from "../reducers/userReducer";

export const data = {
    User: {
        "Email": "mandra@gmail.com",
        "Password": "123456",
    }
}

export const registration = async (name,surname,location,phoneNumber,email,password,accessLevel = 2 ) =>
{
    try {
        const response = await axios.post(variables.API_URL+'user/add',{
            name,
            surname,
            location,
            phoneNumber,
            email,
            password,
            accessLevel      
        })
        alert("Congradulations")
    }
    catch (e) {
        alert(e)
    }

}

export const login =  (email, password) => {

    return async dispatch => {
       
        try {
            if(email != data.User.Email || password != data.User.Password){
                alert('User not found')
            }
            else{
            dispatch(setUser(data.User))
            localStorage.setItem('token', data.token)
            }
            
        } catch (e) {
            alert(e)
        }
    }
    
}
