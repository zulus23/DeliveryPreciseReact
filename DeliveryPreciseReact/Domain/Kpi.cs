namespace DeliveryPreciseReact.Domain
{
    public sealed class Kpi
    {
        private string _description;
        private double _target;
        private double _fact;
        private double _deviation;
        private double _countOrder;

        public Kpi()
        {
        }

        public Kpi(string description, double target, double fact, double deviation, double countOrder)
        {
            _description = description;
            _target = target;
            _fact = fact;
            _deviation = deviation;
            _countOrder = countOrder;
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