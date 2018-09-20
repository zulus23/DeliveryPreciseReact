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
         if(this.props.selectCalculateKpi !== null  ){ localData =  this.props.selectCalculateKpi.Detail}
         console.log(localData);
        return (
            <Chart>
                <ChartTitle text={this.props.typeChart} />
                <ChartSeriesDefaults type="column" labels={{ visible: true, format: 'N02' }}/>
                <ChartSeries>
                    <ChartSeriesItem data={ localData} type="column" 
                                     field="Fact" categoryField="Month"/>
                    <ChartSeriesItem data={localData} type="line"
                                     field="Deviation" categoryField="Month"/>
                    <ChartSeriesItem data={localData} type="line"
                                     field="Target" categoryField="Month"/>
                    <ChartSeriesItem data={localData} type="line"
                                     field="CountOrder" categoryField="Month"/>
                </ChartSeries>
            </Chart>
        );
    }
}

function mapStateToProps(state){
    const {selectCalculateKpi} = state;
    return {selectCalculateKpi}
    
}

export default connect(mapStateToProps)(KpiChart);