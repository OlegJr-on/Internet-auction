import axios from 'axios'
import { variables } from '../ApiEndPoints/Variables'
import {setUser} from "../reducers/userReducer";


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
            const response = await axios.post(variables.API_URL+'Authenticate', 
            { 
                "EmailAddress" : email, 
                "Password": password
            })

            console.log(response.data)
            dispatch(setUser(response.data.User))
            localStorage.setItem('token', response.data.token)

        } catch (e) {
            alert(e)
        }
    }
}

// export const auth =  () => {
//     return async dispatch => {
//         try {
//             const response = await axios.get(`http://localhost:5000/api/auth/auth`,
//                 {headers:{Authorization:`Bearer ${localStorage.getItem('token')}`}}
//             )
//             dispatch(setUser(response.data.user))
//             localStorage.setItem('token', response.data.token)
//         } catch (e) {
//             alert(e.response.data.message)
//             localStorage.removeItem('token')
//         }
//     }
// }