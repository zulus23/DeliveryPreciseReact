using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryPreciseReact.Common;
using DeliveryPreciseReact.Domain;
using DeliveryPreciseReact.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OfficeOpenXml;

namespace DeliveryPreciseReact
{
    [Route("api/[controller]")]
    public class UtilsController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;


        private readonly IDataService _dataService;
        private UtilService _utilService;

        public UtilsController(IDataService dataService, UtilService utilService,
            IHostingEnvironment hostingEnvironment)
        {
            _dataService = dataService;
            _utilService = utilService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("enterprise")]
        public ActionResult GetEnterprise()
        {
            List<string> temp = _dataService.ListEnterprise();
            return Ok(temp);
        }

        [HttpPost("customers")]
        public ActionResult GetCustomer([FromBody] ParamsForSelectCustomer selectParams)
        {
            List<Customer> customers = new List<Customer>();
            customers = _dataService.ListCustomerByEnterprise(selectParams.Enterprise, selectParams.TypeCustomer);
            return Ok(customers);
        }


        [HttpGet("kpis")]
        public ActionResult GetPki()
        {
            return Ok(_dataService.ListKpis());
        }

        [HttpPost("calculatekpi")]
        public ActionResult CalculateKpi([FromBody] ParamsCalculateKpi data)
        {
            List<PreciseDelivery> preciseDeliveries = new List<PreciseDelivery>();

            List<PreciseDelivery> preciseDelivery = _dataService.CalculateKpi(data);
            preciseDeliveries.AddRange(preciseDelivery);
            return Ok(preciseDeliveries);
        }

        [HttpPost("customerdelivery")]
        public ActionResult GetCustomersDelivery([FromBody] ParamsForSelectCustomerDelivery selectParams)
        {
            List<Customer> customers = new List<Customer>();
            customers = _dataService.ListCustomerDeliveryByEnterprise(selectParams.Enterprise, selectParams.Customer);
            return Ok(customers);
        }

        [HttpPost("reportdriver")]
        public async Task<ActionResult> DownloadReportDriver([FromBody] ParamsCalculateKpi data)
        {
            string sFileName = @"driveorder.xlsx";
            var streamResult = await _utilService.OrderDrivingXLSFileStreamResult(data);

            streamResult.Position = 0;
            var result = File(streamResult,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

            Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
            {
                FileName = sFileName
            }.ToString();


            return result;
        }
        [HttpPost("reportkpi")]
        public async Task<ActionResult> DownloadReportKpi([FromBody] ParamsCalculateKpi data)
        {
            string sFileName = @"driveorder.xlsx";
            var streamResult = await _utilService.KpiXLSFileStreamResult(data);

            streamResult.Position = 0;
            var result = File(streamResult,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

            Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
            {
                FileName = sFileName
            }.ToString();


            return result;
        }
        
        [HttpGet("testreport")]
        public IActionResult ExportListUsingEPPlus()
        {
            const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var data = new[]{ 
                new{ Name="Ram", Email="ram@techbrij.com", Phone="111-222-3333" },
                new{ Name="Shyam", Email="shyam@techbrij.com", Phone="159-222-1596" },
                new{ Name="Mohan", Email="mohan@techbrij.com", Phone="456-222-4569" },
                new{ Name="Sohan", Email="sohan@techbrij.com", Phone="789-456-3333" },
                new{ Name="Karan", Email="karan@techbrij.com", Phone="111-222-1234" },
                new{ Name="Brij", Email="brij@techbrij.com", Phone="111-222-3333" }                       
            };
 
            
            
            byte[] reportBytes;
            using (ExcelPackage package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 1].LoadFromCollection(data, true);
                reportBytes = package.GetAsByteArray();
            }

            return File(reportBytes, XlsxContentType, "report.xlsx");
            
            
            
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
}