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
                                                                  " WHERE " +
                                                                  "  RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL and c.cust_num = '{0}' ) as customer  " +
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
            string _selectSeqCustomer = CreateInSectionForCust_Seq(paramsCalculateKpi);
            
            PreciseDelivery result = null;

            String query = string.Format($"select description,month,year,target,fact,deviation,countorder from ("+
                                          " select max(kpi_description) as description, MONTH(s.DateDostFact) as month, YEAR(s.DateDostFact) as year,"+ 
                                          " max(t.Kpi_target) as target, avg(s.KPI_stat) as fact," +
                                          " (avg(s.KPI_stat)  - max(t.Kpi_target)) as deviation,count(*) as countorder " +
                                          " from gtk_group_report.dbo.gtk_kpi_ship s " +
                                          " join (select * from dbo.gtk_cust_kpi_lns where " +
                                          " kpi_description = '{4}') as t on t.cust_num = s.cust_num " +
                                          " where s.cust_num  {0} and s.DateDostFact between '{1}' and '{2}' and s.site = '{5}' " +
                                          /*" and s.cust_seq = {3} " +*/
                                          " {3} " +
                                          " group by MONTH(s.DateDostFact), YEAR(s.DateDostFact) " +
                                          " union all " +
                                          " select max(kpi_description) as description,'-1' as month, MAX(YEAR(s.DateDostFact)) as year, max(t.Kpi_target) as target," +
                                          " avg(s.KPI_stat) as fact,(avg(s.KPI_stat)  - max(t.Kpi_target)) as deviation, count(*) as countorder" +
                                          " from gtk_group_report.dbo.gtk_kpi_ship s join (select * from dbo.gtk_cust_kpi_lns " +
                                          " where kpi_description = '{4}') as t on t.cust_num = s.cust_num" +
                                          " where s.cust_num  {0} and s.DateDostFact between '{1}' and '{2}'   and s.site = '{5}'" +
                                          /*" and s.cust_seq = {3}) as t" +*/
                                          " {3}) as t" +
                                          " order by month",_selectCustomer,
                                            paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                                            paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"),_selectSeqCustomer,"Точность поставки по времени, %",
                                            DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise)  
                                         );
            
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(paramsCalculateKpi.Enterprise)))
            {
                List<PreciseDelivery> _all = connection.Query<PreciseDelivery>(query).AsList();
                
                result = _all?.Find(item => item.Month == -1);

                

                result?.Detail?.AddRange(_all.FindAll(e => e.Month != -1));
                if (result?.Detail?.Count > 0)
                {
                    Tuple<double, double> trend = CalculateTrend(result?.Detail);

                    result.Detail.ForEach(e => e.Trend = trend.Item2 + e.Month*trend.Item1);
                }
                //result = connection.QueryFirst<PreciseDelivery>(query);
            }

            return result;
        }

        public List<PreciseDelivery> CalculateKpi(ParamsCalculateKpi paramsCalculateKpi)
        {
            List<PreciseDelivery> _all = new List<PreciseDelivery>();


            if(paramsCalculateKpi.SelectKpi.Any(e=> e.Name.Equals("Все")))
            {
                ListKpis().FindAll(k => !k.Name.Equals("Все")) .ForEach(kpi =>
                {
                    SelectCalculateKpi(paramsCalculateKpi, kpi, _all);
                });    
            }
            else
            {
                paramsCalculateKpi.SelectKpi.FindAll(k => !k.Name.Equals("Все")) .ForEach(kpi =>
                {
                    SelectCalculateKpi(paramsCalculateKpi, kpi, _all);
                });    
            }

            

            return _all;
        }

        private void SelectCalculateKpi(ParamsCalculateKpi paramsCalculateKpi, Kpi kpi, List<PreciseDelivery> _all)
        {
            switch (kpi.Name)
            {
                case "Точность поставки по времени, %":
                {
                    _all.Add(GetPreciseDeliveryByEnterprise(paramsCalculateKpi));
                    break;
                }

                case "Точность выхода на склад %":
                {
                    _all.Add(GetPreciseEnterToWhseByEnterprise(paramsCalculateKpi));
                    break;
                }
                default:
                {
                    _all.Add(GetKPIByName(paramsCalculateKpi, kpi.Name));
                    break;
                }
            }
        }

        public List<Kpi> ListKpis()
        {
            List<Kpi> list = new List<Kpi>();
            list.Add(new Kpi("Все", 0, 0, 0, 0));
            list.Add(new Kpi("Точность поставки по времени, %", 0, 0, 0, 0));
            list.Add(new Kpi("Точность выхода на склад %", 0, 0, 0, 0));
            list.Add(new Kpi("Точность поставки по количеству, %", 0, 0, 0, 0));
            list.Add(new Kpi("Уровень качества продукции, %", 0, 0, 0, 0));
            list.Add(new Kpi("Скорость урегулирования претензий, дни", 0, 0, 0, 0));
            list.Add(new Kpi("Производство тестов, дни", 0, 0, 0, 0));
            list.Add(new Kpi("Производство макетов, дни", 0, 0, 0, 0));
            return list;
        }

        /*select MONTH(s.DateWHSFact), avg(KPI_whse),count(*) from gtk_group_report.dbo. gtk_kpi_ship s where cust_num = 'K009154' and DateWHSFact is not null
        group by MONTH(DateWHSFact)*/
        
        private PreciseDelivery GetPreciseEnterToWhseByEnterprise(ParamsCalculateKpi paramsCalculateKpi)
        {
            string _selectCustomer =
                CreateInSectionForAllCustomer(paramsCalculateKpi.Customer, paramsCalculateKpi.TypeCustomer);
            string _selectSeqCustomer = CreateInSectionForCust_Seq(paramsCalculateKpi);
            
            PreciseDelivery result = null;

            String query = string.Format($"select description,month,year,target,fact,deviation,countorder from ("+
                                          " select max(kpi_description) as description, MONTH(s.DateWHSFact) as month,YEAR(s.DateWHSFact) as year,"+ 
                                          " max(t.Kpi_target) as target, avg(s.KPI_whse) as fact," +
                                          " (avg(s.KPI_whse)  - max(t.Kpi_target)) as deviation,count(*) as countorder " +
                                          " from gtk_group_report.dbo.gtk_kpi_ship s " +
                                          " join (select * from dbo.gtk_cust_kpi_lns where " +
                                          " kpi_description = '{4}') as t on t.cust_num = s.cust_num " +
                                          " where s.cust_num  {0} and s.DateWHSFact between '{1}' and '{2}'  and s.site = '{5}'  " +
                                          /*" and s.cust_seq = {3} " +*/
                                          " {3} " +
                                          " group by MONTH(s.DateWHSFact),YEAR(s.DateWHSFact) " +
                                          " union all " +
                                          " select max(kpi_description) as description,'-1' as month,MAX(YEAR(s.DateWHSFact)) as year, max(t.Kpi_target) as target," +
                                          " avg(s.KPI_whse) as fact,(avg(s.KPI_whse)  - max(t.Kpi_target)) as deviation, count(*) as countorder" +
                                          " from gtk_group_report.dbo.gtk_kpi_ship s join (select * from dbo.gtk_cust_kpi_lns " +
                                          " where kpi_description = '{4}') as t on t.cust_num = s.cust_num" +
                                          " where s.cust_num  {0} and s.DateWHSFact between '{1}' and '{2}' and s.site = '{5}'  " +
                                          /*" and s.cust_seq = {3}) as t" +*/
                                          " {3}) as t" +
                                          " order by month",_selectCustomer,
                                            paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                                            paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"),_selectSeqCustomer,"Точность выхода на склад %",
                                            DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise)
                                          );
            
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(paramsCalculateKpi.Enterprise)))
            {
                List<PreciseDelivery> _all = connection.Query<PreciseDelivery>(query).AsList();
                result = _all?.Find(item => item.Month == -1);
                result?.Detail?.AddRange(_all.FindAll(e => e.Month != -1));
                if (result?.Detail?.Count > 0)
                {
                    Tuple<double, double> trend = CalculateTrend(result?.Detail);

                    result.Detail.ForEach(e => e.Trend = trend.Item2 + e.Month*trend.Item1);
                }
            }

            return result;
        }

        private PreciseDelivery GetKPIByName(ParamsCalculateKpi paramsCalculateKpi,string nameKpi)
        {
            string _selectCustomer =
                CreateInSectionForAllCustomer(paramsCalculateKpi.Customer, paramsCalculateKpi.TypeCustomer);
            PreciseDelivery result = null;
            String query = string.Format($"select description,month,year,target,fact,deviation,countorder from ("+
                                          " select max(s.kpi_description) as description, MONTH(s.Date_Calc) as month, YEAR(s.Date_Calc) as year,"+ 
                                          " max(t.Kpi_target) as target, avg(s.KPI_Fact) as fact," +
                                          "  (avg(s.KPI_Fact)  - max(t.Kpi_target)) as deviation,count(*) as countorder  " +
                                          " from gtk_group_report.dbo.gtk_site_cust_kpi s  " +
                                          " join (select * from dbo.gtk_cust_kpi_lns where " +
                                          " kpi_description = '{3}') as t on t.cust_num = s.cust_num " +
                                          " where s.cust_num  {0} and s.Date_Calc between '{1}' and '{2}' and s.site = '{4}' " +
                                          " and s.KPI_description = t.KPI_description  " +
                                          " group by MONTH(s.Date_Calc) , YEAR(s.Date_Calc)  " +
                                          " union all " +
                                          " select max(s.kpi_description) as description, -1 as month, MAX(YEAR(s.Date_Calc)) as year, max(t.Kpi_target) as target," +
                                          " avg(s.KPI_Fact) as fact,(avg(s.KPI_Fact)  - max(t.Kpi_target)) as deviation,count(*) as countorder " +
                                          " from gtk_group_report.dbo.gtk_site_cust_kpi s join (select * from dbo.gtk_cust_kpi_lns " +
                                          " where kpi_description = '{3}') as t on t.cust_num = s.cust_num" +
                                          " where s.cust_num  {0} and s.Date_Calc between '{1}' and '{2}'   and s.site = '{4}'" +
                                         "  and s.KPI_description = t.KPI_description  " +
                                          " ) as t" +
                                          " order by month",_selectCustomer,
                                            paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                                            paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"),nameKpi,
                                            DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise)  
                                         );
            
            using (var connection = new SqlConnection(DataConnection.GetConnectionString(paramsCalculateKpi.Enterprise)))
            {
                List<PreciseDelivery> _all = connection.Query<PreciseDelivery>(query).AsList();
                result = _all?.Find(item => item.Month == -1);
                result?.Detail?.AddRange(_all.FindAll(e => e.Month != -1));
                if (result?.Detail?.Count > 0)
                {
                    Tuple<double, double> trend = CalculateTrend(result?.Detail);

                    result.Detail.ForEach(e => e.Trend = trend.Item2 + e.Month*trend.Item1);
                }
            }
            
            
            return result;
        }
        
        private static  string CreateInSectionForCust_Seq(ParamsCalculateKpi paramsCalculateKpi)
        {
            string result = "";
            if (!paramsCalculateKpi.CustomerDelivery.Name.Equals("Все"))
            {
                result = string.Format(" and s.cust_seq = {0} ", paramsCalculateKpi.CustomerDelivery.Seq);
            }

            return result;
        }

        private Tuple<double,double> CalculateTrend(List<PreciseDelivery> preciseDeliveries)
        {


            return LinearLeastSquares(preciseDeliveries.ToArray());
        }
        
        private  Tuple<double,double> LinearLeastSquares(PreciseDelivery[] deliveries ) 
        {
            if (deliveries.Length == 1)
            {
                return new Tuple<double, double>(0,0);
            }
            
            double a11 = 0.0, a12 = 0.0, a22 = deliveries.Length, b1 = 0.0, b2 = 0.0;
            for (int i = 0; i < deliveries.Length; i++) {
                a11 += deliveries[i].Month  * deliveries[i].Month;
                a12 += deliveries[i].Month;
                b1 += deliveries[i].Month * deliveries[i].Fact;
                b2 += deliveries[i].Fact;
            }
            double det = a11 * a22 - a12 * a12;
            /*if (Math.Abs (det) < 1e-17)
                throw new ArgumentException ("Данные не верны");*/
            double a = (b1 * a22 - a12 * b2) / det;
            double b = (a11 * b2 - b1 * a12) / det;
            return  new Tuple<double, double>(a,b);
        }
        
        /*public  void LinearLeastSquares(double[] x, double[] y, out double a, out double b) 
        {   
            if (x.Length != y.Length || x.Length <= 1)
                throw new ArgumentException ("Неверные размеры данных");
            double a11 = 0.0, a12 = 0.0, a22 = x.Length, b1 = 0.0, b2 = 0.0;
            for (int i = 0; i < x.Length; i++) {
                a11 += x [i] * x [i];
                a12 += x [i];
                b1 += x [i] * y [i];
                b2 += y [i];
            }
            double det = a11 * a22 - a12 * a12;
            if (Math.Abs (det) < 1e-17)
                throw new ArgumentException ("Данные не верны");
            a = (b1 * a22 - a12 * b2) / det;
            b = (a11 * b2 - b1 * a12) / det;
        }
        */

        
        
    }
}