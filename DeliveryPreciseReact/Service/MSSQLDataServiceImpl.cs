using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
                                                          " RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name FROM dbo.customer c " +
                                                          " JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = c.cust_seq" +
                                                          " WHERE ca.cust_seq = 0 AND c.Uf_OrganizLegalForm IS NOT NULL "+
                                                          " AND c.Uf_OrganizLegalForm <> 15 AND c.cust_type IS NOT null"+
                                                          " AND RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL  "+ 
                                                          " ORDER BY name").AsList();
                
            }

            return result;
        }
        
        public List<Customer> ListCustomerByEnterprise(string nameEnterprise,List<string> typeCustomer )
        {
            List<Customer> result = null;
            string typeIn =  string.Join(",", typeCustomer.ToArray());
            
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(nameEnterprise)))
            {
                result = connection.Query<Customer>("SELECT  c.cust_num AS code," +
                                                    " COALESCE(ca.RUSExtName,ca.name) as  name FROM dbo.customer c " +
                                                    " JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = c.cust_seq" +
                                                    " WHERE ca.cust_seq = 0 ").AsList();
            }

            return result;
        }
        
    }
}