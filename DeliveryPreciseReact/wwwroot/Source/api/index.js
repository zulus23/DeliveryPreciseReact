import  axios from 'axios'

const  client = axios.create();



export function fetchEnterprise(){
    return client.get('/api/utils/enterprise')
}
export function fetchCustomers(data){
    return client.post('/api/utils/customers',data)
}
export function fetchCustomerDelivery(data){
    return client.post('/api/utils/customerdelivery',data)
}


export function fetchKpis(){
    return client.get('/api/utils/kpis')
}
export function fetchKpisByEnterprise(data){
    
    return client.get('/api/utils/kpisbyenterprise',{
        params:{
            enterprise:data
        }
    })
}



export function calculateKpi(data){
    return client.post('/api/utils/calculatekpi',data);
}

export function createReportByDriveOrder(data){
    return client({
        method: 'post',
        headers: { 'Accept': 'application/vnd.ms-excel' },
        data:data,
        /*responseType: 'stream',*/
        responseType: 'blob',
        url:'/api/utils/reportdriver',
    });
}

export function createReportByPki(data){
    return client({
        method: 'post',
        headers: { 'Accept': 'application/vnd.ms-excel' },
        data:data,
        /*responseType: 'stream',*/
        responseType: 'blob',
        url:'/api/utils/reportkpi',
    });
}
export function createReportByReduce(data){
    return client({
        method: 'post',
        headers: { 'Accept': 'application/vnd.ms-excel' },
        data:data,
        /*responseType: 'stream',*/
        responseType: 'blob',
        url:'/api/utils/reportreduce',
    });
}


