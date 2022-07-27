import React,{Component}  from 'react';
import {data} from '../actions/user'
import { variables } from '../ApiEndPoints/Variables';
import {NavLink} from 'react-router-dom'


export class Basket extends Component
{
    constructor(props){
        super(props);
        this.state = {
            orders: []
        }
    }

    deleteOrder(id){
        fetch(variables.API_URL+'order/Delete/'+ id,{
                method: 'DELETE',
                headers:{
                    'Accept':'application/json',
                    'Content-Type':'application/json'
                }
                })
    }

    refreshList(){
        fetch(variables.API_URL+'order/GetAllUserOrdersById/'+ data.User.Id)
        .then(response => response.json())
        .then(data => {
            this.setState({orders:data});
        });
    }

    componentDidMount(){
        this.refreshList();
    }

    render(){
        const{
            orders
        } = this.state;

        return (
        <div className="basket">
           <h2>Basket</h2>
            <div className='photo'>
                <img height="220" width="250" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQHdZvnPel64LW_X67XToN2hClYgZCtuVX-Cyue3gGWPD9ItLOY_HYGReSAfXqAMP5-l8w&usqp=CAU"/>
            </div>

                <div className='orders'>
                {orders.length != 0 && 
                     <table className='receipts-data'>
                            <thead>
                                <tr>
                                    <th>Img</th>
                                    <th>Lot №</th>
                                    <th>Title</th>
                                    <th>Current price </th>
                                    <th>End date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {orders.map(order => 
                                    <tr key= {order.Id}>
                                        <td><img height="65" width="75" src={order.PhotoSrc}/></td>
                                        <td>{order.id}</td>
                                        <td> 
                                            <NavLink  to={`/Auction/${order.id}`}>
                                                {order.Title}
                                            </NavLink>
                                        </td>
                                        <td>{order.CurrentPrice} $</td>
                                        <td>{order.EndDate}</td>
                                        <td>
                                            <button className='del-order' onClick={() => this.deleteOrder(order.OrderId)}
                                            type="submit" >
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
                                                    <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z"/>
                                                    </svg>
                                            </button>
                                        </td>
                                    </tr>
                                    )}
                            </tbody>
                        </table>
                    }
                    {orders.length == 0 && 
                        <h2>You haven't orders now 
                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-emoji-frown" viewBox="0 0 16 16">
                        <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/>
                        <path d="M4.285 12.433a.5.5 0 0 0 .683-.183A3.498 3.498 0 0 1 8 10.5c1.295 0 2.426.703 3.032 1.75a.5.5 0 0 0 .866-.5A4.498 4.498 0 0 0 8 9.5a4.5 4.5 0 0 0-3.898 2.25.5.5 0 0 0 .183.683zM7 6.5C7 7.328 6.552 8 6 8s-1-.672-1-1.5S5.448 5 6 5s1 .672 1 1.5zm4 0c0 .828-.448 1.5-1 1.5s-1-.672-1-1.5S9.448 5 10 5s1 .672 1 1.5z"/>
                        </svg>
                        </h2>
                    }
                </div>

        </div>
        )
    } 
}