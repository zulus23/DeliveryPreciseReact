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
          
                <Grid
                    data={this.props.calculateKpi.map(
                        (kpi) => (
                            
                            {...kpi,selected: kpi.Description === this.props.selectedKpiDescription})
                    )}
                    selectedField="selected"
                    onRowClick={this.handleSelectCalculateKpi}>
                    <Column field="Description" title="Наименование KPI" width="500px"  className="text-left  "/>
                    <Column field="Target" title="Цель" format="{0:0.00}" />
                    <Column field="Fact" title="Факт" format="{0:0.00}" />
                    <Column field="Deviation" title="Откл." format="{0:0.00}" />
                    <Column field="CountOrder" title="Заказы"  format="{0:0}"/>
                </Grid>
          
        );
    }
}

function mapStateToProps(state){
    const {calculateKpi,selectCalculateKpi,selectedKpiDescription} = state;
    return {calculateKpi,selectCalculateKpi,selectedKpiDescription}
}

export default connect(mapStateToProps)(KpiResult);