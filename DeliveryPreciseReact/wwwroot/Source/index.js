import React, {Component} from 'react';
import ReactDom from 'react-dom'
import {createStore, applyMiddleware} from 'redux'
import {Provider} from 'react-redux'
import thunk from 'redux-thunk'
import appReduce from './redusers'


import ReactDOM from 'react-dom';

/* CLDR Data */

import likelySubtags from 'cldr-core/supplemental/likelySubtags.json';
import currencyData from 'cldr-core/supplemental/currencyData.json';
import weekData from 'cldr-core/supplemental/weekData.json';


import usNumbers from 'cldr-numbers-full/main/en/numbers.json';
import usLocalCurrency from 'cldr-numbers-full/main/en/currencies.json';
import usCaGregorian from 'cldr-dates-full/main/en/ca-gregorian.json';
import usDateFields from'cldr-dates-full/main/en/dateFields.json';


import ruNumbers from 'cldr-numbers-full/main/ru/numbers.json';
import ruLocalCurrency from 'cldr-numbers-full/main/ru/currencies.json';
import ruCaGregorian from 'cldr-dates-full/main/ru/ca-gregorian.json';
import ruDateFields from'cldr-dates-full/main/ru/dateFields.json';




import { IntlProvider, load } from '@progress/kendo-react-intl';
load(
    likelySubtags,
    currencyData,
    weekData,
    usNumbers,
    usLocalCurrency,
    usCaGregorian,
    usDateFields,
    ruNumbers,
    ruLocalCurrency,
    ruCaGregorian,
    ruDateFields
);



import App from "./components/app/App";
import '../../node_modules/bootstrap/dist/css/bootstrap.css'
import '@progress/kendo-theme-bootstrap'
import '../css/main.css'








const store  = createStore(appReduce,applyMiddleware(thunk)/*,window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()*/);



ReactDom.render(<Provider store={store}><IntlProvider locale='ru'><App/></IntlProvider></Provider>,document.getElementById("root"));