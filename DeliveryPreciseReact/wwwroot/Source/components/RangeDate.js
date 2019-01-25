import React, {Component} from 'react';
import PropTypes from 'prop-types';
import { DatePicker } from '@progress/kendo-react-dateinputs';

class RangeDate extends Component {
    constructor(props) {
        super(props);

    }
   

    render() {
        return (
            <div className="row">
                <div className="col-6">
                    <DatePicker id="start"
                       
                    />
                    </div>
                <div className="col-6">
                    <DatePicker id="end"
                                
                       
                    />
                    </div>

            </div>
        );
    }
}

RangeDate.propTypes = {};

export default RangeDate;