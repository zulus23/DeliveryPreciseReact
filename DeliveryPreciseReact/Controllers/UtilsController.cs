using System;
using System.Collections.Generic;
using DeliveryPreciseReact.Domain;
using DeliveryPreciseReact.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DeliveryPreciseReact
{
    
    
    
    [Route("api/[controller]")]
    public class UtilsController : Controller
    {
        
        private IDataService _dataService;

        public UtilsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("enterprise")]
        public ActionResult GetEnterprise()
        {
            List<string> temp = new List<string>();
            temp.Add("ГОТЭК");
            temp.Add("ЦЕНТР");
            temp.Add("СЕВЕР");
            temp.Add("ПРИНТ");
            temp.Add("ПОЛИПАК");
            temp.Add("ЛИТАР");
            return Ok(temp);
        }

        [HttpPost("customers")]
        public ActionResult GetCustomer([FromBody]ParamsForSelectCustomer selectParams)
        {
            List<Customer> customers = new List<Customer>();
            customers =   _dataService.ListCustomerByEnterprise(selectParams.Enterprise,selectParams.TypeCustomer);
            return Ok(customers);
        }

    
        [HttpGet("kpis")]
        public ActionResult GetPki()
        {
            List<Kpi> kpis = new List<Kpi>();
            kpis.Add(new Kpi("Все",0,0,0,0));
            kpis.Add(new Kpi("Точность поставки по времени %",0,0,0,0));
            kpis.Add(new Kpi("Точность выхода на склад %",0,0,0,0));
            kpis.Add(new Kpi("Точность поставки по количеству %",0,0,0,0));
            kpis.Add(new Kpi("Уровень качества продукции %",0,0,0,0));
            kpis.Add(new Kpi("Скорость урегулирования претензий дни",0,0,0,0));
            kpis.Add(new Kpi("Производство тестов дни",0,0,0,0));
            kpis.Add(new Kpi("Производство макетов дни",0,0,0,0));

            return Ok(kpis);

        }

        [HttpPost("calculatekpi")]
        public ActionResult CalculateKpi([FromBody]ParamsCalculateKpi data)
        {
            List<PreciseDelivery> preciseDeliveries = new List<PreciseDelivery>();
            Console.WriteLine(data);
            PreciseDelivery preciseDelivery =  _dataService.GetPreciseDeliveryByEnterprise(data.Enterprise, data.Customer);
            preciseDeliveries.Add(preciseDelivery);
            
            return Ok(preciseDeliveries);
        } 
        
        
    }

    public class ParamsCalculateKpi
    {
        private string _enterprise;
        private DateRange _rangeDate;
        private List<Kpi> _selectKpi;
        private Customer _customer;

        public ParamsCalculateKpi()
        {
        }

        public string Enterprise
        {
            get => _enterprise;
            set => _enterprise = value;
        }

        public DateRange RangeDate
        {
            get => _rangeDate;
            set => _rangeDate = value;
        }

        public List<Kpi> SelectKpi
        {
            get => _selectKpi;
            set => _selectKpi = value;
        }

        public Customer Customer
        {
            get => _customer;
            set => _customer = value;
        }
    }

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
    
    public class ParamsForSelectCustomer
    {
        private string _enterprise;
        private List<string> _typeCustomer;
      
        public ParamsForSelectCustomer()
        {
        }

        public string Enterprise
        {
            get => _enterprise;
            set => _enterprise = value;
        }

        public List<string> TypeCustomer
        {
            get => _typeCustomer;
            set => _typeCustomer = value;
        }

      
    }

}