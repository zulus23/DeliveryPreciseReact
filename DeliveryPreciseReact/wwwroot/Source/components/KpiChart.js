import React, {Component} from 'react';
import {
    Chart,
    ChartSeries,
    ChartSeriesItem,
    ChartSeriesDefaults,
    ChartCategoryAxis,
    ChartCategoryAxisItem,
    ChartTitle,
    ChartLegend
} from '@progress/kendo-react-charts';
import {connect} from "react-redux";





class KpiChart extends Component {
    render(){ 
         let localData;
         if(this.props.calculateKpi.length >0  ){ localData =  this.props.calculateKpi[0].Detail}
         else {localData = this.props.calculateKpi.Detail}
         console.log(localData);
        return (
            <Chart>
                <ChartTitle text={this.props.typeChart} />
                <ChartSeriesDefaults type="column" labels={{ visible: true, format: 'N02' }}/>
                <ChartSeries>
                    <ChartSeriesItem data={ localData} type="column" 
                                     field="Fact" categoryField="Month"/>
                    <ChartSeriesItem data={ localData} type="line"
                                     field="Deviation" categoryField="Month"/>
                    <ChartSeriesItem data={ localData} type="line"
                                     field="Target" categoryField="Month"/>
                    <ChartSeriesItem data={ localData} type="line"
                                     field="CountOrder" categoryField="Month"/>
                </ChartSeries>
            </Chart>
        );
    }
}

function mapStateToProps(state){
    const {calculateKpi} = state;
    return {calculateKpi}
    
}

export default connect(mapStateToProps)(KpiChart);