using System;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace DeliveryPreciseReact.Common
{
    public class DataConnection
    {
        private const string GOTEK = "Server=SRV-SLDB;Database=SL_GOTEK; User Id=report;Password=report;";
        private const string PRINT = "Server=SRV-SLDB;Database=SL_PRINT; User Id=report;Password=report;";
        private const string LITAR = "Server=SRV-SLDB;Database=SL_LITAR; User Id=report;Password=report;";
        private const string POLYPACK = "Server=SRV-SLDB;Database=SL_POLYPACK; User Id=report;Password=report;";
        private const string CENTER = "Server=SRV-SLDB;Database=SL_CENTER; User Id=report;Password=report;";
        private const string SPB = "Server=SRV-SLDB;Database=SL_SPB; User Id=report;Password=report;";
                


        public static string GetConnectionString(string enterprise)
        {
            switch (enterprise)
            {
                case EnterpriseConst.GOTEK:
                {
                    return GOTEK;
                    
                }
                case EnterpriseConst.POLYPACK:
                {
                    return POLYPACK;
                    
                }
                case EnterpriseConst.LITAR:
                {
                    return LITAR;
                    
                }
                case EnterpriseConst.PRINT:
                {
                    return PRINT;
                    /*return PRINT_HOME;*/

                }
                case EnterpriseConst.CENTER:
                {
                    return CENTER;
                    
                }
                case EnterpriseConst.SPB:
                {
                    return SPB;
                }

                default:
                {
                    return "";
                }
            }
        }

        public static string GetNameDbInGotekGroup(string myNameDb)
        {
            switch (myNameDb)
            {
                case EnterpriseConst.GOTEK:
                {
                    return "GOTEK";
                    
                }
                case EnterpriseConst.POLYPACK:
                {
                    return "Polypack";
                    
                }
                case EnterpriseConst.LITAR:
                {
                    return "Litar";
                    
                }
                case EnterpriseConst.PRINT:
                {
                    return "Print";
                }
                case EnterpriseConst.CENTER:
                {
                    return "Center";
                    
                }
                case EnterpriseConst.SPB:
                {
                    return "SPB";
                }

                default:
                {
                    return "";
                }
            }
        }
        
        
    }
}