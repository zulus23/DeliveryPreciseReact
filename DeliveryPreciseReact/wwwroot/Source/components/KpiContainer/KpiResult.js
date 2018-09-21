import React, {Component} from 'react';
import { Grid, GridColumn as Column } from '@progress/kendo-react-grid';
import {connect} from "react-redux";
import {updateSelectCalculateKpi} from "../../actions";

class KpiResult extends Component {


    /*constructor(props) {
        super(props)
    }*/
    
    handleSelectCalculateKpi = (event) => {
      this.props.dispatch(updateSelectCalculateKpi(event.dataItem))          
    };
    
    
    render() {
        return (
            <div  onMouseDown={e => e.preventDefault() /* prevents browser text selection */}>
                <Grid
                    data={this.props.calculateKpi}
                    selectedField="selected"
                    onRowClick={this.handleSelectCalculateKpi}>
                    <Column field="Description" title="Наименование KPI" width="500px" />
                    <Column field="Target" title="Цель" />
                    <Column field="Fact" title="Факт" />
                    <Column field="Deviation" title="Откл." />
                    <Column field="CountOrder" title="Заказы" />
                </Grid>
            </div>
        );
    }
}

function mapStateToProps(state){
    const {calculateKpi,selectCalculateKpi} = state;
    return {calculateKpi,selectCalculateKpi}
}

export default connect(mapStateToProps)(KpiResult);