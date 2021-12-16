using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC1
{
    public class Async_Main_Search_DatabaseData
    {
        public string unitOrderNumber { get; set; }
        public string unitPoNumber { get; set; }
        public string unitDnNumber { get; set; }
        public int orderUnitCount { get; set; }
        public int orderTestedUnits { get; set; }
        public string unitFamily { get; set; }
        public string unitTimeCreated { get; set; }
        public string unitTimeUpdated { get; set; }
        public string unitLocation { get; set; }
        public string unitLastStation { get; set; }
        public string unitNextStationName { get; set; }
        public string unitM4U_code { get; set; }
        //--------------------------------------
        public string NextWC { get; set; }
        public string LastWC { get; set; }
        public DataTable TDS_Progress { get; set; }
        public string MacAdressHistory { get; set; }
        public string MacAdressCurrent { get; set; }
        public DataTable dbSnoInfo { get; set; }
        public DataTable dtInternalDisks { get; set; }
    }
}
