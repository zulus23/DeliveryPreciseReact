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
            List<Customer> customers = impl.ListCustomerByEnterprise("ГОТЭК");
            
            Assert.True(customers.Count > 0);
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