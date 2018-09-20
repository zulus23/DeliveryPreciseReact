import React from 'react';
import {ComboBox} from '@progress/kendo-react-dropdowns'
import Auxiliary from "../hoc/Auxiliary";

const Customer = (props) => {
    return (
        <Auxiliary>
            <p>Клиент</p>
            <ComboBox data={props.data} 
                          textField="Name"
                          placeholder="Клиент" style={{width:"100%"}}
                          value={props.value}
                          onChange={props.onChangeCustomerHandler}/>
        </Auxiliary>
    );
};

export default Customer;