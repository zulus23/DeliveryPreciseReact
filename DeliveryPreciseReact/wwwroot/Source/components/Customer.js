import React from 'react';
import {AutoComplete} from '@progress/kendo-react-dropdowns'

const Customer = (props) => {
    return (
        <div>
            <AutoComplete data={props.customers} placeholder="Клиент"/>
        </div>
    );
};

export default Customer;