import React, {Component} from 'react';
import {connect} from 'react-redux'
import {RingLoader} from 'react-spinners'
import Auxiliary from "../hoc/Auxiliary";

class LoaderSpinner extends Component {
    render() {
        let styleName = this.props.isLoading ? 'p-visible' : 'p-no-visible';
        return (
            <Auxiliary>
                
                <div className={styleName}>
                    <div className="p-spin-backdrop"/>
                    <div className="p-spin-center">
                        <RingLoader 
                            size={100}       
                            color={'#d73900'}
                            loading={this.props.isLoading}/>
                    </div>
                </div>
            </Auxiliary>
        );
    }
}

export default LoaderSpinner;