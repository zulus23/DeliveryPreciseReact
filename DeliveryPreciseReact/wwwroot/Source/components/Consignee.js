import React from 'react';
import DropDownList from "@progress/kendo-react-dropdowns/dist/npm/DropDownList/DropDownList";
import Auxiliary from "../hoc/Auxiliary";

const Consignee = (props) => {
    return (
        <Auxiliary>
            <p className="mb-1">Грузополучатель</p>
            <DropDownList
                data={props.data}   style={{width:"100%"}}
                textField="FullName"
                value={props.value}
                onChange={props.onChangeCustomerDeliveryHandler}
            />
        </Auxiliary>
    );
};

export default Consignee;