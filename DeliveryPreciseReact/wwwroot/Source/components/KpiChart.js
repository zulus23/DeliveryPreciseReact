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
import 'hammerjs'






class KpiChart extends Component {
    render(){ 
         let localData;
         let chartDescription; 
         if(this.props.selectCalculateKpi !== null  ){ 
             localData =  this.props.selectCalculateKpi.Detail;
             chartDescription = this.props.selectCalculateKpi.Description;
         }
        
        return (
            <Chart>
                <ChartTitle text={chartDescription} />
                <ChartLegend position="bottom" orientation="horizontal" />
                <ChartSeriesDefaults type="column" labels={{ visible: true, format: 'N02' }}/>
                <ChartCategoryAxis>
                    <ChartCategoryAxisItem baseUnit='months'/>
                </ChartCategoryAxis>    
                <ChartSeries>
                    <ChartSeriesItem data={ localData} type="column" name="Факт" 
                                     field="Fact" categoryField="Date"/>
                    <ChartSeriesItem data={localData} type="line" name="Цель" 
                                     field="Target" categoryField="Date" color="red" visible={chartDescription !== 'Скорость урегулирования претензий, дни'}/>
                    <ChartSeriesItem data={localData} type="line" name="Кол-во заказов"
                                     field="CountOrder" categoryField="Date" visible={chartDescription !== 'Скорость урегулирования претензий, дни'}/>
                    <ChartSeriesItem data={localData} type="line" name="Тренд"
                                     field="Trend" categoryField="Date" labels={{ visible: false}} visible={chartDescription !== 'Скорость урегулирования претензий, дни'}/>
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