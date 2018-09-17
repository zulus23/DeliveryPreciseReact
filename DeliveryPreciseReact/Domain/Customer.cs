namespace DeliveryPreciseReact.Domain
{
    public class Customer
    {
        private string _name;
        public Customer(string name)
        {
            this._name = name;
        }

        public string Name => _name;
    }
}