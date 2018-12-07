namespace DeliveryPreciseReact.Domain
{
    public class KpiHelper
    {
        private string _name;
      

        public KpiHelper()
        {
        }

        public KpiHelper(string name)
        {
            _name = name;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

    }
}