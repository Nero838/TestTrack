//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MC1
//{
//    public class Declare
//    {
//        public static List<string> Get_ServerList()
//        {
//            List<string> ServerListForMap = new List<string>();
//            ServerListForMap.Add("L1");
//            ServerListForMap.Add("L2");
//            ServerListForMap.Add("L3");
//            ServerListForMap.Add("L8");
//            ServerListForMap.Add("LAB");
//            return ServerListForMap;
//        }

//        public static List<string> Get_AllAreaList()
//        {
//            List<string> AreaList = new List<string>();
//            AreaList.Add("Test");
//            AreaList.Add("Logs");
//            AreaList.Add("Arch");
//            return AreaList;
//        }

//        public static List<string> Get_LogTypeList()
//        {
//            List<string> LogTypeList = new List<string>();
//            LogTypeList.Add("Start");
//            LogTypeList.Add("Fail");
//            LogTypeList.Add("Pass");
//            LogTypeList.Add("TDS");
//            LogTypeList.Add("OA3");
//            LogTypeList.Add("Adler");
//            return LogTypeList;        
//        }
//        public static List<string> Get_SaveTypeList()
//        {
//            List<string> SaveTypeList = new List<string>();
//            SaveTypeList.Add("Adler");
//            SaveTypeList.Add("OA3");
//            return SaveTypeList;
//        }

//        public static Dictionary<string,string> Get_LogTypeSuffixDictionary()
//        {
//            Dictionary<string, string> LogTypeSuffixDictionary = new Dictionary<string, string>();
//            LogTypeSuffixDictionary.Add("Start", ".55S");
//            LogTypeSuffixDictionary.Add("Fail", ".55F");
//            LogTypeSuffixDictionary.Add("Pass", ".55P");
//            LogTypeSuffixDictionary.Add("TDS", ".TDS");
//            LogTypeSuffixDictionary.Add("Adler", ".ADL");
//            LogTypeSuffixDictionary.Add("OA3", "-OA30.xml");
//            return LogTypeSuffixDictionary;
//        }

//        public static Dictionary<string, string> Get_IPAdressDictionary()
//        {
//            Dictionary<string, string> IPAdressDictionary = new Dictionary<string, string>();
//            IPAdressDictionary.Add("L1", @"\\10.13.82.2\");
//            IPAdressDictionary.Add("L2", @"\\10.13.82.3\");
//            IPAdressDictionary.Add("L3", @"\\10.13.82.4\");
//            IPAdressDictionary.Add("L8", @"\\10.13.82.8\");
//            IPAdressDictionary.Add("LAB", @"\\10.13.82.200\");
//            return IPAdressDictionary;
//        }

//        public static Dictionary<string, string> Get_LineheadName()
//        {
//            Dictionary<string, string> LineheadName = new Dictionary<string, string>();
//            LineheadName.Add("L1", "Lineahead 1");
//            LineheadName.Add("L2", "Lineahead 2");
//            LineheadName.Add("L3", "Lineahead 3");
//            LineheadName.Add("L8", "Lineahead 8");
//            LineheadName.Add("LAB", "LAB");
//            return LineheadName;
//        }

//        public static Dictionary<string, string> Get_AreasDictionary()
//        {
//            Dictionary<string, string> AreasDictionary = new Dictionary<string, string>();
//            AreasDictionary.Add("Test", @"comm_uut\Testresults\");
//            AreasDictionary.Add("Logs", @"comm_uut\Testresults\LOG\");
//            AreasDictionary.Add("Arch", @"comm_uut\Archivefolder_orderdata\");
//            return AreasDictionary;

//        }

//    }
//}
