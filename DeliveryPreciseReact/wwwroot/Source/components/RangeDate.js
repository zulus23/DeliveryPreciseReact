import React, {Component} from 'react';
import PropTypes from 'prop-types';
import DayPickerInput from "./RangeDatePicker";

class RangeDate extends Component {
    constructor(props) {
        super(props);

    }
   

    render() {
        return (
            <div className="row">
                <div className="col-6">
                    </div>
                <div className="col-6">
                    </div>

            </div>
        );
    }
}

RangeDate.propTypes = {};

export default RangeDate;