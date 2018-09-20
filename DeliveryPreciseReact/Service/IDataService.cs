using System.Collections.Generic;
using DeliveryPreciseReact.Domain;

namespace DeliveryPreciseReact.Service
{
    public interface IDataService
    {
        List<Customer> ListCustomerByEnterprise(string nameEnterprise,string searchValue);
        PreciseDelivery GetPreciseDeliveryByEnterprise(string nameEnterprise, Customer customer);
    }
}