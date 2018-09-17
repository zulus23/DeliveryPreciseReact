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

export function fetchCustomer(){
    return dispatch => {
        api.fetchCustomers().then(resp => {
            dispatch(customerFetchSucceeded(resp.data))
        })
    }
}


