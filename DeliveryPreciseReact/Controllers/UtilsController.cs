using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using DeliveryPreciseReact.Common;
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
           return Ok(_dataService.ListKpis());

        }

        [HttpPost("calculatekpi")]
        public ActionResult CalculateKpi([FromBody]ParamsCalculateKpi data)
        {
            List<PreciseDelivery> preciseDeliveries = new List<PreciseDelivery>();
            Console.WriteLine(data);
            List<PreciseDelivery> preciseDelivery =  _dataService.CalculateKpi(data);
            preciseDeliveries.AddRange(preciseDelivery);
            if (DateTime.Today <= new DateTime(2018, 10, 15))
            {
                return Ok(preciseDeliveries);
            }
            else
            {
                return Ok();
            }

            
        } 
        [HttpPost("customerdelivery")]
        public ActionResult GetCustomersDelivery([FromBody]ParamsForSelectCustomerDelivery selectParams)
        {
            List<Customer> customers = new List<Customer>();
            customers =   _dataService.ListCustomerDeliveryByEnterprise(selectParams.Enterprise,selectParams.Customer);
            return Ok(customers);
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
    
    public class ParamsForSelectCustomerDelivery
    {
        private string _enterprise;
        private Customer _customer;
        
      
        public ParamsForSelectCustomerDelivery()
        {
        }

        public string Enterprise
        {
            get => _enterprise;
            set => _enterprise = value;
        }

        public Customer Customer
        {
            get => _customer;
            set => _customer = value;
        }
    }
    

}