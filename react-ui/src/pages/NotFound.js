import React from 'react';
import { Link } from 'react-router-dom';

class NotFound extends React.Component{
    render(){
        return <div className='NotFoundPage-fragment'>

                <img height='450' width='600' src='https://www.webtekno.com/images/editor/default/0003/49/d69c8ccfa20fc2ef66b4655df8631cd433a037a3.jpeg'/>
                
                <div className='link'>
                    <p className='btn waves-effect waves-light grey darken-2'>
                        <Link to="/">Go to Home </Link>
                    </p>
                </div>

          </div>;
    }
}

export {NotFound}