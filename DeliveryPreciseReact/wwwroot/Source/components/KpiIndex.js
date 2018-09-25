import React from 'react';
import Auxiliary from "../hoc/Auxiliary";
import {MultiSelect} from "@progress/kendo-react-dropdowns";

const KpiIndex = (props) => {
    return (
        <Auxiliary>
            <p className="mb-1">Показатель</p>
            <MultiSelect
                data={props.data} textField="Name"  style={{width:"100%"}} className="p-shadow"
                onChange={props.changeSelectKpi}/>   
        </Auxiliary>
    );
};

export default KpiIndex;