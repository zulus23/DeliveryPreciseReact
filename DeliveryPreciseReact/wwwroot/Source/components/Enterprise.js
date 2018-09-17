import React from 'react';
import {DropDownList} from '@progress/kendo-react-dropdowns'
import Auxiliary from "../hoc/Auxiliary";

const Enterprise = (props) => {
    return (
        <Auxiliary>
            <p>Предприятие</p>
            <DropDownList
                data={props.data} onChange={props.onChangeCurrentEnterprise}  style={{width:"100%"}}/>
        </Auxiliary>
    );
};

export default Enterprise;