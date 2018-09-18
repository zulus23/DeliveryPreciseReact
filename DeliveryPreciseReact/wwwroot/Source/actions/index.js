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

export function fetchCustomer(enterprise){
    return dispatch => {
        console.log("==================")
        api.fetchCustomers(enterprise).then(resp => {
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
            dispatch(calculateSelectKpi(resp.data))
        })
    }
}




