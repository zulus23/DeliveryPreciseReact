using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using DeliveryPreciseReact.Common;
using DeliveryPreciseReact.Domain;

namespace DeliveryPreciseReact.Service
{
    public class MssqlDataServiceImpl :IDataService
    {
        
        
        public List<Customer> ListCustomerByEnterprise(string nameEnterprise)
        {
            List<Customer> result = null;
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(nameEnterprise)))
            {
                result = connection.Query<Customer>("SELECT  c.cust_num AS code," +
                                                          "ca.RUSExtName AS name FROM dbo.customer c " +
                                                          " JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = c.cust_seq" +
                                                          " WHERE ca.cust_seq = 0").AsList();
            }

            return result;
        }
    }
}