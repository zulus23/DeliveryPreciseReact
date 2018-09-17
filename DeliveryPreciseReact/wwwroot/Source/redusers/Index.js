
const initState = {
    enterprise:[],
    customers:[]
    
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
        default: {
            return state;
        }
        
    }
    
    
}