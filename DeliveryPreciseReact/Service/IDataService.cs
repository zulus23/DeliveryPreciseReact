using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryPreciseReact.Common;
using DeliveryPreciseReact.Domain;

namespace DeliveryPreciseReact.Service
{
    public interface IDataService
    {
        List<string> ListEnterprise();
        List<Customer> ListCustomerByEnterprise(string nameEnterprise);
        List<Customer> ListCustomerByEnterprise(string nameEnterprise,List<string> typeCustomer);
        List<Customer> ListCustomerDeliveryByEnterprise(string nameEnterprise,Customer customer);
        
       //  PreciseDelivery GetPreciseDeliveryByEnterprise(ParamsCalculateKpi paramsCalculateKpi);
        List<PreciseDelivery> CalculateKpi(ParamsCalculateKpi paramsCalculateKpi);

        List<KpiHelper> ListKpis();
        List<KpiHelper> ListKpiSelected(ParamsCalculateKpi paramsCalculateKpi);
        List<KpiHelper> ListKpiByEnterprise(String enterprise);

        Task<List<DeliveryRecord>> GetDeliveryRecordsAsync(ParamsCalculateKpi paramsCalculateKpi);

    }
}