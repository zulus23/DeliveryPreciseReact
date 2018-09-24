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
        /* TODO второй показатель
         * select MONTH(s.DateWHSFact), avg(KPI_whse),count(*) from gtk_group_report.dbo. gtk_kpi_ship s where cust_num = 'K009154' and DateWHSFact is not null
           group by MONTH(DateWHSFact)

         */
        
        
        
        public List<Customer> ListCustomerByEnterprise(string nameEnterprise)
        {
            List<Customer> result = null;
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(nameEnterprise)))
            {
                result = connection.Query<Customer>(string.Format("SELECT  c.cust_num AS code," +
                                                          " RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name , c.cust_seq as seq FROM dbo.customer c " +
                                                          " JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = c.cust_seq" +
                                                          " join dbo.gtk_cust_kpi_hdr h on ca.cust_num = h.cust_num " +        
                                                          " WHERE ca.cust_seq = 0 "+
                                                          " AND RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL"  + 
                                                          " ORDER BY name")).AsList();
                
            }

            return result;
        }

        

        public List<Customer> ListCustomerByEnterprise(string nameEnterprise,List<string> typeCustomer )
        {
            List<Customer> result = null;
            string typeIn = CreateCustomerTypeInSection(typeCustomer);
           

            string _query = string.Format(" SELECT code, name,seq, type,sortcode FROM (" +
                                          " SELECT 'К000001' AS code,'Все' AS name,  0 as seq, '--' AS type ,0 AS sortcode " +
                                          " UNION all " +
                                          " SELECT  c.cust_num AS code," +
                                          " RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name, c.cust_seq as seq, " +
                                          " CASE WHEN uf_strategcust = '1'  THEN 'СК' " +
                                          "      WHEN uf_strategprospect = '1' THEN 'СП' " +
                                          "      WHEN (uf_strategcust IS NULL AND uf_strategprospect IS NULL)  THEN 'ПР' " +
                                          "      ELSE 'ПР' END AS type,1 AS sortcode " +
                                          " FROM dbo.customer c " +
                                          " JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = c.cust_seq" +
                                          " join dbo.gtk_cust_kpi_hdr h on ca.cust_num = h.cust_num " +
                                          " WHERE ca.cust_seq = 0 " +
                                          " AND RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL ) as customer  " +
                                          "  where 1 = 1  {0}" +
                                          "ORDER BY customer.sortcode,customer.name", typeIn); 
            
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(nameEnterprise)))
                
            {
                result = connection.Query<Customer>(_query).AsList();
            }

            return result;
        }

        public List<Customer> ListCustomerDeliveryByEnterprise(string nameEnterprise, Customer customer)
        {
            List<Customer> result = null;
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(nameEnterprise)))
            {
                result = connection.Query<Customer>(string.Format(" SELECT code, name,seq, type,sortcode,address FROM (" +
                                                                  " SELECT 'К000001' AS code,'Все' AS name,  0 as seq, '--' AS type ,0 AS sortcode, '' as address" +
                                                                  " UNION all " +
                                                                  " SELECT  c.cust_num AS code," +
                                                                  " RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name, c.cust_seq as seq, " +
                                                                  " '==' as  type,1 AS sortcode, dbo.GTKFormatAddress(c.cust_num,c.cust_seq,'custaddr') as address " +
                                                                  " FROM dbo.customer c " +
                                                                  " JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = c.cust_seq" +
                                                                  " WHERE ca.cust_seq <> 0 " +
                                                                  " AND RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL and c.cust_num = '{0}' ) as customer  " +
                                                                  "  where 1 = 1 " +
                                                                  "ORDER BY customer.sortcode,customer.name", customer.Code)).AsList();
                
            }

            return result;
        }

        private static string CreateCustomerTypeInSection(List<string> typeCustomer)
        {
            string typeIn = "";
            if (typeCustomer.Count > 0)
            {
                typeIn = typeCustomer.Aggregate(" and  customer.type  IN (", (i, a) => i = i + "'" + a + "'" + ",",
                    e => e + "'--','' )").Replace(",''", "");
            }

            return typeIn;
        }

        private static string CreateInSectionForAllCustomer(Customer customer,List<string> typeCustomer)
        {
            string _insertCodeCustomer = "";
            if (!customer.Name.Equals("Все"))
            {
                _insertCodeCustomer = $" AND customer.code  = '{customer.Code}'";
            }
            
                
            string typeIn = CreateCustomerTypeInSection(typeCustomer);
            string _query = string.Format(" in ( SELECT code FROM (" +
                                          
                                          " SELECT  c.cust_num AS code," +
                                          " RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name, " +
                                          " CASE WHEN uf_strategcust = '1'  THEN 'СК' " +
                                          "      WHEN uf_strategprospect = '1' THEN 'СП' " +
                                          "      WHEN (uf_strategcust IS NULL AND uf_strategprospect IS NULL)  THEN 'ПР' " +
                                          "      ELSE 'ПР' END AS type,1 AS sortcode " +
                                          " FROM dbo.customer c " +
                                          " JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = c.cust_seq" +
                                          " join dbo.gtk_cust_kpi_hdr h on ca.cust_num = h.cust_num " +
                                          " WHERE ca.cust_seq = 0 " +
                                          " AND RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL ) as customer  " +
                                          "  where 1 = 1  {0} {1})" , typeIn,_insertCodeCustomer);
            return _query;
        }
        
        
        
        private PreciseDelivery GetPreciseDeliveryByEnterprise(ParamsCalculateKpi paramsCalculateKpi)
        {
            string _selectCustomer =
                CreateInSectionForAllCustomer(paramsCalculateKpi.Customer, paramsCalculateKpi.TypeCustomer); 
            
            PreciseDelivery result = null;

            String query = string.Format($"select description,month,year,target,fact,deviation,countorder from ("+
                                          " select max(kpi_description) as description, MONTH(s.DateDostFact) as month, YEAR(s.DateDostFact) as year,"+ 
                                          " max(t.Kpi_target) as target, avg(s.KPI_stat) as fact," +
                                          " (avg(s.KPI_stat)  - max(t.Kpi_target)) as deviation,count(*) as countorder " +
                                          " from gtk_group_report.dbo.gtk_kpi_ship s " +
                                          " join (select * from dbo.gtk_cust_kpi_lns where " +
                                          " kpi_description = '{4}') as t on t.cust_num = s.cust_num " +
                                          " where s.cust_num  {0} and s.DateDostFact between '{1}' and '{2}' " +
                                          " and s.cust_seq = {3} group by MONTH(s.DateDostFact), YEAR(s.DateDostFact) " +
                                          " union all " +
                                          " select max(kpi_description) as description,'-1' as month, MAX(YEAR(s.DateDostFact)) as year, max(t.Kpi_target) as target," +
                                          " avg(s.KPI_stat) as fact,(avg(s.KPI_stat)  - max(t.Kpi_target)) as deviation, count(*) as countorder" +
                                          " from gtk_group_report.dbo.gtk_kpi_ship s join (select * from dbo.gtk_cust_kpi_lns " +
                                          " where kpi_description = '{4}') as t on t.cust_num = s.cust_num" +
                                          " where s.cust_num  {0} and s.DateDostFact between '{1}' and '{2}' and s.cust_seq = {3}) as t" +
                                          " order by month",_selectCustomer,
                                            paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                                            paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"),0,"Точность поставки по времени, %");
            
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(paramsCalculateKpi.Enterprise)))
            {
                List<PreciseDelivery> _all = connection.Query<PreciseDelivery>(query).AsList();
                result = _all?.Find(item => item.Month == -1);
                result?.Detail?.AddRange(_all.FindAll(e => e.Month != -1));
                //result = connection.QueryFirst<PreciseDelivery>(query);
            }

            return result;
        }

        public List<PreciseDelivery> CalculateKpi(ParamsCalculateKpi paramsCalculateKpi)
        {
            List<PreciseDelivery> _all = new List<PreciseDelivery>();

            paramsCalculateKpi.SelectKpi.ForEach(kpi =>
            {
                if (kpi.Name.Contains("Точность поставки по времени, %"))
                {
                    _all.Add(GetPreciseDeliveryByEnterprise(paramsCalculateKpi));
                }

                if (kpi.Name.Contains("Точность выхода на склад %"))
                {
                    _all.Add(GetPreciseEnterToWhseByEnterprise(paramsCalculateKpi));
                }
                {
                    
                }
            });

            return _all;
        }

        /*select MONTH(s.DateWHSFact), avg(KPI_whse),count(*) from gtk_group_report.dbo. gtk_kpi_ship s where cust_num = 'K009154' and DateWHSFact is not null
        group by MONTH(DateWHSFact)*/
        
        private PreciseDelivery GetPreciseEnterToWhseByEnterprise(ParamsCalculateKpi paramsCalculateKpi)
        {
            string _selectCustomer =
                CreateInSectionForAllCustomer(paramsCalculateKpi.Customer, paramsCalculateKpi.TypeCustomer); 
            
            PreciseDelivery result = null;

            String query = string.Format($"select description,month,year,target,fact,deviation,countorder from ("+
                                          " select max(kpi_description) as description, MONTH(s.DateWHSFact) as month,YEAR(s.DateWHSFact) as year,"+ 
                                          " max(t.Kpi_target) as target, avg(s.KPI_whse) as fact," +
                                          " (avg(s.KPI_whse)  - max(t.Kpi_target)) as deviation,count(*) as countorder " +
                                          " from gtk_group_report.dbo.gtk_kpi_ship s " +
                                          " join (select * from dbo.gtk_cust_kpi_lns where " +
                                          " kpi_description = '{4}') as t on t.cust_num = s.cust_num " +
                                          " where s.cust_num  {0} and s.DateWHSFact between '{1}' and '{2}' " +
                                          " and s.cust_seq = {3} group by MONTH(s.DateWHSFact),YEAR(s.DateWHSFact) " +
                                          " union all " +
                                          " select max(kpi_description) as description,'-1' as month,MAX(YEAR(s.DateWHSFact)) as year, max(t.Kpi_target) as target," +
                                          " avg(s.KPI_whse) as fact,(avg(s.KPI_whse)  - max(t.Kpi_target)) as deviation, count(*) as countorder" +
                                          " from gtk_group_report.dbo.gtk_kpi_ship s join (select * from dbo.gtk_cust_kpi_lns " +
                                          " where kpi_description = '{4}') as t on t.cust_num = s.cust_num" +
                                          " where s.cust_num  {0} and s.DateWHSFact between '{1}' and '{2}' and s.cust_seq = {3}) as t" +
                                          " order by month",_selectCustomer,
                                            paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                                            paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"),0,"Точность выхода на склад %");
            
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(paramsCalculateKpi.Enterprise)))
            {
                List<PreciseDelivery> _all = connection.Query<PreciseDelivery>(query).AsList();
                result = _all?.Find(item => item.Month == -1);
                result?.Detail?.AddRange(_all.FindAll(e => e.Month != -1));
                //result = connection.QueryFirst<PreciseDelivery>(query);
            }

            return result;
        }

        private string CreateInSectionForCust_Seq(ParamsCalculateKpi paramsCalculateKpi)
        {
            string result = "";
            if (!paramsCalculateKpi.CustomerDelivery.Name.Equals("Все"))
            {
                
            }

            return result;
        }
        
    }
}