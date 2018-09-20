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
    }
}