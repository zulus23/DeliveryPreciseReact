import  axios from 'axios'

const  client = axios.create();



export function fetchEnterprise(){
    return client.get('/api/utils/enterprise')
}
export function fetchCustomers(enterprise){
    return client.get('/api/utils/customers',{params:{enterprise:enterprise}})
}
export function fetchKpis(){
    return client.get('/api/utils/kpis')
}

export function calculateKpi(data){
    return client.post('/api/utils/calculatekpi',data);
}


