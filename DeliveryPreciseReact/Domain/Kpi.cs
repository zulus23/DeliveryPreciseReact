namespace DeliveryPreciseReact.Domain
{
    public sealed class Kpi
    {
        
        private string _customerName;
        private string _description;
        private double _target;
        private double _fact;
        private double _deviation;
        private double _countOrder;
        private int _order_;

        public Kpi()
        {
        }

        public Kpi(string customerName, string description, double target, double fact, double deviation, double countOrder, int order)
        {
            _customerName = customerName;
            _description = description;
            _target = target;
            _fact = fact;
            _deviation = deviation;
            _countOrder = countOrder;
            _order_ = order;
        }

        public string CustomerName
        {
            get => _customerName;
            set => _customerName = value;
        }

        public int Order_
        {
            get => _order_;
            set => _order_ = value;
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