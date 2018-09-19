import React from 'react';
import {AutoComplete} from '@progress/kendo-react-dropdowns'
import Auxiliary from "../hoc/Auxiliary";

const Customer = (props) => {
    return (
        <Auxiliary>
            <p>Клиент</p>
            <AutoComplete data={props.data} 
                          textField="Name"
                          suggest={true}
                          placeholder="Клиент" style={{width:"100%"}} onChange={props.onChangeCustomerHandler}/>
        </Auxiliary>
    );
};

export default Customer;