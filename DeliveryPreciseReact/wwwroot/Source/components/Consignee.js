import React from 'react';
import DropDownList from "@progress/kendo-react-dropdowns/dist/npm/DropDownList/DropDownList";
import Auxiliary from "../hoc/Auxiliary";

const Consignee = (props) => {
    return (
        <Auxiliary>
            <p>Грузополучатель</p>
            <DropDownList
                data={props.data}   style={{width:"100%"}}
                textField="Name"
                value={props.value}
                onChange={props.onChangeCustomerDeliveryHandler}
            />
        </Auxiliary>
    );
};

export default Consignee;