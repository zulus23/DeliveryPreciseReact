import React from 'react';
import {DropDownList} from '@progress/kendo-react-dropdowns'

const Enterprise = (props) => {
    return (
        <div>
            <DropDownList
                data={props.data} onChange={props.onChangeCurrentEnterprise}
            />
        </div>
    );
};

export default Enterprise;