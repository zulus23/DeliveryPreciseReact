using System;
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
        
        
        public List<Customer> ListCustomerByEnterprise(string nameEnterprise, string searchValue)
        {
            List<Customer> result = null;
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(nameEnterprise)))
            {
                result = connection.Query<Customer>(string.Format("SELECT  c.cust_num AS code," +
                                                          " RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name FROM dbo.customer c " +
                                                          " JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = c.cust_seq" +
                                                          " join dbo.gtk_cust_kpi_hdr h on ca.cust_num = h.cust_num " +        
                                                          " WHERE ca.cust_seq = 0 AND c.Uf_OrganizLegalForm IS NOT NULL "+
                                                          " AND c.cust_type IS NOT null"+
                                                          " AND RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL  and ca.name like '%{0}%' "+ 
                                                          " ORDER BY name",searchValue)).AsList();
                
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
        
        public PreciseDelivery GetPreciseDeliveryByEnterprise(string nameEnterprise, Customer customer)
        {
            PreciseDelivery result = null;

            String query = string.Format($"select description,month,target,fact,deviation,countorder from ("+
                                          " select max(kpi_description) as description, MONTH(s.DateDostFact) as month,"+ 
                                          " max(t.Kpi_target) as target, avg(s.KPI_stat) as fact," +
                                          " (avg(s.KPI_stat)  - max(t.Kpi_target)) as deviation,count(*) as countorder " +
                                          " from gtk_group_report.dbo.gtk_kpi_ship s " +
                                          " join (select * from dbo.gtk_cust_kpi_lns where " +
                                          " kpi_description = 'Точность поставки по времени, %') as t on t.cust_num = s.cust_num " +
                                          " where s.cust_num = {0} and s.DateDostFact between {1} and {2} " +
                                          " and s.cust_seq = {3} group by MONTH(s.DateDostFact) " +
                                          " union all " +
                                          " select max(kpi_description) as description,'-1' as month, max(t.Kpi_target) as target," +
                                          " avg(s.KPI_stat) as fact,(avg(s.KPI_stat)  - max(t.Kpi_target)) as deviation, count(*) as countorder" +
                                          " from gtk_group_report.dbo.gtk_kpi_ship s join (select * from dbo.gtk_cust_kpi_lns " +
                                          " where kpi_description = 'Точность поставки по времени, %') as t on t.cust_num = s.cust_num" +
                                          " where s.cust_num = {0} and s.DateDostFact between {1} and {2} and s.cust_seq = {3}) as t" +
                                          " order by month",1,2,3,4);
            
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(nameEnterprise)))
            {
                
            }

            return result;
        }
        
    }
}