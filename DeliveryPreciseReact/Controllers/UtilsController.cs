using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DeliveryPreciseReact.Common;
using DeliveryPreciseReact.Domain;
using DeliveryPreciseReact.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace DeliveryPreciseReact
{
    
    
    
    [Route("api/[controller]")]
    public class UtilsController : Controller
    {
        
        
        private readonly IHostingEnvironment _hostingEnvironment;

        

        
        private readonly IDataService _dataService;
        private Utils _utils;

        public UtilsController(IDataService dataService, Utils utils,IHostingEnvironment hostingEnvironment)
        {
            _dataService = dataService;
            _utils = utils;
            _hostingEnvironment = hostingEnvironment;
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

        [HttpPost("report")]
        public async Task<ActionResult> DownloadReport([FromBody]ParamsCalculateKpi data)
        {
            
            var stream = new System.IO.MemoryStream();
            
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"demo.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);

            List<DeliveryRecord> _delivery = await _dataService.GetDeliveryRecordsAsync(data);
            
            
            
            ExcelPackage package;
            using (package = new ExcelPackage(stream))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KPI");
                using (var range = worksheet.Cells["A1:AC1"])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.WrapText = true;



                }
                //First add the headers
                worksheet.Cells[1, 1].Value = @"Сайт";
                worksheet.Cells[1, 2].Value = @"Код клиента";
                worksheet.Cells[1, 3].Value = @"Код адреса дост.";
                worksheet.Cells[1, 4].Value = @"Номер заказа";
                worksheet.Cells[1, 5].Value = @"Номер строки";
                worksheet.Cells[1, 6].Value = @"Дата заявки";
                worksheet.Cells[1, 7].Value = @"№ заявки продаж";
                worksheet.Cells[1, 8].Value = @"Заявка ДСТЛ";
                worksheet.Cells[1, 9].Value = @"Дата входа план";
                worksheet.Cells[1, 10].Value = @"Дата входа факт";
                worksheet.Cells[1, 11].Value = @"Дата выхода план";
                worksheet.Cells[1, 12].Value = @"Дата входа факт";
                worksheet.Cells[1, 13].Value = @"Дата отгрузки план";
                worksheet.Cells[1, 14].Value = @"Дата отгрузки в ПЭ";
                worksheet.Cells[1, 15].Value = @"Дата отгрузки факт";
                worksheet.Cells[1, 16].Value = @"Дата доставки план";
                worksheet.Cells[1, 17].Value = @"Дата доставки в ПЭ";
                worksheet.Cells[1, 18].Value = @"Дата доставки факт";
                worksheet.Cells[1, 19].Value = @"Stat_Row";
                worksheet.Cells[1, 20].Value = @"Причина срыва в производстве";
                worksheet.Cells[1, 21].Value = @"DayMFG";
                worksheet.Cells[1, 22].Value = @"Причина срыва/переноса отгрузки";
                worksheet.Cells[1, 23].Value = @"DayShip";
                worksheet.Cells[1, 24].Value = @"Причина срыва доставки";
                worksheet.Cells[1, 25].Value = @"Точность доставки %";
                worksheet.Cells[1, 26].Value = @"KPI_stat";
                worksheet.Cells[1, 27].Value = @"CreateDate";
                worksheet.Cells[1, 28].Value = @"distince";
                worksheet.Cells[1, 29].Value = @"KPI_whse";


                using (var range = worksheet.Cells[$"A2:AC{_delivery.Count+1}"])
                {
                    
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.WrapText = true;
                     


                }
                
                for (int i = 0; i < _delivery.Count; i++)
                {
                    worksheet.Cells[$"A{i+2}"].Value = _delivery[i].Site;
                    worksheet.Column(1).Width = 20;
                    worksheet.Column(2).Width = 20;
                    worksheet.Cells[$"B{i+2}"].Value = _delivery[i].CustNum;
                    worksheet.Cells[$"C{i+2}"].Value = _delivery[i].CustSeq;
                    worksheet.Cells[$"D{i+2}"].Value = _delivery[i].CoNum;
                    worksheet.Cells[$"E{i+2}"].Value = _delivery[i].CoLine;
                    worksheet.Cells[$"F{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"F{i+2}"].Value = _delivery[i].DateZay;
                    worksheet.Cells[$"G{i+2}"].Value = _delivery[i].MerchZayNum;
                    worksheet.Cells[$"H{i+2}"].Value = _delivery[i].ShipZayNum;
                    worksheet.Cells[$"I{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"I{i+2}"].Value = _delivery[i].DateMfgPlan;
                    worksheet.Cells[$"J{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"J{i+2}"].Value = _delivery[i].DateMfgFact;
                    worksheet.Cells[$"K{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"K{i+2}"].Value = _delivery[i].DateWhsPlan;
                    worksheet.Cells[$"L{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"L{i+2}"].Value = _delivery[i].DateWhsFact;
                    worksheet.Cells[$"M{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"M{i+2}"].Value = _delivery[i].DateShipPlan;
                    worksheet.Cells[$"N{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"N{i+2}"].Value = _delivery[i].DateShipFact;
                    worksheet.Cells[$"O{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"O{i+2}"].Value = _delivery[i].DateDostPlan;
                    worksheet.Cells[$"P{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"P{i+2}"].Value = _delivery[i].DateDostPor;
                    worksheet.Cells[$"Q{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"Q{i+2}"].Value = _delivery[i].DateDostFact;
                    worksheet.Cells[$"S{i+2}"].Value = _delivery[i].StatRow;
                    worksheet.Cells[$"T{i+2}"].Value = _delivery[i].StatMfg;
                    worksheet.Cells[$"U{i+2}"].Value = _delivery[i].DayMfg;
                    worksheet.Cells[$"V{i+2}"].Value = _delivery[i].StatShip;
                    worksheet.Cells[$"W{i+2}"].Value = _delivery[i].DayShip;
                    worksheet.Cells[$"X{i+2}"].Value = _delivery[i].StatDost;
                    worksheet.Cells[$"Y{i+2}"].Value = _delivery[i].DayDost;
                    worksheet.Cells[$"Z{i+2}"].Value = _delivery[i].KpiStat;
                    worksheet.Cells[$"AA{i+2}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"AA{i+2}"].Value = _delivery[i].CreateDate;
                    worksheet.Cells[$"AB{i+2}"].Value = _delivery[i].Distance;
                    worksheet.Cells[$"AC{i+2}"].Value = _delivery[i].KpiWhse;
                    
                    
                }
                

                
                package.Save(); //Save the workbook.
            }

            
            stream.Position = 0;
            var result = File(stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",sFileName);

            Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
            {
                FileName = sFileName
            }.ToString();
            
            return result;

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