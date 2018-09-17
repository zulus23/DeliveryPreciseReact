import React, {Component} from 'react';
import Layout from "../Layout";
import {connect} from "react-redux";
import {fetchEnterprise} from "../../actions";

class App extends Component {
    
    componentDidMount(){
         this.props.dispatch(fetchEnterprise())    
    }
    render() {
        return (
            <Layout enterprise={this.props.enterprise} customers/>
            
        );
    }
}
function mapStateProps(state){
    const {enterprise,customers} =  state;
    return {enterprise,customers}  
    
}
export default connect(mapStateProps)(App);