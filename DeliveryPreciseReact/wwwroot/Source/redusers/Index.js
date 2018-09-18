
const initState = {
    enterprise:[],
    customers:[],
    currentEnterprise:"ГОТЭК",
    kpi:[],
    dateRangeSelected:{start:new Date(),end: new Date()},
    selectKpi:[]
    
    
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
        
        default: {
            return state;
        }
        
    }
    
    
}