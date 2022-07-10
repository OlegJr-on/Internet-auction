import React,{Component} from 'react';
import { variables } from '../ApiEndPoints/Variables';
import {NavLink} from 'react-router-dom'

export class Auction extends Component{
    
    constructor(props){
        super(props);
        this.state = {
            lots: []
        }
    }
    
    refreshList(){
        fetch(variables.API_URL+'lot/GetAllLotsWithPhoto')
        .then(response => response.json())
        .then(data => {
            this.setState({lots:data});
        });
    }

    componentDidMount(){
        this.refreshList();
    }

    render(){
        const{
            lots
        } = this.state;

        return (
                <div className='auction'> 
                    <table className="table-auction striped">
                        <thead>
                            <tr>
                                <th>Img</th>
                                <th>Lot №</th>
                                <th>Title</th>
                                <th>Start price </th>
                                <th>Start date</th>
                                <th>End date</th>
                            </tr>
                        </thead>
                        <tbody>
                            {lots.map(lot => 
                                <tr key= {lot.Id}>
                                    <td><img height="65" width="75" src={lot.PhotoSrc}/></td>
                                    <td>{lot.id}</td>
                                    <td> 
                                        <NavLink  to={`${lot.id}`}>
                                            {lot.Title}
                                        </NavLink>
                                    </td>
                                    <td>{lot.StartPrice} $</td>
                                    <td>{lot.StartDate}</td>
                                    <td>{lot.EndDate}</td>
                                </tr>
                                )}
                        </tbody>
                    </table>
                </div>
        )
    }
}
