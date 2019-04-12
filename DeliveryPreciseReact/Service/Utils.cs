using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using DeliveryPreciseReact.Common;
using DeliveryPreciseReact.Domain;

namespace DeliveryPreciseReact.Service
{
    public static class Utils
    {
        public static string CreateInSectionForAllCustomer(Customer customer, List<string> typeCustomer)
        {
            string _insertCodeCustomer = "";
            if (!customer.Name.Equals(KpiConst.ALL))
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
                                          "  where 1 = 1  {0} {1})", typeIn, _insertCodeCustomer);
            return _query;
        }

        public static string CreateInSectionForCust_Seq(ParamsCalculateKpi paramsCalculateKpi)
        {
            string result = "";
            if (!paramsCalculateKpi.CustomerDelivery.Name.Equals(KpiConst.ALL))
            {
                result = string.Format(" and s.cust_seq = {0} ", paramsCalculateKpi.CustomerDelivery.Seq);
            }

            return result;
        }

        public static string CreateInSectionForCust_Seq_By_C(ParamsCalculateKpi paramsCalculateKpi)
        {
            string result = "";
            if (!paramsCalculateKpi.CustomerDelivery.Name.Equals(KpiConst.ALL))
            {
                result = string.Format(" and c.cust_seq = {0} ", paramsCalculateKpi.CustomerDelivery.Seq);
            }

            return result;
        }

        
        
        public static string CreateCustomerTypeInSection(List<string> typeCustomer)
        {
            string typeIn = "";
            if (typeCustomer.Count > 0)
            {
                typeIn = typeCustomer.Aggregate(" and  customer.type  IN (", (i, a) => i = i + "'" + a + "'" + ",",
                    e => e + "'--','' )").Replace(",''", "");
            }

            return typeIn;
        }


        private static string GetStringPreciseDelivery(ParamsCalculateKpi paramsCalculateKpi, string nameKpi,int order)
        {
            string _selectCustomer =
                Utils.CreateInSectionForAllCustomer(paramsCalculateKpi.Customer, paramsCalculateKpi.TypeCustomer);
            string _selectSeqCustomer = Utils.CreateInSectionForCust_Seq_By_C(paramsCalculateKpi);
            
            string query = string.Format(
                $"select customer.name as customername,max(kpi_description) as description, avg(t.Kpi_target) as target, avg(s.KPI_stat) as fact, " +
                " (avg(s.KPI_stat)  - max(t.Kpi_target)) as deviation, count(*) as countorder ,{6} as order_ " +
                " from gtk_group_report.dbo.gtk_kpi_ship s join (select * from dbo.gtk_cust_kpi_lns where  kpi_description = '{4}') as t  on t.cust_num = s.cust_num " +
                " join ( SELECT name,code,cust_seq FROM ( SELECT  c.cust_num AS code, RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name,  CASE WHEN uf_strategcust = '1'  THEN 'СК'  " +
                "      WHEN uf_strategprospect = '1' THEN 'СП'       WHEN (uf_strategcust IS NULL AND uf_strategprospect IS NULL)  THEN 'ПР'       ELSE 'ПР' END AS type," +
                " c.cust_seq  FROM dbo.customer c  JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = 0 " +
                " join dbo.gtk_cust_kpi_hdr h on ca.cust_num = h.cust_num  WHERE 1 = 1  {3}  AND " +
                " RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL  ) as customer    where 1 = 1    AND customer.code  {0}) customer on customer.code = s.cust_num  and customer.cust_seq = s.cust_seq " +
                " where s.DateDostFact between '{1}' and '{2}' and s.site = '{5}' group by  customer.name",_selectCustomer,
                paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"), _selectSeqCustomer,
                nameKpi,
                DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise),order);

            return query;

        }
        
        private static string GetStringPreciseDeliveryWithOutGroup(ParamsCalculateKpi paramsCalculateKpi, string nameKpi,int order)
        {
            string _selectCustomer =
                Utils.CreateInSectionForAllCustomer(paramsCalculateKpi.Customer, paramsCalculateKpi.TypeCustomer);
            string _selectSeqCustomer = Utils.CreateInSectionForCust_Seq_By_C(paramsCalculateKpi);
            
            string query = string.Format(
                $"select customer.name as customername,MAX(kpi_description) as description, MONTH(s.DateDostFact) AS month,YEAR(s.DateDostFact) AS year," +
                $" AVG(t.Kpi_target) as target, SUM(t.KPI_Target) AS targetSumma,COUNT(t.KPI_Target) AS targetCount,AVG(s.KPI_stat) as fact, " +
                " SUM(s.KPI_stat) AS factSumma,COUNT(s.KPI_stat) AS factCount,AVG(s.KPI_stat  - t.Kpi_target) as deviation, count(*) as countorder ,{6} as order_ " +
                " from gtk_group_report.dbo.gtk_kpi_ship s " +
                " join (select * from dbo.gtk_cust_kpi_lns where  kpi_description = '{4}') as t  on t.cust_num = s.cust_num " +
                " CROSS APPLY  (SELECT a.name,a.RUSExtName,a.cust_seq, a.cust_num AS code FROM dbo.custaddr a WHERE s.cust_num = a.cust_num AND a.cust_seq = 0)  as customer" +
                " where s.cust_num  {0} and s.DateDostFact between '{1}' and '{2}' and s.site = '{5}' " +
                " group by   customer.name,MONTH(s.DateDostFact),YEAR(s.DateDostFact)",_selectCustomer,
                paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"), _selectSeqCustomer,
                nameKpi,
                DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise),order);

            return query;

        }
        

        private static string GetStringPreciseWhse(ParamsCalculateKpi paramsCalculateKpi, string nameKpi,int order)
        {
             string _selectCustomer =
                Utils.CreateInSectionForAllCustomer(paramsCalculateKpi.Customer, paramsCalculateKpi.TypeCustomer);
            string _selectSeqCustomer = Utils.CreateInSectionForCust_Seq_By_C(paramsCalculateKpi);

            /*string query = string.Format(
                $" select customer.name as customername,max(kpi_description) as description, max(t.Kpi_target) as target, avg(s.KPI_whse) as fact, " +
                " (avg(s.KPI_whse)  - max(t.Kpi_target)) as deviation, count(*) as countorder, {6} as order_  from gtk_group_report.dbo.gtk_kpi_ship s " +
                " join (select * from dbo.gtk_cust_kpi_lns where  kpi_description = '{4}') as t on t.cust_num = s.cust_num " +
                " join  ( SELECT name,code,cust_seq FROM ( SELECT  c.cust_num AS code, RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name, " +
                "  CASE WHEN uf_strategcust = '1'  THEN 'СК' WHEN uf_strategprospect = '1' THEN 'СП' WHEN (uf_strategcust IS NULL AND uf_strategprospect IS NULL)  " +
                "  THEN 'ПР' ELSE 'ПР' END AS type, c.cust_seq FROM dbo.customer c  " +
                " JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = 0 join dbo.gtk_cust_kpi_hdr h on ca.cust_num = h.cust_num  " +
                " WHERE 1 = 1  {3}  AND RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL ) as customer    " +
                " where 1 = 1    AND customer.code  {0}) customer  on customer.code = s.cust_num  and customer.cust_seq = s.cust_seq " +
                "  where s.DateWHSFact between '{1}' and '{2}'  and s.site = '{5}' group by customer.name",
                 _selectCustomer,
                 paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                 paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"), _selectSeqCustomer, nameKpi,
                 DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise) ,order);
*/
            
            string query = string.Format(
                $"select customer.name as customername,MAX(kpi_description) as description, MONTH(s.DateWHSFact) AS month,YEAR(s.DateWHSFact) AS year," +
                $" AVG(t.Kpi_target) as target, SUM(t.KPI_Target) AS targetSumma,COUNT(t.KPI_Target) AS targetCount,AVG(s.KPI_whse) as fact, " +
                " SUM(s.KPI_whse) AS factSumma,COUNT(s.KPI_whse) AS factCount,AVG(s.KPI_whse  - t.Kpi_target) as deviation, count(*) as countorder ,{6} as order_ " +
                " from gtk_group_report.dbo.gtk_kpi_ship s " +
                " join (select * from dbo.gtk_cust_kpi_lns where  kpi_description = '{4}') as t  on t.cust_num = s.cust_num " +
                " CROSS APPLY  (SELECT a.name,a.RUSExtName,a.cust_seq, a.cust_num AS code FROM dbo.custaddr a WHERE s.cust_num = a.cust_num AND a.cust_seq = 0)  as customer" +
                " where s.cust_num  {0} and s.DateWHSFact between '{1}' and '{2}' and s.site = '{5}' " +
                " group by   customer.name,MONTH(s.DateWHSFact),YEAR(s.DateWHSFact)",_selectCustomer,
                paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"), _selectSeqCustomer,
                nameKpi,
                DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise),order);
            
            
            
            return query;
        }

        private static string GetStringKpiByName(ParamsCalculateKpi paramsCalculateKpi, string nameKpi, int order)
        {
            string _selectCustomer =
                Utils.CreateInSectionForAllCustomer(paramsCalculateKpi.Customer, paramsCalculateKpi.TypeCustomer);
            
            /*string query = string.Format($"select customer.name as customername, max(s.kpi_description) as description, max(t.Kpi_target) as target, avg(s.KPI_Fact) as fact," +
                                            " (avg(s.KPI_Fact)  - max(t.Kpi_target)) as deviation, sum(s.KPI_Qty) as countorder,{5} as order_  from gtk_group_report.dbo.gtk_site_cust_kpi s   " +
                                            "  join (select * from dbo.gtk_cust_kpi_lns where  kpi_description = '{3}') as t on t.cust_num = s.cust_num " +
                                            "  join ( SELECT name,code,cust_seq FROM ( SELECT  c.cust_num AS code, RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name,  " +
                                            "          CASE WHEN uf_strategcust = '1'  THEN 'СК'       WHEN uf_strategprospect = '1' THEN 'СП'       " +
                                            "              WHEN (uf_strategcust IS NULL AND uf_strategprospect IS NULL)  THEN 'ПР'       ELSE 'ПР' END AS type," +
                                            "          c.cust_seq  " +
                                            "         FROM dbo.customer c  " +
                                            "         JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = 0 " +
                                            "          join dbo.gtk_cust_kpi_hdr h on ca.cust_num = h.cust_num  " +
                                            "         WHERE ca.cust_seq = 0  AND RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL ) as customer    " +
                                            "       where 1 = 1    AND customer.code  {0}) customer on customer.code = s.cust_num  and customer.cust_seq = 0 " +
                                            " where s.Date_Calc between '{1}' and '{2}' and s.site = '{4}'  and s.KPI_description = t.KPI_description group by customer.name",
                                            _selectCustomer,
                                            paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                                            paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"), nameKpi,
                                            DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise),order);
            */
            string query = string.Format(
                $"select customer.name as customername,MAX(s.kpi_description) as description, MONTH(s.Date_Calc) AS month,YEAR(s.Date_Calc) AS year," +
                $" AVG(t.Kpi_target) as target, SUM(t.KPI_Target) AS targetSumma,COUNT(t.KPI_Target) AS targetCount,AVG(s.KPI_Fact) as fact, " +
                " SUM(s.KPI_Fact) AS factSumma,COUNT(s.KPI_Fact) AS factCount,AVG(s.KPI_Fact  - t.Kpi_target) as deviation, count(s.KPI_Qty) as countorder ,{6} as order_ " +
                " from gtk_group_report.dbo.gtk_site_cust_kpi s " +
                " join (select * from dbo.gtk_cust_kpi_lns where  kpi_description = '{4}') as t  on t.cust_num = s.cust_num  and s.KPI_description = '{4}'" +
                " CROSS APPLY  (SELECT a.name,a.RUSExtName,a.cust_seq, a.cust_num AS code FROM dbo.custaddr a WHERE s.cust_num = a.cust_num AND a.cust_seq = 0)  as customer" +
                " where s.cust_num  {0} and s.Date_Calc between '{1}' and '{2}' and s.site = '{5}'  " +
                " group by   customer.name,MONTH(s.Date_Calc),YEAR(s.Date_Calc)",
                _selectCustomer,
                paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd") ,"",
                nameKpi,
                DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise),order);
            
            
            return query;
        }
        
        public static string SelectKpiByCustomer(ParamsCalculateKpi paramsCalculateKpi, KpiHelper kpiHelper,int order)
        {
            switch (kpiHelper.Name)
            {
                case KpiConst.PRECISEDELIVERY/* "Точность поставки по времени, %"*/:
                {
                    //return Utils.GetStringPreciseDelivery(paramsCalculateKpi,KpiConst.PRECISEDELIVERY,order);
                    return Utils.GetStringPreciseDeliveryWithOutGroup(paramsCalculateKpi,KpiConst.PRECISEDELIVERY,order);
                    
                }

                case KpiConst.PRECISEENTERSTORAGE/*"Точность выхода на склад %"*/:
                {
                    return Utils.GetStringPreciseWhse(paramsCalculateKpi,KpiConst.PRECISEENTERSTORAGE,order);
                    
                }
                default:
                {
                    return Utils.GetStringKpiByName(paramsCalculateKpi, kpiHelper.Name,order);
                    
                }
            }
        }
        
        public static List<KpiHelper> GetSelectedKpi(ParamsCalculateKpi paramsCalculateKpi)
        {
            List<KpiHelper> _selectedKpi;
            if (paramsCalculateKpi.SelectKpi.Any(e => e.Name.Equals(KpiConst.ALL)))
            {
                _selectedKpi = GetKpiHelpers().FindAll(k => !k.Name.Equals(KpiConst.ALL));
            }
            else
            {
                _selectedKpi = paramsCalculateKpi.SelectKpi.FindAll(k => !k.Name.Equals(KpiConst.ALL));
            }

            return _selectedKpi;
        }
        public static List<KpiHelper> GetKpiHelpers()
        {
            List<KpiHelper> list = new List<KpiHelper>();
            list.Add(new KpiHelper(KpiConst.ALL /*"Все"*/));
            list.Add(new KpiHelper(KpiConst.PRECISEDELIVERY /*"Точность поставки по времени, %"*/));
            list.Add(new KpiHelper(KpiConst.PRECISEENTERSTORAGE /*"Точность выхода на склад %"*/));
            list.Add(new KpiHelper(KpiConst.PRECISEDELIVERYBYAMOUNT /*"Точность поставки по количеству, %"*/));
            list.Add(new KpiHelper(KpiConst.LEVELQUALITYPRODUCT /*"Уровень качества продукции, %"*/));
            list.Add(new KpiHelper(KpiConst.SPEEDCLAIM /*"Скорость урегулирования претензий, дни"*/));
            list.Add(new KpiHelper(KpiConst.PRODUCETEST /*"Производство тестов, дни"*/));
            list.Add(new KpiHelper(KpiConst.PRODUCEMODEL /*"Производство макетов, дни"*/));
            return list;
        }
    }
}