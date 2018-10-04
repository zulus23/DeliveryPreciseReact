import React, {Component} from 'react';
import {ComboBox} from '@progress/kendo-react-dropdowns'
import Auxiliary from "../hoc/Auxiliary";
import { filterBy } from '@progress/kendo-data-query';
import {connect} from 'react-redux'



const delay = 500;
class Customer extends Component {
    state = {
        data: [],
        loading: false
    };

    
    
    
    filterChange = (event) => {
        clearTimeout(this.timeout);
       
        this.timeout = setTimeout(() => {
            this.setState({
                data: this.filterData(event.filter),
                loading: false
            });
        }, delay);

        this.setState({
            loading: true
        });
    }

    filterData = (filter) =>{
       
        const data = this.props.AllData.slice();
       return filterBy(data, filter);
    }
    render() {
        return (

            <Auxiliary>
                <p className="mb-1 text-center">Клиент</p>
                <ComboBox data={this.state.data}
                          textField="Name"
                          placeholder="Клиент" style={{width: "100%"}}
                          filterable={true}
                        
                          dataItemKey="Code"
                          value={this.props.value}
                          onChange={this.props.onChangeCustomerHandler}
                          onFilterChange={this.filterChange}/>
            </Auxiliary>
        )
            ;
    }
};

function mapStateToProps(state) {
    
    return {
        AllData: state.customers
    }
}

export default connect(mapStateToProps)(Customer);