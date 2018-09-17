import React from 'react';
import {AutoComplete} from '@progress/kendo-react-dropdowns'

const Customer = (props) => {
    return (
        <div>
            <AutoComplete data={props.data} textField="Name" placeholder="Клиент"/>
        </div>
    );
};

export default Customer;