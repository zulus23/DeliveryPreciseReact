import  axios from 'axios'

const  client = axios.create();



export function fetchEnterprise(){
    return client.get('/api/utils/enterprise')
}
export function fetchCustomers(){
    return client.get('/api/utils/customers')
}

