using System.Collections.Generic;

namespace DeliveryPreciseReact.Domain
{
    public sealed class KpiByCustomer
    {
        private string _customer;
        private List<Kpi> _kpis;

        public KpiByCustomer()
        {
        }

        public KpiByCustomer(string customer, List<Kpi> kpis)
        {
            _customer = customer;
            _kpis = kpis;
        }

        public string Customer
        {
            get => _customer;
            set => _customer = value;
        }

        public List<Kpi> Kpis
        {
            get => _kpis;
            set => _kpis = value;
        }
    }
}