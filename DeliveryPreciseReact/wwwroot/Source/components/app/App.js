import React, {Component} from 'react';

import {connect} from "react-redux";
import {
    calculateSelectKpi,
    changeDateInterval,
    changeEnterprise,
    changeSelectKpi,
    createReport,
    fetchCustomer,
    fetchEnterprise,
    fetchKpi,
    updateSearchValueCustomer,
    updateSearchValueCustomerDelivery,
    updateSelectedTypeReportSucceeded,
    updateSelectTypeCustomer
} from "../../actions";
import Enterprise from "../Enterprise";
import Customer from "../Customer";
import {DateRangePicker} from "@progress/kendo-react-dateinputs";
import {Button} from '@progress/kendo-react-buttons';

import moment from 'moment';



import KpiIndex from "../KpiIndex";
import Consignee from "../Consignee";
import KpiChart from "../KpiChart";
import KpiResult from "../KpiContainer/KpiResult";
import FlashMessage from "../FlashMessage";
import Auxiliary from "../../hoc/Auxiliary";
import {ToastContainer} from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import LoaderSpinner from "../LoaderSpinner";
import SelectReport from "../SelectReport";




class App extends Component {
    startDateInputSettings = {
        format: 'dd/MM/yyyy',
        label: ''
    };


    componentDidMount() {
        this.props.dispatch(fetchEnterprise());
        this.props.dispatch(fetchKpi());
        const dataSelect = this.extractedParameterSearch(this.props.currentEnterprise);
        this.props.dispatch(fetchCustomer(dataSelect))
    }

    changeCurrentEnterprise = (event) => {
        const selectedEnterprise =event.target.value;
        this.props.dispatch(changeEnterprise(selectedEnterprise));
       
    };

    extractedParameterSearch= (selectedEnterprise) =>{
        
        
        const typeCustomer = this.collectTypeCustomer();

        const dataSelect = {
            enterprise: selectedEnterprise,
            typeCustomer: typeCustomer,
        };
        return dataSelect;
    };

    collectTypeCustomer = ()=> {
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
        return typeCustomer;
    };

    handleChange = (event) => {
        
        this.props.dispatch(changeDateInterval(event.target.value))
    };

    handleCalculateKpi = (event) =>{
         const _typeCustomer = this.collectTypeCustomer();
        const data = {
           enterprise:this.props.currentEnterprise,
           rangeDate: { start:moment(this.props.dateRangeSelected.start).format("YYYY-MM-DD"),
                        end : moment(this.props.dateRangeSelected.end).format("YYYY-MM-DD")
                       },
           selectKpi: this.props.selectKpi,
           customer: this.props.searchingCustomer, 
           typeCustomer: _typeCustomer,
           customerDelivery: this.props.searchingCustomerDelivery 
        };
        const date =  moment(this.props.dateRangeSelected.start).format("YYYY-MM-DD");
        
        this.props.dispatch(calculateSelectKpi(data))    
    };
    handlerSelectKpi = (event) => {
        const kpi = event.target.value;
        this.props.dispatch(changeSelectKpi(kpi))
    };
    handlerCheckBox = (event) => {
        const typeBox = event.target.value;
        this.props.dispatch(updateSelectTypeCustomer(typeBox));
       /* const dataSelect = this.extractedParameterSearch(this.props.currentEnterprise);
        this.props.dispatch(fetchCustomer(dataSelect))*/
    };

    changeCustomerHandler = (event) => {
        const searchValue = event.target.value;
        console.log(event);
        this.props.dispatch(updateSearchValueCustomer(searchValue));
    };
    
    
    
    changeCustomerDeliveryHandler = (event) => {
        const searchValue = event.target.value;

        this.props.dispatch(updateSearchValueCustomerDelivery(searchValue)); 
    };

    createReportHandler = (event) =>{
        const _typeCustomer = this.collectTypeCustomer();
        const data = {
            enterprise:this.props.currentEnterprise,
            rangeDate: { start:moment(this.props.dateRangeSelected.start).format("YYYY-MM-DD"),
                end : moment(this.props.dateRangeSelected.end).format("YYYY-MM-DD")
            },
            selectKpi: this.props.selectKpi,
            customer: this.props.searchingCustomer,
            typeCustomer: _typeCustomer,
            customerDelivery: this.props.searchingCustomerDelivery
        };
      
        this.props.dispatch(createReport(data))
        
    };
    
    handleSelectReport = (event) => {
       
      const data = event.target.value;  
      this.props.dispatch(updateSelectedTypeReportSucceeded(data))
    };
    
    
    render() {
        return (
            <Auxiliary>
                <LoaderSpinner isLoading={this.props.isLoading}/> 

                <div className="container">
                <div className="p-caption-params">KPI</div>
                {/*{this.props.error && <FlashMessage message={this.props.error}/> }*/}
                <ToastContainer autoClose={4000}/>
                <div className="mt-1 mb-2">
                  <div className="row justify-content-center">
                    <div className="col-xl-12">
                        

                            <div className="row mt-2">
                                <div className="col-12 col-sm-12 col-md-4 col-lg-4 col-xl-4 ">
                                    <Enterprise data={this.props.enterprise}
                                                onChangeCurrentEnterprise={this.changeCurrentEnterprise} currentEnterprise={this.props.currentEnterprise}/>
                                </div>
                                <div className="col-12 col-sm-12 col-md-5 col-lg-4 col-xl-3">
                                    <p className="mb-1 text-center">Период</p>
                                    <DateRangePicker startDateInputSettings={this.startDateInputSettings}
                                                     endDateInputSettings={this.startDateInputSettings}
                                       value={this.props.dateRangeSelected}
                                       onChange={this.handleChange}/>

                                </div>
                                <div className=" col-auto col-md-3 col-lg-4 col-xl-5 d-flex justify-content-start">
                                        <div className="row">
                                         <div className="col-2">   
                                         <button className="p-shadow p-button" 
                                                 onClick={this.createReportHandler}
                                                 disabled={!this.props.isDriverReport  && !this.props.isKPIReport}>Отчет</button>
                                         </div>
                                         <div className="col-10">   
                                            <SelectReport onChange={this.handleSelectReport}/>
                                         </div>                                             
                                        </div>    
                                         
                                </div>
                                
                            </div>
                            <div className="row mt-2">
                                <div className="col-12 col-xl-4">
                                    <Customer className="p-shadow" data={this.props.customers} value = {this.props.searchingCustomer} onChangeCustomerHandler={this.changeCustomerHandler}/>
                                    <div className="row mt-1">
                                        <div className="col-sm-4 text-sm-center">
                                            <label htmlFor="isSKChecked" >
                                                <input className="align-middle" key="1"  id="isSKChecked" type="checkbox" value="СК" checked={this.props.isSKChecked} onChange={this.handlerCheckBox}/>
                                                <span className="align-text-middle pl-2">СК</span>
                                            </label>
                                            
                                        </div>
                                        <div className="col-sm-4 text-sm-center">
                                            <label htmlFor="isSPChecked" >
                                              <input className="align-middle" key="2" id="isSPChecked" 
                                                     type="checkbox" value="СП" checked={this.props.isSPChecked} 
                                                     onChange={this.handlerCheckBox}/>
                                                <span className="align-text-middle pl-2">СП</span>
                                            </label>    
                                        </div>
                                        <div className="col-sm-4 text-sm-center">
                                            <label htmlFor="isPRChecked" >
                                                <input className="align-middle" key="3" id="isPRChecked" type="checkbox" value="ПР" checked={this.props.isPRChecked} onChange={this.handlerCheckBox}/>
                                                <span className="align-text-middle pl-2">ПР</span>
                                            </label>    
                                        </div>    
                                    </div>
                                </div>
                                <div className="col-12 col-xl-8">
                                    <Consignee data={this.props.customerDelivery} 
                                               value = {this.props.searchingCustomerDelivery}
                                               onChangeCustomerDeliveryHandler={this.changeCustomerDeliveryHandler }
                                    />
                                </div>
                                
                              
                            </div>
                            <div className="row mt-1">
                                <div className="col-12 col-sm-12 col-md-12 col-xl-11">
                                    <KpiIndex  data={this.props.kpi} changeSelectKpi={this.handlerSelectKpi}/>

                                </div>
                                <div className="col-lg-1 col-xl-1 col-lg-auto col-xl-auto pl-xl-0 d-flex justify-content-start"> 
                                    
                                    <button  className="p-button" onClick={this.handleCalculateKpi}
                                             
                                             disabled={!this.props.selectKpi.length > 0} >Загрузить</button>
                                     

                                </div>
{/*
                                <div className="col-sm-1 d-flex flex-column-reverse justify-content-between" >
                                    
                                    <Button  className="p-shadow w-100" primary={true} onClick={this.handleCalculateKpi} 
                                            disabled={!this.props.selectKpi.length > 0} >Загрузить</Button>
                                       
  
                                </div>
*/}
                            </div>
                            <div className="row mt-1">
                                <span className="spacer5"></span>
                            </div>
                        
                    </div>
                </div>
                </div>    
            </div>
            <div className="container mt-5" >
                
                {/* ===========================================*/}
                <div className="row justify-content-center mt-2">
                    <div className="col-xl-12 mt-1">
                            <KpiResult data={this.props.calculateKpi}/>
                    </div>

                </div>
                <div className="row justify-content-center mt-2">
                    <div className="col-sm-12">
                        <KpiChart />
                    </div>
                </div>
                <div className="row mt-1">
                    <span className="spacer5"></span>
                </div>

            </div>
            </Auxiliary>     

        );
    }

    
}

function mapStateProps(state) {
    const {enterprise, customers,currentEnterprise,kpi,dateRangeSelected
           ,selectKpi,isSKChecked,isSPChecked,isPRChecked,searchingCustomer
           ,error,calculateKpi,customerDelivery,searchingCustomerDelivery,isLoading,isDriverReport,isKPIReport} = state;

    return {enterprise, customers,currentEnterprise,kpi,
            dateRangeSelected,selectKpi,
            isSKChecked,isSPChecked,isPRChecked,searchingCustomer,error,calculateKpi,customerDelivery,searchingCustomerDelivery,isLoading,isDriverReport,isKPIReport}

}

export default connect(mapStateProps)(App);