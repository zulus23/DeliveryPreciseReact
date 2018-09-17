import React, {Component} from 'react';
import Layout from "../Layout";
import {connect} from "react-redux";
import {fetchCustomer, fetchEnterprise} from "../../actions";
import Enterprise from "../Enterprise";
import Customer from "../Customer";
import {DateRangePicker} from "@progress/kendo-react-dateinputs";
import {Button} from '@progress/kendo-react-buttons';
import KpiIndex from "../KpiIndex";
import Consignee from "../Consignee";
import KpiContainer from "../KpiContainer/KpiContainer";

class App extends Component {
    startDateInputSettings = {
        format: 'dd/MM/yyyy',
        label: ''
    }


    componentDidMount() {
        this.props.dispatch(fetchEnterprise())
    }

    changeCurrentEnterprise = (event) => {
        console.log(event.target.value);
        this.props.dispatch(fetchCustomer())
    };

    render() {
        return (
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-xl-12">
                        <div className="card">

                            <div className="row">
                                <div className="col-sm-4 align-content-center">
                                    <Enterprise data={this.props.enterprise}
                                                onChangeCurrentEnterprise={this.changeCurrentEnterprise}/>
                                </div>
                                <div className="col-sm-4 align-content-center">
                                    <p>Период</p>
                                    <DateRangePicker startDateInputSettings={this.startDateInputSettings}
                                                     endDateInputSettings={this.startDateInputSettings}/>

                                </div>
                                <div className="col-sm-3">
                                </div>
                                <div className="col-sm-1">
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-sm-4">
                                    <Customer data={this.props.customers}/>
                                    <div>
                                        <input key="1" type="checkbox" value="СК"/>
                                        <label>СК</label>
                                        <input key="2" type="checkbox" value="СП"/>
                                        <label>СП</label>
                                        <input key="3" type="checkbox" value="ПР"/>
                                        <label>ПР</label>
                                    </div>
                                </div>
                                <div className="col-sm-4 w-100">
                                    <Consignee data={this.props.enterprise}/>
                                </div>
                                <div className="col-sm-3">
                                    <KpiIndex/>
                                </div>
                                <div className="col-sm-1 align-self-center">
                                    <Button primary={true}>Загрузить</Button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                {/* ===========================================*/}
                <div className="row justify-content-center">
                    <div className="col-xl-12">
                        <div className="card">
                            <KpiContainer/>

                        </div>

                    </div>

                </div>

            </div>

        )
            ;
    }
}

function mapStateProps(state) {
    const {enterprise, customers} = state;
    return {enterprise, customers}

}

export default connect(mapStateProps)(App);