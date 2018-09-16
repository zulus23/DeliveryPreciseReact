using System.Collections.Generic;
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
            temp.Add("GOTEK");
            temp.Add("Print");
            return Ok(temp);
        }      
    }
}