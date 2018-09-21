using System.Collections.Generic;
using DeliveryPreciseReact.Common;
using DeliveryPreciseReact.Domain;

namespace DeliveryPreciseReact.Service
{
    public interface IDataService
    {
        List<Customer> ListCustomerByEnterprise(string nameEnterprise);
        List<Customer> ListCustomerByEnterprise(string nameEnterprise,List<string> typeCustomer);
        PreciseDelivery GetPreciseDeliveryByEnterprise(ParamsCalculateKpi paramsCalculateKpi);
    }
}