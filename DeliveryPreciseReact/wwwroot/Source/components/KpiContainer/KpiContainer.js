import React, {Component} from 'react';

class KpiContainer extends Component {
    render() {
        return (
            <div className="row" style={{width: '400px'}}>
                <div className="col-4">Наименование</div>
                <div className="col-2">Цель</div>
                <div className="col-2">Факт</div>
                <div className="col-2">Откл</div>
                <div className="col-2">Заказы</div>
            </div>
        );
    }
}

export default KpiContainer;