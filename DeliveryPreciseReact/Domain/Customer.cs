namespace DeliveryPreciseReact.Domain
{
    public class Customer
    {
        private string _code;
        private string _name;
        private int _seq;
        private string _address;
        private string _fullName;
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

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public string FullName => $"{(_name.Equals("Все")?"":_seq+" - ")}{_name}{(_address?.Length < 10 ? "" : " " + _address)}";
    }
}