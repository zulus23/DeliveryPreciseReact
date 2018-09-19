import * as api from "../api";

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


