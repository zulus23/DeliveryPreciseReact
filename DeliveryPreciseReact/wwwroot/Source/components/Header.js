import React from 'react';
import Auxiliary from "../hoc/Auxiliary";

const Header = (props) => {
    return (
        <Auxiliary>
            <nav className="navbar navbar-light bg-light">
                <span className="navbar-brand mb-0 h1">Navbar</span>
            </nav>
            
        </Auxiliary>
    );
};

export default Header;