namespace DeliveryPreciseReact.Models
{
    public class ApiError
    {
        private string _message;
        private string _detail;

        public string Message
        {
            get => _message;
            set => _message = value;
        }

        public string Detail
        {
            get => _detail;
            set => _detail = value;
        }
    }
}