import {setup} from "../constants/types";


const initState = {
    enterprise:[],
    customers:[],
    currentEnterprise:"ГОТЭК",
    kpi:[],
    dateRangeSelected:{start:new Date(),end: new Date()},
    selectKpi:[],
    isSKChecked:false,
    isSPChecked:false,
    isPRChecked:false,
    searchingCustomer:{},
    error:"",
    calculateKpi:[],
    selectCalculateKpi:null,
    selectedKpiDescription:null,
    customerDelivery:[],
    searchingCustomerDelivery:{},
    isLoading:false,
    isDriverReport:false,
    isKPIReport:false,
    isReduceReport:false,
    
};

export default function appReduce(state=initState,action){
    
    switch (action.type) {
        
        case setup.FETCH_DATA_STARTED :{
            return {
                ...state,
                isLoading: true
            }
        }
        
        case setup.ENTERPRISE_FETCH_SUCCEEDED:{
            return {
                ...state,
                enterprise: action.payload.data
            }
        }
        case setup.CUSTOMER_FETCH_SUCCEEDED:{
            return {
                ...state,
                customers:action.payload.data,
                isLoading:false,
            }
        }
        case setup.CUSTOMER_DELIVERY_FETCH_SUCCEEDED :{
            return {
                ...state,
                customerDelivery:action.payload.data,
                isLoading:false,
                
            }
        }
        case setup.ENTERPRISE_CHANGE_SUCCEEDED:{
            
            return {
                ...state,
                currentEnterprise:action.payload.data
            }
        }
        case setup.KPI_FETCH_SUCCEEDED: {
            return {
                ...state,
                kpi: action.payload.data
            }
        }
        case setup.RANGE_DATE_SELECT_SUCCEEDED:{
            return {
                ...state,
                dateRangeSelected:action.payload.data
            }
        }
        case setup.SELECT_KPI_SUCCEEDED : {
            return {
                ...state,
                selectKpi: action.payload.data
            }
        }
        case setup.UPDATE_TYPE_CUSTOMER_SUCCEEDED: {
            
            switch (action.payload.data) {
                case 'СК' :{
                    return {
                        ... state,
                        isSKChecked: !state.isSKChecked
                    }
                }
                case 'СП' :{
                    return {
                        ... state,
                        isSPChecked: !state.isSPChecked
                    }
                }
                case 'ПР' :{
                    return {
                        ... state,
                        isPRChecked: !state.isPRChecked
                    }
                }
            }
        }
        case setup.UPDATE_SEARCH_VALUE_CUSTOMER : {
            return {
                ...state,
                searchingCustomer: action.payload.data
            }
        }
        case setup.UPDATE_SEARCH_VALUE_CUSTOMER_DELIVERY :{
            return {
                ...state,
                searchingCustomerDelivery: action.payload.data
            }
        }
        case setup.FETCH_LOAD_DATE_FAILED :{
            
            return {
                ...state,
                error:action.payload.error,
                isLoading:false,
            }
        }
        case setup.CALCULATE_KPI_SUCCEEDED :{
            return {
                ...state,
                calculateKpi: action.payload.data,
                isLoading:false,
            }
        }
        case setup.CHANGE_SELECT_CALCULATE_KPI : {
            return {
                ...state,
                selectCalculateKpi: action.payload.data,
                selectedKpiDescription: action.payload.data.Description
            }
        }
        case setup.UPDATE_TYPE_REPORT_SUCCEEDED: {
            
            switch (action.payload.data) {
                case 'Движение заказов' :{
                    return {
                        ... state,
                        isDriverReport: !state.isDriverReport
                    }
                }
                case 'KPI' :{
                    return {
                        ... state,
                        isKPIReport: !state.isKPIReport
                    }
                }
                case 'Сводный' :{
                    return {
                        ... state,
                        isReduceReport: !state.isReduceReport
                    }
                }
            }
        }
        case setup.CREATE_REPORT_SUCCEEDED : {
            return {
                ...state,
                isLoading:false,
            }
        }
        
        
        default: {
            return state;
        }
        
    }
    
    
}