import * as api from "../api";
import React from "react";


function loadDataFailed(error) {
    return {
        type:'FETCH_LOAD_DATE_FAILED',
        payload: {
            error
        }
        
    }
}

function enterpriseFetchSucceeded(data) {
    return {
        type:'ENTERPRISE_FETCH_SUCCEEDED',
        payload: {
            data
        }
        
    }
    
}

export function fetchEnterprise(){
    return dispatch => {
        api.fetchEnterprise().then(resp =>{
            dispatch(enterpriseFetchSucceeded(resp.data))
        }).catch(error => {
            dispatch(loadDataFailed(error.message))
        })
    }
} 

function customerFetchSucceeded(data){
    return{
        type: 'CUSTOMER_FETCH_SUCCEEDED',
        payload: {
            data
        }
    }
}

export function fetchCustomer(data){
    return dispatch => {
        console.log(data);  
        api.fetchCustomers(data).then(resp => {
            dispatch(customerFetchSucceeded(resp.data))
        }).catch(error => {
            dispatch(loadDataFailed(error.message))
        })
    }
}
function enterpriseChangeSucceeded(data) {
    return {
        type:'ENTERPRISE_CHANGE_SUCCEEDED',
        payload: {
            data
        }

    }

}
export function changeEnterprise(selectedEnterprise){
    return dispatch => {
      
        dispatch(enterpriseChangeSucceeded(selectedEnterprise));
    }
}

function kpiFetchSucceeded(data){
    return{
        type: 'KPI_FETCH_SUCCEEDED',
        payload: {
            data
        }
    }
}

export function fetchKpi(){
    return dispatch => {
        api.fetchKpis().then(resp => {
            dispatch(kpiFetchSucceeded(resp.data))
        }).catch(error => {
            dispatch(loadDataFailed("Ошибка при загрузки KPI:  "+ error.message))
        })
    }
}


function selectRangeDateSucceeded(data){
    return{
        type: 'RANGE_DATE_SELECT_SUCCEEDED',
        payload: {
            data
        }
    }
}
export function changeDateInterval(data){
    return dispatch => {
        dispatch(selectRangeDateSucceeded(data));
    }
}
function selectKpiSucceeded(data){
    return{
        type: 'SELECT_KPI_SUCCEEDED',
        payload: {
            data
        }
    }
}
export function changeSelectKpi(data){
    return dispatch => {
        dispatch(selectKpiSucceeded(data));
    }
}

function calculateKpiSucceeded(data){
    return{
        type: 'CALCULATE_KPI_SUCCEEDED',
        payload: {
            data
        }
    }
}
export function calculateSelectKpi(data){
    return dispatch => {
        api.calculateKpi(data).then(resp => {
            dispatch(calculateKpiSucceeded(resp.data))
        }).catch(error => {
            dispatch(loadDataFailed(error.message))
        })
    }
}

function updateSelectedTypeCustomerSucceeded(data){
    return {
        type:'UPDATE_TYPE_CUSTOMER_SUCCEEDED',
        payload: {
            data
        }
    }
}

export function updateSelectTypeCustomer(data){
    return dispatch => {
        dispatch(updateSelectedTypeCustomerSucceeded(data));
    }
}
function updateSearchValueCustomerSucceeded(data){
    return {
        type:'UPDATE_SEARCH_VALUE_CUSTOMER',
        payload: {
            data
        }
    }
}
export function updateSearchValueCustomer(data) {
    return dispatch => {
        dispatch(updateSearchValueCustomerSucceeded(data))
    }
    
}





