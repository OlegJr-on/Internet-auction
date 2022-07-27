import React,{ useEffect, useState} from 'react';
import { useParams } from 'react-router-dom';
import { variables } from '../ApiEndPoints/Variables';
import {useSelector} from 'react-redux'
import {data} from '../actions/user'
import axios from 'axios'



function MakeBid(price,lotId) {

    let date = new Date();
    let output = `${date.getFullYear()}-${date.getMonth()+1}-${date.getDate()}T${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}.030Z`;

    if(price > 0){
        try {
            //create order
            axios.post(variables.API_URL+'order/add',{
                "userId": data.User.Id,
                "operationDate": output,
              })

            //   axios.get(variables.API_URL+'order/GetLast')
            //         .then(res => {
            //             order = res.data;
                        
            //         })

            //   console.log(order)
              // create order detail
            //   const responseOrderDetail = axios.post(`${variables.API_URL}order/AddLot?orderId=${order+1}&lotId=${lotId}`
            //   )
             
            
            alert("Congratulations. Order created ")
        }
        catch (e) {
            alert(e)
        }
    }
    else{
        alert("Price incorrectly")
    }

}

export function Lot(){

    const [bid,setBid] = useState('')
    const params = useParams()
    const lotId = params.lotId
    const [lot,setLot] = useState({})
    const [order,setOrder] = useState({})
    const [photo,setPhotos] = useState([])
    const isAuth = useSelector(state => state.user.isAuth)
    
    function refreshList(){
        fetch(variables.API_URL+ 'lot/GetPhotoInGroup/' + lotId)
        .then(response => response.json())
        .then(data => { setPhotos(data) 
        });

        fetch(variables.API_URL+ 'lot/GetById/'+ lotId)
        .then(response => response.json())
        .then(data => { setLot(data) 
        });
    }

    useEffect(
        () => refreshList ,[]
    )

    return(
        <div className="lot-description">
           
            <div className='photos'>
                {
                    photo.map(photo => 
                        <p key= {photo.Id}>
                            <img height="350" width="500" src={photo.PhotoSrc}/>
                        </p>
                    )
                }
            </div>

                <table>

                    <tr>
                        <td><h3>{lot.Title}</h3></td>
                    </tr>
                    <tr>
                        <td>Price Now:</td>
                        <td> <b>$ {lot.CurrentPrice}</b></td>
                    </tr>
                    <tr>
                        <td>Start price:</td>
                        <td> <b>$ {lot.StartPrice}</b></td>
                    </tr>
                    <tr>
                        <td>Minimum rate: </td>
                        <td> <b>{lot.MinRate} $</b></td>
                    </tr>
                    <tr>
                        <td>Status lot:</td> <td>
                             <b>
                                {lot.Status}
                             </b>
                        </td>
                    </tr>
                    <tr>
                        <td><h4>Period:</h4></td>
                    </tr>
                    <tr>
                        <td>Start: </td>
                        <td> <b>{lot.StartDate}</b></td>
                    </tr>
                    <tr>
                        <td>End: </td>
                        <td> <b> {lot.EndDate} </b></td>
                    </tr>

                </table>

                <div className='buy-lot'>
                    <div className='xz'>
                        <label><h4>Your bid:</h4> <h6>*price must be higher than the current price + min rate</h6></label>
                        <input value={bid}  onChange={(e) => setBid(e.target.value) } 
                               type="text" className="form-control" name="bid"
                               placeholder="$"
                         />
                    </div> 
                    <button disabled ={!isAuth} type="submit"
                             className="waves-effect waves-light btn-large"
                              onClick={() => MakeBid(bid,lotId,order,setOrder())}> Bid now</button>
                </div>
        </div>
             
        ) 
}