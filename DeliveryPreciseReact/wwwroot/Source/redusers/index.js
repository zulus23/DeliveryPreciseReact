
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
    
    
};

export default function appReduce(state=initState,action){
    
    switch (action.type) {
        
        case 'ENTERPRISE_FETCH_SUCCEEDED':{
            return {
                ...state,
                enterprise: action.payload.data
            }
        }
        case 'CUSTOMER_FETCH_SUCCEEDED':{
            return {
                ...state,
                customers:action.payload.data
            }
        }
        case 'ENTERPRISE_CHANGE_SUCCEEDED':{
            return {
                ...state,
                currentEnterprise:action.payload.data
            }
        }
        case 'KPI_FETCH_SUCCEEDED': {
            return {
                ...state,
                kpi: action.payload.data
            }
        }
        case 'RANGE_DATE_SELECT_SUCCEEDED':{
            return {
                ...state,
                dateRangeSelected:action.payload.data
            }
        }
        case 'SELECT_KPI_SUCCEEDED' : {
            return {
                ...state,
                selectKpi: action.payload.data
            }
        }
        case 'UPDATE_TYPE_CUSTOMER_SUCCEEDED': {
            
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
        case 'UPDATE_SEARCH_VALUE_CUSTOMER' : {
            return {
                ...state,
                searchingCustomer: action.payload.data
            }
        }
        case 'FETCH_LOAD_DATE_FAILED' :{
            return {
                ...state,
                error:action.payload.error
            }
        }
        
        
        default: {
            return state;
        }
        
    }
    
    
}