import React, {Component} from 'react';
import ReactDom from 'react-dom'
import {createStore, applyMiddleware} from 'redux'
import {Provider} from 'react-redux'
import appReduce from './redusers'
import App from "./components/app/App";
import '../../node_modules/bootstrap/dist/css/bootstrap.css'

const store  = createStore(appReduce);



ReactDom.render(<Provider store={store}><App/></Provider>,document.getElementById("root"))