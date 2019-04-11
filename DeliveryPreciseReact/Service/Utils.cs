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
                $"select customer.name as customername,kpi_description as description, t.Kpi_target as target, s.KPI_stat as fact, " +
                " (s.KPI_stat  - t.Kpi_target) as deviation, count(*) as countorder ,{6} as order_ " +
                " from gtk_group_report.dbo.gtk_kpi_ship s join (select * from dbo.gtk_cust_kpi_lns where  kpi_description = '{4}') as t  on t.cust_num = s.cust_num " +
                " join ( SELECT name,code,cust_seq FROM ( SELECT  c.cust_num AS code, RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name,  " +
                " c.cust_seq  FROM dbo.customer c  JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = 0 " +
                " join dbo.gtk_cust_kpi_hdr h on ca.cust_num = h.cust_num  WHERE 1 = 1  {3}  AND " +
                " RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL  ) as customer    where 1 = 1    AND customer.code  {0}) customer on customer.code = s.cust_num  and customer.cust_seq = s.cust_seq " +
                " where s.DateDostFact between '{1}' and '{2}' and s.site = '{5}' group by  customer.name, kpi_description, t.Kpi_target, s.KPI_stat ,  (s.KPI_stat  - t.Kpi_target) ",_selectCustomer,
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

            string query = string.Format(
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
            
            
            
            return query;
        }

        private static string GetStringKpiByName(ParamsCalculateKpi paramsCalculateKpi, string nameKpi, int order)
        {
            string _selectCustomer =
                Utils.CreateInSectionForAllCustomer(paramsCalculateKpi.Customer, paramsCalculateKpi.TypeCustomer);
            
            string query = string.Format($"select customer.name as customername, max(s.kpi_description) as description, max(t.Kpi_target) as target, avg(s.KPI_Fact) as fact," +
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