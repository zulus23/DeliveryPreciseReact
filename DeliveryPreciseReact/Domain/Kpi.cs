namespace DeliveryPreciseReact.Domain
{
    public class Kpi
    {
        private string _name;
        private int _target;
        private int _fact;
        private int _deviation;
        private int _countOrder;


        public Kpi(string name, int target, int fact, int deviation, int countOrder)
        {
            _name = name;
            _target = target;
            _fact = fact;
            _deviation = deviation;
            _countOrder = countOrder;
        }

        public string Name => _name;

        public int Target => _target;

        public int Fact => _fact;

        public int Deviation => _deviation;

        public int CountOrder => _countOrder;
    }
}