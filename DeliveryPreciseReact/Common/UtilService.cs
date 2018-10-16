using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeliveryPreciseReact.Domain;
using DeliveryPreciseReact.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace DeliveryPreciseReact.Common
{
    public class UtilService

    {
        private IDataService _dataService;

        public UtilService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<Stream> OrderDrivingXLSFileStreamResult(ParamsCalculateKpi data)
        {
            var stream = new MemoryStream();

            
            
            

            List<DeliveryRecord> _delivery = await _dataService.GetDeliveryRecordsAsync(data);


            ExcelPackage package;
            using (package = new ExcelPackage(stream))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KPI");
                using (var range = worksheet.Cells["A1:AA2"])
                {
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.WrapText = true;
                }

                //First add the headers
                worksheet.Cells["A1:A2"].Merge = true;
                worksheet.Cells[1, 1].Value = @"№";
                worksheet.Cells["B1:B2"].Merge = true;
                worksheet.Cells[1, 2].Value = @"Клиент";
                worksheet.Cells["C1:C2"].Merge = true;
                worksheet.Cells[1, 3].Value = @"Грузополучатель";
                worksheet.Cells["D1:D2"].Merge = true;
                worksheet.Cells[1, 4].Value = @"Заказ";
                worksheet.Cells["E1:E2"].Merge = true;
                worksheet.Cells[1, 5].Value = @"Строка";
                worksheet.Cells["F1:F2"].Merge = true;
                worksheet.Cells[1, 6].Value = @"Дата заявки";
                worksheet.Cells["G1:G2"].Merge = true;
                worksheet.Cells[1, 7].Value = @"№ заявки продаж";
                worksheet.Cells["H1:H2"].Merge = true;
                worksheet.Cells[1, 8].Value = @"Заявка ДСТЛ";
                worksheet.Cells["I1:I2"].Merge = true;
                worksheet.Cells[1, 9].Value = @"Дата входа план";
                worksheet.Cells["J1:J2"].Merge = true;
                worksheet.Cells[1, 10].Value = @"Дата входа факт";
                worksheet.Cells["K1:K2"].Merge = true;
                worksheet.Cells[1, 11].Value = @"Дата выхода план";
                worksheet.Cells["L1:L2"].Merge = true;
                worksheet.Cells[1, 12].Value = @"Дата входа факт";
                
                worksheet.Cells["M1:M2"].Merge = true;
                worksheet.Cells[1, 13].Value = @"Дата отгрузки план";
                
                worksheet.Cells["N1:N2"].Merge = true;
                worksheet.Cells[1, 14].Value = @"Дата отгрузки в ПЭ";
                
                worksheet.Cells["O1:O2"].Merge = true;
                worksheet.Cells[1, 15].Value = @"Дата отгрузки факт";
                
                worksheet.Cells["P1:P2"].Merge = true;
                worksheet.Cells[1, 16].Value = @"Дата доставки план";
                
                worksheet.Cells["Q1:Q2"].Merge = true;
                worksheet.Cells[1, 17].Value = @"Дата доставки в ПЭ";
                
                worksheet.Cells["R1:R2"].Merge = true;
                worksheet.Cells[1, 18].Value = @"Дата доставки факт";
                
                worksheet.Cells["S1:T1"].Merge = true;
                worksheet.Cells["S1:T1"].Value = @"Производство";
                worksheet.Cells[2, 19].Value = @"Причина срыва в производстве";
                worksheet.Cells[2, 20].Value = @"Количество дней";
                worksheet.Cells["U1:V1"].Merge = true;
                worksheet.Cells["U1:V1"].Value = "Отгрузка";
                worksheet.Cells[2, 21].Value = @"Причина срыва/переноса отгрузки";
                worksheet.Cells[2, 22].Value = @"Количество дней";
                worksheet.Cells["W1:X1"].Merge = true;
                worksheet.Cells["W1:X1"].Value = "Доставка";
                worksheet.Cells[2, 23].Value = @"Причина срыва доставки";
                worksheet.Cells[2, 24].Value = @"Количество дней";
                /*worksheet.Cells["Y1:Y2"].Merge = true;
                worksheet.Cells[1, 25].Value = @"Точность доставки %";*/
                worksheet.Cells["Y1:Y2"].Merge = true;
                worksheet.Cells[1, 25].Value = @"Точность доставки %";
                /*worksheet.Cells["AA1:AA2"].Merge = true;
                worksheet.Cells[1, 27].Value = @"CreateDate";*/
                worksheet.Cells["Z1:Z2"].Merge = true;
                worksheet.Cells[1, 26].Value = @"Расстояние, км.";
                worksheet.Cells["AA1:AA2"].Merge = true;
                worksheet.Cells[1, 27].Value = @"Точность поступления на склад, %";


                using (var range = worksheet.Cells[$"A2:AA{_delivery.Count + 2}"])
                {
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    range.Style.WrapText = true;
                }

                int _beginRow = 3;

                for (int i = 0; i < _delivery.Count; i++)
                {
                    worksheet.Column(1).Width = 5;
                    worksheet.Cells[$"A{i + _beginRow}"].Value = i + 1;
                    worksheet.Column(2).Width = 50;
                    worksheet.Cells[$"B{i + _beginRow}"].Value = _delivery[i].NameCustomer;
                    worksheet.Column(3).Width = 55;
                    worksheet.Cells[$"C{i + _beginRow}"].Value = _delivery[i].AddressCustomer;
                    worksheet.Column(4).Width = 30;
                    worksheet.Cells[$"D{i + _beginRow}"].Value = _delivery[i].CoNum;
                    worksheet.Column(5).Width = 7;
                    worksheet.Cells[$"E{i + _beginRow}"].Value = _delivery[i].CoLine;
                    worksheet.Column(6).Width = 13;
                    worksheet.Cells[$"F{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"F{i + _beginRow}"].Value = _delivery[i].DateZay;
                    worksheet.Column(7).Width = 25;
                    worksheet.Cells[$"G{i + _beginRow}"].Value = _delivery[i].MerchZayNum;
                    worksheet.Column(8).Width = 28;
                    worksheet.Cells[$"H{i + _beginRow}"].Value = _delivery[i].ShipZayNum;
                    worksheet.Column(9).Width = 13;
                    worksheet.Cells[$"I{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"I{i + _beginRow}"].Value = _delivery[i].DateMfgPlan;
                    worksheet.Column(10).Width = 13;
                    worksheet.Cells[$"J{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"J{i + _beginRow}"].Value = _delivery[i].DateMfgFact;
                    worksheet.Column(11).Width = 13;
                    worksheet.Cells[$"K{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"K{i + _beginRow}"].Value = _delivery[i].DateWhsPlan;
                    worksheet.Column(12).Width = 13;
                    worksheet.Cells[$"L{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"L{i + _beginRow}"].Value = _delivery[i].DateWhsFact;
                    
                    worksheet.Column(13).Width = 13;
                    worksheet.Cells[$"M{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"M{i + _beginRow}"].Value = _delivery[i].DateShipPlan;
                    
                    worksheet.Column(14).Width = 13;
                    worksheet.Cells[$"N{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"N{i + _beginRow}"].Value = _delivery[i].DateShipZay;
                    worksheet.Column(15).Width = 13;
                    worksheet.Cells[$"O{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"O{i + _beginRow}"].Value = _delivery[i].DateShipFact;
                    
                    worksheet.Column(16).Width = 13;
                    worksheet.Cells[$"P{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"P{i + _beginRow}"].Value = _delivery[i].DateDostPlan;
                    worksheet.Column(17).Width = 13;
                    worksheet.Cells[$"Q{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"Q{i + _beginRow}"].Value = _delivery[i].DateDostPor;
                    worksheet.Column(18).Width = 13;
                    worksheet.Cells[$"R{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Cells[$"R{i + _beginRow}"].Value = _delivery[i].DateDostFact;
                    worksheet.Column(19).Width = 20;
                    worksheet.Cells[$"S{i + _beginRow}"].Value = _delivery[i].StatMfg;
                    worksheet.Column(20).Width = 20;
                    worksheet.Cells[$"T{i + _beginRow}"].Value = _delivery[i].StatRow;
                    worksheet.Column(21).Width = 13;
                    worksheet.Cells[$"U{i + _beginRow}"].Value = _delivery[i].StatShip;
                    worksheet.Column(22).Width = 20;
                    worksheet.Cells[$"V{i + _beginRow}"].Value = _delivery[i].DayMfg;
                    worksheet.Column(23).Width = 13;
                    worksheet.Cells[$"W{i + _beginRow}"].Value = _delivery[i].StatDost;
                    worksheet.Column(24).Width = 20;
                    worksheet.Cells[$"X{i + _beginRow}"].Value = _delivery[i].DayShip;
                    /*worksheet.Column(25).Width = 13;
                    worksheet.Cells[$"Y{i + _beginRow}"].Value = _delivery[i].DayDost;*/
                    /*worksheet.Column(26).Width = 20;
                    worksheet.Cells[$"Z{i + _beginRow}"].Value = _delivery[i].KpiStat;
                    worksheet.Column(27).Width = 13;
                    worksheet.Cells[$"AA{i + _beginRow}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    worksheet.Column(28).Width = 13;
                    worksheet.Cells[$"AA{i + _beginRow}"].Value = _delivery[i].CreateDate;
                    worksheet.Column(29).Width = 13;
                    worksheet.Cells[$"AB{i + _beginRow}"].Value = _delivery[i].Distance;
                    worksheet.Column(30).Width = 13;
                    worksheet.Cells[$"AC{i + _beginRow}"].Value = _delivery[i].KpiWhse;*/
                    worksheet.Column(25).Width = 13;
                    worksheet.Cells[$"Y{i + _beginRow}"].Value = _delivery[i].KpiStat;
                    worksheet.Column(26).Width = 13;
                    worksheet.Cells[$"Z{i + _beginRow}"].Value = _delivery[i].Distance;
                    worksheet.Column(27).Width = 13;
                    worksheet.Cells[$"AA{i + _beginRow}"].Value = _delivery[i].KpiWhse;
                }


                package.Save(); //Save the workbook.
            }


            
            
            return stream;
        }


        public async Task<Stream> KpiXLSFileStreamResult(ParamsCalculateKpi data)
        {
            var stream = new MemoryStream();
            ExcelPackage package;
            int startByRow = 4;
            int startByColumn = 2;
            List<PreciseDelivery> _kpis =  _dataService.CalculateKpi(data);
            int countKpi = _kpis.Count;

            PreciseDelivery delivery =  _kpis.First(e => e.Detail.Count == _kpis.Max(p => p.Detail.Count));
            
            
            using (package = new ExcelPackage(stream))
            {
             
              
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KPI");
                using (var range = worksheet.Cells[startByRow, startByColumn,startByRow+1,
                    startByColumn + countKpi+ 1])
                {
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.WrapText = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(238,236,225));

                }
                /*  ===========================  Caption ========================================= */
                worksheet.Cells[startByRow - 2, startByColumn + 1].Value = $"Клиент {data.Customer.Name}";
                worksheet.Cells[startByRow - 1, startByColumn + 1].Value = 
                                $"Период с {data.RangeDate.Start} по  {data.RangeDate.End}";
                /*  ------------------------------------------------------------------------------ */
                
                worksheet.Cells[startByRow, startByColumn,startByRow + 1, startByColumn].Merge = true;
                worksheet.Cells[startByRow, startByColumn].Value = @"Месяц";
                worksheet.Column(startByColumn).Width = 20;
                
                worksheet.Cells[startByRow, startByColumn + 1,startByRow + 1, startByColumn + 1].Merge = true;
                worksheet.Cells[startByRow, startByColumn + 1].Value = @"Показатель";
                worksheet.Column(startByColumn + 1).Width = 20;
                
                worksheet.Cells[startByRow, startByColumn + 2, startByRow, startByColumn + countKpi + 1].Merge = true;
                worksheet.Cells[startByRow, startByColumn + 2, startByRow, startByColumn + countKpi + 1].Value =
                    @"KPI";
                Dictionary<int,string> dictionary = new Dictionary<int, string>();
                for (int i = startByColumn+2; i < startByColumn+countKpi+2; i++)
                {
                    worksheet.Cells[startByRow+1, i].Value = _kpis[i-(startByColumn+2)].Description;
                    dictionary.Add(i,_kpis[i-(startByColumn+2)].Description);
                    
                    worksheet.Column(i).Width = 25;
                }

                int beginKpiValue = startByRow + 2;
                int lastRow = beginKpiValue; 
                foreach (var kpi in _kpis)
                {
                    int k = dictionary.FirstOrDefault(x => x.Value.Equals(kpi.Description)).Key;
                    beginKpiValue = startByRow + 2;
                    lastRow = beginKpiValue;
                    foreach (var dev in kpi.Detail)
                    {
                        worksheet.Cells[beginKpiValue , startByColumn].Value = new DateTime(dev.Year,dev.Month,1);
                        worksheet.Cells[beginKpiValue , startByColumn].Style.Numberformat.Format = "MMMM";
                        worksheet.Cells[beginKpiValue, startByColumn,beginKpiValue+3,startByColumn].Merge = true;
                        worksheet.Cells[beginKpiValue , startByColumn+1].Value = "Цель";
                        worksheet.Cells[beginKpiValue+1, startByColumn+1].Value = "Факт";
                        worksheet.Cells[beginKpiValue+2, startByColumn+1].Value = "Откл";
                        worksheet.Cells[beginKpiValue+3 , startByColumn+1].Value = "Заказов";

                        worksheet.Cells[beginKpiValue , k].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[beginKpiValue , k].Value = dev.Target;
                        worksheet.Cells[beginKpiValue+1 , k].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[beginKpiValue+1, k].Value = dev.Fact;
                        worksheet.Cells[beginKpiValue+2 , k].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[beginKpiValue+2, k].Value = dev.Deviation;
                        worksheet.Cells[beginKpiValue+3 , k].Style.Numberformat.Format = "0";
                        worksheet.Cells[beginKpiValue+3 , k].Value = dev.CountOrder;
 
                        beginKpiValue += 4;
                        lastRow += beginKpiValue;
                    }
                    
                }
                
                /*int k =   dictionary.FirstOrDefault(x => x.Value.Equals(delivery.Description)).Key;    
                int beginKpiValue = startByRow + 2; 
                
                foreach (var dev in delivery.Detail)
                {
                    
                        worksheet.Cells[beginKpiValue , startByColumn].Value = dev.Date;
                        worksheet.Cells[beginKpiValue , startByColumn].Style.Numberformat.Format = "MMMM";
                        worksheet.Cells[beginKpiValue , startByColumn+1].Value = "Цель";
                        worksheet.Cells[beginKpiValue+1, startByColumn+1].Value = "Факт";
                        worksheet.Cells[beginKpiValue+2, startByColumn+1].Value = "Откл";
                        worksheet.Cells[beginKpiValue+3 , startByColumn+1].Value = "Заказов";
                        worksheet.Cells[beginKpiValue , k].Value = dev.Target;
                        worksheet.Cells[beginKpiValue+1, k].Value = dev.Fact;
                        worksheet.Cells[beginKpiValue+2, k].Value = dev.Deviation;
                        worksheet.Cells[beginKpiValue+3 , k].Value = dev.CountOrder;
 
                    beginKpiValue += 4;



                }*/
                

                using (var range = worksheet.Cells[startByRow + 2, startByColumn,beginKpiValue-1,
                    startByColumn + countKpi+ 1])
                {
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.WrapText = true;
                }                
                
                package.Save();

            }

            return stream;
        }
    }
}