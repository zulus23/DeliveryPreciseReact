import React, {Component} from 'react';

class Layout extends Component {
    render() {
        return (
            <div className="container-flued">
                {this.props.children}
                
                <h1>Hello react</h1>
            </div>
        );
    }
}

export default Layout;