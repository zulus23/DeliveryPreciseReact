using System;
using System.Collections.Generic;
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
            PreciseDelivery delivery = impl.GetPreciseDeliveryByEnterprise("ГОТЭК",customer);
            
            Assert.True(delivery != null);
        }
        
    }
}