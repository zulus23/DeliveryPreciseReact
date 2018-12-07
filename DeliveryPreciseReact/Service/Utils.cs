using System.Collections.Generic;
using System.Linq;
using DeliveryPreciseReact.Common;
using DeliveryPreciseReact.Domain;

namespace DeliveryPreciseReact.Service
{
    public static   class Utils
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

        
    }
}