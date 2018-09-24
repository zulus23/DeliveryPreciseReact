using System;
using System.Collections.Generic;

namespace DeliveryPreciseReact.Domain
{
    public class PreciseDelivery
    {
        private int _month;
        private string _description;
        private double _target;
        private double _fact;
        private double _deviation;
        private double _countOrder;
        private List<PreciseDelivery> _detail = new List<PreciseDelivery>();
        private int _year;
        private DateTime _date;

        

        public PreciseDelivery()
        {
        }

        public int Month
        {
            get => _month;
            set => _month = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public double Target
        {
            get => _target;
            set => _target = value;
        }

        public double Fact
        {
            get => _fact;
            set => _fact = value;
        }

        public double Deviation
        {
            get => _deviation;
            set => _deviation = value;
        }

        public double CountOrder
        {
            get => _countOrder;
            set => _countOrder = value;
        }

        public List<PreciseDelivery> Detail
        {
            get => _detail;
            set => _detail = value;
        }
        public DateTime Date
        {
            get { return _month != -1 ? new DateTime(_year, _month, 1) : DateTime.Now; }


    }

        public int Year
        {
            get => _year;
            set => _year = value;
        }
    }
}