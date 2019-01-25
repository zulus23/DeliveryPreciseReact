import React, {Component} from 'react';
import PropTypes from 'prop-types';
import { DatePicker } from '@progress/kendo-react-dateinputs';



class RangeDate extends Component {
    constructor(props) {
        super(props);
        this.state = {
            start:this.props.startDate,
            end:this.props.endDate
        }
        
    }
   
    changeDateStart = (e) => {
        this.setState({start:e.target.value},
        () => {this.props.changeRange({start:this.state.start,end:this.state.end})});
        
    };
    changeDateEnd = (e) => {
        this.setState({end:e.target.value},()=> this.props.changeRange({start:this.state.start,end:this.state.end}));
    };

    render() {
        return (
            <div className="row">
                <div className="col-6">
                    <DatePicker  id="start"
                    value={this.state.start}            
                    onChange={this.changeDateStart}                
                    />
                    </div>
                <div className="col-6">
                    <DatePicker id="end" 
                                value={this.state.end} 
                                onChange={this.changeDateEnd}           
                       
                    />
                    </div>

            </div>
        );
    }
}

RangeDate.propTypes = {};

export default RangeDate;