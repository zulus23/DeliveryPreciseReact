import React from 'react';
import Auxiliary from "../hoc/Auxiliary";
import {MultiSelect} from "@progress/kendo-react-dropdowns";

const KpiIndex = (props) => {
    return (
        <Auxiliary>
            <p>Показатель</p>
            <MultiSelect
                data={props.data} textField="Name"  style={{width:"100%"}}/>   
        </Auxiliary>
    );
};

export default KpiIndex;