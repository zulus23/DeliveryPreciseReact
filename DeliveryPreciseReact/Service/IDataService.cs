using System.Collections.Generic;
using DeliveryPreciseReact.Domain;

namespace DeliveryPreciseReact.Service
{
    public interface IDataService
    {
        List<Customer> ListCustomerByEnterprise(string nameEnterprise);
        List<Customer> ListCustomerByEnterprise(string nameEnterprise,List<string> typeCustomer);
        PreciseDelivery GetPreciseDeliveryByEnterprise(string nameEnterprise, Customer customer);
    }
}