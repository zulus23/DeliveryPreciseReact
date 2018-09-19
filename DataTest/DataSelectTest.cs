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
            List<Customer> customers = impl.ListCustomerByEnterprise("ГОТЭК","");
            Assert.True(customers.Count > 0);
        }
    }
}