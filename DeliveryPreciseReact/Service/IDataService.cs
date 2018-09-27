using System.Collections.Generic;
using DeliveryPreciseReact.Common;
using DeliveryPreciseReact.Domain;

namespace DeliveryPreciseReact.Service
{
    public interface IDataService
    {
        List<Customer> ListCustomerByEnterprise(string nameEnterprise);
        List<Customer> ListCustomerByEnterprise(string nameEnterprise,List<string> typeCustomer);
        List<Customer> ListCustomerDeliveryByEnterprise(string nameEnterprise,Customer customer);
        
       //  PreciseDelivery GetPreciseDeliveryByEnterprise(ParamsCalculateKpi paramsCalculateKpi);
        List<PreciseDelivery> CalculateKpi(ParamsCalculateKpi paramsCalculateKpi);

        List<Kpi> ListKpis();

    }
}