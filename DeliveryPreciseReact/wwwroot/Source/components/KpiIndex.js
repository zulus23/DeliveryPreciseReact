import React from 'react';
import Auxiliary from "../hoc/Auxiliary";
import DropDownList from "@progress/kendo-react-dropdowns/dist/npm/DropDownList/DropDownList";

const KpiIndex = (props) => {
    return (
        <Auxiliary>
            <p>Показатель</p>
            <DropDownList
                data={props.data}   style={{width:"100%"}}/>   
        </Auxiliary>
    );
};

export default KpiIndex;