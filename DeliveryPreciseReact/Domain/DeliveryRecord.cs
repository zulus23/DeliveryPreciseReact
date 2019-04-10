using System;

namespace DeliveryPreciseReact.Domain
{
    public class DeliveryRecord
    {
        private string _site;
        private string _custNum;
        private string _custSeq;
        private string _nameCustomer;
        private string _addressCustomer;     
        private string _coNum;
        private string _coLine;
        private DateTime? _dateZay;
        private string _merchZayNum;
        private string _shipZayNum;

        private DateTime? _dateMFGPlan;
        private DateTime? _dateMFGFact;
        private DateTime? _dateWHSPlan;
        private DateTime? _dateWHSFact;
        private DateTime? _dateShipPlan;
        private DateTime? _dateShipZay;
        private DateTime? _dateShipFact;
        private DateTime? _dateDostPlan;
        private DateTime? _dateDostPor;
        private DateTime? _dateDostFact;
        private int _stat_Row;
        private string _statMFG;
        private int _dayMFG;
        private string _statShip;
        private int _dayShip;
        private string _statDost;
        private int _dayDost;
        private double _kpiStat;
        private string _createBy;
        private DateTime? _createDate;
        private int _distance;
        private double _kpiWhse;
        private string _plantShip;
        private string _poNum;
        private string _job;
        private string _vidOtgr;
        private int _inList; //filed stat_kpi_list

        public int InList
        {
            get => _inList;
            set => _inList = value;
        }


        public string Site
        {
            get => _site;
            set => _site = value;
        }

        public string CustNum
        {
            get => _custNum;
            set => _custNum = value;
        }

        public string CustSeq
        {
            get => _custSeq;
            set => _custSeq = value;
        }

        public string NameCustomer
        {
            get => _nameCustomer;
            set => _nameCustomer = value;
        }

        public string AddressCustomer
        {
            get => _addressCustomer;
            set => _addressCustomer = value;
        }

        public string CoNum
        {
            get => _coNum;
            set => _coNum = value;
        }

        public string CoLine
        {
            get => _coLine;
            set => _coLine = value;
        }

      

        public string MerchZayNum
        {
            get => _merchZayNum;
            set => _merchZayNum = value;
        }

        public string ShipZayNum
        {
            get => _shipZayNum;
            set => _shipZayNum = value;
        }

        public DateTime? DateZay
        {
            get => _dateZay;
            set => _dateZay = value;
        }

        public DateTime? DateMfgPlan
        {
            get => _dateMFGPlan;
            set => _dateMFGPlan = value;
        }

        public DateTime? DateMfgFact
        {
            get => _dateMFGFact;
            set => _dateMFGFact = value;
        }

        public DateTime? DateWhsPlan
        {
            get => _dateWHSPlan;
            set => _dateWHSPlan = value;
        }

        public DateTime? DateWhsFact
        {
            get => _dateWHSFact;
            set => _dateWHSFact = value;
        }

        public DateTime? DateShipPlan
        {
            get => _dateShipPlan;
            set => _dateShipPlan = value;
        }

        public DateTime? DateShipZay
        {
            get => _dateShipZay;
            set => _dateShipZay = value;
        }

        public DateTime? DateShipFact
        {
            get => _dateShipFact;
            set => _dateShipFact = value;
        }

        public DateTime? DateDostPlan
        {
            get => _dateDostPlan;
            set => _dateDostPlan = value;
        }

        public DateTime? DateDostPor
        {
            get => _dateDostPor;
            set => _dateDostPor = value;
        }

        public DateTime? DateDostFact
        {
            get => _dateDostFact;
            set => _dateDostFact = value;
        }

       

        public DateTime? CreateDate
        {
            get => _createDate;
            set => _createDate = value;
        }


        public int StatRow
        {
            get => _stat_Row;
            set => _stat_Row = value;
        }

        public string StatMfg
        {
            get => _statMFG;
            set => _statMFG = value;
        }

        public int DayMfg
        {
            get => _dayMFG;
            set => _dayMFG = value;
        }

        public string StatShip
        {
            get => _statShip;
            set => _statShip = value;
        }

        public int DayShip
        {
            get => _dayShip;
            set => _dayShip = value;
        }

        public string StatDost
        {
            get => _statDost;
            set => _statDost = value;
        }

        public int DayDost
        {
            get => _dayDost;
            set => _dayDost = value;
        }

        public double KpiStat
        {
            get => _kpiStat;
            set => _kpiStat = value;
        }

        public string CreateBy
        {
            get => _createBy;
            set => _createBy = value;
        }

      
        public int Distance
        {
            get => _distance;
            set => _distance = value;
        }

        public double KpiWhse
        {
            get => _kpiWhse;
            set => _kpiWhse = value;
        }

        public string PlantShip
        {
            get => _plantShip;
            set => _plantShip = value;
        }

 
        public string PoNum
        {
            get => _poNum;
            set => _poNum = value;
        }

        public string Job
        {
            get => _job;
            set => _job = value;
        }

        public string VidOtgr
        {
            get => _vidOtgr;
            set => _vidOtgr = value;
        }
    }
}