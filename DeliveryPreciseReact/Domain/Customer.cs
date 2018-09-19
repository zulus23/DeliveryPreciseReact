namespace DeliveryPreciseReact.Domain
{
    public class Customer
    {
        private string _code;
        private string _name;

        public Customer()
        {
        }

        public Customer(string name)
        {
            this._name = name;
        }

        public string Code
        {
            get => _code;
            set => _code = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }
    }
}