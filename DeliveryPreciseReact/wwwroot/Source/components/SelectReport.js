import React, {Component} from 'react';
import {connect} from 'react-redux';


class SelectReport extends Component {
    render() {
        return (
            <div className="row">
               <div className="col-12 p-select-report-drive"> 
                <label htmlFor="isDriverReport"  >
                    <input className="align-middle  pb-0" key="1"  id="isDriverReport" 
                           type="checkbox" value="Движение заказов"
                           checked={this.props.isDriverReport}
                           onChange={this.props.onChange} />
                    <span className="align-text-middle pl-2">Движение заказов</span> 
                </label>
               </div>
               <div className="col-12  p-select-report-kpi">
                <label htmlFor="isKPIReport"  >
                    <input className="align-middle pb-0" key="1"  id="isKPIReport" 
                           type="checkbox" value="KPI"
                           checked={this.props.isKPIReport}
                           onChange={this.props.onChange}
                    />
                    <span className="align-text-middle pl-2">KPI</span>
                </label>
               </div>
                <div className="col-12  p-select-report-kpi">
                    <label htmlFor="isReduceReport"  >
                        <input className="align-middle pb-0" key="1"  id="isReduceReport"
                               type="checkbox" value="Сводный"
                               checked={this.props.isReduceReport}
                               onChange={this.props.onChange}
                        />
                        <span className="align-text-middle pl-2">Сводный по клиентам</span>
                    </label>
                </div> 
            </div>
        );
    }
}

function mapStateToProps(state){
    const {isDriverReport,isKPIReport,isReduceReport} = state;
    return {isDriverReport,isKPIReport,isReduceReport}
}

export default connect(mapStateToProps)(SelectReport);