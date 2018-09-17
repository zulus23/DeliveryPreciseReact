import React, {Component} from 'react';
import Enterprise from "./Enterprise";

class Layout extends Component {
    
    render() {
        console.log(this.props);
        return (
            <div className="container-flued">
                {this.props.children}
                
                <Enterprise data={this.props.enterprise}/>
            </div>
        );
    }
}

export default Layout;