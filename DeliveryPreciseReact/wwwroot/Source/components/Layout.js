import React, {Component} from 'react';

class Layout extends Component {
    render() {
        return (
            <div className="container-flued">
                {this.props.children}
            </div>
        );
    }
}

export default Layout;