import React, {Component} from 'react';

import {connect} from "react-redux";
import {
    calculateSelectKpi,
    changeDateInterval,
    changeEnterprise,
    changeSelectKpi,
    fetchCustomer,
    fetchEnterprise,
    fetchKpi, updateSearchValueCustomer, updateSelectTypeCustomer
} from "../../actions";
import Enterprise from "../Enterprise";
import Customer from "../Customer";
import {DateRangePicker} from "@progress/kendo-react-dateinputs";
import {Button} from '@progress/kendo-react-buttons';



import KpiIndex from "../KpiIndex";
import Consignee from "../Consignee";
import KpiChart from "../KpiChart";
import KpiResult from "../KpiContainer/KpiResult";
import FlashMessage from "../FlashMessage";

class App extends Component {
    startDateInputSettings = {
        format: 'dd/MM/yyyy',
        label: ''
    };


    componentDidMount() {
        this.props.dispatch(fetchEnterprise());
        this.props.dispatch(fetchKpi());
    }

    changeCurrentEnterprise = (event) => {
        const selectedEnterprise =event.target.value;
        this.props.dispatch(changeEnterprise(selectedEnterprise));
        const dataSelect = this.extractedParameterSearch(selectedEnterprise);

       // this.props.dispatch(fetchCustomer(dataSelect))
    };

    extractedParameterSearch= (selectedEnterprise) =>{
        
        const typeCustomer = [];
        if (this.props.isPRChecked === true) {
            typeCustomer.push("ПР");
        }
        if (this.props.isSKChecked === true) {
            typeCustomer.push("СК");
        }
        if (this.props.isSPChecked === true) {
            typeCustomer.push("СП");
        }

        const dataSelect = {
            enterprise: selectedEnterprise,
            typeCustomer: typeCustomer,
            searchingCustomer: this.props.searchingCustomer

        };
        return dataSelect;
    }

    handleChange = (event) => {
        this.props.dispatch(changeDateInterval(event.target.value))
    };

    handleCalculateKpi = (event) =>{
        
        const data = {
           enterprise:this.props.currentEnterprise,
           rangeDate: this.props.dateRangeSelected,
           selectKpi: this.props.selectKpi 
        }
        this.props.dispatch(calculateSelectKpi(data))    
    };
    handlerSelectKpi = (event) => {
        const kpi = event.target.value;
        this.props.dispatch(changeSelectKpi(kpi))
    };
    handlerCheckBox = (event) => {
        const typeBox = event.target.value;
        this.props.dispatch(updateSelectTypeCustomer(typeBox))
    };

    changeCustomerHandler = (event) => {
        const searchValue = event.target.value;
        console.log(event.target.value)
        this.props.dispatch(updateSearchValueCustomer(searchValue));
        
        const dataSelect = this.extractedParameterSearch(this.props.currentEnterprise);
        this.props.dispatch(fetchCustomer(dataSelect))
        
        
    };
    
    render() {
        return (
            <div className="container">
                {this.props.error && <FlashMessage message={this.props.error}/> }
                <div className="row justify-content-center">
                    <div className="col-xl-12">
                        

                            <div className="row">
                                <div className="col-sm-4 ">
                                    <Enterprise data={this.props.enterprise}
                                                onChangeCurrentEnterprise={this.changeCurrentEnterprise} currentEnterprise={this.props.currentEnterprise}/>
                                </div>
                                <div className="col-sm-5 ">
                                    <p>Период</p>
                                    <DateRangePicker startDateInputSettings={this.startDateInputSettings}
                                                     endDateInputSettings={this.startDateInputSettings}
                                       value={this.props.dateRangeSelected}
                                       onChange={this.handleChange}/>

                                </div>
                                <div className="col-sm-2">
                                </div>
                                <div className="col-sm-1">
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-sm-4">
                                    <Customer data={this.props.customers} value = {this.props.searchingCustomer} onChangeCustomerHandler={this.changeCustomerHandler}/>
                                    <div className="row">
                                        <div className="col-sm-4 text-sm-center">
                                            <input key="1" type="checkbox" value="СК" checked={this.props.isSKChecked} onChange={this.handlerCheckBox}/>
                                            <label>СК</label>
                                        </div>
                                        <div className="col-sm-4 text-sm-center">
                                            <input key="2" type="checkbox" value="СП" checked={this.props.isSPChecked} onChange={this.handlerCheckBox}/>
                                            <label>СП</label>
                                        </div>
                                        <div className="col-sm-4 text-sm-center">
                                            <input key="3" type="checkbox" value="ПР" checked={this.props.isPRChecked} onChange={this.handlerCheckBox}/>
                                            <label>ПР</label>
                                        </div>    
                                    </div>
                                </div>
                                <div className="col-sm-4 w-100">
                                    <Consignee data={this.props.enterprise}/>
                                </div>
                                <div className="col-sm-4">
                                    
                                </div>
                              
                            </div>
                            <div className="row">
                                <div className="col-sm-11 w-100">
                                    <KpiIndex data={this.props.kpi} changeSelectKpi={this.handlerSelectKpi}/>
                                </div>
                                <div className="col-sm-1 d-flex align-items-end justify-content-start p-0">
                                    <Button primary={true} onClick={this.handleCalculateKpi} 
                                            disabled={!this.props.selectKpi.length > 0} >Загрузить</Button>
  
                                </div>
                            </div>
                        
                    </div>
                </div>
                {/* ===========================================*/}
                <div className="row justify-content-center">
                    <div className="col-xl-12">
                            <KpiResult data={this.props.selectKpi}/>
                    </div>

                </div>
                <div className="row justify-content-center">
                    <div className="col-sm-12">
                        <KpiChart/>
                    </div>
                </div>    

            </div>

        );
    }

    
}

function mapStateProps(state) {
    const {enterprise, customers,currentEnterprise,kpi,dateRangeSelected
          ,selectKpi,isSKChecked,isSPChecked,isPRChecked,searchingCustomer,error} = state;
    console.log(state);
    return {enterprise, customers,currentEnterprise,kpi,
            dateRangeSelected,selectKpi,
            isSKChecked,isSPChecked,isPRChecked,searchingCustomer,error}

}

export default connect(mapStateProps)(App);