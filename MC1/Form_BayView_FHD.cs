using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using LiveCharts;
using System.Diagnostics;

namespace MC1
{
    public partial class Form_BayView_FHD : Form
    {
        private int refreshCount = 0;
        public int counter;
        private int maxCount;
        private int maxCount_GraphLimit = 1440;
        private int SwitchResetCount = 0;
        //public SQL_Tracker SQL;
        //private bool graphClear6pm = false;
        //private bool graphClearToday = true;
        //private int graphDayCount = 0;
        private ListNDictiomary _alpha;
        public bool keepLoopAlive;
        private BayLoadGraph bayLoad;
        private BayProportionGraph bayProportion;
        //private List<int> UsedBaysRepeatCounterList { get; set; } = new List<int>();

        private TimeSpan maxBootingTime = new TimeSpan(0, 20, 0);
        private TimeSpan maxIniTime = new TimeSpan(0, 20, 0);
        private TimeSpan maxPhase0Time = new TimeSpan(0, 10, 0);
        private TimeSpan maxPhase1Time = new TimeSpan(0, 10, 0);
        private TimeSpan maxPhase2Time = new TimeSpan(0, 30, 0);
        private TimeSpan maxPhase3Time = new TimeSpan(0, 10, 0);
        private TimeSpan maxPhase4Time = new TimeSpan(0, 60, 0);
        private TimeSpan maxPhase5Time = new TimeSpan(0, 10, 0);
        private TimeSpan maxPhase6Time = new TimeSpan(0, 10, 0);
        private TimeSpan maxPhase7Time = new TimeSpan(0, 10, 0);
        private TimeSpan maxPhaseMfiTime = new TimeSpan(0, 40, 0);
        private TimeSpan maxTestTime = new TimeSpan(1, 30, 0);

        private TimeSpan maxPassTime = new TimeSpan(0, 10, 0);
        private TimeSpan maxAdlTime = new TimeSpan(0, 10, 0);

        private DataTable dtBV = new DataTable();
        private DataTable dtBVDead = new DataTable();
        private DataTable dtBVFails = new DataTable();
        private DataTable dtPendingTDS = new DataTable();

        private int totalBays;
        private List<Button> ListBaysButtons = new List<Button>();
        private Dictionary<string,Button> DictionaryBaysButtons = new Dictionary<string, Button>();
        //private Dictionary<string, string> DictionaryBaysOccupation = BayOverview_LoadBaysList.LoadList_BayPositions();

        int bays_runIn = 0;
        int bays_problem = 0;
        int bays_fail = 0;
        int bays_pass = 0;
        int bays_adler = 0;
        int bays_onlineLoad = 0;
        int bays_booting = 0;
        int bays_ini = 0;

        Form_ConnectionIssue connectionIssueWindow;
        Stopwatch stopWatch = new Stopwatch();

        public Form_BayView_FHD(string user, string lvl, ListNDictiomary compositedAlpha)
        {
            keepLoopAlive = true;
            bayLoad = new BayLoadGraph();
            bayProportion = new BayProportionGraph();
            InitializeComponent();
            _alpha = compositedAlpha;
            //this.SQL = new SQL_Tracker();
            Defaults();
            SetupGraph();
            connectionIssueWindow = new Form_ConnectionIssue();
            //UsedBaysRepeatCounterList.Add(0);

            ListBaysButtons.Add(buttonLINE2_B_25);
            ListBaysButtons.Add(buttonLINE2_B_26);
            ListBaysButtons.Add(buttonLINE2_B_27);
            ListBaysButtons.Add(buttonLINE2_B_28);
            ListBaysButtons.Add(buttonLINE2_B_29);
            ListBaysButtons.Add(buttonLINE2_B_30);
            ListBaysButtons.Add(buttonLINE2_B_31);
            ListBaysButtons.Add(buttonLINE2_B_32);
            ListBaysButtons.Add(buttonLINE2_B_33);
            ListBaysButtons.Add(buttonLINE2_B_34);
            ListBaysButtons.Add(buttonLINE2_B_35);
            ListBaysButtons.Add(buttonLINE2_B_36);
            ListBaysButtons.Add(buttonLINE3_Lower_02);
            ListBaysButtons.Add(buttonLINE3_Lower_04);
            ListBaysButtons.Add(buttonLINE3_Lower_06);
            ListBaysButtons.Add(buttonLINE3_Lower_08);
            ListBaysButtons.Add(buttonLINE3_Lower_10);
            ListBaysButtons.Add(buttonLINE3_Lower_12);
            ListBaysButtons.Add(buttonLINE3_Lower_14);
            ListBaysButtons.Add(buttonLINE3_Lower_16);
            ListBaysButtons.Add(buttonLINE3_Lower_18);
            ListBaysButtons.Add(buttonLINE3_Lower_20);
            ListBaysButtons.Add(buttonLINE3_Lower_22);
            ListBaysButtons.Add(buttonLINE3_Lower_24);
            ListBaysButtons.Add(buttonLINE3_Lower_26);
            ListBaysButtons.Add(buttonLINE3_Lower_28);
            ListBaysButtons.Add(buttonLINE3_Lower_30);
            ListBaysButtons.Add(buttonLINE3_Lower_32);
            ListBaysButtons.Add(buttonLINE3_Lower_34);
            ListBaysButtons.Add(buttonLINE3_Lower_36);
            ListBaysButtons.Add(buttonLINE3_Lower_38);
            ListBaysButtons.Add(buttonLINE3_Lower_40);
            ListBaysButtons.Add(buttonLINE3_Lower_42);
            ListBaysButtons.Add(buttonLINE3_Lower_44);
            ListBaysButtons.Add(buttonLINE3_Lower_46);
            ListBaysButtons.Add(buttonLINE3_Lower_48);
            ListBaysButtons.Add(buttonLINE3_Lower_50);
            ListBaysButtons.Add(buttonLINE3_Lower_52);
            ListBaysButtons.Add(buttonLINE3_Lower_54);
            ListBaysButtons.Add(buttonLINE3_Lower_56);
            ListBaysButtons.Add(buttonLINE3_Lower_58);
            ListBaysButtons.Add(buttonLINE3_Lower_60);
            ListBaysButtons.Add(buttonLINE3_Lower_62);
            ListBaysButtons.Add(buttonLINE3_Lower_64);
            ListBaysButtons.Add(buttonLINE3_Lower_66);
            ListBaysButtons.Add(buttonLINE3_Lower_68);
            ListBaysButtons.Add(buttonLINE3_Lower_70);
            ListBaysButtons.Add(buttonLINE3_Lower_72);
            ListBaysButtons.Add(buttonLINE3_Upper_01);
            ListBaysButtons.Add(buttonLINE3_Upper_03);
            ListBaysButtons.Add(buttonLINE3_Upper_05);
            ListBaysButtons.Add(buttonLINE3_Upper_07);
            ListBaysButtons.Add(buttonLINE3_Upper_09);
            ListBaysButtons.Add(buttonLINE3_Upper_11);
            ListBaysButtons.Add(buttonLINE3_Upper_13);
            ListBaysButtons.Add(buttonLINE3_Upper_15);
            ListBaysButtons.Add(buttonLINE3_Upper_17);
            ListBaysButtons.Add(buttonLINE3_Upper_19);
            ListBaysButtons.Add(buttonLINE3_Upper_21);
            ListBaysButtons.Add(buttonLINE3_Upper_23);
            ListBaysButtons.Add(buttonLINE3_Upper_25);
            ListBaysButtons.Add(buttonLINE3_Upper_27);
            ListBaysButtons.Add(buttonLINE3_Upper_29);
            ListBaysButtons.Add(buttonLINE3_Upper_31);
            ListBaysButtons.Add(buttonLINE3_Upper_33);
            ListBaysButtons.Add(buttonLINE3_Upper_35);
            ListBaysButtons.Add(buttonLINE3_Upper_37);
            ListBaysButtons.Add(buttonLINE3_Upper_39);
            ListBaysButtons.Add(buttonLINE3_Upper_41);
            ListBaysButtons.Add(buttonLINE3_Upper_43);
            ListBaysButtons.Add(buttonLINE3_Upper_45);
            ListBaysButtons.Add(buttonLINE3_Upper_47);
            ListBaysButtons.Add(buttonLINE3_Upper_49);
            ListBaysButtons.Add(buttonLINE3_Upper_51);
            ListBaysButtons.Add(buttonLINE3_Upper_53);
            ListBaysButtons.Add(buttonLINE3_Upper_55);
            ListBaysButtons.Add(buttonLINE3_Upper_57);
            ListBaysButtons.Add(buttonLINE3_Upper_59);
            ListBaysButtons.Add(buttonLINE3_Upper_61);
            ListBaysButtons.Add(buttonLINE3_Upper_63);
            ListBaysButtons.Add(buttonLINE3_Upper_65);
            ListBaysButtons.Add(buttonLINE3_Upper_67);
            ListBaysButtons.Add(buttonLINE3_Upper_69);
            ListBaysButtons.Add(buttonLINE3_Upper_71);
            ListBaysButtons.Add(buttonLINE4_Lower_02);
            ListBaysButtons.Add(buttonLINE4_Lower_04);
            ListBaysButtons.Add(buttonLINE4_Lower_06);
            ListBaysButtons.Add(buttonLINE4_Lower_08);
            ListBaysButtons.Add(buttonLINE4_Lower_10);
            ListBaysButtons.Add(buttonLINE4_Lower_12);
            ListBaysButtons.Add(buttonLINE4_Lower_14);
            ListBaysButtons.Add(buttonLINE4_Lower_16);
            ListBaysButtons.Add(buttonLINE4_Lower_18);
            ListBaysButtons.Add(buttonLINE4_Lower_20);
            ListBaysButtons.Add(buttonLINE4_Lower_22);
            ListBaysButtons.Add(buttonLINE4_Lower_24);
            ListBaysButtons.Add(buttonLINE4_Lower_26);
            ListBaysButtons.Add(buttonLINE4_Lower_28);
            ListBaysButtons.Add(buttonLINE4_Lower_30);
            ListBaysButtons.Add(buttonLINE4_Lower_32);
            ListBaysButtons.Add(buttonLINE4_Lower_34);
            ListBaysButtons.Add(buttonLINE4_Lower_36);
            ListBaysButtons.Add(buttonLINE4_Lower_38);
            ListBaysButtons.Add(buttonLINE4_Lower_40);
            ListBaysButtons.Add(buttonLINE4_Lower_42);
            ListBaysButtons.Add(buttonLINE4_Lower_44);
            ListBaysButtons.Add(buttonLINE4_Lower_46);
            ListBaysButtons.Add(buttonLINE4_Lower_48);
            ListBaysButtons.Add(buttonLINE4_Lower_50);
            ListBaysButtons.Add(buttonLINE4_Lower_52);
            ListBaysButtons.Add(buttonLINE4_Lower_54);
            ListBaysButtons.Add(buttonLINE4_Lower_56);
            ListBaysButtons.Add(buttonLINE4_Lower_58);
            ListBaysButtons.Add(buttonLINE4_Lower_60);
            ListBaysButtons.Add(buttonLINE4_Lower_62);
            ListBaysButtons.Add(buttonLINE4_Lower_64);
            ListBaysButtons.Add(buttonLINE4_Lower_66);
            ListBaysButtons.Add(buttonLINE4_Lower_68);
            ListBaysButtons.Add(buttonLINE4_Lower_70);
            ListBaysButtons.Add(buttonLINE4_Lower_72);
            ListBaysButtons.Add(buttonLINE4_Upper_01);
            ListBaysButtons.Add(buttonLINE4_Upper_03);
            ListBaysButtons.Add(buttonLINE4_Upper_05);
            ListBaysButtons.Add(buttonLINE4_Upper_07);
            ListBaysButtons.Add(buttonLINE4_Upper_09);
            ListBaysButtons.Add(buttonLINE4_Upper_11);
            ListBaysButtons.Add(buttonLINE4_Upper_13);
            ListBaysButtons.Add(buttonLINE4_Upper_15);
            ListBaysButtons.Add(buttonLINE4_Upper_17);
            ListBaysButtons.Add(buttonLINE4_Upper_19);
            ListBaysButtons.Add(buttonLINE4_Upper_21);
            ListBaysButtons.Add(buttonLINE4_Upper_23);
            ListBaysButtons.Add(buttonLINE4_Upper_25);
            ListBaysButtons.Add(buttonLINE4_Upper_27);
            ListBaysButtons.Add(buttonLINE4_Upper_29);
            ListBaysButtons.Add(buttonLINE4_Upper_31);
            ListBaysButtons.Add(buttonLINE4_Upper_33);
            ListBaysButtons.Add(buttonLINE4_Upper_35);
            ListBaysButtons.Add(buttonLINE4_Upper_37);
            ListBaysButtons.Add(buttonLINE4_Upper_39);
            ListBaysButtons.Add(buttonLINE4_Upper_41);
            ListBaysButtons.Add(buttonLINE4_Upper_43);
            ListBaysButtons.Add(buttonLINE4_Upper_45);
            ListBaysButtons.Add(buttonLINE4_Upper_47);
            ListBaysButtons.Add(buttonLINE4_Upper_49);
            ListBaysButtons.Add(buttonLINE4_Upper_51);
            ListBaysButtons.Add(buttonLINE4_Upper_53);
            ListBaysButtons.Add(buttonLINE4_Upper_55);
            ListBaysButtons.Add(buttonLINE4_Upper_57);
            ListBaysButtons.Add(buttonLINE4_Upper_59);
            ListBaysButtons.Add(buttonLINE4_Upper_61);
            ListBaysButtons.Add(buttonLINE4_Upper_63);
            ListBaysButtons.Add(buttonLINE4_Upper_65);
            ListBaysButtons.Add(buttonLINE4_Upper_67);
            ListBaysButtons.Add(buttonLINE4_Upper_69);
            ListBaysButtons.Add(buttonLINE4_Upper_71);
            ListBaysButtons.Add(buttonLINE5_Lower_02);
            ListBaysButtons.Add(buttonLINE5_Lower_04);
            ListBaysButtons.Add(buttonLINE5_Lower_06);
            ListBaysButtons.Add(buttonLINE5_Lower_08);
            ListBaysButtons.Add(buttonLINE5_Lower_10);
            ListBaysButtons.Add(buttonLINE5_Lower_12);
            ListBaysButtons.Add(buttonLINE5_Lower_14);
            ListBaysButtons.Add(buttonLINE5_Lower_16);
            ListBaysButtons.Add(buttonLINE5_Lower_18);
            ListBaysButtons.Add(buttonLINE5_Lower_20);
            ListBaysButtons.Add(buttonLINE5_Lower_22);
            ListBaysButtons.Add(buttonLINE5_Lower_24);
            ListBaysButtons.Add(buttonLINE5_Lower_26);
            ListBaysButtons.Add(buttonLINE5_Lower_28);
            ListBaysButtons.Add(buttonLINE5_Lower_30);
            ListBaysButtons.Add(buttonLINE5_Lower_32);
            ListBaysButtons.Add(buttonLINE5_Lower_34);
            ListBaysButtons.Add(buttonLINE5_Lower_36);
            ListBaysButtons.Add(buttonLINE5_Lower_38);
            ListBaysButtons.Add(buttonLINE5_Lower_40);
            ListBaysButtons.Add(buttonLINE5_Lower_42);
            ListBaysButtons.Add(buttonLINE5_Lower_44);
            ListBaysButtons.Add(buttonLINE5_Lower_46);
            ListBaysButtons.Add(buttonLINE5_Lower_48);
            ListBaysButtons.Add(buttonLINE5_Lower_50);
            ListBaysButtons.Add(buttonLINE5_Lower_52);
            ListBaysButtons.Add(buttonLINE5_Lower_54);
            ListBaysButtons.Add(buttonLINE5_Lower_56);
            ListBaysButtons.Add(buttonLINE5_Lower_58);
            ListBaysButtons.Add(buttonLINE5_Lower_60);
            ListBaysButtons.Add(buttonLINE5_Lower_62);
            ListBaysButtons.Add(buttonLINE5_Lower_64);
            ListBaysButtons.Add(buttonLINE5_Lower_66);
            ListBaysButtons.Add(buttonLINE5_Lower_68);
            ListBaysButtons.Add(buttonLINE5_Lower_70);
            ListBaysButtons.Add(buttonLINE5_Lower_72);
            ListBaysButtons.Add(buttonLINE5_Lower_74);
            ListBaysButtons.Add(buttonLINE5_Lower_76);
            ListBaysButtons.Add(buttonLINE5_Lower_78);
            ListBaysButtons.Add(buttonLINE5_Lower_80);
            ListBaysButtons.Add(buttonLINE5_Upper_01);
            ListBaysButtons.Add(buttonLINE5_Upper_03);
            ListBaysButtons.Add(buttonLINE5_Upper_05);
            ListBaysButtons.Add(buttonLINE5_Upper_07);
            ListBaysButtons.Add(buttonLINE5_Upper_09);
            ListBaysButtons.Add(buttonLINE5_Upper_11);
            ListBaysButtons.Add(buttonLINE5_Upper_13);
            ListBaysButtons.Add(buttonLINE5_Upper_15);
            ListBaysButtons.Add(buttonLINE5_Upper_17);
            ListBaysButtons.Add(buttonLINE5_Upper_19);
            ListBaysButtons.Add(buttonLINE5_Upper_21);
            ListBaysButtons.Add(buttonLINE5_Upper_23);
            ListBaysButtons.Add(buttonLINE5_Upper_25);
            ListBaysButtons.Add(buttonLINE5_Upper_27);
            ListBaysButtons.Add(buttonLINE5_Upper_29);
            ListBaysButtons.Add(buttonLINE5_Upper_31);
            ListBaysButtons.Add(buttonLINE5_Upper_33);
            ListBaysButtons.Add(buttonLINE5_Upper_35);
            ListBaysButtons.Add(buttonLINE5_Upper_37);
            ListBaysButtons.Add(buttonLINE5_Upper_39);
            ListBaysButtons.Add(buttonLINE5_Upper_41);
            ListBaysButtons.Add(buttonLINE5_Upper_43);
            ListBaysButtons.Add(buttonLINE5_Upper_45);
            ListBaysButtons.Add(buttonLINE5_Upper_47);
            ListBaysButtons.Add(buttonLINE5_Upper_49);
            ListBaysButtons.Add(buttonLINE5_Upper_51);
            ListBaysButtons.Add(buttonLINE5_Upper_53);
            ListBaysButtons.Add(buttonLINE5_Upper_55);
            ListBaysButtons.Add(buttonLINE5_Upper_57);
            ListBaysButtons.Add(buttonLINE5_Upper_59);
            ListBaysButtons.Add(buttonLINE5_Upper_61);
            ListBaysButtons.Add(buttonLINE5_Upper_63);
            ListBaysButtons.Add(buttonLINE5_Upper_65);
            ListBaysButtons.Add(buttonLINE5_Upper_67);
            ListBaysButtons.Add(buttonLINE5_Upper_69);
            ListBaysButtons.Add(buttonLINE5_Upper_71);
            ListBaysButtons.Add(buttonLINE5_Upper_73);
            ListBaysButtons.Add(buttonLINE5_Upper_75);
            ListBaysButtons.Add(buttonLINE5_Upper_77);
            ListBaysButtons.Add(buttonLINE5_Upper_79);
            ListBaysButtons.Add(buttonLINE6_Lower_02);
            ListBaysButtons.Add(buttonLINE6_Lower_04);
            ListBaysButtons.Add(buttonLINE6_Lower_06);
            ListBaysButtons.Add(buttonLINE6_Lower_08);
            ListBaysButtons.Add(buttonLINE6_Lower_10);
            ListBaysButtons.Add(buttonLINE6_Lower_12);
            ListBaysButtons.Add(buttonLINE6_Lower_14);
            ListBaysButtons.Add(buttonLINE6_Lower_16);
            ListBaysButtons.Add(buttonLINE6_Lower_18);
            ListBaysButtons.Add(buttonLINE6_Lower_20);
            ListBaysButtons.Add(buttonLINE6_Lower_22);
            ListBaysButtons.Add(buttonLINE6_Lower_24);
            ListBaysButtons.Add(buttonLINE6_Lower_26);
            ListBaysButtons.Add(buttonLINE6_Lower_28);
            ListBaysButtons.Add(buttonLINE6_Lower_30);
            ListBaysButtons.Add(buttonLINE6_Lower_32);
            ListBaysButtons.Add(buttonLINE6_Lower_34);
            ListBaysButtons.Add(buttonLINE6_Lower_36);
            ListBaysButtons.Add(buttonLINE6_Lower_38);
            ListBaysButtons.Add(buttonLINE6_Lower_40);
            ListBaysButtons.Add(buttonLINE6_Upper_01);
            ListBaysButtons.Add(buttonLINE6_Upper_03);
            ListBaysButtons.Add(buttonLINE6_Upper_05);
            ListBaysButtons.Add(buttonLINE6_Upper_07);
            ListBaysButtons.Add(buttonLINE6_Upper_09);
            ListBaysButtons.Add(buttonLINE6_Upper_11);
            ListBaysButtons.Add(buttonLINE6_Upper_13);
            ListBaysButtons.Add(buttonLINE6_Upper_15);
            ListBaysButtons.Add(buttonLINE6_Upper_17);
            ListBaysButtons.Add(buttonLINE6_Upper_19);
            ListBaysButtons.Add(buttonLINE6_Upper_21);
            ListBaysButtons.Add(buttonLINE6_Upper_23);
            ListBaysButtons.Add(buttonLINE6_Upper_25);
            ListBaysButtons.Add(buttonLINE6_Upper_27);
            ListBaysButtons.Add(buttonLINE6_Upper_29);
            ListBaysButtons.Add(buttonLINE6_Upper_31);
            ListBaysButtons.Add(buttonLINE6_Upper_33);
            ListBaysButtons.Add(buttonLINE6_Upper_35);
            ListBaysButtons.Add(buttonLINE6_Upper_37);
            ListBaysButtons.Add(buttonLINE6_Upper_39);
            ListBaysButtons.Add(buttonLINE7_Lower_02);
            ListBaysButtons.Add(buttonLINE7_Lower_04);
            ListBaysButtons.Add(buttonLINE7_Lower_06);
            ListBaysButtons.Add(buttonLINE7_Lower_08);
            ListBaysButtons.Add(buttonLINE7_Lower_10);
            ListBaysButtons.Add(buttonLINE7_Lower_12);
            ListBaysButtons.Add(buttonLINE7_Lower_14);
            ListBaysButtons.Add(buttonLINE7_Lower_16);
            ListBaysButtons.Add(buttonLINE7_Lower_18);
            ListBaysButtons.Add(buttonLINE7_Lower_20);
            ListBaysButtons.Add(buttonLINE7_Lower_22);
            ListBaysButtons.Add(buttonLINE7_Lower_24);
            ListBaysButtons.Add(buttonLINE7_Lower_26);
            ListBaysButtons.Add(buttonLINE7_Lower_28);
            ListBaysButtons.Add(buttonLINE7_Lower_30);
            ListBaysButtons.Add(buttonLINE7_Lower_32);
            ListBaysButtons.Add(buttonLINE7_Lower_34);
            ListBaysButtons.Add(buttonLINE7_Lower_36);
            ListBaysButtons.Add(buttonLINE7_Lower_38);
            ListBaysButtons.Add(buttonLINE7_Lower_40);
            ListBaysButtons.Add(buttonLINE7_Lower_50);
            ListBaysButtons.Add(buttonLINE7_Lower_52);
            ListBaysButtons.Add(buttonLINE7_Lower_54);
            ListBaysButtons.Add(buttonLINE7_Lower_56);
            ListBaysButtons.Add(buttonLINE7_Lower_58);
            ListBaysButtons.Add(buttonLINE7_Lower_60);
            ListBaysButtons.Add(buttonLINE7_Lower_62);
            ListBaysButtons.Add(buttonLINE7_Lower_64);
            ListBaysButtons.Add(buttonLINE7_Lower_66);
            ListBaysButtons.Add(buttonLINE7_Lower_68);
            ListBaysButtons.Add(buttonLINE7_Lower_70);
            ListBaysButtons.Add(buttonLINE7_Lower_72);
            ListBaysButtons.Add(buttonLINE7_Upper_01);
            ListBaysButtons.Add(buttonLINE7_Upper_03);
            ListBaysButtons.Add(buttonLINE7_Upper_05);
            ListBaysButtons.Add(buttonLINE7_Upper_07);
            ListBaysButtons.Add(buttonLINE7_Upper_09);
            ListBaysButtons.Add(buttonLINE7_Upper_11);
            ListBaysButtons.Add(buttonLINE7_Upper_13);
            ListBaysButtons.Add(buttonLINE7_Upper_15);
            ListBaysButtons.Add(buttonLINE7_Upper_17);
            ListBaysButtons.Add(buttonLINE7_Upper_19);
            ListBaysButtons.Add(buttonLINE7_Upper_21);
            ListBaysButtons.Add(buttonLINE7_Upper_23);
            ListBaysButtons.Add(buttonLINE7_Upper_25);
            ListBaysButtons.Add(buttonLINE7_Upper_27);
            ListBaysButtons.Add(buttonLINE7_Upper_29);
            ListBaysButtons.Add(buttonLINE7_Upper_31);
            ListBaysButtons.Add(buttonLINE7_Upper_33);
            ListBaysButtons.Add(buttonLINE7_Upper_35);
            ListBaysButtons.Add(buttonLINE7_Upper_37);
            ListBaysButtons.Add(buttonLINE7_Upper_39);
            ListBaysButtons.Add(buttonLINE7_Upper_49);
            ListBaysButtons.Add(buttonLINE7_Upper_51);
            ListBaysButtons.Add(buttonLINE7_Upper_53);
            ListBaysButtons.Add(buttonLINE7_Upper_55);
            ListBaysButtons.Add(buttonLINE7_Upper_57);
            ListBaysButtons.Add(buttonLINE7_Upper_59);
            ListBaysButtons.Add(buttonLINE7_Upper_61);
            ListBaysButtons.Add(buttonLINE7_Upper_63);
            ListBaysButtons.Add(buttonLINE7_Upper_65);
            ListBaysButtons.Add(buttonLINE7_Upper_67);
            ListBaysButtons.Add(buttonLINE7_Upper_69);
            ListBaysButtons.Add(buttonLINE7_Upper_71);
            ListBaysButtons.Add(buttonLINE8_Lower_02);
            ListBaysButtons.Add(buttonLINE8_Lower_04);
            ListBaysButtons.Add(buttonLINE8_Lower_06);
            ListBaysButtons.Add(buttonLINE8_Lower_08);
            ListBaysButtons.Add(buttonLINE8_Lower_10);
            ListBaysButtons.Add(buttonLINE8_Lower_12);
            ListBaysButtons.Add(buttonLINE8_Lower_14);
            ListBaysButtons.Add(buttonLINE8_Lower_16);
            ListBaysButtons.Add(buttonLINE8_Lower_18);
            ListBaysButtons.Add(buttonLINE8_Lower_20);
            ListBaysButtons.Add(buttonLINE8_Lower_22);
            ListBaysButtons.Add(buttonLINE8_Lower_24);
            ListBaysButtons.Add(buttonLINE8_Lower_26);
            ListBaysButtons.Add(buttonLINE8_Lower_28);
            ListBaysButtons.Add(buttonLINE8_Lower_30);
            ListBaysButtons.Add(buttonLINE8_Lower_32);
            ListBaysButtons.Add(buttonLINE8_Lower_34);
            ListBaysButtons.Add(buttonLINE8_Lower_36);
            ListBaysButtons.Add(buttonLINE8_Lower_38);
            ListBaysButtons.Add(buttonLINE8_Lower_40);
            ListBaysButtons.Add(buttonLINE8_Lower_50);
            ListBaysButtons.Add(buttonLINE8_Lower_52);
            ListBaysButtons.Add(buttonLINE8_Lower_54);
            ListBaysButtons.Add(buttonLINE8_Lower_56);
            ListBaysButtons.Add(buttonLINE8_Lower_58);
            ListBaysButtons.Add(buttonLINE8_Lower_60);
            ListBaysButtons.Add(buttonLINE8_Lower_62);
            ListBaysButtons.Add(buttonLINE8_Lower_64);
            ListBaysButtons.Add(buttonLINE8_Lower_66);
            ListBaysButtons.Add(buttonLINE8_Lower_68);
            ListBaysButtons.Add(buttonLINE8_Lower_70);
            ListBaysButtons.Add(buttonLINE8_Lower_72);
            ListBaysButtons.Add(buttonLINE8_Upper_01);
            ListBaysButtons.Add(buttonLINE8_Upper_03);
            ListBaysButtons.Add(buttonLINE8_Upper_05);
            ListBaysButtons.Add(buttonLINE8_Upper_07);
            ListBaysButtons.Add(buttonLINE8_Upper_09);
            ListBaysButtons.Add(buttonLINE8_Upper_11);
            ListBaysButtons.Add(buttonLINE8_Upper_13);
            ListBaysButtons.Add(buttonLINE8_Upper_15);
            ListBaysButtons.Add(buttonLINE8_Upper_17);
            ListBaysButtons.Add(buttonLINE8_Upper_19);
            ListBaysButtons.Add(buttonLINE8_Upper_21);
            ListBaysButtons.Add(buttonLINE8_Upper_23);
            ListBaysButtons.Add(buttonLINE8_Upper_25);
            ListBaysButtons.Add(buttonLINE8_Upper_27);
            ListBaysButtons.Add(buttonLINE8_Upper_29);
            ListBaysButtons.Add(buttonLINE8_Upper_31);
            ListBaysButtons.Add(buttonLINE8_Upper_33);
            ListBaysButtons.Add(buttonLINE8_Upper_35);
            ListBaysButtons.Add(buttonLINE8_Upper_37);
            ListBaysButtons.Add(buttonLINE8_Upper_39);
            ListBaysButtons.Add(buttonLINE8_Upper_49);
            ListBaysButtons.Add(buttonLINE8_Upper_51);
            ListBaysButtons.Add(buttonLINE8_Upper_53);
            ListBaysButtons.Add(buttonLINE8_Upper_55);
            ListBaysButtons.Add(buttonLINE8_Upper_57);
            ListBaysButtons.Add(buttonLINE8_Upper_59);
            ListBaysButtons.Add(buttonLINE8_Upper_61);
            ListBaysButtons.Add(buttonLINE8_Upper_63);
            ListBaysButtons.Add(buttonLINE8_Upper_65);
            ListBaysButtons.Add(buttonLINE8_Upper_67);
            ListBaysButtons.Add(buttonLINE8_Upper_69);
            ListBaysButtons.Add(buttonLINE8_Upper_71);
            ListBaysButtons.Add(buttonLINEDMR_Lower_02);
            ListBaysButtons.Add(buttonLINEDMR_Lower_04);
            ListBaysButtons.Add(buttonLINEDMR_Lower_06);
            ListBaysButtons.Add(buttonLINEDMR_Lower_08);
            ListBaysButtons.Add(buttonLINEDMR_Lower_10);
            ListBaysButtons.Add(buttonLINEDMR_Lower_12);
            ListBaysButtons.Add(buttonLINEDMR_Lower_14);
            ListBaysButtons.Add(buttonLINEDMR_Lower_16);
            ListBaysButtons.Add(buttonLINEDMR_Upper_01);
            ListBaysButtons.Add(buttonLINEDMR_Upper_03);
            ListBaysButtons.Add(buttonLINEDMR_Upper_05);
            ListBaysButtons.Add(buttonLINEDMR_Upper_07);
            ListBaysButtons.Add(buttonLINEOBA_Lower_02);
            ListBaysButtons.Add(buttonLINEOBA_Lower_04);
            ListBaysButtons.Add(buttonLINEOBA_Lower_06);
            ListBaysButtons.Add(buttonLINEOBA_Lower_08);
            ListBaysButtons.Add(buttonLINEOBA_Lower_10);
            ListBaysButtons.Add(buttonLINEOBA_Lower_12);
            ListBaysButtons.Add(buttonLINEOBA_Lower_14);
            ListBaysButtons.Add(buttonLINEOBA_Lower_16);
            ListBaysButtons.Add(buttonLINEOBA_Upper_01);
            ListBaysButtons.Add(buttonLINEOBA_Upper_03);
            ListBaysButtons.Add(buttonLINEOBA_Upper_05);
            ListBaysButtons.Add(buttonLINEOBA_Upper_07);
            ListBaysButtons.Add(buttonLINEOBA_Upper_09);
            ListBaysButtons.Add(buttonLINEOBA_Upper_11);
            ListBaysButtons.Add(buttonLINEOBA_Upper_13);
            ListBaysButtons.Add(buttonLINEOBA_Upper_15);
            ListBaysButtons.Add(buttonLINEPRI_Lower_10);
            ListBaysButtons.Add(buttonLINEPRI_Lower_12);
            ListBaysButtons.Add(buttonLINEPRI_Lower_14);
            ListBaysButtons.Add(buttonLINEPRI_Lower_16);
            ListBaysButtons.Add(buttonLINEPRI_Lower_18);
            ListBaysButtons.Add(buttonLINEPRI_Lower_20);
            ListBaysButtons.Add(buttonLINEPRI_Lower_22);
            ListBaysButtons.Add(buttonLINEPRI_Lower_24);
            ListBaysButtons.Add(buttonLINEPRI_Upper_09);
            ListBaysButtons.Add(buttonLINEPRI_Upper_11);
            ListBaysButtons.Add(buttonLINEPRI_Upper_13);
            ListBaysButtons.Add(buttonLINEPRI_Upper_15);
            ListBaysButtons.Add(buttonLINEPRI_Upper_17);
            ListBaysButtons.Add(buttonLINEPRI_Upper_19);
            ListBaysButtons.Add(buttonLINEPRI_Upper_21);
            ListBaysButtons.Add(buttonLINEPRI_Upper_23);

            DictionaryBaysButtons.Add("LINE2_B_25", buttonLINE2_B_25);
            DictionaryBaysButtons.Add("LINE2_B_26", buttonLINE2_B_26);
            DictionaryBaysButtons.Add("LINE2_B_27", buttonLINE2_B_27);
            DictionaryBaysButtons.Add("LINE2_B_28", buttonLINE2_B_28);
            DictionaryBaysButtons.Add("LINE2_B_29", buttonLINE2_B_29);
            DictionaryBaysButtons.Add("LINE2_B_30", buttonLINE2_B_30);
            DictionaryBaysButtons.Add("LINE2_B_31", buttonLINE2_B_31);
            DictionaryBaysButtons.Add("LINE2_B_32", buttonLINE2_B_32);
            DictionaryBaysButtons.Add("LINE2_B_33", buttonLINE2_B_33);
            DictionaryBaysButtons.Add("LINE2_B_34", buttonLINE2_B_34);
            DictionaryBaysButtons.Add("LINE2_B_35", buttonLINE2_B_35);
            DictionaryBaysButtons.Add("LINE2_B_36", buttonLINE2_B_36);
            DictionaryBaysButtons.Add("LINE3_Lower_02", buttonLINE3_Lower_02);
            DictionaryBaysButtons.Add("LINE3_Lower_04", buttonLINE3_Lower_04);
            DictionaryBaysButtons.Add("LINE3_Lower_06", buttonLINE3_Lower_06);
            DictionaryBaysButtons.Add("LINE3_Lower_08", buttonLINE3_Lower_08);
            DictionaryBaysButtons.Add("LINE3_Lower_10", buttonLINE3_Lower_10);
            DictionaryBaysButtons.Add("LINE3_Lower_12", buttonLINE3_Lower_12);
            DictionaryBaysButtons.Add("LINE3_Lower_14", buttonLINE3_Lower_14);
            DictionaryBaysButtons.Add("LINE3_Lower_16", buttonLINE3_Lower_16);
            DictionaryBaysButtons.Add("LINE3_Lower_18", buttonLINE3_Lower_18);
            DictionaryBaysButtons.Add("LINE3_Lower_20", buttonLINE3_Lower_20);
            DictionaryBaysButtons.Add("LINE3_Lower_22", buttonLINE3_Lower_22);
            DictionaryBaysButtons.Add("LINE3_Lower_24", buttonLINE3_Lower_24);
            DictionaryBaysButtons.Add("LINE3_Lower_26", buttonLINE3_Lower_26);
            DictionaryBaysButtons.Add("LINE3_Lower_28", buttonLINE3_Lower_28);
            DictionaryBaysButtons.Add("LINE3_Lower_30", buttonLINE3_Lower_30);
            DictionaryBaysButtons.Add("LINE3_Lower_32", buttonLINE3_Lower_32);
            DictionaryBaysButtons.Add("LINE3_Lower_34", buttonLINE3_Lower_34);
            DictionaryBaysButtons.Add("LINE3_Lower_36", buttonLINE3_Lower_36);
            DictionaryBaysButtons.Add("LINE3_Lower_38", buttonLINE3_Lower_38);
            DictionaryBaysButtons.Add("LINE3_Lower_40", buttonLINE3_Lower_40);
            DictionaryBaysButtons.Add("LINE3_Lower_42", buttonLINE3_Lower_42);
            DictionaryBaysButtons.Add("LINE3_Lower_44", buttonLINE3_Lower_44);
            DictionaryBaysButtons.Add("LINE3_Lower_46", buttonLINE3_Lower_46);
            DictionaryBaysButtons.Add("LINE3_Lower_48", buttonLINE3_Lower_48);
            DictionaryBaysButtons.Add("LINE3_Lower_50", buttonLINE3_Lower_50);
            DictionaryBaysButtons.Add("LINE3_Lower_52", buttonLINE3_Lower_52);
            DictionaryBaysButtons.Add("LINE3_Lower_54", buttonLINE3_Lower_54);
            DictionaryBaysButtons.Add("LINE3_Lower_56", buttonLINE3_Lower_56);
            DictionaryBaysButtons.Add("LINE3_Lower_58", buttonLINE3_Lower_58);
            DictionaryBaysButtons.Add("LINE3_Lower_60", buttonLINE3_Lower_60);
            DictionaryBaysButtons.Add("LINE3_Lower_62", buttonLINE3_Lower_62);
            DictionaryBaysButtons.Add("LINE3_Lower_64", buttonLINE3_Lower_64);
            DictionaryBaysButtons.Add("LINE3_Lower_66", buttonLINE3_Lower_66);
            DictionaryBaysButtons.Add("LINE3_Lower_68", buttonLINE3_Lower_68);
            DictionaryBaysButtons.Add("LINE3_Lower_70", buttonLINE3_Lower_70);
            DictionaryBaysButtons.Add("LINE3_Lower_72", buttonLINE3_Lower_72);
            DictionaryBaysButtons.Add("LINE3_Upper_01", buttonLINE3_Upper_01);
            DictionaryBaysButtons.Add("LINE3_Upper_03", buttonLINE3_Upper_03);
            DictionaryBaysButtons.Add("LINE3_Upper_05", buttonLINE3_Upper_05);
            DictionaryBaysButtons.Add("LINE3_Upper_07", buttonLINE3_Upper_07);
            DictionaryBaysButtons.Add("LINE3_Upper_09", buttonLINE3_Upper_09);
            DictionaryBaysButtons.Add("LINE3_Upper_11", buttonLINE3_Upper_11);
            DictionaryBaysButtons.Add("LINE3_Upper_13", buttonLINE3_Upper_13);
            DictionaryBaysButtons.Add("LINE3_Upper_15", buttonLINE3_Upper_15);
            DictionaryBaysButtons.Add("LINE3_Upper_17", buttonLINE3_Upper_17);
            DictionaryBaysButtons.Add("LINE3_Upper_19", buttonLINE3_Upper_19);
            DictionaryBaysButtons.Add("LINE3_Upper_21", buttonLINE3_Upper_21);
            DictionaryBaysButtons.Add("LINE3_Upper_23", buttonLINE3_Upper_23);
            DictionaryBaysButtons.Add("LINE3_Upper_25", buttonLINE3_Upper_25);
            DictionaryBaysButtons.Add("LINE3_Upper_27", buttonLINE3_Upper_27);
            DictionaryBaysButtons.Add("LINE3_Upper_29", buttonLINE3_Upper_29);
            DictionaryBaysButtons.Add("LINE3_Upper_31", buttonLINE3_Upper_31);
            DictionaryBaysButtons.Add("LINE3_Upper_33", buttonLINE3_Upper_33);
            DictionaryBaysButtons.Add("LINE3_Upper_35", buttonLINE3_Upper_35);
            DictionaryBaysButtons.Add("LINE3_Upper_37", buttonLINE3_Upper_37);
            DictionaryBaysButtons.Add("LINE3_Upper_39", buttonLINE3_Upper_39);
            DictionaryBaysButtons.Add("LINE3_Upper_41", buttonLINE3_Upper_41);
            DictionaryBaysButtons.Add("LINE3_Upper_43", buttonLINE3_Upper_43);
            DictionaryBaysButtons.Add("LINE3_Upper_45", buttonLINE3_Upper_45);
            DictionaryBaysButtons.Add("LINE3_Upper_47", buttonLINE3_Upper_47);
            DictionaryBaysButtons.Add("LINE3_Upper_49", buttonLINE3_Upper_49);
            DictionaryBaysButtons.Add("LINE3_Upper_51", buttonLINE3_Upper_51);
            DictionaryBaysButtons.Add("LINE3_Upper_53", buttonLINE3_Upper_53);
            DictionaryBaysButtons.Add("LINE3_Upper_55", buttonLINE3_Upper_55);
            DictionaryBaysButtons.Add("LINE3_Upper_57", buttonLINE3_Upper_57);
            DictionaryBaysButtons.Add("LINE3_Upper_59", buttonLINE3_Upper_59);
            DictionaryBaysButtons.Add("LINE3_Upper_61", buttonLINE3_Upper_61);
            DictionaryBaysButtons.Add("LINE3_Upper_63", buttonLINE3_Upper_63);
            DictionaryBaysButtons.Add("LINE3_Upper_65", buttonLINE3_Upper_65);
            DictionaryBaysButtons.Add("LINE3_Upper_67", buttonLINE3_Upper_67);
            DictionaryBaysButtons.Add("LINE3_Upper_69", buttonLINE3_Upper_69);
            DictionaryBaysButtons.Add("LINE3_Upper_71", buttonLINE3_Upper_71);
            DictionaryBaysButtons.Add("LINE4_Lower_02", buttonLINE4_Lower_02);
            DictionaryBaysButtons.Add("LINE4_Lower_04", buttonLINE4_Lower_04);
            DictionaryBaysButtons.Add("LINE4_Lower_06", buttonLINE4_Lower_06);
            DictionaryBaysButtons.Add("LINE4_Lower_08", buttonLINE4_Lower_08);
            DictionaryBaysButtons.Add("LINE4_Lower_10", buttonLINE4_Lower_10);
            DictionaryBaysButtons.Add("LINE4_Lower_12", buttonLINE4_Lower_12);
            DictionaryBaysButtons.Add("LINE4_Lower_14", buttonLINE4_Lower_14);
            DictionaryBaysButtons.Add("LINE4_Lower_16", buttonLINE4_Lower_16);
            DictionaryBaysButtons.Add("LINE4_Lower_18", buttonLINE4_Lower_18);
            DictionaryBaysButtons.Add("LINE4_Lower_20", buttonLINE4_Lower_20);
            DictionaryBaysButtons.Add("LINE4_Lower_22", buttonLINE4_Lower_22);
            DictionaryBaysButtons.Add("LINE4_Lower_24", buttonLINE4_Lower_24);
            DictionaryBaysButtons.Add("LINE4_Lower_26", buttonLINE4_Lower_26);
            DictionaryBaysButtons.Add("LINE4_Lower_28", buttonLINE4_Lower_28);
            DictionaryBaysButtons.Add("LINE4_Lower_30", buttonLINE4_Lower_30);
            DictionaryBaysButtons.Add("LINE4_Lower_32", buttonLINE4_Lower_32);
            DictionaryBaysButtons.Add("LINE4_Lower_34", buttonLINE4_Lower_34);
            DictionaryBaysButtons.Add("LINE4_Lower_36", buttonLINE4_Lower_36);
            DictionaryBaysButtons.Add("LINE4_Lower_38", buttonLINE4_Lower_38);
            DictionaryBaysButtons.Add("LINE4_Lower_40", buttonLINE4_Lower_40);
            DictionaryBaysButtons.Add("LINE4_Lower_42", buttonLINE4_Lower_42);
            DictionaryBaysButtons.Add("LINE4_Lower_44", buttonLINE4_Lower_44);
            DictionaryBaysButtons.Add("LINE4_Lower_46", buttonLINE4_Lower_46);
            DictionaryBaysButtons.Add("LINE4_Lower_48", buttonLINE4_Lower_48);
            DictionaryBaysButtons.Add("LINE4_Lower_50", buttonLINE4_Lower_50);
            DictionaryBaysButtons.Add("LINE4_Lower_52", buttonLINE4_Lower_52);
            DictionaryBaysButtons.Add("LINE4_Lower_54", buttonLINE4_Lower_54);
            DictionaryBaysButtons.Add("LINE4_Lower_56", buttonLINE4_Lower_56);
            DictionaryBaysButtons.Add("LINE4_Lower_58", buttonLINE4_Lower_58);
            DictionaryBaysButtons.Add("LINE4_Lower_60", buttonLINE4_Lower_60);
            DictionaryBaysButtons.Add("LINE4_Lower_62", buttonLINE4_Lower_62);
            DictionaryBaysButtons.Add("LINE4_Lower_64", buttonLINE4_Lower_64);
            DictionaryBaysButtons.Add("LINE4_Lower_66", buttonLINE4_Lower_66);
            DictionaryBaysButtons.Add("LINE4_Lower_68", buttonLINE4_Lower_68);
            DictionaryBaysButtons.Add("LINE4_Lower_70", buttonLINE4_Lower_70);
            DictionaryBaysButtons.Add("LINE4_Lower_72", buttonLINE4_Lower_72);
            DictionaryBaysButtons.Add("LINE4_Upper_01", buttonLINE4_Upper_01);
            DictionaryBaysButtons.Add("LINE4_Upper_03", buttonLINE4_Upper_03);
            DictionaryBaysButtons.Add("LINE4_Upper_05", buttonLINE4_Upper_05);
            DictionaryBaysButtons.Add("LINE4_Upper_07", buttonLINE4_Upper_07);
            DictionaryBaysButtons.Add("LINE4_Upper_09", buttonLINE4_Upper_09);
            DictionaryBaysButtons.Add("LINE4_Upper_11", buttonLINE4_Upper_11);
            DictionaryBaysButtons.Add("LINE4_Upper_13", buttonLINE4_Upper_13);
            DictionaryBaysButtons.Add("LINE4_Upper_15", buttonLINE4_Upper_15);
            DictionaryBaysButtons.Add("LINE4_Upper_17", buttonLINE4_Upper_17);
            DictionaryBaysButtons.Add("LINE4_Upper_19", buttonLINE4_Upper_19);
            DictionaryBaysButtons.Add("LINE4_Upper_21", buttonLINE4_Upper_21);
            DictionaryBaysButtons.Add("LINE4_Upper_23", buttonLINE4_Upper_23);
            DictionaryBaysButtons.Add("LINE4_Upper_25", buttonLINE4_Upper_25);
            DictionaryBaysButtons.Add("LINE4_Upper_27", buttonLINE4_Upper_27);
            DictionaryBaysButtons.Add("LINE4_Upper_29", buttonLINE4_Upper_29);
            DictionaryBaysButtons.Add("LINE4_Upper_31", buttonLINE4_Upper_31);
            DictionaryBaysButtons.Add("LINE4_Upper_33", buttonLINE4_Upper_33);
            DictionaryBaysButtons.Add("LINE4_Upper_35", buttonLINE4_Upper_35);
            DictionaryBaysButtons.Add("LINE4_Upper_37", buttonLINE4_Upper_37);
            DictionaryBaysButtons.Add("LINE4_Upper_39", buttonLINE4_Upper_39);
            DictionaryBaysButtons.Add("LINE4_Upper_41", buttonLINE4_Upper_41);
            DictionaryBaysButtons.Add("LINE4_Upper_43", buttonLINE4_Upper_43);
            DictionaryBaysButtons.Add("LINE4_Upper_45", buttonLINE4_Upper_45);
            DictionaryBaysButtons.Add("LINE4_Upper_47", buttonLINE4_Upper_47);
            DictionaryBaysButtons.Add("LINE4_Upper_49", buttonLINE4_Upper_49);
            DictionaryBaysButtons.Add("LINE4_Upper_51", buttonLINE4_Upper_51);
            DictionaryBaysButtons.Add("LINE4_Upper_53", buttonLINE4_Upper_53);
            DictionaryBaysButtons.Add("LINE4_Upper_55", buttonLINE4_Upper_55);
            DictionaryBaysButtons.Add("LINE4_Upper_57", buttonLINE4_Upper_57);
            DictionaryBaysButtons.Add("LINE4_Upper_59", buttonLINE4_Upper_59);
            DictionaryBaysButtons.Add("LINE4_Upper_61", buttonLINE4_Upper_61);
            DictionaryBaysButtons.Add("LINE4_Upper_63", buttonLINE4_Upper_63);
            DictionaryBaysButtons.Add("LINE4_Upper_65", buttonLINE4_Upper_65);
            DictionaryBaysButtons.Add("LINE4_Upper_67", buttonLINE4_Upper_67);
            DictionaryBaysButtons.Add("LINE4_Upper_69", buttonLINE4_Upper_69);
            DictionaryBaysButtons.Add("LINE4_Upper_71", buttonLINE4_Upper_71);
            DictionaryBaysButtons.Add("LINE5_Lower_02", buttonLINE5_Lower_02);
            DictionaryBaysButtons.Add("LINE5_Lower_04", buttonLINE5_Lower_04);
            DictionaryBaysButtons.Add("LINE5_Lower_06", buttonLINE5_Lower_06);
            DictionaryBaysButtons.Add("LINE5_Lower_08", buttonLINE5_Lower_08);
            DictionaryBaysButtons.Add("LINE5_Lower_10", buttonLINE5_Lower_10);
            DictionaryBaysButtons.Add("LINE5_Lower_12", buttonLINE5_Lower_12);
            DictionaryBaysButtons.Add("LINE5_Lower_14", buttonLINE5_Lower_14);
            DictionaryBaysButtons.Add("LINE5_Lower_16", buttonLINE5_Lower_16);
            DictionaryBaysButtons.Add("LINE5_Lower_18", buttonLINE5_Lower_18);
            DictionaryBaysButtons.Add("LINE5_Lower_20", buttonLINE5_Lower_20);
            DictionaryBaysButtons.Add("LINE5_Lower_22", buttonLINE5_Lower_22);
            DictionaryBaysButtons.Add("LINE5_Lower_24", buttonLINE5_Lower_24);
            DictionaryBaysButtons.Add("LINE5_Lower_26", buttonLINE5_Lower_26);
            DictionaryBaysButtons.Add("LINE5_Lower_28", buttonLINE5_Lower_28);
            DictionaryBaysButtons.Add("LINE5_Lower_30", buttonLINE5_Lower_30);
            DictionaryBaysButtons.Add("LINE5_Lower_32", buttonLINE5_Lower_32);
            DictionaryBaysButtons.Add("LINE5_Lower_34", buttonLINE5_Lower_34);
            DictionaryBaysButtons.Add("LINE5_Lower_36", buttonLINE5_Lower_36);
            DictionaryBaysButtons.Add("LINE5_Lower_38", buttonLINE5_Lower_38);
            DictionaryBaysButtons.Add("LINE5_Lower_40", buttonLINE5_Lower_40);
            DictionaryBaysButtons.Add("LINE5_Lower_42", buttonLINE5_Lower_42);
            DictionaryBaysButtons.Add("LINE5_Lower_44", buttonLINE5_Lower_44);
            DictionaryBaysButtons.Add("LINE5_Lower_46", buttonLINE5_Lower_46);
            DictionaryBaysButtons.Add("LINE5_Lower_48", buttonLINE5_Lower_48);
            DictionaryBaysButtons.Add("LINE5_Lower_50", buttonLINE5_Lower_50);
            DictionaryBaysButtons.Add("LINE5_Lower_52", buttonLINE5_Lower_52);
            DictionaryBaysButtons.Add("LINE5_Lower_54", buttonLINE5_Lower_54);
            DictionaryBaysButtons.Add("LINE5_Lower_56", buttonLINE5_Lower_56);
            DictionaryBaysButtons.Add("LINE5_Lower_58", buttonLINE5_Lower_58);
            DictionaryBaysButtons.Add("LINE5_Lower_60", buttonLINE5_Lower_60);
            DictionaryBaysButtons.Add("LINE5_Lower_62", buttonLINE5_Lower_62);
            DictionaryBaysButtons.Add("LINE5_Lower_64", buttonLINE5_Lower_64);
            DictionaryBaysButtons.Add("LINE5_Lower_66", buttonLINE5_Lower_66);
            DictionaryBaysButtons.Add("LINE5_Lower_68", buttonLINE5_Lower_68);
            DictionaryBaysButtons.Add("LINE5_Lower_70", buttonLINE5_Lower_70);
            DictionaryBaysButtons.Add("LINE5_Lower_72", buttonLINE5_Lower_72);
            DictionaryBaysButtons.Add("LINE5_Lower_74", buttonLINE5_Lower_74);
            DictionaryBaysButtons.Add("LINE5_Lower_76", buttonLINE5_Lower_76);
            DictionaryBaysButtons.Add("LINE5_Lower_78", buttonLINE5_Lower_78);
            DictionaryBaysButtons.Add("LINE5_Lower_80", buttonLINE5_Lower_80);
            DictionaryBaysButtons.Add("LINE5_Upper_01", buttonLINE5_Upper_01);
            DictionaryBaysButtons.Add("LINE5_Upper_03", buttonLINE5_Upper_03);
            DictionaryBaysButtons.Add("LINE5_Upper_05", buttonLINE5_Upper_05);
            DictionaryBaysButtons.Add("LINE5_Upper_07", buttonLINE5_Upper_07);
            DictionaryBaysButtons.Add("LINE5_Upper_09", buttonLINE5_Upper_09);
            DictionaryBaysButtons.Add("LINE5_Upper_11", buttonLINE5_Upper_11);
            DictionaryBaysButtons.Add("LINE5_Upper_13", buttonLINE5_Upper_13);
            DictionaryBaysButtons.Add("LINE5_Upper_15", buttonLINE5_Upper_15);
            DictionaryBaysButtons.Add("LINE5_Upper_17", buttonLINE5_Upper_17);
            DictionaryBaysButtons.Add("LINE5_Upper_19", buttonLINE5_Upper_19);
            DictionaryBaysButtons.Add("LINE5_Upper_21", buttonLINE5_Upper_21);
            DictionaryBaysButtons.Add("LINE5_Upper_23", buttonLINE5_Upper_23);
            DictionaryBaysButtons.Add("LINE5_Upper_25", buttonLINE5_Upper_25);
            DictionaryBaysButtons.Add("LINE5_Upper_27", buttonLINE5_Upper_27);
            DictionaryBaysButtons.Add("LINE5_Upper_29", buttonLINE5_Upper_29);
            DictionaryBaysButtons.Add("LINE5_Upper_31", buttonLINE5_Upper_31);
            DictionaryBaysButtons.Add("LINE5_Upper_33", buttonLINE5_Upper_33);
            DictionaryBaysButtons.Add("LINE5_Upper_35", buttonLINE5_Upper_35);
            DictionaryBaysButtons.Add("LINE5_Upper_37", buttonLINE5_Upper_37);
            DictionaryBaysButtons.Add("LINE5_Upper_39", buttonLINE5_Upper_39);
            DictionaryBaysButtons.Add("LINE5_Upper_41", buttonLINE5_Upper_41);
            DictionaryBaysButtons.Add("LINE5_Upper_43", buttonLINE5_Upper_43);
            DictionaryBaysButtons.Add("LINE5_Upper_45", buttonLINE5_Upper_45);
            DictionaryBaysButtons.Add("LINE5_Upper_47", buttonLINE5_Upper_47);
            DictionaryBaysButtons.Add("LINE5_Upper_49", buttonLINE5_Upper_49);
            DictionaryBaysButtons.Add("LINE5_Upper_51", buttonLINE5_Upper_51);
            DictionaryBaysButtons.Add("LINE5_Upper_53", buttonLINE5_Upper_53);
            DictionaryBaysButtons.Add("LINE5_Upper_55", buttonLINE5_Upper_55);
            DictionaryBaysButtons.Add("LINE5_Upper_57", buttonLINE5_Upper_57);
            DictionaryBaysButtons.Add("LINE5_Upper_59", buttonLINE5_Upper_59);
            DictionaryBaysButtons.Add("LINE5_Upper_61", buttonLINE5_Upper_61);
            DictionaryBaysButtons.Add("LINE5_Upper_63", buttonLINE5_Upper_63);
            DictionaryBaysButtons.Add("LINE5_Upper_65", buttonLINE5_Upper_65);
            DictionaryBaysButtons.Add("LINE5_Upper_67", buttonLINE5_Upper_67);
            DictionaryBaysButtons.Add("LINE5_Upper_69", buttonLINE5_Upper_69);
            DictionaryBaysButtons.Add("LINE5_Upper_71", buttonLINE5_Upper_71);
            DictionaryBaysButtons.Add("LINE5_Upper_73", buttonLINE5_Upper_73);
            DictionaryBaysButtons.Add("LINE5_Upper_75", buttonLINE5_Upper_75);
            DictionaryBaysButtons.Add("LINE5_Upper_77", buttonLINE5_Upper_77);
            DictionaryBaysButtons.Add("LINE5_Upper_79", buttonLINE5_Upper_79);
            DictionaryBaysButtons.Add("LINE6_Lower_02", buttonLINE6_Lower_02);
            DictionaryBaysButtons.Add("LINE6_Lower_04", buttonLINE6_Lower_04);
            DictionaryBaysButtons.Add("LINE6_Lower_06", buttonLINE6_Lower_06);
            DictionaryBaysButtons.Add("LINE6_Lower_08", buttonLINE6_Lower_08);
            DictionaryBaysButtons.Add("LINE6_Lower_10", buttonLINE6_Lower_10);
            DictionaryBaysButtons.Add("LINE6_Lower_12", buttonLINE6_Lower_12);
            DictionaryBaysButtons.Add("LINE6_Lower_14", buttonLINE6_Lower_14);
            DictionaryBaysButtons.Add("LINE6_Lower_16", buttonLINE6_Lower_16);
            DictionaryBaysButtons.Add("LINE6_Lower_18", buttonLINE6_Lower_18);
            DictionaryBaysButtons.Add("LINE6_Lower_20", buttonLINE6_Lower_20);
            DictionaryBaysButtons.Add("LINE6_Lower_22", buttonLINE6_Lower_22);
            DictionaryBaysButtons.Add("LINE6_Lower_24", buttonLINE6_Lower_24);
            DictionaryBaysButtons.Add("LINE6_Lower_26", buttonLINE6_Lower_26);
            DictionaryBaysButtons.Add("LINE6_Lower_28", buttonLINE6_Lower_28);
            DictionaryBaysButtons.Add("LINE6_Lower_30", buttonLINE6_Lower_30);
            DictionaryBaysButtons.Add("LINE6_Lower_32", buttonLINE6_Lower_32);
            DictionaryBaysButtons.Add("LINE6_Lower_34", buttonLINE6_Lower_34);
            DictionaryBaysButtons.Add("LINE6_Lower_36", buttonLINE6_Lower_36);
            DictionaryBaysButtons.Add("LINE6_Lower_38", buttonLINE6_Lower_38);
            DictionaryBaysButtons.Add("LINE6_Lower_40", buttonLINE6_Lower_40);
            DictionaryBaysButtons.Add("LINE6_Upper_01", buttonLINE6_Upper_01);
            DictionaryBaysButtons.Add("LINE6_Upper_03", buttonLINE6_Upper_03);
            DictionaryBaysButtons.Add("LINE6_Upper_05", buttonLINE6_Upper_05);
            DictionaryBaysButtons.Add("LINE6_Upper_07", buttonLINE6_Upper_07);
            DictionaryBaysButtons.Add("LINE6_Upper_09", buttonLINE6_Upper_09);
            DictionaryBaysButtons.Add("LINE6_Upper_11", buttonLINE6_Upper_11);
            DictionaryBaysButtons.Add("LINE6_Upper_13", buttonLINE6_Upper_13);
            DictionaryBaysButtons.Add("LINE6_Upper_15", buttonLINE6_Upper_15);
            DictionaryBaysButtons.Add("LINE6_Upper_17", buttonLINE6_Upper_17);
            DictionaryBaysButtons.Add("LINE6_Upper_19", buttonLINE6_Upper_19);
            DictionaryBaysButtons.Add("LINE6_Upper_21", buttonLINE6_Upper_21);
            DictionaryBaysButtons.Add("LINE6_Upper_23", buttonLINE6_Upper_23);
            DictionaryBaysButtons.Add("LINE6_Upper_25", buttonLINE6_Upper_25);
            DictionaryBaysButtons.Add("LINE6_Upper_27", buttonLINE6_Upper_27);
            DictionaryBaysButtons.Add("LINE6_Upper_29", buttonLINE6_Upper_29);
            DictionaryBaysButtons.Add("LINE6_Upper_31", buttonLINE6_Upper_31);
            DictionaryBaysButtons.Add("LINE6_Upper_33", buttonLINE6_Upper_33);
            DictionaryBaysButtons.Add("LINE6_Upper_35", buttonLINE6_Upper_35);
            DictionaryBaysButtons.Add("LINE6_Upper_37", buttonLINE6_Upper_37);
            DictionaryBaysButtons.Add("LINE6_Upper_39", buttonLINE6_Upper_39);
            DictionaryBaysButtons.Add("LINE7_Lower_02", buttonLINE7_Lower_02);
            DictionaryBaysButtons.Add("LINE7_Lower_04", buttonLINE7_Lower_04);
            DictionaryBaysButtons.Add("LINE7_Lower_06", buttonLINE7_Lower_06);
            DictionaryBaysButtons.Add("LINE7_Lower_08", buttonLINE7_Lower_08);
            DictionaryBaysButtons.Add("LINE7_Lower_10", buttonLINE7_Lower_10);
            DictionaryBaysButtons.Add("LINE7_Lower_12", buttonLINE7_Lower_12);
            DictionaryBaysButtons.Add("LINE7_Lower_14", buttonLINE7_Lower_14);
            DictionaryBaysButtons.Add("LINE7_Lower_16", buttonLINE7_Lower_16);
            DictionaryBaysButtons.Add("LINE7_Lower_18", buttonLINE7_Lower_18);
            DictionaryBaysButtons.Add("LINE7_Lower_20", buttonLINE7_Lower_20);
            DictionaryBaysButtons.Add("LINE7_Lower_22", buttonLINE7_Lower_22);
            DictionaryBaysButtons.Add("LINE7_Lower_24", buttonLINE7_Lower_24);
            DictionaryBaysButtons.Add("LINE7_Lower_26", buttonLINE7_Lower_26);
            DictionaryBaysButtons.Add("LINE7_Lower_28", buttonLINE7_Lower_28);
            DictionaryBaysButtons.Add("LINE7_Lower_30", buttonLINE7_Lower_30);
            DictionaryBaysButtons.Add("LINE7_Lower_32", buttonLINE7_Lower_32);
            DictionaryBaysButtons.Add("LINE7_Lower_34", buttonLINE7_Lower_34);
            DictionaryBaysButtons.Add("LINE7_Lower_36", buttonLINE7_Lower_36);
            DictionaryBaysButtons.Add("LINE7_Lower_38", buttonLINE7_Lower_38);
            DictionaryBaysButtons.Add("LINE7_Lower_40", buttonLINE7_Lower_40);
            DictionaryBaysButtons.Add("LINE7_Lower_50", buttonLINE7_Lower_50);
            DictionaryBaysButtons.Add("LINE7_Lower_52", buttonLINE7_Lower_52);
            DictionaryBaysButtons.Add("LINE7_Lower_54", buttonLINE7_Lower_54);
            DictionaryBaysButtons.Add("LINE7_Lower_56", buttonLINE7_Lower_56);
            DictionaryBaysButtons.Add("LINE7_Lower_58", buttonLINE7_Lower_58);
            DictionaryBaysButtons.Add("LINE7_Lower_60", buttonLINE7_Lower_60);
            DictionaryBaysButtons.Add("LINE7_Lower_62", buttonLINE7_Lower_62);
            DictionaryBaysButtons.Add("LINE7_Lower_64", buttonLINE7_Lower_64);
            DictionaryBaysButtons.Add("LINE7_Lower_66", buttonLINE7_Lower_66);
            DictionaryBaysButtons.Add("LINE7_Lower_68", buttonLINE7_Lower_68);
            DictionaryBaysButtons.Add("LINE7_Lower_70", buttonLINE7_Lower_70);
            DictionaryBaysButtons.Add("LINE7_Lower_72", buttonLINE7_Lower_72);
            DictionaryBaysButtons.Add("LINE7_Upper_01", buttonLINE7_Upper_01);
            DictionaryBaysButtons.Add("LINE7_Upper_03", buttonLINE7_Upper_03);
            DictionaryBaysButtons.Add("LINE7_Upper_05", buttonLINE7_Upper_05);
            DictionaryBaysButtons.Add("LINE7_Upper_07", buttonLINE7_Upper_07);
            DictionaryBaysButtons.Add("LINE7_Upper_09", buttonLINE7_Upper_09);
            DictionaryBaysButtons.Add("LINE7_Upper_11", buttonLINE7_Upper_11);
            DictionaryBaysButtons.Add("LINE7_Upper_13", buttonLINE7_Upper_13);
            DictionaryBaysButtons.Add("LINE7_Upper_15", buttonLINE7_Upper_15);
            DictionaryBaysButtons.Add("LINE7_Upper_17", buttonLINE7_Upper_17);
            DictionaryBaysButtons.Add("LINE7_Upper_19", buttonLINE7_Upper_19);
            DictionaryBaysButtons.Add("LINE7_Upper_21", buttonLINE7_Upper_21);
            DictionaryBaysButtons.Add("LINE7_Upper_23", buttonLINE7_Upper_23);
            DictionaryBaysButtons.Add("LINE7_Upper_25", buttonLINE7_Upper_25);
            DictionaryBaysButtons.Add("LINE7_Upper_27", buttonLINE7_Upper_27);
            DictionaryBaysButtons.Add("LINE7_Upper_29", buttonLINE7_Upper_29);
            DictionaryBaysButtons.Add("LINE7_Upper_31", buttonLINE7_Upper_31);
            DictionaryBaysButtons.Add("LINE7_Upper_33", buttonLINE7_Upper_33);
            DictionaryBaysButtons.Add("LINE7_Upper_35", buttonLINE7_Upper_35);
            DictionaryBaysButtons.Add("LINE7_Upper_37", buttonLINE7_Upper_37);
            DictionaryBaysButtons.Add("LINE7_Upper_39", buttonLINE7_Upper_39);
            DictionaryBaysButtons.Add("LINE7_Upper_49", buttonLINE7_Upper_49);
            DictionaryBaysButtons.Add("LINE7_Upper_51", buttonLINE7_Upper_51);
            DictionaryBaysButtons.Add("LINE7_Upper_53", buttonLINE7_Upper_53);
            DictionaryBaysButtons.Add("LINE7_Upper_55", buttonLINE7_Upper_55);
            DictionaryBaysButtons.Add("LINE7_Upper_57", buttonLINE7_Upper_57);
            DictionaryBaysButtons.Add("LINE7_Upper_59", buttonLINE7_Upper_59);
            DictionaryBaysButtons.Add("LINE7_Upper_61", buttonLINE7_Upper_61);
            DictionaryBaysButtons.Add("LINE7_Upper_63", buttonLINE7_Upper_63);
            DictionaryBaysButtons.Add("LINE7_Upper_65", buttonLINE7_Upper_65);
            DictionaryBaysButtons.Add("LINE7_Upper_67", buttonLINE7_Upper_67);
            DictionaryBaysButtons.Add("LINE7_Upper_69", buttonLINE7_Upper_69);
            DictionaryBaysButtons.Add("LINE7_Upper_71", buttonLINE7_Upper_71);
            DictionaryBaysButtons.Add("LINE8_Lower_02", buttonLINE8_Lower_02);
            DictionaryBaysButtons.Add("LINE8_Lower_04", buttonLINE8_Lower_04);
            DictionaryBaysButtons.Add("LINE8_Lower_06", buttonLINE8_Lower_06);
            DictionaryBaysButtons.Add("LINE8_Lower_08", buttonLINE8_Lower_08);
            DictionaryBaysButtons.Add("LINE8_Lower_10", buttonLINE8_Lower_10);
            DictionaryBaysButtons.Add("LINE8_Lower_12", buttonLINE8_Lower_12);
            DictionaryBaysButtons.Add("LINE8_Lower_14", buttonLINE8_Lower_14);
            DictionaryBaysButtons.Add("LINE8_Lower_16", buttonLINE8_Lower_16);
            DictionaryBaysButtons.Add("LINE8_Lower_18", buttonLINE8_Lower_18);
            DictionaryBaysButtons.Add("LINE8_Lower_20", buttonLINE8_Lower_20);
            DictionaryBaysButtons.Add("LINE8_Lower_22", buttonLINE8_Lower_22);
            DictionaryBaysButtons.Add("LINE8_Lower_24", buttonLINE8_Lower_24);
            DictionaryBaysButtons.Add("LINE8_Lower_26", buttonLINE8_Lower_26);
            DictionaryBaysButtons.Add("LINE8_Lower_28", buttonLINE8_Lower_28);
            DictionaryBaysButtons.Add("LINE8_Lower_30", buttonLINE8_Lower_30);
            DictionaryBaysButtons.Add("LINE8_Lower_32", buttonLINE8_Lower_32);
            DictionaryBaysButtons.Add("LINE8_Lower_34", buttonLINE8_Lower_34);
            DictionaryBaysButtons.Add("LINE8_Lower_36", buttonLINE8_Lower_36);
            DictionaryBaysButtons.Add("LINE8_Lower_38", buttonLINE8_Lower_38);
            DictionaryBaysButtons.Add("LINE8_Lower_40", buttonLINE8_Lower_40);
            DictionaryBaysButtons.Add("LINE8_Lower_50", buttonLINE8_Lower_50);
            DictionaryBaysButtons.Add("LINE8_Lower_52", buttonLINE8_Lower_52);
            DictionaryBaysButtons.Add("LINE8_Lower_54", buttonLINE8_Lower_54);
            DictionaryBaysButtons.Add("LINE8_Lower_56", buttonLINE8_Lower_56);
            DictionaryBaysButtons.Add("LINE8_Lower_58", buttonLINE8_Lower_58);
            DictionaryBaysButtons.Add("LINE8_Lower_60", buttonLINE8_Lower_60);
            DictionaryBaysButtons.Add("LINE8_Lower_62", buttonLINE8_Lower_62);
            DictionaryBaysButtons.Add("LINE8_Lower_64", buttonLINE8_Lower_64);
            DictionaryBaysButtons.Add("LINE8_Lower_66", buttonLINE8_Lower_66);
            DictionaryBaysButtons.Add("LINE8_Lower_68", buttonLINE8_Lower_68);
            DictionaryBaysButtons.Add("LINE8_Lower_70", buttonLINE8_Lower_70);
            DictionaryBaysButtons.Add("LINE8_Lower_72", buttonLINE8_Lower_72);
            DictionaryBaysButtons.Add("LINE8_Upper_01", buttonLINE8_Upper_01);
            DictionaryBaysButtons.Add("LINE8_Upper_03", buttonLINE8_Upper_03);
            DictionaryBaysButtons.Add("LINE8_Upper_05", buttonLINE8_Upper_05);
            DictionaryBaysButtons.Add("LINE8_Upper_07", buttonLINE8_Upper_07);
            DictionaryBaysButtons.Add("LINE8_Upper_09", buttonLINE8_Upper_09);
            DictionaryBaysButtons.Add("LINE8_Upper_11", buttonLINE8_Upper_11);
            DictionaryBaysButtons.Add("LINE8_Upper_13", buttonLINE8_Upper_13);
            DictionaryBaysButtons.Add("LINE8_Upper_15", buttonLINE8_Upper_15);
            DictionaryBaysButtons.Add("LINE8_Upper_17", buttonLINE8_Upper_17);
            DictionaryBaysButtons.Add("LINE8_Upper_19", buttonLINE8_Upper_19);
            DictionaryBaysButtons.Add("LINE8_Upper_21", buttonLINE8_Upper_21);
            DictionaryBaysButtons.Add("LINE8_Upper_23", buttonLINE8_Upper_23);
            DictionaryBaysButtons.Add("LINE8_Upper_25", buttonLINE8_Upper_25);
            DictionaryBaysButtons.Add("LINE8_Upper_27", buttonLINE8_Upper_27);
            DictionaryBaysButtons.Add("LINE8_Upper_29", buttonLINE8_Upper_29);
            DictionaryBaysButtons.Add("LINE8_Upper_31", buttonLINE8_Upper_31);
            DictionaryBaysButtons.Add("LINE8_Upper_33", buttonLINE8_Upper_33);
            DictionaryBaysButtons.Add("LINE8_Upper_35", buttonLINE8_Upper_35);
            DictionaryBaysButtons.Add("LINE8_Upper_37", buttonLINE8_Upper_37);
            DictionaryBaysButtons.Add("LINE8_Upper_39", buttonLINE8_Upper_39);
            DictionaryBaysButtons.Add("LINE8_Upper_49", buttonLINE8_Upper_49);
            DictionaryBaysButtons.Add("LINE8_Upper_51", buttonLINE8_Upper_51);
            DictionaryBaysButtons.Add("LINE8_Upper_53", buttonLINE8_Upper_53);
            DictionaryBaysButtons.Add("LINE8_Upper_55", buttonLINE8_Upper_55);
            DictionaryBaysButtons.Add("LINE8_Upper_57", buttonLINE8_Upper_57);
            DictionaryBaysButtons.Add("LINE8_Upper_59", buttonLINE8_Upper_59);
            DictionaryBaysButtons.Add("LINE8_Upper_61", buttonLINE8_Upper_61);
            DictionaryBaysButtons.Add("LINE8_Upper_63", buttonLINE8_Upper_63);
            DictionaryBaysButtons.Add("LINE8_Upper_65", buttonLINE8_Upper_65);
            DictionaryBaysButtons.Add("LINE8_Upper_67", buttonLINE8_Upper_67);
            DictionaryBaysButtons.Add("LINE8_Upper_69", buttonLINE8_Upper_69);
            DictionaryBaysButtons.Add("LINE8_Upper_71", buttonLINE8_Upper_71);
            DictionaryBaysButtons.Add("LINEDMR_Lower_02", buttonLINEDMR_Lower_02);
            DictionaryBaysButtons.Add("LINEDMR_Lower_04", buttonLINEDMR_Lower_04);
            DictionaryBaysButtons.Add("LINEDMR_Lower_06", buttonLINEDMR_Lower_06);
            DictionaryBaysButtons.Add("LINEDMR_Lower_08", buttonLINEDMR_Lower_08);
            DictionaryBaysButtons.Add("LINEDMR_Lower_10", buttonLINEDMR_Lower_10);
            DictionaryBaysButtons.Add("LINEDMR_Lower_12", buttonLINEDMR_Lower_12);
            DictionaryBaysButtons.Add("LINEDMR_Lower_14", buttonLINEDMR_Lower_14);
            DictionaryBaysButtons.Add("LINEDMR_Lower_16", buttonLINEDMR_Lower_16);
            DictionaryBaysButtons.Add("LINEDMR_Upper_01", buttonLINEDMR_Upper_01);
            DictionaryBaysButtons.Add("LINEDMR_Upper_03", buttonLINEDMR_Upper_03);
            DictionaryBaysButtons.Add("LINEDMR_Upper_05", buttonLINEDMR_Upper_05);
            DictionaryBaysButtons.Add("LINEDMR_Upper_07", buttonLINEDMR_Upper_07);
            DictionaryBaysButtons.Add("LINEOBA_Lower_02", buttonLINEOBA_Lower_02);
            DictionaryBaysButtons.Add("LINEOBA_Lower_04", buttonLINEOBA_Lower_04);
            DictionaryBaysButtons.Add("LINEOBA_Lower_06", buttonLINEOBA_Lower_06);
            DictionaryBaysButtons.Add("LINEOBA_Lower_08", buttonLINEOBA_Lower_08);
            DictionaryBaysButtons.Add("LINEOBA_Lower_10", buttonLINEOBA_Lower_10);
            DictionaryBaysButtons.Add("LINEOBA_Lower_12", buttonLINEOBA_Lower_12);
            DictionaryBaysButtons.Add("LINEOBA_Lower_14", buttonLINEOBA_Lower_14);
            DictionaryBaysButtons.Add("LINEOBA_Lower_16", buttonLINEOBA_Lower_16);
            DictionaryBaysButtons.Add("LINEOBA_Upper_01", buttonLINEOBA_Upper_01);
            DictionaryBaysButtons.Add("LINEOBA_Upper_03", buttonLINEOBA_Upper_03);
            DictionaryBaysButtons.Add("LINEOBA_Upper_05", buttonLINEOBA_Upper_05);
            DictionaryBaysButtons.Add("LINEOBA_Upper_07", buttonLINEOBA_Upper_07);
            DictionaryBaysButtons.Add("LINEOBA_Upper_09", buttonLINEOBA_Upper_09);
            DictionaryBaysButtons.Add("LINEOBA_Upper_11", buttonLINEOBA_Upper_11);
            DictionaryBaysButtons.Add("LINEOBA_Upper_13", buttonLINEOBA_Upper_13);
            DictionaryBaysButtons.Add("LINEOBA_Upper_15", buttonLINEOBA_Upper_15);
            DictionaryBaysButtons.Add("LINEPRI_Lower_10", buttonLINEPRI_Lower_10);
            DictionaryBaysButtons.Add("LINEPRI_Lower_12", buttonLINEPRI_Lower_12);
            DictionaryBaysButtons.Add("LINEPRI_Lower_14", buttonLINEPRI_Lower_14);
            DictionaryBaysButtons.Add("LINEPRI_Lower_16", buttonLINEPRI_Lower_16);
            DictionaryBaysButtons.Add("LINEPRI_Lower_18", buttonLINEPRI_Lower_18);
            DictionaryBaysButtons.Add("LINEPRI_Lower_20", buttonLINEPRI_Lower_20);
            DictionaryBaysButtons.Add("LINEPRI_Lower_22", buttonLINEPRI_Lower_22);
            DictionaryBaysButtons.Add("LINEPRI_Lower_24", buttonLINEPRI_Lower_24);
            DictionaryBaysButtons.Add("LINEPRI_Upper_09", buttonLINEPRI_Upper_09);
            DictionaryBaysButtons.Add("LINEPRI_Upper_11", buttonLINEPRI_Upper_11);
            DictionaryBaysButtons.Add("LINEPRI_Upper_13", buttonLINEPRI_Upper_13);
            DictionaryBaysButtons.Add("LINEPRI_Upper_15", buttonLINEPRI_Upper_15);
            DictionaryBaysButtons.Add("LINEPRI_Upper_17", buttonLINEPRI_Upper_17);
            DictionaryBaysButtons.Add("LINEPRI_Upper_19", buttonLINEPRI_Upper_19);
            DictionaryBaysButtons.Add("LINEPRI_Upper_21", buttonLINEPRI_Upper_21);
            DictionaryBaysButtons.Add("LINEPRI_Upper_23", buttonLINEPRI_Upper_23);

            //DictionaryBaysButtons.Add("LINE1_A_05", buttonLINE1_A_05);
            //DictionaryBaysButtons.Add("LINE1_A_06", buttonLINE1_A_06);
            //DictionaryBaysButtons.Add("LINE1_A_07", buttonLINE1_A_07);
            //DictionaryBaysButtons.Add("LINE1_A_08", buttonLINE1_A_08);
            //DictionaryBaysButtons.Add("LINE1_A_09", buttonLINE1_A_09);
            //DictionaryBaysButtons.Add("LINE1_A_10", buttonLINE1_A_10);
            //DictionaryBaysButtons.Add("LINE1_A_11", buttonLINE1_A_11);
            //DictionaryBaysButtons.Add("LINE1_A_12", buttonLINE1_A_12);
            //DictionaryBaysButtons.Add("LINE1_A_13", buttonLINE1_A_13);
            //DictionaryBaysButtons.Add("LINE1_A_14", buttonLINE1_A_14);
            //DictionaryBaysButtons.Add("LINE1_A_15", buttonLINE1_A_15);
            //DictionaryBaysButtons.Add("LINE1_A_16", buttonLINE1_A_16);
            //DictionaryBaysButtons.Add("LINE1_A_17", buttonLINE1_A_17);
            //DictionaryBaysButtons.Add("LINE1_A_18", buttonLINE1_A_18);
            //DictionaryBaysButtons.Add("LINE1_A_19", buttonLINE1_A_19);
            //DictionaryBaysButtons.Add("LINE1_A_20", buttonLINE1_A_20);
            //DictionaryBaysButtons.Add("LINE1_A_21", buttonLINE1_A_21);
            //DictionaryBaysButtons.Add("LINE1_A_22", buttonLINE1_A_22);
            //DictionaryBaysButtons.Add("LINE1_A_23", buttonLINE1_A_23);
            //DictionaryBaysButtons.Add("LINE1_A_24", buttonLINE1_A_24);
            //DictionaryBaysButtons.Add("LINE1_A_25", buttonLINE1_A_25);
            //DictionaryBaysButtons.Add("LINE1_A_26", buttonLINE1_A_26);
            //DictionaryBaysButtons.Add("LINE1_A_27", buttonLINE1_A_27);
            //DictionaryBaysButtons.Add("LINE1_A_28", buttonLINE1_A_28);
            //DictionaryBaysButtons.Add("LINE1_A_29", buttonLINE1_A_29);
            //DictionaryBaysButtons.Add("LINE1_A_30", buttonLINE1_A_30);
            //DictionaryBaysButtons.Add("LINE1_A_31", buttonLINE1_A_31);
            //DictionaryBaysButtons.Add("LINE1_A_32", buttonLINE1_A_32);
            //DictionaryBaysButtons.Add("LINE1_A_33", buttonLINE1_A_33);
            //DictionaryBaysButtons.Add("LINE1_A_34", buttonLINE1_A_34);
            //DictionaryBaysButtons.Add("LINE1_A_35", buttonLINE1_A_35);
            //DictionaryBaysButtons.Add("LINE1_A_36", buttonLINE1_A_36);
            //DictionaryBaysButtons.Add("LINE1_B_05", buttonLINE1_B_05);
            //DictionaryBaysButtons.Add("LINE1_B_06", buttonLINE1_B_06);
            //DictionaryBaysButtons.Add("LINE1_B_07", buttonLINE1_B_07);
            //DictionaryBaysButtons.Add("LINE1_B_08", buttonLINE1_B_08);
            //DictionaryBaysButtons.Add("LINE1_B_09", buttonLINE1_B_09);
            //DictionaryBaysButtons.Add("LINE1_B_10", buttonLINE1_B_10);
            //DictionaryBaysButtons.Add("LINE1_B_11", buttonLINE1_B_11);
            //DictionaryBaysButtons.Add("LINE1_B_12", buttonLINE1_B_12);
            //DictionaryBaysButtons.Add("LINE1_B_13", buttonLINE1_B_13);
            //DictionaryBaysButtons.Add("LINE1_B_14", buttonLINE1_B_14);
            //DictionaryBaysButtons.Add("LINE1_B_15", buttonLINE1_B_15);
            //DictionaryBaysButtons.Add("LINE1_B_16", buttonLINE1_B_16);
            //DictionaryBaysButtons.Add("LINE1_B_17", buttonLINE1_B_17);
            //DictionaryBaysButtons.Add("LINE1_B_18", buttonLINE1_B_18);
            //DictionaryBaysButtons.Add("LINE1_B_19", buttonLINE1_B_19);
            //DictionaryBaysButtons.Add("LINE1_B_20", buttonLINE1_B_20);
            //DictionaryBaysButtons.Add("LINE1_B_21", buttonLINE1_B_21);
            //DictionaryBaysButtons.Add("LINE1_B_22", buttonLINE1_B_22);
            //DictionaryBaysButtons.Add("LINE1_B_23", buttonLINE1_B_23);
            //DictionaryBaysButtons.Add("LINE1_B_24", buttonLINE1_B_24);
            //DictionaryBaysButtons.Add("LINE1_B_25", buttonLINE1_B_25);
            //DictionaryBaysButtons.Add("LINE1_B_26", buttonLINE1_B_26);
            //DictionaryBaysButtons.Add("LINE1_B_27", buttonLINE1_B_27);
            //DictionaryBaysButtons.Add("LINE1_B_28", buttonLINE1_B_28);
            //DictionaryBaysButtons.Add("LINE1_B_29", buttonLINE1_B_29);
            //DictionaryBaysButtons.Add("LINE1_B_30", buttonLINE1_B_30);
            //DictionaryBaysButtons.Add("LINE1_B_31", buttonLINE1_B_31);
            //DictionaryBaysButtons.Add("LINE1_B_32", buttonLINE1_B_32);
            //DictionaryBaysButtons.Add("LINE1_B_33", buttonLINE1_B_33);
            //DictionaryBaysButtons.Add("LINE1_B_34", buttonLINE1_B_34);
            //DictionaryBaysButtons.Add("LINE1_B_35", buttonLINE1_B_35);
            //DictionaryBaysButtons.Add("LINE1_B_36", buttonLINE1_B_36);
            //DictionaryBaysButtons.Add("LINE2_A_05", buttonLINE2_A_05);
            //DictionaryBaysButtons.Add("LINE2_A_06", buttonLINE2_A_06);
            //DictionaryBaysButtons.Add("LINE2_A_07", buttonLINE2_A_07);
            //DictionaryBaysButtons.Add("LINE2_A_08", buttonLINE2_A_08);
            //DictionaryBaysButtons.Add("LINE2_A_09", buttonLINE2_A_09);
            //DictionaryBaysButtons.Add("LINE2_A_10", buttonLINE2_A_10);
            //DictionaryBaysButtons.Add("LINE2_A_11", buttonLINE2_A_11);
            //DictionaryBaysButtons.Add("LINE2_A_12", buttonLINE2_A_12);
            //DictionaryBaysButtons.Add("LINE2_A_13", buttonLINE2_A_13);
            //DictionaryBaysButtons.Add("LINE2_A_14", buttonLINE2_A_14);
            //DictionaryBaysButtons.Add("LINE2_A_15", buttonLINE2_A_15);
            //DictionaryBaysButtons.Add("LINE2_A_16", buttonLINE2_A_16);
            //DictionaryBaysButtons.Add("LINE2_A_17", buttonLINE2_A_17);
            //DictionaryBaysButtons.Add("LINE2_A_18", buttonLINE2_A_18);
            //DictionaryBaysButtons.Add("LINE2_A_19", buttonLINE2_A_19);
            //DictionaryBaysButtons.Add("LINE2_A_20", buttonLINE2_A_20);
            //DictionaryBaysButtons.Add("LINE2_A_21", buttonLINE2_A_21);
            //DictionaryBaysButtons.Add("LINE2_A_22", buttonLINE2_A_22);
            //DictionaryBaysButtons.Add("LINE2_A_23", buttonLINE2_A_23);
            //DictionaryBaysButtons.Add("LINE2_A_24", buttonLINE2_A_24);
            //DictionaryBaysButtons.Add("LINE2_A_25", buttonLINE2_A_25);
            //DictionaryBaysButtons.Add("LINE2_A_26", buttonLINE2_A_26);
            //DictionaryBaysButtons.Add("LINE2_A_27", buttonLINE2_A_27);
            //DictionaryBaysButtons.Add("LINE2_A_28", buttonLINE2_A_28);
            //DictionaryBaysButtons.Add("LINE2_A_29", buttonLINE2_A_29);
            //DictionaryBaysButtons.Add("LINE2_A_30", buttonLINE2_A_30);
            //DictionaryBaysButtons.Add("LINE2_A_31", buttonLINE2_A_31);
            //DictionaryBaysButtons.Add("LINE2_A_32", buttonLINE2_A_32);
            //DictionaryBaysButtons.Add("LINE2_A_33", buttonLINE2_A_33);
            //DictionaryBaysButtons.Add("LINE2_A_34", buttonLINE2_A_34);
            //DictionaryBaysButtons.Add("LINE2_A_35", buttonLINE2_A_35);
            //DictionaryBaysButtons.Add("LINE2_A_36", buttonLINE2_A_36);
            //DictionaryBaysButtons.Add("LINE2_B_05", buttonLINE2_B_05);
            //DictionaryBaysButtons.Add("LINE2_B_06", buttonLINE2_B_06);
            //DictionaryBaysButtons.Add("LINE2_B_07", buttonLINE2_B_07);
            //DictionaryBaysButtons.Add("LINE2_B_08", buttonLINE2_B_08);
            //DictionaryBaysButtons.Add("LINE2_B_09", buttonLINE2_B_09);
            //DictionaryBaysButtons.Add("LINE2_B_10", buttonLINE2_B_10);
            //DictionaryBaysButtons.Add("LINE2_B_11", buttonLINE2_B_11);
            //DictionaryBaysButtons.Add("LINE2_B_12", buttonLINE2_B_12);
            //DictionaryBaysButtons.Add("LINE2_B_13", buttonLINE2_B_13);
            //DictionaryBaysButtons.Add("LINE2_B_14", buttonLINE2_B_14);
            //DictionaryBaysButtons.Add("LINE2_B_15", buttonLINE2_B_15);
            //DictionaryBaysButtons.Add("LINE2_B_16", buttonLINE2_B_16);
            //DictionaryBaysButtons.Add("LINE2_B_17", buttonLINE2_B_17);
            //DictionaryBaysButtons.Add("LINE2_B_18", buttonLINE2_B_18);
            //DictionaryBaysButtons.Add("LINE2_B_19", buttonLINE2_B_19);
            //DictionaryBaysButtons.Add("LINE2_B_20", buttonLINE2_B_20);
            //DictionaryBaysButtons.Add("LINE2_B_21", buttonLINE2_B_21);
            //DictionaryBaysButtons.Add("LINE2_B_22", buttonLINE2_B_22);
            //DictionaryBaysButtons.Add("LINE2_B_23", buttonLINE2_B_23);
            //DictionaryBaysButtons.Add("LINE2_B_24", buttonLINE2_B_24);
            //DictionaryBaysButtons.Add("LINEPRI_Lower_02", buttonLINEPRI_Lower_02);
            //DictionaryBaysButtons.Add("LINEPRI_Lower_04", buttonLINEPRI_Lower_04);
            //DictionaryBaysButtons.Add("LINEPRI_Lower_06", buttonLINEPRI_Lower_06);
            //DictionaryBaysButtons.Add("LINEPRI_Lower_08", buttonLINEPRI_Lower_08);
            //DictionaryBaysButtons.Add("LINEPRI_Upper_01", buttonLINEPRI_Upper_01);
            //DictionaryBaysButtons.Add("LINEPRI_Upper_03", buttonLINEPRI_Upper_03);
            //DictionaryBaysButtons.Add("LINEPRI_Upper_05", buttonLINEPRI_Upper_05);
            //DictionaryBaysButtons.Add("LINEPRI_Upper_07", buttonLINEPRI_Upper_07);
        }

        internal Form_Main Form_Main { get; set; }

        private void Form_BayView_FHD_Load(object sender, EventArgs e)
        {
            GetReady();
        }

        private void Defaults()
        {
            comboBoxRate.SelectedIndex = 0;
        }

        private void GetReady()
        {
            //UpdateDt();
            StatusUpdate("Getting ready.. please wait.");
            //CompleteUpdate();
            Wait(1000);
            //ManualRefresh();
            button2.Enabled = true;
            buttonAuto.Enabled = true;
            StatusUpdate("Load the bays by clicking on \"Refresh\" button. Please note: Before closing app - if active, stop auto-refresh with \"Kill Refresh\" button, othervise app will keep running in background.");
        }

        public async void ManualRefresh()
        {
            //RefreshBays();
            string check = await CompleteUpdate();
            if (check == "OK")
            {
                StatusUpdate("Updated at " + DateTime.Now.ToString("HH:mm:ss"));
            }
            else
            {

            }
        }

        public async void AutoRefresh(int timer)
        {
            refreshCount = 0;
            counter = 0;
            while (true)
            {
                refreshCount = refreshCount + 1;
                counter = counter + 1;
                //RefreshBays();
                string check = await CompleteUpdate();
                if (check == "OK") //to reduce first DB update fail -> overflow exception (0/0)
                {
                    if (connectionIssueWindow.Visible == true)
                    {
                        _alpha.InsertAction(_alpha.userName, _alpha.accessLevel, "BayOverView reconnected*", "OK");
                        connectionIssueWindow.Hide();
                        this.Focus();
                    }
                    StatusUpdate("Bays updated " + refreshCount.ToString() + " times, last refresh: " + DateTime.Now.ToString("HH:mm:ss") + ", refreshing in " + (timer / 1000).ToString() + " seconds.");
                }
                else if (check == "ConnectionIssue")
                {
                    connectionIssueWindow.Show();
                    connectionIssueWindow.Focus();
                    StatusUpdate("Connection fail in attempt " + refreshCount.ToString() + ", last attempt: " + DateTime.Now.ToString("HH:mm:ss") + "- failed - refreshing in " + (timer / 1000).ToString() + " seconds.");
                }
                else
                {
                    StatusUpdate("Bays updated " + refreshCount.ToString() + " times, last refresh: " + DateTime.Now.ToString("HH:mm:ss") + "- failed - refreshing in " + (timer / 1000).ToString() + " seconds.");
                }
                Wait(timer);
                continue;
            }
        }

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    dtBV = _alpha.GetBVDataTable_Important();
        //    dtBVDead = _alpha.GetBVDataTable_DeadBays();
        //    dtBVFails = _alpha.GetBVDataTable_Fails();
        //    totalBays = _alpha.GetTotalBaysCount();
        //}

        private bool CountCheck()
        {
            int dtBVcountCheck = dtBV.Rows.Count;
            if (dtBVcountCheck < 1 || dtBVcountCheck > 1000)
            {
                return false;
            }
            if (totalBays < 100 || totalBays > 1000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //public void UpdateDt()
        //{
        //    if (!backgroundWorker1.IsBusy)
        //    {
        //        backgroundWorker1.RunWorkerAsync();
        //    }
        //}

        private async Task<string> CompleteUpdate()
        {
            stopWatch.Start();
            StatusUpdate("Updating database..");
            //UpdateDt();
            string checkConnection = _alpha.GetMicCheck();
            if (checkConnection == "MicCheck")
            {
                var task_dtBV = _alpha.GetBVDataTable_Important();
                var task_dtBVDead = _alpha.GetBVDataTable_DeadBays();
                var task_dtBVFails = _alpha.GetBVDataTable_Fails();
                var task_totalBays = _alpha.GetTotalBaysCount();
                var task_dtPendingTDS = _alpha.GetBVDataTable_Pending("120");

                dtBVDead = await task_dtBVDead;
                dtBVFails = await task_dtBVFails;
                totalBays = await task_totalBays;
                dtPendingTDS = await task_dtPendingTDS;
                dtBV = await task_dtBV;

                bool isCountOK = CountCheck(); //to reduce first DB update fail -> overflow exception (0/0)
                if (isCountOK == true)
                {
                    StatusUpdate("Refreshing bays..");
                    RefreshBays();

                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00} seconds.", ts.Seconds, ts.Milliseconds / 10);
                    labelRefreshTimeVar.Text = elapsedTime;
                    stopWatch.Reset();

                    return "OK";
                }
                else
                {
                    //StatusUpdate("Database not loaded properly, try to refresh again..");

                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00} seconds.", ts.Seconds, ts.Milliseconds / 10);
                    labelRefreshTimeVar.Text = elapsedTime;
                    stopWatch.Reset();

                    return "Fail";
                }
            }
            else
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00} seconds.", ts.Seconds, ts.Milliseconds / 10);
                labelRefreshTimeVar.Text = elapsedTime;
                stopWatch.Reset();

                return "ConnectionIssue";
            }
        }

        public void RefreshBays()
        {
            bays_runIn = 0;
            bays_problem = 0;
            bays_fail = 0;
            bays_pass = 0;
            bays_adler = 0;
            bays_onlineLoad = 0;
            bays_booting = 0;
            bays_ini = 0;

            StatusUpdate("Loading positions...");

            DebugProgress(10);

            Clear();

            DebugProgress(20);

            labelBaysTotalVar.Text = totalBays.ToString();
            int failBays = dtBVFails.Rows.Count;
            int deadBays = dtBVDead.Rows.Count;

            labelBaysClosedVar.Text = deadBays.ToString();
            int workingBays = totalBays - deadBays;
            labelBaysWorkingVar.Text = workingBays.ToString();
            int usedBays = dtBV.Rows.Count;

            DebugProgress(30);
            //reset Switchloggeru pokud zaznamenava stale stejna data ---------------
            ////if (UsedBaysRepeatCounterList.Count != 0)
            ////{
            ////    if (usedBays == (UsedBaysRepeatCounterList[UsedBaysRepeatCounterList.Count - 1]))
            ////    {
            ////        UsedBaysRepeatCounterList.Add(usedBays);
            ////    }
            ////    else
            ////    {
            ////        UsedBaysRepeatCounterList.Clear();
            ////        UsedBaysRepeatCounterList.Add(0);
            ////        UsedBaysRepeatCounterList.Add(usedBays);
            ////    }

            ////    if (UsedBaysRepeatCounterList.Count == 12)
            ////    {
            ////        SwitchloggerReset switchlogger = new SwitchloggerReset(_alpha);
            ////        switchlogger.Reset();
            ////        SwitchResetCount = SwitchResetCount + 1;
            ////        labelLastRestart.Text = DateTime.Now.ToString("ddd, HH:mm");
            ////        UsedBaysRepeatCounterList.Clear();
            ////        UsedBaysRepeatCounterList.Add(0);
            ////    }
            ////    else
            ////    {
            ////    }
            ////}
            ////labelSwitchlogger.Text = SwitchResetCount.ToString() + "x";
            ////labelUsageRepeated.Text = (UsedBaysRepeatCounterList.Count - 2).ToString() + "/10";
            //-----------------------------------------------------------------------

            if (_alpha.autoRefreshActive == true && _alpha.autoRefreshAdmin == true)
            {
                if (counter >= maxCount )
                {
                    SwitchloggerReset switchlogger = new SwitchloggerReset(_alpha);
                    switchlogger.Reset();
                    SwitchResetCount = SwitchResetCount + 1;
                    labelLastRestart.Text = DateTime.Now.ToString("ddd, HH:mm");
                    counter = 0;
                }

                labelSwitchlogger.Text = SwitchResetCount.ToString() + "x";
                labelUsageRepeated.Text = counter.ToString() + " / " + maxCount;
            }
            // ----------------------------------------------------------------------
            // Keeping only values for last 24h in graph
            DebugProgress(40);

            if (bayProportion.BayProportionTable.Rows.Count > maxCount_GraphLimit)
            {
                bayProportion.BayProportionTable.Rows[0].Delete();
                bayProportion.BayProportionTable.AcceptChanges();
            }

            if (bayLoad.BayLoadTable.Rows.Count > maxCount_GraphLimit)
            {
                bayLoad.BayLoadTable.Rows[0].Delete();
                bayLoad.BayLoadTable.AcceptChanges();
            }

            // ---------------------------------------------------------------------- replaced with 24h values
            //Cleareaning graph every 3th at 6pm:

            //string thisHour = DateTime.Now.ToString("HH");
            //if (thisHour == "18")
            //{
            //    graphClear6pm = true;
            //    if (graphClear6pm == true && graphClearToday == false)
            //    {
            //        graphDayCount = graphDayCount + 1;
            //        graphClearToday = true;
            //        if (graphDayCount == 2)
            //        {
            //            graphDayCount = 0;
            //            bayLoad.BayLoadTable.Clear();
            //            bayProportion.BayProportionTable.Clear();
            //            _alpha.InsertAction(_alpha.userName, _alpha.accessLevel, "BayOverView Graph cleared at 18pm", "OK");
            //        }
            //    }
            //}

            //if (thisHour == "19")
            //{
            //    graphClearToday = false;
            //    graphClear6pm = false;
            //}
            //labelGraphClearCount.Text = graphDayCount.ToString() + " / 2";
            // ----------------------------------------------------------------------
            DebugProgress(50);
            labelBaysUsedVar.Text = usedBays.ToString();
            float usageFloat = (float)usedBays / (float)workingBays * 100;
            int usage = (int)usageFloat;
            labelBaysUsageVar.Text = usage.ToString() + " %";

            float failsPercentageFloat = (float)failBays / (float)workingBays * 100;
            int failsPercentage = (int)failsPercentageFloat;

            DebugProgress(60);
            bayLoad.AddRow(DateTime.Now.ToString("ddd, HH:mm"), usage, failsPercentage);
            if(checkBoxRecord.Checked == true)
            {
                _alpha.InsertBayUsage(DateTime.Now.ToString("ddd, HH:mm"), usage.ToString(), failsPercentage.ToString());
            }
            //dataGridViewBays.DataSource = bayLoad.BayLoadTable;
            //dataGridViewBays.Update();

            //UpdateGraph();
            DebugProgress(70);
            dataGridViewBV.DataSource = dtBV;
            dataGridViewBVFails.DataSource = dtBVFails;
            DebugProgress(75);
            try
            {
                if (dataGridViewBVFails.Rows.Count != 0)
                {
                    dataGridViewBVFails.Columns[0].Width = (int)(dataGridViewBVFails.Width * 0.05);
                    dataGridViewBVFails.Columns[1].Width = (int)(dataGridViewBVFails.Width * 0.05);
                    dataGridViewBVFails.Columns[2].Width = (int)(dataGridViewBVFails.Width * 0.05);
                    dataGridViewBVFails.Columns[3].Width = (int)(dataGridViewBVFails.Width * 0.1);
                    dataGridViewBVFails.Columns[4].Width = (int)(dataGridViewBVFails.Width * 0.1);
                    dataGridViewBVFails.Columns[5].Width = (int)(dataGridViewBVFails.Width * 0.05);
                    dataGridViewBVFails.Columns[6].Width = (int)(dataGridViewBVFails.Width * 0.1);
                    dataGridViewBVFails.Columns[7].Width = (int)(dataGridViewBVFails.Width * 0.3);
                    dataGridViewBVFails.Columns[8].Width = (int)(dataGridViewBVFails.Width * 0.05);
                    dataGridViewBVFails.Columns[9].Width = (int)(dataGridViewBVFails.Width * 0.05);
                    dataGridViewBVFails.Columns[10].Width = (int)(dataGridViewBVFails.Width * 0.05);
                    dataGridViewBVFails.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch
            {

            }

            DebugProgress(70);
            foreach (DataRow row in dtBVDead.Rows)
            {
                string BAY_Dead = row["Bay"].ToString();
                ColorPosition(BAY_Dead, "DEAD", "");
            }
            DebugProgress(75);

            Dictionary<string, string> DictionaryBaysOccupation = BayOverview_LoadBaysList.LoadDict_BayPositions();

            foreach (DataRow row in dtBV.Rows)
            {
                DateTime currentTime = DateTime.Now;

                string ConnectionTimeString = row["Connection_Time"].ToString();
                DateTime ConnectionTime = DateTime.Parse(ConnectionTimeString);

                string IpTimeString = row["IP_Add_Assigned"].ToString();
                DateTime IpTime;
                if (IpTimeString != "")
                {
                    IpTime = DateTime.Parse(IpTimeString);
                }
                else
                {
                    IpTime = new DateTime(2001, 01, 01);
                }

                string TestTimeString = row["LatestStatus_Cdt"].ToString();
                DateTime TestTime;
                if (TestTimeString != "")
                {
                    TestTime = DateTime.Parse(TestTimeString);
                }
                else
                {
                    TestTime = new DateTime(2001, 01, 01);
                }

                string PhaseTimeString = row["LatestSubPart_Cdt"].ToString();
                DateTime PhaseTime;
                if (PhaseTimeString != "")
                {
                    PhaseTime = DateTime.Parse(PhaseTimeString);
                }
                else
                {
                    PhaseTime = new DateTime(2001, 01, 01);
                }

                string BAY_SNO_NextWc = row["NextWc"].ToString();
                int BAY_SNO_NextWc_int;
                try
                {
                    BAY_SNO_NextWc_int = Int32.Parse(BAY_SNO_NextWc);
                }
                catch
                {
                    BAY_SNO_NextWc_int = 999;
                }
                DebugProgress(80);
                string SubPart = row["SubPart"].ToString();
                string PartStatus = row["PartStatus"].ToString();
                string BAY_SNO = row["SNO"].ToString();
                string BAY_Status = row["Status_Final"].ToString();
                string BAY_Phase = row["Test_Phase"].ToString();
                string BAY_Position = row["Position"].ToString();
                DebugProgress(85);
                if (DictionaryBaysOccupation.ContainsKey(BAY_Position))
                {
                    DictionaryBaysOccupation[BAY_Position] = "1";
                }
                //COLOURS:
                if (BAY_Status == "BOOTING")
                {
                    TimeSpan ConnTimeDelta = currentTime - ConnectionTime;
                    if (ConnTimeDelta > maxBootingTime)
                    {
                        BAY_SNO = "BOOT PROBLEM, Booting for: " + ConnTimeDelta.ToString(@"dd\.hh\:mm\:ss");
                        ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                    }
                    else
                    {
                        ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                    }
                }
                else if (BAY_Status == "INI")
                {
                    TimeSpan IpTimeDelta = currentTime - IpTime;
                    if (IpTimeDelta > maxIniTime)
                    {
                        BAY_SNO = "Initializing PROBLEM, Initializing for: " + IpTimeDelta.ToString(@"dd\.hh\:mm\:ss");
                        ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                    }

                    else
                    {
                        ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                    }
                }
                else if (BAY_Status == "55S")
                {
                    TimeSpan TestTimeDelta = currentTime - TestTime;
                    TimeSpan PhaseTimeDelta = currentTime - PhaseTime;
                    if (TestTimeDelta > maxTestTime)
                    {
                        ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                    }
                    else
                    {
                        if (BAY_Phase == "0")
                        {
                            if (PhaseTimeDelta > maxPhase0Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                                MarkPosition(BAY_Position, "P0", BAY_SNO);
                            }
                            else
                            {
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                                MarkPosition(BAY_Position, "P0", BAY_SNO);
                            }
                        }
                        if (BAY_Phase == "1")
                        {
                            if (PhaseTimeDelta > maxPhase1Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                                MarkPosition(BAY_Position, "P1", BAY_SNO);
                            }
                            else
                            {
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                                MarkPosition(BAY_Position, "P1", BAY_SNO);
                            }
                        }
                        if (BAY_Phase == "2")
                        {
                            if (PhaseTimeDelta > maxPhase2Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                                MarkPosition(BAY_Position, "P2", BAY_SNO);
                            }
                            else
                            {
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                                MarkPosition(BAY_Position, "P2", BAY_SNO);
                            }
                        }
                        if (BAY_Phase == "3")
                        {
                            if (PhaseTimeDelta > maxPhase3Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                                MarkPosition(BAY_Position, "P3", BAY_SNO);
                            }
                            else
                            {
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                                MarkPosition(BAY_Position, "P3", BAY_SNO);
                            }
                        }
                        if (BAY_Phase == "4")
                        {
                            if (PhaseTimeDelta > maxPhase4Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                                MarkPosition(BAY_Position, "P4", BAY_SNO);
                            }
                            else
                            {
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                                MarkPosition(BAY_Position, "P4", BAY_SNO);
                            }
                        }
                        if (BAY_Phase == "5")
                        {
                            if (PhaseTimeDelta > maxPhase5Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                                MarkPosition(BAY_Position, "P5", BAY_SNO);
                            }
                            else
                            {
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                                MarkPosition(BAY_Position, "P5", BAY_SNO);
                            }
                        }
                        if (BAY_Phase == "6")
                        {
                            if (PhaseTimeDelta > maxPhase6Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                                MarkPosition(BAY_Position, "P6", BAY_SNO);
                            }
                            else
                            {
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                                MarkPosition(BAY_Position, "P6", BAY_SNO);
                            }
                        }
                        if (BAY_Phase == "7")
                        {
                            if (PartStatus == "ADL_MFI")
                            {
                                if (PhaseTimeDelta > maxPhaseMfiTime)
                                {
                                    ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                                    MarkPosition(BAY_Position, "MFI", BAY_SNO);
                                }
                                else
                                {
                                    ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                                    MarkPosition(BAY_Position, "MFI", BAY_SNO);
                                }
                            }
                            else
                            {
                                if (PhaseTimeDelta > maxPhase7Time)
                                {
                                    ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                                    MarkPosition(BAY_Position, "P7", BAY_SNO);
                                }
                                else
                                {
                                    ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                                    MarkPosition(BAY_Position, "P7", BAY_SNO);
                                }
                            }
                        }
                    }
                }

                else if (BAY_Status == "55P")
                {
                    TimeSpan IpTimeDelta = currentTime - IpTime;
                    if (IpTimeDelta > maxPassTime)
                    {
                        ColorPosition(BAY_Position, "55Px", BAY_SNO);
                    }
                    else
                    {
                        ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        MarkPosition(BAY_Position, "P7", BAY_SNO);
                    }
                }

                else if (BAY_Status == "60P")
                {
                    TimeSpan IpTimeDelta = currentTime - IpTime;
                    if (IpTimeDelta > maxPassTime)
                    {
                        ColorPosition(BAY_Position, "60Px", BAY_SNO);
                    }
                    else
                    {
                        ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        MarkPosition(BAY_Position, "P7", BAY_SNO);
                    }
                }

                else if (BAY_Status == "ADL")
                {
                    TimeSpan IpTimeDelta = currentTime - IpTime;
                    if (IpTimeDelta > maxAdlTime)
                    {
                        ColorPosition(BAY_Position, "ADLx", BAY_SNO);
                    }
                    else
                    {
                        ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        MarkPosition(BAY_Position, "P7", BAY_SNO);
                    }
                }

                else
                {
                    ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                }
                DebugProgress(90);
                //MARKS:
                if (BAY_SNO_NextWc_int > 50)
                {
                    if (SubPart == "ODD_Raus" && PartStatus == "1")
                    {
                        MarkPosition(BAY_Position, "Mark_CDOUT", BAY_SNO);
                    }
                    else if (SubPart == "ODD_Retry" && PartStatus == "1")
                    {
                        MarkPosition(BAY_Position, "Mark_CDIN", BAY_SNO);
                    }
                    else if (SubPart == "Speaker_test" && PartStatus == "1")
                    {
                        MarkPosition(BAY_Position, "Mark_AudioTest", BAY_SNO);
                    }
                    else if (SubPart == "BIOS" && PartStatus == "1")
                    {
                        MarkPosition(BAY_Position, "Mark_BiosFlash", BAY_SNO);
                    }
                    else if (SubPart == "SmartCard" && PartStatus == "1")
                    {
                        MarkPosition(BAY_Position, "Mark_SmartCard", BAY_SNO);
                    }
                    else { }
                }
                else
                {
                    MarkPosition(BAY_Position, "Mark_NoHipot_NoMVS", BAY_SNO);
                }
            }
            DebugProgress(92);
            dataGridViewPendingTDS.DataSource = dtPendingTDS;
            dataGridViewPendingTDS.Update();

            try
            {
                if (dataGridViewBVFails.Rows.Count != 0)
                {
                    dataGridViewPendingTDS.Columns[0].Width = (int)(dataGridViewPendingTDS.Width * 0.25);
                    dataGridViewPendingTDS.Columns[1].Width = (int)(dataGridViewPendingTDS.Width * 0.12);
                    dataGridViewPendingTDS.Columns[2].Width = (int)(dataGridViewPendingTDS.Width * 0.12);
                    dataGridViewPendingTDS.Columns[3].Width = (int)(dataGridViewPendingTDS.Width * 0.3);
                    dataGridViewPendingTDS.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch
            {

            }
            dataGridViewPendingTDS.Update();
            DebugProgress(93);
            if (checkBoxRecord.Checked == true)
            {
                foreach (string bay in DictionaryBaysOccupation.Keys)
                {
                    string test = bay + " / " + DictionaryBaysOccupation[bay];
                }
                _alpha.InsertBayPositionUsage(DateTime.Now.ToString("ddd, HH:mm"), DictionaryBaysOccupation);
            }

            //DataTable DictionaryBaysOccupation_Datatable = BayOverview_LoadBaysList.LoadDataTable_BayPositions();
            //DataRow newRow = DictionaryBaysOccupation_Datatable.NewRow();
            //foreach (string key in DictionaryBaysOccupation.Keys)
            //{
            //    newRow[key] = DictionaryBaysOccupation[key];
            //}
            //DictionaryBaysOccupation_Datatable.Rows.Add(newRow);

            DebugProgress(95);
            bayProportion.AddRow(DateTime.Now.ToString("ddd, HH:mm"), bays_booting, bays_ini, bays_runIn, bays_problem, bays_fail, bays_pass, bays_adler, bays_onlineLoad);
            //dataGridViewBays.DataSource = bayProportion.BayProportionTable;
            //dataGridViewBays.Update();
            DebugProgress(98);
            UpdateGraph();
            DebugProgress(100);
        }

        public void Clear()
        {

            foreach (Button button in ListBaysButtons)
            {
                button.BackColor = Color.FromArgb(255, 255, 255);
                button.Image = null;
                button.BackgroundImage = null;
                button.Text = "";
            }
        }

        public void ColorPosition(string position, string status, string SN)
        {
            if (DictionaryBaysButtons.ContainsKey(position))
            {
                Button tlacitko = DictionaryBaysButtons[position];
                if (status == "55F")
                {
                    tlacitko.BackColor = Color.FromArgb(255, 0, 0);
                    tlacitko.Text = SN;
                    tlacitko.Update();
                    bays_fail = bays_fail + 1;
                }

                if (status == "PROBLEM")
                {
                    tlacitko.BackColor = Color.FromArgb(255, 191, 0);
                    tlacitko.Text = SN;
                    tlacitko.Update();
                    bays_problem = bays_problem + 1;
                }
                if (status == "55S")
                {
                    tlacitko.BackColor = Color.FromArgb(255, 255, 0);
                    tlacitko.Text = SN;
                    tlacitko.Update();
                    bays_runIn = bays_runIn + 1;
                }
                if (status == "INI")
                {
                    tlacitko.BackColor = Color.FromArgb(238, 204, 255);
                    tlacitko.Text = SN;
                    tlacitko.Update();
                    bays_ini = bays_ini + 1;
                }
                if (status == "55P")
                {
                    tlacitko.BackColor = Color.FromArgb(204, 255, 102);
                    tlacitko.Text = SN;
                    tlacitko.Update();
                    bays_pass = bays_pass + 1;
                }
                if (status == "55Px")
                {
                    tlacitko.Image = TestTrack.Properties.Resources.BV_gif_PassToAdler3;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                    bays_pass = bays_pass + 1;
                }
                if (status == "60P")
                {
                    tlacitko.BackColor = Color.FromArgb(204, 255, 102);
                    tlacitko.Text = SN;
                    tlacitko.Update();
                    bays_pass = bays_pass + 1;
                }
                if (status == "60Px")
                {
                    tlacitko.Image = TestTrack.Properties.Resources.BV_gif_MfiPassToAdler3;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                    bays_pass = bays_pass + 1;
                }
                if (status == "ADL")
                {
                    tlacitko.BackColor = Color.FromArgb(30, 220, 0);
                    tlacitko.Text = SN;
                    tlacitko.Update();
                    bays_adler = bays_adler + 1;
                }
                if (status == "ADLx")
                {
                    tlacitko.Image = TestTrack.Properties.Resources.BV_gif_AdlerToEmpty4;
                    tlacitko.Text = SN;
                    tlacitko.Update();


                    tlacitko.BackColor = Color.FromArgb(166, 166, 166);
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }
            }
        }

        public void MarkPosition(string position, string status, string SN)
        {
            if (DictionaryBaysButtons.ContainsKey(position))
            {
                Button tlacitko = DictionaryBaysButtons[position];

                if (status == "Mark_NoHipot_NoMVS")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.NoHipot2_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "Mark_CDIN")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_InsertCD_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "Mark_CDOUT")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_RemoveCD_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "Mark_AudioTest")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_AudioTest_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "Mark_BiosFlash")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_BiosFlash_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "Mark_SmartCard")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_SmartCard_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "P0")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_P0_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "P1")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_P1_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "P2")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_P2_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "P3")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_P3_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "P4")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_P4_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "P5")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_P5_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "P6")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_P6_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }

                if (status == "P7")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_P7_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }
                if (status == "MFI")
                {
                    tlacitko.BackgroundImage = TestTrack.Properties.Resources.BV_Ico_MFI_FHD;
                    tlacitko.Text = SN;
                    tlacitko.Update();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            buttonAuto.Enabled = false;
            _alpha.InsertAction(_alpha.userName, _alpha.accessLevel, "Bayview Tracker Refresh", "OK");
            ManualRefresh();
            button2.Enabled = true;
            buttonAuto.Enabled = true;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (_alpha.autoRefreshActive == true)
            {
                StatusUpdate("Auto-refresh active! Exit BayOverview with \"Kill Refresh\" button highlighted orange.");
                buttonSTOP.BackColor = Color.FromArgb(255, 191, 0);
            }
            else
            this.Hide();
        }

        public void Wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        private void Bay_Click(object sender, EventArgs e)
        {
            string button_SN = (sender as Button).Text;
            if (button_SN.Contains("PROBLEM"))
            {
                MessageBox.Show(button_SN, "Problem", MessageBoxButtons.OK);
            }
            else if (button_SN.Length == 10)
            {
                this.Form_Main.inputBox2.Text = button_SN;
                this.Form_Main.buttonSearch.PerformClick();
                this.Form_Main.Focus();
            }
            else
            {
                StatusUpdate("Selected bay is empty");
            }
        }
        private void StatusUpdate(string labelText)
        {
            labelStatusVar.Text = labelText;
            labelStatusVar.Update();
        }

        private void buttonAuto_Click(object sender, EventArgs e)
        {
            //if (textBoxPW.Text == "startloop")
            //{
            if (textBoxPW.Text == "1254")
            {
                _alpha.autoRefreshAdmin = true;
                groupBoxAdmin.Visible = true;
            }

            _alpha.autoRefreshActive = true;
            labelRefActive.Visible = true;
            pictureBoxRefActive.Visible = true;
            button2.Enabled = false;
            buttonAuto.Enabled = false;

            int timer = 240000;
            maxCount = 15;
            if (comboBoxRate.SelectedIndex == 0)
            {
                timer = 30000;
                maxCount = 120;
                maxCount_GraphLimit = 2880;
            }
            if (comboBoxRate.SelectedIndex == 1)
            {
                timer = 60000;
                maxCount = 60;
                maxCount_GraphLimit = 1440;
            }
            if (comboBoxRate.SelectedIndex == 2)
            {
                timer = 120000;
                maxCount = 30;
                maxCount_GraphLimit = 720;
            }

            _alpha.InsertAction(_alpha.userName, _alpha.accessLevel, "Started BV auto-refresh", "OK");
            StatusUpdate("Before closing app - please stop auto-refresh with \"Kill Refresh\", othervise app will keep running in background");
            AutoRefresh(timer);

            //}
            //else
            //{
            //    StatusUpdate("Wrong password. Thread loops are evil.");
            //    _alpha.InsertAction(_alpha.userName, _alpha.accessLevel, "Tried BV auto-refresh", "Fail");
            //}
        }

        private void Test()
        {
            Control control = Controls.Find("buttonTest", true).FirstOrDefault();
            if (control != null)
            {
                toolTip1.SetToolTip(control, "TestTest");
            }
        }

        //private void PrepareGraph()
        //{
        //    cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
        //    {
        //        Title = "Time",
        //        LabelFormatter = value => value.ToString()
        //    }) ;

        //    cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
        //    {
        //        Title = "Usage",
        //        LabelFormatter = value => value.ToString()
        //    });
        //    cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
        //}

        private void UpdateGraph()
        {
            //cartesianChart1.Series.Clear();
            //SeriesCollection series = new SeriesCollection();
            //List<string> Listtt = bayLoad.BayLoadTable.AsEnumerable().Select(x => x[2].ToString()).ToList();
            //cartesianChart1.Series.Add(new LineSeries
            //{
            //    ChartValues 
            //}
            //    );


            chartBayUsage.DataSource = bayLoad.BayLoadTable;
            chartBayUsage.Series["BayLoad"].XValueMember = "Time";
            chartBayUsage.Series["BayLoad"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayUsage.Series["BayLoad"].YValueMembers = "Load [%]";
            chartBayUsage.Series["BayLoad"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

            chartBayUsage.Series["Fails"].XValueMember = "Time";
            chartBayUsage.Series["Fails"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayUsage.Series["Fails"].YValueMembers = "Fails [%]";
            chartBayUsage.Series["Fails"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

            chartBayProportion.DataSource = bayProportion.BayProportionTable;
            chartBayProportion.Series["Booting"].XValueMember = "Time";
            chartBayProportion.Series["Booting"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Booting"].YValueMembers = "Booting";
            chartBayProportion.Series["Booting"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Ini"].XValueMember = "Time";
            chartBayProportion.Series["Ini"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Ini"].YValueMembers = "Ini";
            chartBayProportion.Series["Ini"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["RunIn"].XValueMember = "Time";
            chartBayProportion.Series["RunIn"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["RunIn"].YValueMembers = "RunIn";
            chartBayProportion.Series["RunIn"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Problem"].XValueMember = "Time";
            chartBayProportion.Series["Problem"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Problem"].YValueMembers = "Problem";
            chartBayProportion.Series["Problem"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Fail"].XValueMember = "Time";
            chartBayProportion.Series["Fail"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Fail"].YValueMembers = "Fail";
            chartBayProportion.Series["Fail"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Pass"].XValueMember = "Time";
            chartBayProportion.Series["Pass"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Pass"].YValueMembers = "Pass";
            chartBayProportion.Series["Pass"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Adler"].XValueMember = "Time";
            chartBayProportion.Series["Adler"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Adler"].YValueMembers = "Adler";
            chartBayProportion.Series["Adler"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Online"].XValueMember = "Time";
            chartBayProportion.Series["Online"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartBayProportion.Series["Online"].YValueMembers = "Online";
            chartBayProportion.Series["Online"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
        }

        private void DebugProgress(int progress)
        {
            labelDebugVar.Text = progress.ToString() + " %";
            labelDebugVar.Update();
        }
        private void SetupGraph()
        {
            chartBayUsage.ChartAreas[0].AxisY.Maximum = 100;
            chartBayUsage.ChartAreas[0].AxisY.Minimum = 0;
            chartBayUsage.ChartAreas[0].AxisY.Title = "Usage [%]";

            chartBayProportion.ChartAreas[0].AxisY.Title = "Units [Count]";
        }

        private void buttonSTOP_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            //Environment.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void buttonMaximize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Button buttonXZ = DictionaryBaysButtons["LINEPRI_Upper_21"];
            buttonXZ.Image = TestTrack.Properties.Resources.NoHipot2_FHD;
        }

        private void buttonClearGraph_Click(object sender, EventArgs e)
        {
            bayLoad.BayLoadTable.Clear();
            bayProportion.BayProportionTable.Clear();
        }

        private void buttonBV2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.49/Tests/BayView/BayView_New.aspx?OPID=111111&TestLine=LINE2&VerifyCode=0&Group=");
        }

        private void buttonBVPri_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.49/Tests/BayView/BayView_New.aspx?OPID=111111&TestLine=LINEPRI&VerifyCode=0&Group=");
        }

        private void buttonBV3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.49/Tests/BayView/BayView_New.aspx?OPID=111111&TestLine=LINE3&VerifyCode=0&Group=");
        }

        private void buttonBV4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.49/Tests/BayView/BayView_New.aspx?OPID=111111&TestLine=LINE4&VerifyCode=0&Group=");
        }

        private void buttonBV5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.49/Tests/BayView/BayView_New.aspx?OPID=111111&TestLine=LINE5&VerifyCode=0&Group=");
        }

        private void buttonBV6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.49/Tests/BayView/BayView_New.aspx?OPID=111111&TestLine=LINE6&VerifyCode=0&Group=");
        }

        private void buttonBV7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.49/Tests/BayView/BayView_New.aspx?OPID=111111&TestLine=LINE7&VerifyCode=0&Group=");
        }

        private void buttonBV8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.49/Tests/BayView/BayView_New.aspx?OPID=111111&TestLine=LINE8&VerifyCode=0&Group=");
        }

        private void buttonSnoIgnore_Click(object sender, EventArgs e)
        {
            string sno = textBoxSnoIgnore.Text;
            _alpha.InsertSnoIgnore(sno);
            _alpha.InsertAction(_alpha.userName, _alpha.accessLevel, "Sno: " + sno + " ignored from unit pending", "OK");
            textBoxSnoIgnore.Text = "";
            StatusUpdate(sno + " will now be ignored for pending report function.");
        }
    }
}
