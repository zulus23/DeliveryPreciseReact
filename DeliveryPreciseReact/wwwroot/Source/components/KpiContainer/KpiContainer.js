import React, {Component} from 'react';
import Kpi from "./Kpi";

class KpiContainer extends Component {


    constructor(props) {
        super(props)
        
    }

    render() {
        return (
            <div className="col-xl-12">
                <div className="row" >
                    <div className="col-sm-1">Д</div>
                    <div className="col-sm-7">Наименование KPI</div>
                    <div className="col-sm-1">Цель</div>
                    <div className="col-sm-1">Факт</div>
                    <div className="col-sm-1">Отл.</div>
                    <div className="col-sm-1">Заказы</div>
                    <Kpi/>
                    <div className="spacer5"/>
                    {this.props.listKpi.map((kpi,index) => (
                        <Kpi key={index} name={kpi.Name}/>      
                    ))}
                    
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    
}

export default KpiContainer;