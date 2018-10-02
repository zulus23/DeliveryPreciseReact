import * as api from "../api";
import React from "react";
import {toast} from "react-toastify";
import FileSaver from 'file-saver'
import {setup} from "../constants/types";

var uuid = require("uuid");

function loadDataFailed(error) {
    return {
        type:setup.FETCH_LOAD_DATE_FAILED,
        payload: {
            error
        }
        
    }
}

function fetchDataStarted(){
    return {
        type:setup.FETCH_DATA_STARTED,
        
    }
}
function createReportSucceeded(){
    return {
        type:setup.CREATE_REPORT_SUCCEEDED,

    }
}



function enterpriseFetchSucceeded(data) {
    return {
        type:setup.ENTERPRISE_FETCH_SUCCEEDED,
        payload: {
            data
        }
        
    }
    
}

export function fetchEnterprise(){
    return (dispatch,getState) => {
        api.fetchEnterprise().then(resp =>{
            dispatch(enterpriseFetchSucceeded(resp.data))
        }).catch(error => {
            dispatch(loadDataFailed("Ошибка при загрузки списка предприятий: "+error.message));
            toast.error(getState().error, {position: toast.POSITION.TOP_RIGHT});
        })
    }
} 

function customerFetchSucceeded(data){
    return{
        type: setup.CUSTOMER_FETCH_SUCCEEDED,
        payload: {
            data
        }
    }
}

export function fetchCustomer(data){
    return (dispatch,getState) => {
        dispatch(fetchDataStarted());  
        api.fetchCustomers(data).then(resp => {
            dispatch(customerFetchSucceeded(resp.data));
            dispatch(updateSearchValueCustomer({Code:'К000001',
                Name:'Все',
                Seq:0,
                Address:'',
                FullName:'Все'}));
        }).catch(error => {
            dispatch(loadDataFailed("Ошибки при загруки клиентов : "+error.message));
            toast.error(getState().error, {position: toast.POSITION.TOP_RIGHT});
        })
    }
}
function customerDeliveryFetchSucceeded(data){
    return{
        type: setup.CUSTOMER_DELIVERY_FETCH_SUCCEEDED,
        payload: {
            data
        }
    }
}

export function fetchCustomerDelivery(data){
    return (dispatch, getState) => {
        dispatch(fetchDataStarted());
        api.fetchCustomerDelivery(data).then(resp => {
            dispatch(customerDeliveryFetchSucceeded(resp.data));
            dispatch(updateSearchValueCustomerDelivery({
                Code:'К000001',
                Name:'Все',
                Seq: 0,
                Address:'',
                FullName:'Все'
            }))
        }).catch(error => {
            dispatch(loadDataFailed("Ошибки при загруки грузополучателей : "+error.message))
            toast.error(getState().error, {position: toast.POSITION.TOP_RIGHT});
        })
    }
}




function enterpriseChangeSucceeded(data) {
    return {
        type:setup.ENTERPRISE_CHANGE_SUCCEEDED,
        payload: {
            data
        }

    }

}
export function changeEnterprise(selectedEnterprise){
    return (dispatch,getState) => {
      
        dispatch(enterpriseChangeSucceeded(selectedEnterprise));
        const {isPRChecked, isSKChecked, isSPChecked,currentEnterprise} = getState();
        const dataSelect = {
            enterprise: currentEnterprise,
            typeCustomer: createTypeCustomer(isPRChecked, isSKChecked, isSPChecked),

        };
        dispatch(fetchCustomer(dataSelect))
        
    }
}

function kpiFetchSucceeded(data){
    return{
        type: setup.KPI_FETCH_SUCCEEDED,
        payload: {
            data
        }
    }
}

export function fetchKpi(){
    return (dispatch,getState) => {
        api.fetchKpis().then(resp => {
            dispatch(kpiFetchSucceeded(resp.data))
        }).catch(error => {
            dispatch(loadDataFailed("Ошибка при загрузки KPI:  "+ error.message));
            toast.error(getState().error, {position: toast.POSITION.TOP_RIGHT});
        })
    }
}


function selectRangeDateSucceeded(data){
    return{
        type: setup.RANGE_DATE_SELECT_SUCCEEDED,
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
        type: setup.SELECT_KPI_SUCCEEDED,
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
        type: setup.CALCULATE_KPI_SUCCEEDED,
        payload: {
            data
        }
    }
}
export function calculateSelectKpi(data){
    return (dispatch,getState) => {
        dispatch(fetchDataStarted());
        api.calculateKpi(data).then(resp => {
            var d = resp.data.map(item => {
               item.Detail.map(de => {
                   de.Date = new Date(de.Date);
                   return de;
                });
               return item;
            });
            dispatch(calculateKpiSucceeded(d));
            dispatch(updateSelectCalculateKpi({}));
        }).catch(error => {
            dispatch(loadDataFailed("При выполнении расчета произошла ошибка: "+error.message));
            toast.error(getState().error, {position: toast.POSITION.TOP_RIGHT});
        })
    }
}

export function createReportByKpi(data) {
    return (dispatch, getState) => {
        dispatch(fetchDataStarted());
        api.createReportByPki(data).then(resp => {

            FileSaver.saveAs(new Blob([resp.data]), uuid.v4()+'.xlsx');
            dispatch(createReportSucceeded());
        }).catch(error => {
            dispatch(loadDataFailed("При формировании отчета произошла ошибка: "+error.message));
            toast.error(getState().error, {position: toast.POSITION.TOP_RIGHT});
        });
    }
}



function updateSelectedTypeCustomerSucceeded(data){
    return {
        type: setup.UPDATE_TYPE_CUSTOMER_SUCCEEDED,
        payload: {
            data
        }
    }
}

function createTypeCustomer(isPRChecked, isSKChecked, isSPChecked) {
    const typeCustomer = [];
    if (isPRChecked === true) {
        typeCustomer.push("ПР");
    }
    if (isSKChecked === true) {
        typeCustomer.push("СК");
    }
    if (isSPChecked === true) {
        typeCustomer.push("СП");
    }
    return typeCustomer;
    
}


export function updateSelectTypeCustomer(data){
    return (dispatch,getState) => {
        dispatch(updateSelectedTypeCustomerSucceeded(data));
        const {isPRChecked, isSKChecked, isSPChecked,currentEnterprise} = getState()
        const dataSelect = {
            enterprise: currentEnterprise,
            typeCustomer: createTypeCustomer(isPRChecked, isSKChecked, isSPChecked),
            
         };
        dispatch(fetchCustomer(dataSelect))
    }
}
function updateSearchValueCustomerSucceeded(data){
    return {
        type: setup.UPDATE_SEARCH_VALUE_CUSTOMER,
        payload: {
            data
        }
    }
}
export function updateSearchValueCustomer(data) {
    return (dispatch,getState)  => {
        dispatch(updateSearchValueCustomerSucceeded(data));
        const {currentEnterprise,searchingCustomer} = getState();
        const dataSelect = {
            enterprise: currentEnterprise,
            customer: searchingCustomer,

        };
        if(searchingCustomer !== null) {
            dispatch(fetchCustomerDelivery(dataSelect));
        }
        
        
        
    }
    
}
function updateSearchValueCustomerDeliverySucceeded(data) {
    return {
        type: setup.UPDATE_SEARCH_VALUE_CUSTOMER_DELIVERY,
        payload: {
            data
        }
    }
}
export function updateSearchValueCustomerDelivery(data) {
    return (dispatch,getState)  => {
        dispatch(updateSearchValueCustomerDeliverySucceeded(data));
        
    }

}



function changeSelectCalculateKpiSucceeded(data) {
    return {
        type : setup.CHANGE_SELECT_CALCULATE_KPI,
        payload : {
            data
        }
    }
}
export  function updateSelectCalculateKpi(data) {
    return dispatch => {
        dispatch(changeSelectCalculateKpiSucceeded(data))
    }
} 



