using System;
using System.Collections.Generic;
using DeliveryPreciseReact.Domain;

namespace DeliveryPreciseReact.Common
{
    public class ParamsCalculateKpi
    {
        private string _enterprise;
        private DateRange _rangeDate;
        private List<KpiHelper> _selectKpi;
        private Customer _customer;
        private List<string> _typeCustomer;
        private Customer _customerDelivery;
        private Boolean _excludeSelfTake;

       
        public ParamsCalculateKpi()
        {
        }

        public string Enterprise
        {
            get => _enterprise;
            set => _enterprise = value;
        }

        public DateRange RangeDate
        {
            get => _rangeDate;
            set => _rangeDate = value;
        }

        public List<KpiHelper> SelectKpi
        {
            get => _selectKpi;
            set => _selectKpi = value;
        }

        public Customer Customer
        {
            get => _customer;
            set => _customer = value;
        }

        public List<string> TypeCustomer
        {
            get => _typeCustomer;
            set => _typeCustomer = value;
        }

        public Customer CustomerDelivery
        {
            get => _customerDelivery;
            set => _customerDelivery = value;
        }
        public bool ExcludeSelfTake
        {
            get => _excludeSelfTake;
            set => _excludeSelfTake = value;
        }

    }
}