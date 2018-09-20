import React, {Component} from 'react';
import { Grid, GridColumn as Column } from '@progress/kendo-react-grid';
import {connect} from "react-redux";

class KpiResult extends Component {


    constructor(props) {
        super(props)
    }

/*
    lastSelectedIndex = 0;
    state = {
        data: products.map(dataItem => Object.assign({ selected: false }, dataItem))
    }

    selectionChange = (event) => {
        event.dataItem.selected = !event.dataItem.selected;
        this.forceUpdate();
    }

    rowClick = (event) => {
        let last = this.lastSelectedIndex;
        const current = this.state.data.findIndex(dataItem => dataItem === event.dataItem);

        if (!event.nativeEvent.shiftKey) {
            this.lastSelectedIndex = last = current;
        }

        if (!event.nativeEvent.ctrlKey) {
            this.state.data.forEach(item => item.selected = false);
        }
        const select = !event.dataItem.selected;
        for (let i = Math.min(last, current); i <= Math.max(last, current); i++) {
            this.state.data[i].selected = select;
        }
        this.forceUpdate();
    }

*/






    render() {
        return (
            <div  onMouseDown={e => e.preventDefault() /* prevents browser text selection */}>
                <Grid
                    data={this.props.kpi}
                    selectedField="selected"
                    
                    /*onSelectionChange={this.selectionChange}
                    onHeaderSelectionChange={this.headerSelectionChange}
                    onRowClick={this.rowClick}*/
                >
                    <Column
                        field="selected"
                        width="50px"
                        title= "Д" 
                        /*headerSelectionValue={
                            this.state.data.findIndex(dataItem => dataItem.selected === false) === -1
                        }*/ />
                    <Column field="Name" title="Наименование KPI" width="500px" />
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
    return {
        kpi: state.selectKpi
    }
}

export default connect(mapStateToProps)(KpiResult);