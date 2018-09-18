import React, {Component} from 'react';
import Enterprise from "./Enterprise";
import Customer from "./Customer";
import {fetchCustomer} from "../actions";
import {connect} from "react-redux";
import KpiContainer from "./KpiContainer/KpiContainer";



class Layout extends Component {

    
    
    render() {
       
        return (
            <div className="container-flued">
                
                {this.props.children}
                
                <Enterprise data={this.props.enterprise} onChangeCurrentEnterprise={this.changeCurrentEnterprise}/>
               
                <KpiContainer/>
                
                
            </div>
        );
    }
}
function mapStoreToProps(state) {
    return{
        state:state
    }
}

export default connect(mapStoreToProps)(Layout);