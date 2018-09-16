import React, {Component} from 'react';
import ReactDom from 'react-dom'

class First extends Component {
    render() {
        return (
            <div>
                <h1>Hello React</h1>
            </div>
        );
    }
}

ReactDom.render(<First/>,document.getElementById("root"))