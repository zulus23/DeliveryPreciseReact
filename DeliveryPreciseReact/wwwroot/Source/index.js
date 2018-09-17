import React, {Component} from 'react';
import ReactDom from 'react-dom'
import {createStore, applyMiddleware} from 'redux'
import {Provider} from 'react-redux'
import thunk from 'redux-thunk'
import appReduce from './redusers'
import App from "./components/app/App";
import '../../node_modules/bootstrap/dist/css/bootstrap.css'
import '@progress/kendo-theme-bootstrap'
import '../css/main.css'

const store  = createStore(appReduce,applyMiddleware(thunk)/*,window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()*/);



ReactDom.render(<Provider store={store}><App/></Provider>,document.getElementById("root"));