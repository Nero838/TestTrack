using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MC1
{
    public abstract class ListNDictiomary : SQL_Tracker
    {
        //property private set
        public string userName { get; set; }
        public string appVersion { get; private set; }
        public string accessLevel { get; private set; }
        //property public set
        public string serverUserName { get; set; }
        public string appDomain { get; set; }
        public Dictionary<string, string> lineheadLoginPasswords { get; set; } = new Dictionary<string, string>();
        //-------
        public bool autoRefreshActive { get; set; }
        public bool autoRefreshAdmin { get; set; }
        public bool LineheadToLines { get; set; }
        public bool fistSaveWarning { get; set; }
        public bool needUpdate { get; set; }
        public bool firstRetestWarning { get; set; }
        public DataTable ModelsDataTable { get; internal set; }
        //field

        public readonly List<string> ListServers = new List<string>();
        public readonly List<string> ListLogTypes = new List<string>();
        public readonly List<string> ListLogTypesSave = new List<string>();
        public readonly List<string> ListAreas = new List<string>();
        public readonly List<string> ListLogsAreas = new List<string>();
        public List<string> ListResultPaths = new List<string>();
        public readonly List<string> ListTypeOD = new List<string>();
        public readonly List<string> ListLogTypesGather = new List<string>();
        public readonly List<string> ListOutputGather = new List<string>();
        public readonly List<string> ListSaverTechs = new List<string>();
        public readonly List<string> PSEXEC_files = new List<string>();
        public readonly List<string> Updater_files = new List<string>();
        public readonly List<string> ListModelPrefix = new List<string>();


        public readonly List<string> ListBaysButtons = new List<string> ();
        public List<string> ListBaysLine3 = new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08" };
        public List<string> ListBaysLine4 = new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08" };
        public readonly List<string> ListLinesBW = new List<string>();

        public readonly List<List<string>> List_SQL_Lines = new List<List<string>>();
        

        public readonly Dictionary<string, string> DictLogTypeSuffix = new Dictionary<string, string>();
        public readonly Dictionary<string, string> DictServerIPs = new Dictionary<string, string>();
        public readonly Dictionary<string, string> DictServerNames = new Dictionary<string, string>();
        public readonly Dictionary<string, string> DictServerLines = new Dictionary<string, string>();
        public readonly Dictionary<string, string> DictAreaPaths = new Dictionary<string, string>();
        public readonly Dictionary<string, string> DictAreaPathsForGather = new Dictionary<string, string>();
        //public Dictionary<string, bool> DictLabelClickable = new Dictionary<string, bool>();
        public readonly Dictionary<string, string> DictODTypeSuffix = new Dictionary<string, string>();
        public readonly Dictionary<string, string> DictODTypeWhole = new Dictionary<string, string>();
        public readonly Dictionary<string, string> DictSnModel = new Dictionary<string, string>();
        public readonly Dictionary<string, string> DictSnMB = new Dictionary<string, string>();
        public readonly Dictionary<string, string> DictServerIPsClear = new Dictionary<string, string>();

        //public List<string> ListServersWorking = new List<string>();

        //constructor
        public ListNDictiomary(string user, string lvl, string ver)

        {
            GetLineheadConnectionData();

            if (String.IsNullOrEmpty(user) || String.IsNullOrEmpty(ver) || String.IsNullOrEmpty(lvl))
            {
                throw new ArgumentNullException("arguments null or empty");
            }
            else
            {
                userName = user;
                appVersion = ver;
                accessLevel = lvl;
            }

            LineheadToLines = true;
            fistSaveWarning = true;
            firstRetestWarning = true;
            needUpdate = true;

            //ListServers.Add("L1");
            //ListServers.Add("L2");
            //ListServers.Add("L3");
            //ListServers.Add("LAB");
            //ListServers.Add("L8");
            //ListServers.Add("L10");

            DataTable activeLineheadServers = new DataTable();
            activeLineheadServers = GetActiveLineheadServers();
            foreach (DataRow dr in activeLineheadServers.Rows)
            {
                ListServers.Add(dr[0].ToString());
            }

            appDomain = AppDomain.CurrentDomain.BaseDirectory;

            ListAreas.Add("Test");
            ListAreas.Add("Logs");
            ListAreas.Add("Arch");

            ListLogsAreas.Add("Logs");
            ListLogsAreas.Add("Arch");

            ListLogTypes.Add("Start");
            ListLogTypes.Add("Fail");
            ListLogTypes.Add("Pass");
            //ListLogTypes.Add("TDS");
            ListLogTypes.Add("OA3");
            ListLogTypes.Add("Adler");
            ListLogTypes.Add("TDS");
            ListLogTypes.Add("60S");
            ListLogTypes.Add("60F");
            ListLogTypes.Add("60P");
            ListLogTypes.Add("60T");

            ListLogTypesSave.Add("OA3");
            ListLogTypesSave.Add("Adler");

            ListTypeOD.Add("UUT");
            ListTypeOD.Add("PDT");
            ListTypeOD.Add("SWL");
            ListTypeOD.Add("OA30");
            ListTypeOD.Add("Head");
            ListTypeOD.Add("Json");
            ListTypeOD.Add("MFIs");

            ListLogTypesGather.Add(".ADL");
            ListLogTypesGather.Add(".TDS");
            ListLogTypesGather.Add(".55F");

            ListOutputGather.Add(".xlsx");
            ListOutputGather.Add(".txt");

            DataTable dtTTs = new DataTable();
            dtTTs = GetTT_Savers();


            foreach (DataRow dr in dtTTs.Rows)
            {
                ListSaverTechs.Add(dr[0].ToString());
            }
            var test = String.Join(",", ListSaverTechs);
            //ListSaverTechs.Add("icz198070");
            //ListSaverTechs.Add("Icz198070");
            //ListSaverTechs.Add("ICZ198070");
            //ListSaverTechs.Add("icz198024");
            //ListSaverTechs.Add("Icz198024");
            //ListSaverTechs.Add("ICZ198024");
            //ListSaverTechs.Add("ICZ208070");
            //ListSaverTechs.Add("ICZ198081");
            //ListSaverTechs.Add("ICZ208270");


            //DataTable testTable = new DataTable();
            //testTable.Columns.Add("Column1", typeof(string));
            //testTable.Columns.Add("Column2", typeof(string));
            //testTable.Columns.Add("Column3", typeof(string));
            //testTable.Rows.Add("A1", "A2", "A3");
            //testTable.Rows.Add("B1", "B2", "B3");
            //testTable.Rows.Add("C1", "C2", "A3");
            //var testDict = new Dictionary<string, string>();
            ////testDict = GetDict(testTable);
            //testDict = testTable.AsEnumerable()
            //  .ToDictionary<DataRow, string, string>(row => row.Field<string>(1),
            //                            row => row.Field<string>(2));

            //string testString = String.Join(",", testDict);

            PSEXEC_files.Add("PsExec.exe");
            PSEXEC_files.Add("PsExec64.exe");
            PSEXEC_files.Add("psfile.exe");
            PSEXEC_files.Add("psfile64.exe");
            PSEXEC_files.Add("PsGetsid.exe");
            PSEXEC_files.Add("PsGetsid64.exe");
            PSEXEC_files.Add("PsInfo.exe");
            PSEXEC_files.Add("PsInfo64.exe");
            PSEXEC_files.Add("pskill.exe");
            PSEXEC_files.Add("pskill64.exe");
            PSEXEC_files.Add("pslist.exe");
            PSEXEC_files.Add("pslist64.exe");
            PSEXEC_files.Add("PsLoggedon.exe");
            PSEXEC_files.Add("PsLoggedon64.exe");
            PSEXEC_files.Add("psloglist.exe");
            PSEXEC_files.Add("psloglist64.exe");
            PSEXEC_files.Add("pspasswd.exe");
            PSEXEC_files.Add("pspasswd64.exe");
            PSEXEC_files.Add("psping.exe");
            PSEXEC_files.Add("psping64.exe");
            PSEXEC_files.Add("PsService.exe");
            PSEXEC_files.Add("PsService64.exe");
            PSEXEC_files.Add("psshutdown.exe");
            PSEXEC_files.Add("pssuspend.exe");
            PSEXEC_files.Add("pssuspend64.exe");
            PSEXEC_files.Add("Pstools.chm");

            Updater_files.Add("Updater.exe");
            Updater_files.Add("Updater.pdb");
            Updater_files.Add("clrcompression.dll");
            Updater_files.Add("clrjit.dll");
            Updater_files.Add("coreclr.dll");
            Updater_files.Add("mscordaccore.dll");

            //SQL lists:
            //ListLinesBW.Add("LINE1");
            //ListLinesBW.Add("LINE2");
            ListLinesBW.Add("LINE3");
            //ListLinesBW.Add("LINE4");
            //ListLinesBW.Add("LINE5");
            //ListLinesBW.Add("LINE6");
            //ListLinesBW.Add("LINE7");
            ListLinesBW.Add("LINE4");
            List_SQL_Lines.Add(new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08" });
            List_SQL_Lines.Add(new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08" });

            DictLogTypeSuffix.Add("Start", ".55S");
            DictLogTypeSuffix.Add("Fail", ".55F");
            DictLogTypeSuffix.Add("Pass", ".55P");
            DictLogTypeSuffix.Add("Adler", ".ADL");
            DictLogTypeSuffix.Add("OA3", "-OA30.xml");
            DictLogTypeSuffix.Add("TDS", ".TDS");
            DictLogTypeSuffix.Add("60S", ".60S");
            DictLogTypeSuffix.Add("60F", ".60F");
            DictLogTypeSuffix.Add("60P", ".60P");
            DictLogTypeSuffix.Add("60T", ".60T");

            DictServerIPs.Add("L1", @"\\10.13.82.1\");
            DictServerIPs.Add("L2", @"\\10.13.82.2\");
            DictServerIPs.Add("L3", @"\\10.13.82.3\");
            DictServerIPs.Add("L4", @"\\10.13.82.4\");
            DictServerIPs.Add("L5", @"\\10.13.82.5\");
            DictServerIPs.Add("L6", @"\\10.13.82.6\");

            DictServerIPsClear.Add("L1", "10.13.82.1");
            DictServerIPsClear.Add("L2", "10.13.82.2");
            DictServerIPsClear.Add("L3", "10.13.82.3");
            DictServerIPsClear.Add("L4", "10.13.82.4");
            DictServerIPsClear.Add("L5", "10.13.82.5");
            DictServerIPsClear.Add("L6", "10.13.82.6");

            DictServerNames.Add("L1", "Lineahead 1");
            DictServerNames.Add("L2", "Lineahead 2");
            DictServerNames.Add("L3", "Lineahead 3");
            DictServerNames.Add("L4", "Lineahead 4");
            DictServerNames.Add("L5", "Lineahead 5");
            DictServerNames.Add("L6", "Lineahead 6");

            //DictServerLines.Add("L1", "Line 1");
            //DictServerLines.Add("L2", "Line 2/3");
            //DictServerLines.Add("L3", "Line 4/7");
            //DictServerLines.Add("LAB", "Line 5/6");
            //DictServerLines.Add("L8", "Line 8/OBA/PRI");
            //DictServerLines.Add("L10", "Line Pri");

            DictAreaPaths.Add("Test", @"comm_uut\Testresults\");
            DictAreaPaths.Add("Logs", @"comm_uut\Testresults\LOG\");
            DictAreaPaths.Add("Arch", @"comm_uut\Archivefolder_orderdata\");

            DictAreaPathsForGather.Add("Test", @"comm_uut\Testresults");
            DictAreaPathsForGather.Add("Logs", @"comm_uut\Testresults\LOG");
            DictAreaPathsForGather.Add("Arch", @"comm_uut\Archivefolder_orderdata");

            DictODTypeSuffix.Add("UUT", ".scd");
            DictODTypeSuffix.Add("PDT", ".PDT");
            DictODTypeSuffix.Add("SWL", ".SWL");
            DictODTypeSuffix.Add("OA30", "xml");
            DictODTypeSuffix.Add("Head", ".scd");
            DictODTypeSuffix.Add("Json", ".json");
            DictODTypeSuffix.Add("MFIs", ".cmd");

            DictODTypeWhole.Add("UUT", "UUTScan.scd");
            DictODTypeWhole.Add("PDT", "PDTORDER.PDT");
            DictODTypeWhole.Add("OA30", "OA30_key.xml");
            DictODTypeWhole.Add("Head", "Header.scd");
            DictODTypeWhole.Add("Json", "HDDSerNr.json");
            DictODTypeWhole.Add("MFIs", "MFIShipmentSetting.cmd");

            //vyplni DictSnModel a DictSnMB z databaze misto hardcoded:
            ModelsDataTable = new DataTable();
            ModelsDataTable = GetModelData();

            //Model data LVL2: Dict from database Datatable
            DictSnModel = ModelsDataTable.AsEnumerable()
              .ToDictionary<DataRow, string, string>(row => row.Field<string>(1),
                                        row => row.Field<string>(2));

            DictSnMB = ModelsDataTable.AsEnumerable()
              .ToDictionary<DataRow, string, string>(row => row.Field<string>(1),
                                        row => row.Field<string>(3));

            //Model data LVL3: LINQ:
            //1. Pomoci .AsEnumerable() můžu prochazet radky
            //2. Vymezim radek, ktery hledam
            //3. Vytahnu z radku presnou promennou
            //Pouzito do funkce GetdataFromModelTable
            //ALTERNATIVA KDYZ NE PRES LINQ -> ListModelPrefix = ModelsDataTable.AsEnumerable().Select(x => x[1].ToString()).ToList();

            ListModelPrefix = ModelsDataTable.AsEnumerable()
            .Select(r => r.Field<string>("Prefix"))
            .ToList();
            //ALTERNATIVA KDYZ NE PRES LINQ -> ListModelPrefix = ModelsDataTable.AsEnumerable().Select(x => x[1].ToString()).ToList();

            //var dataRow = ModelsDataTable.AsEnumerable().Where(x => x.Field<string>("Prefix") == "EIAU").FirstOrDefault();
            //string testtt = dataRow["MB_SN"].ToString();
            //Pouzito do funkce GetdataFromModelTable

            //Model data LVL1: hardcoded:
            //DictSnModel.Add("EIAA", "Celsius R970A");
            //DictSnModel.Add("EIAB", "Celsius R970A Power");
            //DictSnModel.Add("EIAH", "Celsius R970B");
            //DictSnModel.Add("EIAJ", "Celsius R970B Power");
            //DictSnModel.Add("EIAG", "Esprimo K558/24");
            //DictSnModel.Add("EIAK", "Celsius J580");
            //DictSnModel.Add("EIAL", "Celsius C780");
            //DictSnModel.Add("EIAM", "Celsius C780 Power");
            //DictSnModel.Add("EIBV", "Esprimo Q940");
            //DictSnModel.Add("EIAC", "Esprimo Q9010");
            //DictSnModel.Add("EIBU", "Futro S940");
            //DictSnModel.Add("EIBX", "Futro S740");
            //DictSnModel.Add("EIBW", "Futro S540");
            //DictSnModel.Add("EIAF", "Futro S9010");
            //DictSnModel.Add("EIAE", "Futro S7010");
            //DictSnModel.Add("EIAD", "Futro S5010");
            //DictSnModel.Add("EIBB", "Esprimo Q958");
            //DictSnModel.Add("EIBC", "Esprimo Q958/MRE");
            //DictSnModel.Add("EIBA", "Esprimo Q558");
            //DictSnModel.Add("EIAQ", "Celsius M770");
            //DictSnModel.Add("EIAR", "Celsius M770 Power");
            //DictSnModel.Add("EIAS", "Celsius M770X");
            //DictSnModel.Add("EIAT", "Celsius M770X Power");
            //DictSnModel.Add("EIAU", "Celsius M7010");
            //DictSnModel.Add("EIAV", "Celsius M7010 Power");
            //DictSnModel.Add("EIAW", "Celsius M7010X");
            //DictSnModel.Add("EIAX", "Celsius M7010X Power");
            //DictSnModel.Add("EIBM", "Esprimo P758-E85+");
            //DictSnModel.Add("EIBN", "Esprimo P758-E94+");
            //DictSnModel.Add("EIBJ", "Esprimo P958-E85+");
            //DictSnModel.Add("EIBK", "Esprimo P958-E94+");
            //DictSnModel.Add("EIBL", "Esprimo P958 Power");
            //DictSnModel.Add("EIBP", "Esprimo P558-E85+");
            //DictSnModel.Add("EIBQ", "Esprimo P558-E94+");
            //DictSnModel.Add("EIBR", "Esprimo P558 Power");
            //DictSnModel.Add("EICA", "Celsius W550 Power-L");
            //DictSnModel.Add("EICB", "Celsius W580");
            //DictSnModel.Add("EICC", "Celsius W580 POWER");
            //DictSnModel.Add("EICD", "Celsius W580 POWER+");
            //DictSnModel.Add("EICU", "Intelliedge A700");
            //DictSnModel.Add("EICT", "Intelliedge G700");
            //DictSnModel.Add("EICS", "Esprimo G558");
            //DictSnModel.Add("EIBE", "Esprimo D738-E85+");
            //DictSnModel.Add("EIBF", "Esprimo D738-E94+");
            //DictSnModel.Add("EIBS", "Esprimo D958-E85+");
            //DictSnModel.Add("EIBT", "Esprimo D958-E94+");
            //DictSnModel.Add("EIBG", "Esprimo D538-E85+");
            //DictSnModel.Add("EIBH", "Esprimo D538-E94+");
            //DictSnModel.Add("EIBD", "Lifebook A359");
            //DictSnModel.Add("EICH", "Esprimo D9010");
            //DictSnModel.Add("EICK", "Esprimo D7010");
            //DictSnModel.Add("EICL", "Esprimo D7010/8");
            //DictSnModel.Add("EICN", "Esprimo P9910");
            //DictSnModel.Add("EICJ", "Esprimo P9010");
            //DictSnModel.Add("EICM", "Esprimo P7010");
            //DictSnModel.Add("EICP", "Esprimo P5010");
            //DictSnModel.Add("EICF", "Esprimo K5010/24");
            //DictSnModel.Add("EICG", "Esprimo K5010/22");
            //DictSnModel.Add("EICR", "Esprimo G9010");
            //DictSnModel.Add("EICQ", "Esprimo G5010");
            //DictSnModel.Add("EICV", "Celsius J5010");
            //DictSnModel.Add("EICW", "Celsius W5010");
            //DictSnModel.Add("EICX", "Celsius W5010/L");
            //DictSnModel.Add("EIDC", "Esprimo D9011 (RKL)");
            //DictSnModel.Add("EIDB", "Esprimo D7011 (RKL)");
            //DictSnModel.Add("EIDA", "Esprimo D6011 (RKL)");
            //DictSnModel.Add("EIDH", "Esprimo P9011 (RKL)");
            //DictSnModel.Add("EIDG", "Esprimo P7011 (RKL)");
            //DictSnModel.Add("EIDF", "Esprimo P5011 (RKL)");
            //DictSnModel.Add("EICE", "Esprimo Q7010 (CML/RKL)");
            //DictSnModel.Add("EIDD", "Esprimo G5011 (RKL)");
            //DictSnModel.Add("EIDK", "Celsius W5011 (RKL)");

            //DictSnMB.Add("EIAA", "D3488A");
            //DictSnMB.Add("EIAB", "D3488A");
            //DictSnMB.Add("EIAH", "D3488A");
            //DictSnMB.Add("EIAJ", "D3488A");
            //DictSnMB.Add("EIAG", "D3674A");
            //DictSnMB.Add("EIAK", "D3628A");
            //DictSnMB.Add("EIAL", "D3688A");
            //DictSnMB.Add("EIAM", "D3688A");
            //DictSnMB.Add("EIBV", "D3543A");
            //DictSnMB.Add("EIAC", "D3543A");
            //DictSnMB.Add("EIBU", "D3543A");
            //DictSnMB.Add("EIBX", "D3544A");
            //DictSnMB.Add("EIBW", "D3544B");
            //DictSnMB.Add("EIAF", "D3543A");
            //DictSnMB.Add("EIAE", "D3544A");
            //DictSnMB.Add("EIAD", "D3544B");
            //DictSnMB.Add("EIBB", "D3613A");
            //DictSnMB.Add("EIBC", "D3613A");
            //DictSnMB.Add("EIBA", "D3603A");
            //DictSnMB.Add("EIAQ", "D3498A");
            //DictSnMB.Add("EIAR", "D3498A");
            //DictSnMB.Add("EIAS", "D3498U");
            //DictSnMB.Add("EIAT", "D3498U");
            //DictSnMB.Add("EIAU", "D3498A");
            //DictSnMB.Add("EIAV", "D3498A");
            //DictSnMB.Add("EIAW", "D3498U");
            //DictSnMB.Add("EIAX", "D3498U");
            //DictSnMB.Add("EIBM", "D3601A");
            //DictSnMB.Add("EIBN", "D3601A");
            //DictSnMB.Add("EIBJ", "D3602A");
            //DictSnMB.Add("EIBK", "D3602A");
            //DictSnMB.Add("EIBL", "D3602A");
            //DictSnMB.Add("EIBP", "D3600A");
            //DictSnMB.Add("EIBQ", "D3600A");
            //DictSnMB.Add("EIBR", "D3600A");
            //DictSnMB.Add("EICA", "D3417A");
            //DictSnMB.Add("EICB", "D3617A");
            //DictSnMB.Add("EICC", "D3617A");
            //DictSnMB.Add("EICD", "D3617A");
            //DictSnMB.Add("EICU", "D3433-S/J");
            //DictSnMB.Add("EICT", "D3433S");
            //DictSnMB.Add("EICS", "D3654A");
            //DictSnMB.Add("EIBE", "D3601A");
            //DictSnMB.Add("EIBF", "D3601A");
            //DictSnMB.Add("EIBS", "D3632A");
            //DictSnMB.Add("EIBT", "D3632A");
            //DictSnMB.Add("EIBG", "D3600A");
            //DictSnMB.Add("EIBH", "D3600A");
            //DictSnMB.Add("EIBD", "Unknown");
            //DictSnMB.Add("EICH", "D3822A");
            //DictSnMB.Add("EICK", "D3823A");
            //DictSnMB.Add("EICL", "D3823A");
            //DictSnMB.Add("EICN", "D3812A");
            //DictSnMB.Add("EICJ", "D3822A");
            //DictSnMB.Add("EICM", "D3823A");
            //DictSnMB.Add("EICP", "D3820A");
            //DictSnMB.Add("EICF", "D3774A");
            //DictSnMB.Add("EICG", "Unknown");
            //DictSnMB.Add("EICR", "D3814A");
            //DictSnMB.Add("EICQ", "D3804A");
            //DictSnMB.Add("EICV", "D3828A");
            //DictSnMB.Add("EICW", "D3817A");
            //DictSnMB.Add("EICX", "D3817A");
            //DictSnMB.Add("EIDC", "D3922A");
            //DictSnMB.Add("EIDB", "D3923A");
            //DictSnMB.Add("EIDA", "D3923A");
            //DictSnMB.Add("EIDH", "D3922A");
            //DictSnMB.Add("EIDG", "D3923A");
            //DictSnMB.Add("EIDF", "D3920A");
            //DictSnMB.Add("EICE", "D3813A");
            //DictSnMB.Add("EIDD", "D3904A");
            //DictSnMB.Add("EIDK", "D3917A");

            //DictLabelClickable.Add("labelL1TestStart", false);

            //BW:
            //oddelani linky 1 a 2:
            //ListBaysButtons.Add("buttonLINE1_A_05");
            //ListBaysButtons.Add("buttonLINE1_A_06");
            //ListBaysButtons.Add("buttonLINE1_A_07");
            //ListBaysButtons.Add("buttonLINE1_A_08");
            //ListBaysButtons.Add("buttonLINE1_A_09");
            //ListBaysButtons.Add("buttonLINE1_A_10");
            //ListBaysButtons.Add("buttonLINE1_A_11");
            //ListBaysButtons.Add("buttonLINE1_A_12");
            //ListBaysButtons.Add("buttonLINE1_A_13");
            //ListBaysButtons.Add("buttonLINE1_A_14");
            //ListBaysButtons.Add("buttonLINE1_A_15");
            //ListBaysButtons.Add("buttonLINE1_A_16");
            //ListBaysButtons.Add("buttonLINE1_A_17");
            //ListBaysButtons.Add("buttonLINE1_A_18");
            //ListBaysButtons.Add("buttonLINE1_A_19");
            //ListBaysButtons.Add("buttonLINE1_A_20");
            //ListBaysButtons.Add("buttonLINE1_A_21");
            //ListBaysButtons.Add("buttonLINE1_A_22");
            //ListBaysButtons.Add("buttonLINE1_A_23");
            //ListBaysButtons.Add("buttonLINE1_A_24");
            //ListBaysButtons.Add("buttonLINE1_A_25");
            //ListBaysButtons.Add("buttonLINE1_A_26");
            //ListBaysButtons.Add("buttonLINE1_A_27");
            //ListBaysButtons.Add("buttonLINE1_A_28");
            //ListBaysButtons.Add("buttonLINE1_A_29");
            //ListBaysButtons.Add("buttonLINE1_A_30");
            //ListBaysButtons.Add("buttonLINE1_A_31");
            //ListBaysButtons.Add("buttonLINE1_A_32");
            //ListBaysButtons.Add("buttonLINE1_A_33");
            //ListBaysButtons.Add("buttonLINE1_A_34");
            //ListBaysButtons.Add("buttonLINE1_A_35");
            //ListBaysButtons.Add("buttonLINE1_A_36");
            //ListBaysButtons.Add("buttonLINE1_B_05");
            //ListBaysButtons.Add("buttonLINE1_B_06");
            //ListBaysButtons.Add("buttonLINE1_B_07");
            //ListBaysButtons.Add("buttonLINE1_B_08");
            //ListBaysButtons.Add("buttonLINE1_B_09");
            //ListBaysButtons.Add("buttonLINE1_B_10");
            //ListBaysButtons.Add("buttonLINE1_B_11");
            //ListBaysButtons.Add("buttonLINE1_B_12");
            //ListBaysButtons.Add("buttonLINE1_B_13");
            //ListBaysButtons.Add("buttonLINE1_B_14");
            //ListBaysButtons.Add("buttonLINE1_B_15");
            //ListBaysButtons.Add("buttonLINE1_B_16");
            //ListBaysButtons.Add("buttonLINE1_B_17");
            //ListBaysButtons.Add("buttonLINE1_B_18");
            //ListBaysButtons.Add("buttonLINE1_B_19");
            //ListBaysButtons.Add("buttonLINE1_B_20");
            //ListBaysButtons.Add("buttonLINE1_B_21");
            //ListBaysButtons.Add("buttonLINE1_B_22");
            //ListBaysButtons.Add("buttonLINE1_B_23");
            //ListBaysButtons.Add("buttonLINE1_B_24");
            //ListBaysButtons.Add("buttonLINE1_B_25");
            //ListBaysButtons.Add("buttonLINE1_B_26");
            //ListBaysButtons.Add("buttonLINE1_B_27");
            //ListBaysButtons.Add("buttonLINE1_B_28");
            //ListBaysButtons.Add("buttonLINE1_B_29");
            //ListBaysButtons.Add("buttonLINE1_B_30");
            //ListBaysButtons.Add("buttonLINE1_B_31");
            //ListBaysButtons.Add("buttonLINE1_B_32");
            //ListBaysButtons.Add("buttonLINE1_B_33");
            //ListBaysButtons.Add("buttonLINE1_B_34");
            //ListBaysButtons.Add("buttonLINE1_B_35");
            //ListBaysButtons.Add("buttonLINE1_B_36");
            //ListBaysButtons.Add("buttonLINE2_A_05");
            //ListBaysButtons.Add("buttonLINE2_A_06");
            //ListBaysButtons.Add("buttonLINE2_A_07");
            //ListBaysButtons.Add("buttonLINE2_A_08");
            //ListBaysButtons.Add("buttonLINE2_A_09");
            //ListBaysButtons.Add("buttonLINE2_A_10");
            //ListBaysButtons.Add("buttonLINE2_A_11");
            //ListBaysButtons.Add("buttonLINE2_A_12");
            //ListBaysButtons.Add("buttonLINE2_A_13");
            //ListBaysButtons.Add("buttonLINE2_A_14");
            //ListBaysButtons.Add("buttonLINE2_A_15");
            //ListBaysButtons.Add("buttonLINE2_A_16");
            //ListBaysButtons.Add("buttonLINE2_A_17");
            //ListBaysButtons.Add("buttonLINE2_A_18");
            //ListBaysButtons.Add("buttonLINE2_A_19");
            //ListBaysButtons.Add("buttonLINE2_A_20");
            //ListBaysButtons.Add("buttonLINE2_A_21");
            //ListBaysButtons.Add("buttonLINE2_A_22");
            //ListBaysButtons.Add("buttonLINE2_A_23");
            //ListBaysButtons.Add("buttonLINE2_A_24");
            //ListBaysButtons.Add("buttonLINE2_A_25");
            //ListBaysButtons.Add("buttonLINE2_A_26");
            //ListBaysButtons.Add("buttonLINE2_A_27");
            //ListBaysButtons.Add("buttonLINE2_A_28");
            //ListBaysButtons.Add("buttonLINE2_A_29");
            //ListBaysButtons.Add("buttonLINE2_A_30");
            //ListBaysButtons.Add("buttonLINE2_A_31");
            //ListBaysButtons.Add("buttonLINE2_A_32");
            //ListBaysButtons.Add("buttonLINE2_A_33");
            //ListBaysButtons.Add("buttonLINE2_A_34");
            //ListBaysButtons.Add("buttonLINE2_A_35");
            //ListBaysButtons.Add("buttonLINE2_A_36");
            //ListBaysButtons.Add("buttonLINE2_B_05");
            //ListBaysButtons.Add("buttonLINE2_B_06");
            //ListBaysButtons.Add("buttonLINE2_B_07");
            //ListBaysButtons.Add("buttonLINE2_B_08");
            //ListBaysButtons.Add("buttonLINE2_B_09");
            //ListBaysButtons.Add("buttonLINE2_B_10");
            //ListBaysButtons.Add("buttonLINE2_B_11");
            //ListBaysButtons.Add("buttonLINE2_B_12");
            //ListBaysButtons.Add("buttonLINE2_B_13");
            //ListBaysButtons.Add("buttonLINE2_B_14");
            //ListBaysButtons.Add("buttonLINE2_B_15");
            //ListBaysButtons.Add("buttonLINE2_B_16");
            //ListBaysButtons.Add("buttonLINE2_B_17");
            //ListBaysButtons.Add("buttonLINE2_B_18");
            //ListBaysButtons.Add("buttonLINE2_B_19");
            //ListBaysButtons.Add("buttonLINE2_B_20");
            //ListBaysButtons.Add("buttonLINE2_B_21");
            //ListBaysButtons.Add("buttonLINE2_B_22");
            //ListBaysButtons.Add("buttonLINE2_B_23");
            //ListBaysButtons.Add("buttonLINE2_B_24");
            //funkcni:
            ListBaysButtons.Add("buttonLINE2_B_25");
            ListBaysButtons.Add("buttonLINE2_B_26");
            ListBaysButtons.Add("buttonLINE2_B_27");
            ListBaysButtons.Add("buttonLINE2_B_28");
            ListBaysButtons.Add("buttonLINE2_B_29");
            ListBaysButtons.Add("buttonLINE2_B_30");
            ListBaysButtons.Add("buttonLINE2_B_31");
            ListBaysButtons.Add("buttonLINE2_B_32");
            ListBaysButtons.Add("buttonLINE2_B_33");
            ListBaysButtons.Add("buttonLINE2_B_34");
            ListBaysButtons.Add("buttonLINE2_B_35");
            ListBaysButtons.Add("buttonLINE2_B_36");
            ListBaysButtons.Add("buttonLINE3_Lower_02");
            ListBaysButtons.Add("buttonLINE3_Lower_04");
            ListBaysButtons.Add("buttonLINE3_Lower_06");
            ListBaysButtons.Add("buttonLINE3_Lower_08");
            ListBaysButtons.Add("buttonLINE3_Lower_10");
            ListBaysButtons.Add("buttonLINE3_Lower_12");
            ListBaysButtons.Add("buttonLINE3_Lower_14");
            ListBaysButtons.Add("buttonLINE3_Lower_16");
            ListBaysButtons.Add("buttonLINE3_Lower_18");
            ListBaysButtons.Add("buttonLINE3_Lower_20");
            ListBaysButtons.Add("buttonLINE3_Lower_22");
            ListBaysButtons.Add("buttonLINE3_Lower_24");
            ListBaysButtons.Add("buttonLINE3_Lower_26");
            ListBaysButtons.Add("buttonLINE3_Lower_28");
            ListBaysButtons.Add("buttonLINE3_Lower_30");
            ListBaysButtons.Add("buttonLINE3_Lower_32");
            ListBaysButtons.Add("buttonLINE3_Lower_34");
            ListBaysButtons.Add("buttonLINE3_Lower_36");
            ListBaysButtons.Add("buttonLINE3_Lower_38");
            ListBaysButtons.Add("buttonLINE3_Lower_40");
            ListBaysButtons.Add("buttonLINE3_Lower_42");
            ListBaysButtons.Add("buttonLINE3_Lower_44");
            ListBaysButtons.Add("buttonLINE3_Lower_46");
            ListBaysButtons.Add("buttonLINE3_Lower_48");
            ListBaysButtons.Add("buttonLINE3_Lower_50");
            ListBaysButtons.Add("buttonLINE3_Lower_52");
            ListBaysButtons.Add("buttonLINE3_Lower_54");
            ListBaysButtons.Add("buttonLINE3_Lower_56");
            ListBaysButtons.Add("buttonLINE3_Lower_58");
            ListBaysButtons.Add("buttonLINE3_Lower_60");
            ListBaysButtons.Add("buttonLINE3_Lower_62");
            ListBaysButtons.Add("buttonLINE3_Lower_64");
            ListBaysButtons.Add("buttonLINE3_Lower_66");
            ListBaysButtons.Add("buttonLINE3_Lower_68");
            ListBaysButtons.Add("buttonLINE3_Lower_70");
            ListBaysButtons.Add("buttonLINE3_Lower_72");
            ListBaysButtons.Add("buttonLINE3_Upper_01");
            ListBaysButtons.Add("buttonLINE3_Upper_03");
            ListBaysButtons.Add("buttonLINE3_Upper_05");
            ListBaysButtons.Add("buttonLINE3_Upper_07");
            ListBaysButtons.Add("buttonLINE3_Upper_09");
            ListBaysButtons.Add("buttonLINE3_Upper_11");
            ListBaysButtons.Add("buttonLINE3_Upper_13");
            ListBaysButtons.Add("buttonLINE3_Upper_15");
            ListBaysButtons.Add("buttonLINE3_Upper_17");
            ListBaysButtons.Add("buttonLINE3_Upper_19");
            ListBaysButtons.Add("buttonLINE3_Upper_21");
            ListBaysButtons.Add("buttonLINE3_Upper_23");
            ListBaysButtons.Add("buttonLINE3_Upper_25");
            ListBaysButtons.Add("buttonLINE3_Upper_27");
            ListBaysButtons.Add("buttonLINE3_Upper_29");
            ListBaysButtons.Add("buttonLINE3_Upper_31");
            ListBaysButtons.Add("buttonLINE3_Upper_33");
            ListBaysButtons.Add("buttonLINE3_Upper_35");
            ListBaysButtons.Add("buttonLINE3_Upper_37");
            ListBaysButtons.Add("buttonLINE3_Upper_39");
            ListBaysButtons.Add("buttonLINE3_Upper_41");
            ListBaysButtons.Add("buttonLINE3_Upper_43");
            ListBaysButtons.Add("buttonLINE3_Upper_45");
            ListBaysButtons.Add("buttonLINE3_Upper_47");
            ListBaysButtons.Add("buttonLINE3_Upper_49");
            ListBaysButtons.Add("buttonLINE3_Upper_51");
            ListBaysButtons.Add("buttonLINE3_Upper_53");
            ListBaysButtons.Add("buttonLINE3_Upper_55");
            ListBaysButtons.Add("buttonLINE3_Upper_57");
            ListBaysButtons.Add("buttonLINE3_Upper_59");
            ListBaysButtons.Add("buttonLINE3_Upper_61");
            ListBaysButtons.Add("buttonLINE3_Upper_63");
            ListBaysButtons.Add("buttonLINE3_Upper_65");
            ListBaysButtons.Add("buttonLINE3_Upper_67");
            ListBaysButtons.Add("buttonLINE3_Upper_69");
            ListBaysButtons.Add("buttonLINE3_Upper_71");
            ListBaysButtons.Add("buttonLINE4_Lower_02");
            ListBaysButtons.Add("buttonLINE4_Lower_04");
            ListBaysButtons.Add("buttonLINE4_Lower_06");
            ListBaysButtons.Add("buttonLINE4_Lower_08");
            ListBaysButtons.Add("buttonLINE4_Lower_10");
            ListBaysButtons.Add("buttonLINE4_Lower_12");
            ListBaysButtons.Add("buttonLINE4_Lower_14");
            ListBaysButtons.Add("buttonLINE4_Lower_16");
            ListBaysButtons.Add("buttonLINE4_Lower_18");
            ListBaysButtons.Add("buttonLINE4_Lower_20");
            ListBaysButtons.Add("buttonLINE4_Lower_22");
            ListBaysButtons.Add("buttonLINE4_Lower_24");
            ListBaysButtons.Add("buttonLINE4_Lower_26");
            ListBaysButtons.Add("buttonLINE4_Lower_28");
            ListBaysButtons.Add("buttonLINE4_Lower_30");
            ListBaysButtons.Add("buttonLINE4_Lower_32");
            ListBaysButtons.Add("buttonLINE4_Lower_34");
            ListBaysButtons.Add("buttonLINE4_Lower_36");
            ListBaysButtons.Add("buttonLINE4_Lower_38");
            ListBaysButtons.Add("buttonLINE4_Lower_40");
            ListBaysButtons.Add("buttonLINE4_Lower_42");
            ListBaysButtons.Add("buttonLINE4_Lower_44");
            ListBaysButtons.Add("buttonLINE4_Lower_46");
            ListBaysButtons.Add("buttonLINE4_Lower_48");
            ListBaysButtons.Add("buttonLINE4_Lower_50");
            ListBaysButtons.Add("buttonLINE4_Lower_52");
            ListBaysButtons.Add("buttonLINE4_Lower_54");
            ListBaysButtons.Add("buttonLINE4_Lower_56");
            ListBaysButtons.Add("buttonLINE4_Lower_58");
            ListBaysButtons.Add("buttonLINE4_Lower_60");
            ListBaysButtons.Add("buttonLINE4_Lower_62");
            ListBaysButtons.Add("buttonLINE4_Lower_64");
            ListBaysButtons.Add("buttonLINE4_Lower_66");
            ListBaysButtons.Add("buttonLINE4_Lower_68");
            ListBaysButtons.Add("buttonLINE4_Lower_70");
            ListBaysButtons.Add("buttonLINE4_Lower_72");
            ListBaysButtons.Add("buttonLINE4_Upper_01");
            ListBaysButtons.Add("buttonLINE4_Upper_03");
            ListBaysButtons.Add("buttonLINE4_Upper_05");
            ListBaysButtons.Add("buttonLINE4_Upper_07");
            ListBaysButtons.Add("buttonLINE4_Upper_09");
            ListBaysButtons.Add("buttonLINE4_Upper_11");
            ListBaysButtons.Add("buttonLINE4_Upper_13");
            ListBaysButtons.Add("buttonLINE4_Upper_15");
            ListBaysButtons.Add("buttonLINE4_Upper_17");
            ListBaysButtons.Add("buttonLINE4_Upper_19");
            ListBaysButtons.Add("buttonLINE4_Upper_21");
            ListBaysButtons.Add("buttonLINE4_Upper_23");
            ListBaysButtons.Add("buttonLINE4_Upper_25");
            ListBaysButtons.Add("buttonLINE4_Upper_27");
            ListBaysButtons.Add("buttonLINE4_Upper_29");
            ListBaysButtons.Add("buttonLINE4_Upper_31");
            ListBaysButtons.Add("buttonLINE4_Upper_33");
            ListBaysButtons.Add("buttonLINE4_Upper_35");
            ListBaysButtons.Add("buttonLINE4_Upper_37");
            ListBaysButtons.Add("buttonLINE4_Upper_39");
            ListBaysButtons.Add("buttonLINE4_Upper_41");
            ListBaysButtons.Add("buttonLINE4_Upper_43");
            ListBaysButtons.Add("buttonLINE4_Upper_45");
            ListBaysButtons.Add("buttonLINE4_Upper_47");
            ListBaysButtons.Add("buttonLINE4_Upper_49");
            ListBaysButtons.Add("buttonLINE4_Upper_51");
            ListBaysButtons.Add("buttonLINE4_Upper_53");
            ListBaysButtons.Add("buttonLINE4_Upper_55");
            ListBaysButtons.Add("buttonLINE4_Upper_57");
            ListBaysButtons.Add("buttonLINE4_Upper_59");
            ListBaysButtons.Add("buttonLINE4_Upper_61");
            ListBaysButtons.Add("buttonLINE4_Upper_63");
            ListBaysButtons.Add("buttonLINE4_Upper_65");
            ListBaysButtons.Add("buttonLINE4_Upper_67");
            ListBaysButtons.Add("buttonLINE4_Upper_69");
            ListBaysButtons.Add("buttonLINE4_Upper_71");
            ListBaysButtons.Add("buttonLINE5_Lower_02");
            ListBaysButtons.Add("buttonLINE5_Lower_04");
            ListBaysButtons.Add("buttonLINE5_Lower_06");
            ListBaysButtons.Add("buttonLINE5_Lower_08");
            ListBaysButtons.Add("buttonLINE5_Lower_10");
            ListBaysButtons.Add("buttonLINE5_Lower_12");
            ListBaysButtons.Add("buttonLINE5_Lower_14");
            ListBaysButtons.Add("buttonLINE5_Lower_16");
            ListBaysButtons.Add("buttonLINE5_Lower_18");
            ListBaysButtons.Add("buttonLINE5_Lower_20");
            ListBaysButtons.Add("buttonLINE5_Lower_22");
            ListBaysButtons.Add("buttonLINE5_Lower_24");
            ListBaysButtons.Add("buttonLINE5_Lower_26");
            ListBaysButtons.Add("buttonLINE5_Lower_28");
            ListBaysButtons.Add("buttonLINE5_Lower_30");
            ListBaysButtons.Add("buttonLINE5_Lower_32");
            ListBaysButtons.Add("buttonLINE5_Lower_34");
            ListBaysButtons.Add("buttonLINE5_Lower_36");
            ListBaysButtons.Add("buttonLINE5_Lower_38");
            ListBaysButtons.Add("buttonLINE5_Lower_40");
            ListBaysButtons.Add("buttonLINE5_Lower_42");
            ListBaysButtons.Add("buttonLINE5_Lower_44");
            ListBaysButtons.Add("buttonLINE5_Lower_46");
            ListBaysButtons.Add("buttonLINE5_Lower_48");
            ListBaysButtons.Add("buttonLINE5_Lower_50");
            ListBaysButtons.Add("buttonLINE5_Lower_52");
            ListBaysButtons.Add("buttonLINE5_Lower_54");
            ListBaysButtons.Add("buttonLINE5_Lower_56");
            ListBaysButtons.Add("buttonLINE5_Lower_58");
            ListBaysButtons.Add("buttonLINE5_Lower_60");
            ListBaysButtons.Add("buttonLINE5_Lower_62");
            ListBaysButtons.Add("buttonLINE5_Lower_64");
            ListBaysButtons.Add("buttonLINE5_Lower_66");
            ListBaysButtons.Add("buttonLINE5_Lower_68");
            ListBaysButtons.Add("buttonLINE5_Lower_70");
            ListBaysButtons.Add("buttonLINE5_Lower_72");
            ListBaysButtons.Add("buttonLINE5_Lower_74");
            ListBaysButtons.Add("buttonLINE5_Lower_76");
            ListBaysButtons.Add("buttonLINE5_Lower_78");
            ListBaysButtons.Add("buttonLINE5_Lower_80");
            ListBaysButtons.Add("buttonLINE5_Upper_01");
            ListBaysButtons.Add("buttonLINE5_Upper_03");
            ListBaysButtons.Add("buttonLINE5_Upper_05");
            ListBaysButtons.Add("buttonLINE5_Upper_07");
            ListBaysButtons.Add("buttonLINE5_Upper_09");
            ListBaysButtons.Add("buttonLINE5_Upper_11");
            ListBaysButtons.Add("buttonLINE5_Upper_13");
            ListBaysButtons.Add("buttonLINE5_Upper_15");
            ListBaysButtons.Add("buttonLINE5_Upper_17");
            ListBaysButtons.Add("buttonLINE5_Upper_19");
            ListBaysButtons.Add("buttonLINE5_Upper_21");
            ListBaysButtons.Add("buttonLINE5_Upper_23");
            ListBaysButtons.Add("buttonLINE5_Upper_25");
            ListBaysButtons.Add("buttonLINE5_Upper_27");
            ListBaysButtons.Add("buttonLINE5_Upper_29");
            ListBaysButtons.Add("buttonLINE5_Upper_31");
            ListBaysButtons.Add("buttonLINE5_Upper_33");
            ListBaysButtons.Add("buttonLINE5_Upper_35");
            ListBaysButtons.Add("buttonLINE5_Upper_37");
            ListBaysButtons.Add("buttonLINE5_Upper_39");
            ListBaysButtons.Add("buttonLINE5_Upper_41");
            ListBaysButtons.Add("buttonLINE5_Upper_43");
            ListBaysButtons.Add("buttonLINE5_Upper_45");
            ListBaysButtons.Add("buttonLINE5_Upper_47");
            ListBaysButtons.Add("buttonLINE5_Upper_49");
            ListBaysButtons.Add("buttonLINE5_Upper_51");
            ListBaysButtons.Add("buttonLINE5_Upper_53");
            ListBaysButtons.Add("buttonLINE5_Upper_55");
            ListBaysButtons.Add("buttonLINE5_Upper_57");
            ListBaysButtons.Add("buttonLINE5_Upper_59");
            ListBaysButtons.Add("buttonLINE5_Upper_61");
            ListBaysButtons.Add("buttonLINE5_Upper_63");
            ListBaysButtons.Add("buttonLINE5_Upper_65");
            ListBaysButtons.Add("buttonLINE5_Upper_67");
            ListBaysButtons.Add("buttonLINE5_Upper_69");
            ListBaysButtons.Add("buttonLINE5_Upper_71");
            ListBaysButtons.Add("buttonLINE5_Upper_73");
            ListBaysButtons.Add("buttonLINE5_Upper_75");
            ListBaysButtons.Add("buttonLINE5_Upper_77");
            ListBaysButtons.Add("buttonLINE5_Upper_79");
            ListBaysButtons.Add("buttonLINE6_Lower_02");
            ListBaysButtons.Add("buttonLINE6_Lower_04");
            ListBaysButtons.Add("buttonLINE6_Lower_06");
            ListBaysButtons.Add("buttonLINE6_Lower_08");
            ListBaysButtons.Add("buttonLINE6_Lower_10");
            ListBaysButtons.Add("buttonLINE6_Lower_12");
            ListBaysButtons.Add("buttonLINE6_Lower_14");
            ListBaysButtons.Add("buttonLINE6_Lower_16");
            ListBaysButtons.Add("buttonLINE6_Lower_18");
            ListBaysButtons.Add("buttonLINE6_Lower_20");
            ListBaysButtons.Add("buttonLINE6_Lower_22");
            ListBaysButtons.Add("buttonLINE6_Lower_24");
            ListBaysButtons.Add("buttonLINE6_Lower_26");
            ListBaysButtons.Add("buttonLINE6_Lower_28");
            ListBaysButtons.Add("buttonLINE6_Lower_30");
            ListBaysButtons.Add("buttonLINE6_Lower_32");
            ListBaysButtons.Add("buttonLINE6_Lower_34");
            ListBaysButtons.Add("buttonLINE6_Lower_36");
            ListBaysButtons.Add("buttonLINE6_Lower_38");
            ListBaysButtons.Add("buttonLINE6_Lower_40");
            ListBaysButtons.Add("buttonLINE6_Upper_01");
            ListBaysButtons.Add("buttonLINE6_Upper_03");
            ListBaysButtons.Add("buttonLINE6_Upper_05");
            ListBaysButtons.Add("buttonLINE6_Upper_07");
            ListBaysButtons.Add("buttonLINE6_Upper_09");
            ListBaysButtons.Add("buttonLINE6_Upper_11");
            ListBaysButtons.Add("buttonLINE6_Upper_13");
            ListBaysButtons.Add("buttonLINE6_Upper_15");
            ListBaysButtons.Add("buttonLINE6_Upper_17");
            ListBaysButtons.Add("buttonLINE6_Upper_19");
            ListBaysButtons.Add("buttonLINE6_Upper_21");
            ListBaysButtons.Add("buttonLINE6_Upper_23");
            ListBaysButtons.Add("buttonLINE6_Upper_25");
            ListBaysButtons.Add("buttonLINE6_Upper_27");
            ListBaysButtons.Add("buttonLINE6_Upper_29");
            ListBaysButtons.Add("buttonLINE6_Upper_31");
            ListBaysButtons.Add("buttonLINE6_Upper_33");
            ListBaysButtons.Add("buttonLINE6_Upper_35");
            ListBaysButtons.Add("buttonLINE6_Upper_37");
            ListBaysButtons.Add("buttonLINE6_Upper_39");
            ListBaysButtons.Add("buttonLINE7_Lower_02");
            ListBaysButtons.Add("buttonLINE7_Lower_04");
            ListBaysButtons.Add("buttonLINE7_Lower_06");
            ListBaysButtons.Add("buttonLINE7_Lower_08");
            ListBaysButtons.Add("buttonLINE7_Lower_10");
            ListBaysButtons.Add("buttonLINE7_Lower_12");
            ListBaysButtons.Add("buttonLINE7_Lower_14");
            ListBaysButtons.Add("buttonLINE7_Lower_16");
            ListBaysButtons.Add("buttonLINE7_Lower_18");
            ListBaysButtons.Add("buttonLINE7_Lower_20");
            ListBaysButtons.Add("buttonLINE7_Lower_22");
            ListBaysButtons.Add("buttonLINE7_Lower_24");
            ListBaysButtons.Add("buttonLINE7_Lower_26");
            ListBaysButtons.Add("buttonLINE7_Lower_28");
            ListBaysButtons.Add("buttonLINE7_Lower_30");
            ListBaysButtons.Add("buttonLINE7_Lower_32");
            ListBaysButtons.Add("buttonLINE7_Lower_34");
            ListBaysButtons.Add("buttonLINE7_Lower_36");
            ListBaysButtons.Add("buttonLINE7_Lower_38");
            ListBaysButtons.Add("buttonLINE7_Lower_40");
            ListBaysButtons.Add("buttonLINE7_Lower_50");
            ListBaysButtons.Add("buttonLINE7_Lower_52");
            ListBaysButtons.Add("buttonLINE7_Lower_54");
            ListBaysButtons.Add("buttonLINE7_Lower_56");
            ListBaysButtons.Add("buttonLINE7_Lower_58");
            ListBaysButtons.Add("buttonLINE7_Lower_60");
            ListBaysButtons.Add("buttonLINE7_Lower_62");
            ListBaysButtons.Add("buttonLINE7_Lower_64");
            ListBaysButtons.Add("buttonLINE7_Lower_66");
            ListBaysButtons.Add("buttonLINE7_Lower_68");
            ListBaysButtons.Add("buttonLINE7_Lower_70");
            ListBaysButtons.Add("buttonLINE7_Lower_72");
            ListBaysButtons.Add("buttonLINE7_Upper_01");
            ListBaysButtons.Add("buttonLINE7_Upper_03");
            ListBaysButtons.Add("buttonLINE7_Upper_05");
            ListBaysButtons.Add("buttonLINE7_Upper_07");
            ListBaysButtons.Add("buttonLINE7_Upper_09");
            ListBaysButtons.Add("buttonLINE7_Upper_11");
            ListBaysButtons.Add("buttonLINE7_Upper_13");
            ListBaysButtons.Add("buttonLINE7_Upper_15");
            ListBaysButtons.Add("buttonLINE7_Upper_17");
            ListBaysButtons.Add("buttonLINE7_Upper_19");
            ListBaysButtons.Add("buttonLINE7_Upper_21");
            ListBaysButtons.Add("buttonLINE7_Upper_23");
            ListBaysButtons.Add("buttonLINE7_Upper_25");
            ListBaysButtons.Add("buttonLINE7_Upper_27");
            ListBaysButtons.Add("buttonLINE7_Upper_29");
            ListBaysButtons.Add("buttonLINE7_Upper_31");
            ListBaysButtons.Add("buttonLINE7_Upper_33");
            ListBaysButtons.Add("buttonLINE7_Upper_35");
            ListBaysButtons.Add("buttonLINE7_Upper_37");
            ListBaysButtons.Add("buttonLINE7_Upper_39");
            ListBaysButtons.Add("buttonLINE7_Upper_49");
            ListBaysButtons.Add("buttonLINE7_Upper_51");
            ListBaysButtons.Add("buttonLINE7_Upper_53");
            ListBaysButtons.Add("buttonLINE7_Upper_55");
            ListBaysButtons.Add("buttonLINE7_Upper_57");
            ListBaysButtons.Add("buttonLINE7_Upper_59");
            ListBaysButtons.Add("buttonLINE7_Upper_61");
            ListBaysButtons.Add("buttonLINE7_Upper_63");
            ListBaysButtons.Add("buttonLINE7_Upper_65");
            ListBaysButtons.Add("buttonLINE7_Upper_67");
            ListBaysButtons.Add("buttonLINE7_Upper_69");
            ListBaysButtons.Add("buttonLINE7_Upper_71");
            ListBaysButtons.Add("buttonLINE8_Lower_02");
            ListBaysButtons.Add("buttonLINE8_Lower_04");
            ListBaysButtons.Add("buttonLINE8_Lower_06");
            ListBaysButtons.Add("buttonLINE8_Lower_08");
            ListBaysButtons.Add("buttonLINE8_Lower_10");
            ListBaysButtons.Add("buttonLINE8_Lower_12");
            ListBaysButtons.Add("buttonLINE8_Lower_14");
            ListBaysButtons.Add("buttonLINE8_Lower_16");
            ListBaysButtons.Add("buttonLINE8_Lower_18");
            ListBaysButtons.Add("buttonLINE8_Lower_20");
            ListBaysButtons.Add("buttonLINE8_Lower_22");
            ListBaysButtons.Add("buttonLINE8_Lower_24");
            ListBaysButtons.Add("buttonLINE8_Lower_26");
            ListBaysButtons.Add("buttonLINE8_Lower_28");
            ListBaysButtons.Add("buttonLINE8_Lower_30");
            ListBaysButtons.Add("buttonLINE8_Lower_32");
            ListBaysButtons.Add("buttonLINE8_Lower_34");
            ListBaysButtons.Add("buttonLINE8_Lower_36");
            ListBaysButtons.Add("buttonLINE8_Lower_38");
            ListBaysButtons.Add("buttonLINE8_Lower_40");
            ListBaysButtons.Add("buttonLINE8_Lower_50");
            ListBaysButtons.Add("buttonLINE8_Lower_52");
            ListBaysButtons.Add("buttonLINE8_Lower_54");
            ListBaysButtons.Add("buttonLINE8_Lower_56");
            ListBaysButtons.Add("buttonLINE8_Lower_58");
            ListBaysButtons.Add("buttonLINE8_Lower_60");
            ListBaysButtons.Add("buttonLINE8_Lower_62");
            ListBaysButtons.Add("buttonLINE8_Lower_64");
            ListBaysButtons.Add("buttonLINE8_Lower_66");
            ListBaysButtons.Add("buttonLINE8_Lower_68");
            ListBaysButtons.Add("buttonLINE8_Lower_70");
            ListBaysButtons.Add("buttonLINE8_Lower_72");
            ListBaysButtons.Add("buttonLINE8_Upper_01");
            ListBaysButtons.Add("buttonLINE8_Upper_03");
            ListBaysButtons.Add("buttonLINE8_Upper_05");
            ListBaysButtons.Add("buttonLINE8_Upper_07");
            ListBaysButtons.Add("buttonLINE8_Upper_09");
            ListBaysButtons.Add("buttonLINE8_Upper_11");
            ListBaysButtons.Add("buttonLINE8_Upper_13");
            ListBaysButtons.Add("buttonLINE8_Upper_15");
            ListBaysButtons.Add("buttonLINE8_Upper_17");
            ListBaysButtons.Add("buttonLINE8_Upper_19");
            ListBaysButtons.Add("buttonLINE8_Upper_21");
            ListBaysButtons.Add("buttonLINE8_Upper_23");
            ListBaysButtons.Add("buttonLINE8_Upper_25");
            ListBaysButtons.Add("buttonLINE8_Upper_27");
            ListBaysButtons.Add("buttonLINE8_Upper_29");
            ListBaysButtons.Add("buttonLINE8_Upper_31");
            ListBaysButtons.Add("buttonLINE8_Upper_33");
            ListBaysButtons.Add("buttonLINE8_Upper_35");
            ListBaysButtons.Add("buttonLINE8_Upper_37");
            ListBaysButtons.Add("buttonLINE8_Upper_39");
            ListBaysButtons.Add("buttonLINE8_Upper_49");
            ListBaysButtons.Add("buttonLINE8_Upper_51");
            ListBaysButtons.Add("buttonLINE8_Upper_53");
            ListBaysButtons.Add("buttonLINE8_Upper_55");
            ListBaysButtons.Add("buttonLINE8_Upper_57");
            ListBaysButtons.Add("buttonLINE8_Upper_59");
            ListBaysButtons.Add("buttonLINE8_Upper_61");
            ListBaysButtons.Add("buttonLINE8_Upper_63");
            ListBaysButtons.Add("buttonLINE8_Upper_65");
            ListBaysButtons.Add("buttonLINE8_Upper_67");
            ListBaysButtons.Add("buttonLINE8_Upper_69");
            ListBaysButtons.Add("buttonLINE8_Upper_71");
            ListBaysButtons.Add("buttonLINEDMR_Lower_02");
            ListBaysButtons.Add("buttonLINEDMR_Lower_04");
            ListBaysButtons.Add("buttonLINEDMR_Lower_06");
            ListBaysButtons.Add("buttonLINEDMR_Lower_08");
            ListBaysButtons.Add("buttonLINEDMR_Lower_10");
            ListBaysButtons.Add("buttonLINEDMR_Lower_12");
            ListBaysButtons.Add("buttonLINEDMR_Lower_14");
            ListBaysButtons.Add("buttonLINEDMR_Lower_16");
            ListBaysButtons.Add("buttonLINEDMR_Upper_01");
            ListBaysButtons.Add("buttonLINEDMR_Upper_03");
            ListBaysButtons.Add("buttonLINEDMR_Upper_05");
            ListBaysButtons.Add("buttonLINEDMR_Upper_07");
            ListBaysButtons.Add("buttonLINEOBA_Lower_02");
            ListBaysButtons.Add("buttonLINEOBA_Lower_04");
            ListBaysButtons.Add("buttonLINEOBA_Lower_06");
            ListBaysButtons.Add("buttonLINEOBA_Lower_08");
            ListBaysButtons.Add("buttonLINEOBA_Lower_10");
            ListBaysButtons.Add("buttonLINEOBA_Lower_12");
            ListBaysButtons.Add("buttonLINEOBA_Lower_14");
            ListBaysButtons.Add("buttonLINEOBA_Lower_16");
            ListBaysButtons.Add("buttonLINEOBA_Upper_01");
            ListBaysButtons.Add("buttonLINEOBA_Upper_03");
            ListBaysButtons.Add("buttonLINEOBA_Upper_05");
            ListBaysButtons.Add("buttonLINEOBA_Upper_07");
            ListBaysButtons.Add("buttonLINEOBA_Upper_09");
            ListBaysButtons.Add("buttonLINEOBA_Upper_11");
            ListBaysButtons.Add("buttonLINEOBA_Upper_13");
            ListBaysButtons.Add("buttonLINEOBA_Upper_15");
            //ListBaysButtons.Add("buttonLINEPRI_Lower_02");
            //ListBaysButtons.Add("buttonLINEPRI_Lower_04");
            //ListBaysButtons.Add("buttonLINEPRI_Lower_06");
            //ListBaysButtons.Add("buttonLINEPRI_Lower_08");
            ListBaysButtons.Add("buttonLINEPRI_Lower_10");
            ListBaysButtons.Add("buttonLINEPRI_Lower_12");
            ListBaysButtons.Add("buttonLINEPRI_Lower_14");
            ListBaysButtons.Add("buttonLINEPRI_Lower_16");
            ListBaysButtons.Add("buttonLINEPRI_Lower_18");
            ListBaysButtons.Add("buttonLINEPRI_Lower_20");
            ListBaysButtons.Add("buttonLINEPRI_Lower_22");
            ListBaysButtons.Add("buttonLINEPRI_Lower_24");
            //ListBaysButtons.Add("buttonLINEPRI_Upper_01");
            //ListBaysButtons.Add("buttonLINEPRI_Upper_03");
            //ListBaysButtons.Add("buttonLINEPRI_Upper_05");
            //ListBaysButtons.Add("buttonLINEPRI_Upper_07");
            ListBaysButtons.Add("buttonLINEPRI_Upper_09");
            ListBaysButtons.Add("buttonLINEPRI_Upper_11");
            ListBaysButtons.Add("buttonLINEPRI_Upper_13");
            ListBaysButtons.Add("buttonLINEPRI_Upper_15");
            ListBaysButtons.Add("buttonLINEPRI_Upper_17");
            ListBaysButtons.Add("buttonLINEPRI_Upper_19");
            ListBaysButtons.Add("buttonLINEPRI_Upper_21");
            ListBaysButtons.Add("buttonLINEPRI_Upper_23");
        }

        public string GetdataFromModelTable(string prefix, string which_data)
        {
            var dataRow = ModelsDataTable.AsEnumerable().Where(x => x.Field<string>("Prefix") == prefix).FirstOrDefault();
            string returnValue = dataRow[which_data].ToString();
            return returnValue;
        }

        public Dictionary<string, string> GetDict(DataTable dt)
        {
            return dt.AsEnumerable()
              .ToDictionary<DataRow, string, string>(row => row.Field<string>(0),
                                        row => row.Field<string>(1));
        }

        public abstract void GetLineheadConnectionData();

    }
    //create DataTable
    //https://stackoverflow.com/questions/1042618/how-to-create-a-datatable-in-c-sharp-and-how-to-add-rows
}
