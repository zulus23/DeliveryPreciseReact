namespace DeliveryPreciseReact.Domain
{
    public class Customer
    {
        private string _code;
        private string _name;
        private int _seq;
        public Customer()
        {
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

        public int Seq
        {
            get => _seq;
            set => _seq = value;
        }
    }
}