using System;

namespace DeliveryPreciseReact.Common
{
    public class DateRange
    {
        private DateTime _start;
        private DateTime _end;

        public DateRange()
        {
        }

       

        public DateTime Start
        {
            get => _start;
            set => _start = value;
        }

        public DateTime End
        {
            get => _end;
            set => _end = value;
        }
    }

}