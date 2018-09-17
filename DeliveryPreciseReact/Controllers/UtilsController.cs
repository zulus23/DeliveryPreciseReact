using System.Collections.Generic;
using DeliveryPreciseReact.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryPreciseReact
{
    [Route("api/[controller]")]
    public class UtilsController : Controller
    {
        [HttpGet("enterprise")]
        public ActionResult GetEnterprise()
        {
            List<string> temp = new List<string>();
            temp.Add("ГОТЭК");
            temp.Add("Print");
            return Ok(temp);
        }

        [HttpGet("customers")]
        public ActionResult GetCustomer()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer("Test"));
            customers.Add(new Customer("Test2"));
           
            return Ok(customers);
        }
        
    }
}