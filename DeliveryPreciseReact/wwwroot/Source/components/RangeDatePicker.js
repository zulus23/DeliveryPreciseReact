import React from 'react';
import moment from 'moment';
import Helmet from 'react-helmet';
import MomentLocaleUtils from 'react-day-picker/moment';

// Make sure moment.js has the required locale data
import 'moment/locale/ru';


import DayPickerInput from 'react-day-picker/DayPickerInput';
import 'react-day-picker/lib/style.css';

import {formatDate, parseDate} from 'react-day-picker/moment';

export default class RangeDatePicker extends React.Component {
    constructor(props) {
        super(props);
        this.handleFromChange = this.handleFromChange.bind(this);
        this.handleToChange = this.handleToChange.bind(this);
        this.state = {
            from: new Date(),
            to: new Date(),
        };
        this.state = {
            locale: 'ru',
        };
    }

    showFromMonth() {
        const {from, to} = this.state;
        
        this.afterUpdate();    
        if (!from) {
            return;
        }
        if (moment(to).diff(moment(from), 'months') < 2) {
            this.to.getDayPicker().showMonth(from);
        }
        
    }

    
    handleFromChange(from) {
        // Change the from date and focus the "to" input field
        this.setState({from},this.afterUpdate);

    }

    afterUpdate = ()=> {
        const {from, to} = this.state;
        console.log(from,to);
        this.props.changeRange({start:this.state.from, end:this.state.to})
    };

    handleToChange(to) {
        this.setState({to}, this.showFromMonth);
        
    }

    render() {
        const {from, to} = this.state;
        const modifiers = {start: from, end: to};
        const format = "DD/MM/YYYY";
        return (
            <div className="InputFromTo">
             <div className="row"> 
                    <div className="col-6">
                    <DayPickerInput 
                        value={formatDate(from, format)}
                        placeholder="от"
                        format={format}
                        formatDate={formatDate}
                        parseDate={parseDate}
                        
                        dayPickerProps={{
                            selectedDays: [from, {from, to}],
                            disabledDays: {after: to},
                            toMonth: to,
                            modifiers,
                            numberOfMonths: 2,
                            todayButton: "Сегодня",
                            onDayClick: () => this.to.getInput().focus(),
                            localeUtils: MomentLocaleUtils,
                            locale: "ru"
                        }}
                        onDayChange={this.handleFromChange}
                        
                        

                    />
                    </div>

                {/* <span className="InputFromTo-to">*/}
                    <div className="col-6"> 
                    <DayPickerInput
                        ref={el => (this.to = el)}
                        value={formatDate(to, format)}
                        placeholder="до"

                        formatDate={formatDate}
                        parseDate={parseDate}

                        dayPickerProps={{
                            selectedDays: [from, {from, to}],
                            disabledDays: {before: from},
                            modifiers,
                            month: from,
                            fromMonth: from,
                            numberOfMonths: 2,
                            todayButton: "Сегодня",
                            localeUtils: MomentLocaleUtils,
                            locale: "ru"
                        }}
                        onDayChange={this.handleToChange}

                    />
                    </div>
                
                {/*    </span>*/}
             </div>  
                <Helmet>
                    <style>{`
  .InputFromTo .DayPicker-Day--selected:not(.DayPicker-Day--start):not(.DayPicker-Day--end):not(.DayPicker-Day--outside) {
    background-color: #f0f8ff !important;
    color: #4a90e2;
  }
  .InputFromTo .DayPicker-Day {
    border-radius: 0 !important;
  }
  .InputFromTo .DayPicker-Day--start {
    border-top-left-radius: 50% !important;
    border-bottom-left-radius: 50% !important;
  }
  .InputFromTo .DayPicker-Day--end {
    border-top-right-radius: 50% !important;
    border-bottom-right-radius: 50% !important;
  }
  .InputFromTo .DayPickerInput-Overlay {
    width: 500px;
  }
  .InputFromTo-to .DayPickerInput-Overlay {
    margin-left: -198px;
  }
`}</style>
                </Helmet>
            </div>
        );
    }
}