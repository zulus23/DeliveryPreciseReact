using System;
using System.Collections.Generic;
using DeliveryPreciseReact.Common;
using DeliveryPreciseReact.Domain;
using DeliveryPreciseReact.Service;
using Xunit;

namespace DataTest
{
    public class DataSelectTest
    {
        [Fact]
        public void ListCustomersMustBeFill()
        {
            MssqlDataServiceImpl impl = new MssqlDataServiceImpl();
            List<string> list = new List<string>();
            list.Add("СК");
            List<Customer> customers = impl.ListCustomerByEnterprise("ГОТЭК",list);
            
            Assert.True(customers.Count == 35 + 1);
        }
        
        [Fact]
        public void PreciseDeliveryForClient()
        {
            MssqlDataServiceImpl impl = new MssqlDataServiceImpl();
            Customer customer = new Customer();
            customer.Code = "K009154";
           // PreciseDelivery delivery = impl.GetPreciseDeliveryByEnterprise("ГОТЭК",customer);
            
            //Assert.True(delivery != null);
        }
        [Fact]
        public void ListCustomerDelivery()
        {
            MssqlDataServiceImpl impl = new MssqlDataServiceImpl();
            Customer customer = new Customer();
            customer.Code = "K012054";
            // PreciseDelivery delivery = impl.GetPreciseDeliveryByEnterprise("ГОТЭК",customer);
            List<Customer> customers = impl.ListCustomerDeliveryByEnterprise("ПРИНТ", customer);

            Assert.True(customers.Count  == 2);
        }
        [Fact]
        public void ListKpiByCustomer()
        {
            MssqlDataServiceImpl impl = new MssqlDataServiceImpl();
            Customer customer = new Customer();
            customer.Code = "K009154";
            customer.Name = "САВУШКИН ПРОДУКТ ОАО";
            customer.Seq = 0;
            
            DateRange dateRange = new DateRange();
            dateRange.Start = DateTime.Parse("2018-07-01");
            dateRange.End = DateTime.Parse("2018-09-30");
            List<KpiHelper> helpers = new List<KpiHelper>();
            helpers.Add(new KpiHelper(KpiConst.PRECISEDELIVERYBYAMOUNT));
            helpers.Add(new KpiHelper(KpiConst.PRECISEENTERSTORAGE));
            
            ParamsCalculateKpi paramsCalculateKpi = new ParamsCalculateKpi();
            paramsCalculateKpi.Enterprise = "ГОТЭК";
            paramsCalculateKpi.Customer = customer;
            paramsCalculateKpi.RangeDate = dateRange;
            paramsCalculateKpi.SelectKpi = helpers;
            paramsCalculateKpi.TypeCustomer = new List<string>();
            paramsCalculateKpi.CustomerDelivery = customer;
            
            // PreciseDelivery delivery = impl.GetPreciseDeliveryByEnterprise("ГОТЭК",customer);
            impl.ListKpiByCustomers(paramsCalculateKpi);

            
        }
        
        
    }
}