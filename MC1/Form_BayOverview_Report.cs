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
    public partial class Form_BayOverview_Report : Form
    {
        public int counter;
        //public SQL_Tracker SQL;
        //private bool graphClear6pm = false;
        //private bool graphClearToday = true;
        //private int graphDayCount = 0;
        private ListNDictiomary _alpha;
        public bool keepLoopAlive;
        private BayLoadGraph bayLoad;
        private BayProportionGraph bayProportion;
        //private List<int> UsedBaysRepeatCounterList { get; set; } = new List<int>();

       
        private List<Button> ListBaysButtons = new List<Button>();
        private Dictionary<string, Button> DictionaryBaysButtons = new Dictionary<string, Button>();
        //private Dictionary<string, string> DictionaryBaysOccupation = BayOverview_LoadBaysList.LoadList_BayPositions();

        public Form_BayOverview_Report(ListNDictiomary compositedAlpha)
        {
            keepLoopAlive = true;
            bayLoad = new BayLoadGraph();
            bayProportion = new BayProportionGraph();
            InitializeComponent();
            _alpha = compositedAlpha;
            //this.SQL = new SQL_Tracker();
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

        private void Form_BayOverview_Report_Load(object sender, EventArgs e)
        {
            GetReady();
        }

        private void GetReady()
        {
            //UpdateDt();
            StatusUpdate("Loading..");
            //CompleteUpdate();
            Wait(1000);
            //ManualRefresh();
            LoadData();
        }

        private async void LoadData()
        {

            DataTable heatMapDt = _alpha.GetBayUsageHeatMap();

            var task_dtBVDead = _alpha.GetBVDataTable_DeadBays();
            var task_totalBays = _alpha.GetTotalBaysCount();

            DataTable dtBVDead = await task_dtBVDead;
            int deadBays = dtBVDead.Rows.Count;
            int totalBays = await task_totalBays;

            int workingBays = totalBays - deadBays;
            labelBaysTotalVar.Text = totalBays.ToString();
            labelBaysClosedVar.Text = deadBays.ToString();
            labelBaysWorkingVar.Text = workingBays.ToString();

            dataGridView1.DataSource = heatMapDt;

            foreach (DataRow row in heatMapDt.Rows)
            {
                string bay = row["Bay"].ToString();
                string usage = row["Usage"].ToString();
                ColorPosition(bay, usage);
            }

            foreach (DataRow row in dtBVDead.Rows)
            {
                string BAY_Dead = row["Bay"].ToString();
                ColorDeadPosition(BAY_Dead, "DEAD");
            }
            StatusUpdate("Click on bay position to load average bay usage value.");
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

        public void ColorPosition(string position, string usage)
        {
            int usageInt = Int32.Parse(usage);
            if (DictionaryBaysButtons.ContainsKey(position))
            {
                Button tlacitko = DictionaryBaysButtons[position];
                if (usageInt == 0)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 255, 255);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 0 && usageInt <= 5)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 230, 230);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 5 && usageInt <= 10)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 204, 204);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 10 && usageInt <= 15)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 179, 179);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 15 && usageInt <= 20)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 153, 153);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 20 && usageInt <= 25)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 128, 128);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 25 && usageInt <= 30)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 102, 102);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 30 && usageInt <= 35)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 77, 77);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 35 && usageInt <= 40)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 51, 51);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 40 && usageInt <= 45)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 26, 26);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 45 && usageInt <= 50)
                {
                    tlacitko.BackColor = Color.FromArgb(255, 0, 0);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
                if (usageInt > 50)
                {
                    tlacitko.BackColor = Color.FromArgb(204, 0, 0);
                    tlacitko.Text = usage;
                    tlacitko.Update();
                }
            }
        }

        public void ColorDeadPosition(string position, string usage)
        {
            if (DictionaryBaysButtons.ContainsKey(position))
            {
                Button tlacitko = DictionaryBaysButtons[position];
                if (usage == "DEAD")
                {
                    tlacitko.BackColor = Color.FromArgb(166, 166, 166);
                    tlacitko.Text = "bay closed - 0";
                    tlacitko.Update();
                }
            }
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
            string button_text = (sender as Button).Text;
            string status = "Selected Bay usage: " + button_text + "%";
            StatusUpdate(status);
        }
        private void StatusUpdate(string labelText)
        {
            labelStatusVar.Text = labelText;
            labelStatusVar.Update();
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

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
