import React from 'react';
import {DropDownList} from '@progress/kendo-react-dropdowns'
import Auxiliary from "../hoc/Auxiliary";

const Enterprise = (props) => {
    return (
        <Auxiliary>
            <p className="mb-1">Предприятие</p>
            <DropDownList 
                data={props.data} onChange={props.onChangeCurrentEnterprise}   value={props.currentEnterprise}
                className="w-100 p-shadow"
            />
        </Auxiliary>
    );
};

export default Enterprise;