namespace DeliveryPreciseReact.Domain
{
    public class Kpi
    {
        private string _name;
        private int _target;
        private int _fact;
        private int _deviation;
        private int _countOrder;

        public Kpi()
        {
        }

        public Kpi(string name, int target, int fact, int deviation, int countOrder)
        {
            _name = name;
            _target = target;
            _fact = fact;
            _deviation = deviation;
            _countOrder = countOrder;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Target
        {
            get => _target;
            set => _target = value;
        }

        public int Fact
        {
            get => _fact;
            set => _fact = value;
        }

        public int Deviation
        {
            get => _deviation;
            set => _deviation = value;
        }

        public int CountOrder
        {
            get => _countOrder;
            set => _countOrder = value;
        }
    }
}