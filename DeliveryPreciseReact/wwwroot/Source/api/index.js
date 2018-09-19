import  axios from 'axios'

const  client = axios.create();



export function fetchEnterprise(){
    return client.get('/api/utils/enterprise')
}
export function fetchCustomers(data){
    return client.post('/api/utils/customers',data)
}
export function fetchKpis(){
    return client.get('/api/utils/kpis1')
}

export function calculateKpi(data){
    return client.post('/api/utils/calculatekpi',data);
}


