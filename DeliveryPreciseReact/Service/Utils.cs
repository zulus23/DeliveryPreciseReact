using System.Collections.Generic;
using System.Linq;
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


        private static string GetStringPreciseDelivery(ParamsCalculateKpi paramsCalculateKpi, string nameKpi)
        {
            string _selectCustomer =
                Utils.CreateInSectionForAllCustomer(paramsCalculateKpi.Customer, paramsCalculateKpi.TypeCustomer);
            string _selectSeqCustomer = Utils.CreateInSectionForCust_Seq(paramsCalculateKpi);

            PreciseDelivery result = null;

            string query = string.Format($"select description,month,year,target,fact,deviation,countorder from (" +
                                         " select max(kpi_description) as description, MONTH(s.DateDostFact) as month, YEAR(s.DateDostFact) as year," +
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
                                         " order by month", _selectCustomer,
                paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"), _selectSeqCustomer,
                nameKpi,
                DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise)
            );

            string queryNew = string.Format(
                $"select customer.name as customername,max(kpi_description) as description, max(t.Kpi_target) as target, avg(s.KPI_stat) as fact, " +
                " (avg(s.KPI_stat)  - max(t.Kpi_target)) as deviation, count(*) as countorder ,1 " +
                " from gtk_group_report.dbo.gtk_kpi_ship s join (select * from dbo.gtk_cust_kpi_lns where  kpi_description = '{4}') as t  on t.cust_num = s.cust_num " +
                " join ( SELECT name,code,cust_seq FROM ( SELECT  c.cust_num AS code, RTRIM(COALESCE(ca.name,ca.RUSExtName)) as name,  CASE WHEN uf_strategcust = '1'  THEN 'СК'  " +
                "      WHEN uf_strategprospect = '1' THEN 'СП'       WHEN (uf_strategcust IS NULL AND uf_strategprospect IS NULL)  THEN 'ПР'       ELSE 'ПР' END AS type," +
                " c.cust_seq  FROM dbo.customer c  JOIN dbo.custaddr ca ON ca.cust_num = c.cust_num AND ca.cust_seq = c.cust_seq " +
                " join dbo.gtk_cust_kpi_hdr h on ca.cust_num = h.cust_num  WHERE ca.cust_seq = 0  AND " +
                " RTRIM(COALESCE(ca.name,ca.RUSExtName)) IS NOT NULL ) as customer    where 1 = 1    AND customer.code  {0}) customer on customer.code = s.cust_num  and customer.cust_seq = s.cust_seq " +
                " where s.DateDostFact between '{1}' and {2} and s.site = '{5}' group by  customer.name",_selectCustomer,
                paramsCalculateKpi.RangeDate.Start.ToString("yyyyMMdd"),
                paramsCalculateKpi.RangeDate.End.ToString("yyyyMMdd"), _selectSeqCustomer,
                nameKpi,
                DataConnection.GetNameDbInGotekGroup(paramsCalculateKpi.Enterprise));
            
            
            

            return query;


        }
    }
}