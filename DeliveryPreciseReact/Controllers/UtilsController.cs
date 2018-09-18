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
            temp.Add("ЦЕНТР");
            temp.Add("СЕВЕР");
            temp.Add("ПРИНТ");
            temp.Add("ПОЛИПАК");
            temp.Add("ЛИТАР");
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
        [HttpGet("kpis")]
        public ActionResult GetPki()
        {
            List<Kpi> kpis = new List<Kpi>();
            kpis.Add(new Kpi("Точность поставки по времени %",0,0,0,0));
            kpis.Add(new Kpi("Точность выхода на склад %",0,0,0,0));
            kpis.Add(new Kpi("Точность поставки по количеству %",0,0,0,0));
            kpis.Add(new Kpi("Уровень качества продукции %",0,0,0,0));
            kpis.Add(new Kpi("Скорость урегулирования претензий дни",0,0,0,0));
            kpis.Add(new Kpi("Производство тестов дни",0,0,0,0));
            kpis.Add(new Kpi("Производство макетов дни",0,0,0,0));

            return Ok(kpis);

        }
        
        
    }
}