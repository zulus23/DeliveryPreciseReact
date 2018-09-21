using Microsoft.AspNetCore.Hosting.Internal;

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
        private const string PRINT_HOME = "Server=DESKTOP-GBBOAS1;Database=SL_Print; User Id=sa;Password=415631234;";        


        public static string GetConnectionString(string enterprise)
        {
            switch (enterprise)
            {
                case "ГОТЭК":
                {
                    return GOTEK;
                    
                }
                case "ПОЛИПАК":
                {
                    return POLYPACK;
                    
                }
                case "ЛИТАР":
                {
                    return LITAR;
                    
                }
                case "ПРИНТ":
                {
                    //return PRINT;
                    return PRINT_HOME;

                }
                case "ЦЕНТР":
                {
                    return CENTER;
                    
                }
                case "СЕВЕР":
                {
                    return SPB;
                }

                default:
                {
                    return "";
                }
            }
        }
        
    }
}