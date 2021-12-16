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

namespace MC1
{
    public partial class Form_BayView : Form
    {
        private string _userName;
        private string _accessLVL;
        public SQL_Tracker SQL;
        private ListNDictiomary _alpha;
        public bool keepLoopAlive;

        public Form_BayView(string user, string lvl, ListNDictiomary compositedAlpha)
        {

            _userName = user;
            _accessLVL = lvl;
            keepLoopAlive = true;
            InitializeComponent();
            this._alpha = compositedAlpha;
            this.SQL = new SQL_Tracker();
            Defaults();
            ManualRefresh();

        }
        internal Form_Main Form_Main { get; set; }

        private void Defaults()
        {
            comboBoxRate.SelectedIndex = 0;
        }

        public void ManualRefresh()
        {
            RefreshBays();
            StatusUpdate("Updated " + DateTime.Now.ToString("HH:mm:ss"));
        }

        public void AutoRefresh(int timer)
        {
            int count = 0;
            while (true)
            {
                count = count + 1;
                RefreshBays();
                StatusUpdate("Bays loaded " + count.ToString() + " times, last refresh: " + DateTime.Now.ToString("HH:mm:ss") + ", refreshing in " + (timer / 1000).ToString() + " seconds.");
                Wait(timer);
                continue;
            }
        }
        public void RefreshBays()
        {
            StatusUpdate("Loading positions...");
            Clear();
            TimeSpan maxBootingTime = new TimeSpan(0, 20, 0);
            TimeSpan maxIniTime = new TimeSpan(0, 20, 0);
            TimeSpan maxPhase0Time = new TimeSpan(0, 10, 0);
            TimeSpan maxPhase1Time = new TimeSpan(0, 10, 0);
            TimeSpan maxPhase2Time = new TimeSpan(0, 30, 0);
            TimeSpan maxPhase3Time = new TimeSpan(0, 10, 0);
            TimeSpan maxPhase4Time = new TimeSpan(0, 60, 0);
            TimeSpan maxPhase5Time = new TimeSpan(0, 10, 0);
            TimeSpan maxPhase6Time = new TimeSpan(0, 10, 0);
            TimeSpan maxPhase7Time = new TimeSpan(0, 10, 0);
            TimeSpan maxTestTime = new TimeSpan(1, 30, 0);

            DataTable dtBV = new DataTable();
            DataTable dtBVDead = new DataTable();
            DataTable dtBVFails = new DataTable();
            //dtBV = SQL.GetBVDataTable_Important();
            //dtBVDead = SQL.GetBVDataTable_DeadBays();
            //dtBVFails = SQL.GetBVDataTable_Fails();

            dataGridViewBV.DataSource = dtBV;
            dataGridViewBVFails.DataSource = dtBVFails;
            //this.buttonTest.Image = TestTrack.Properties.Resources.info1;

            foreach (DataRow row in dtBVDead.Rows)
            {
                string BAY_Dead = row["Bay"].ToString();
                ColorPosition(BAY_Dead, "DEAD", "");
            }

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
                string SubPart = row["SubPart"].ToString();
                string PartStatus = row["PartStatus"].ToString();
                string BAY_SNO = row["SNO"].ToString();
                string BAY_Status = row["Status_Final"].ToString();
                string BAY_Phase = row["Test_Phase"].ToString();
                string BAY_Position = row["Position"].ToString();

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
                            }
                            else
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        }
                        if (BAY_Phase == "1")
                        {
                            if (PhaseTimeDelta > maxPhase1Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                            }
                            else
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        }
                        if (BAY_Phase == "2")
                        {
                            if (PhaseTimeDelta > maxPhase2Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                            }
                            else
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        }
                        if (BAY_Phase == "3")
                        {
                            if (PhaseTimeDelta > maxPhase3Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                            }
                            else
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        }
                        if (BAY_Phase == "4")
                        {
                            if (PhaseTimeDelta > maxPhase4Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                            }
                            else
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        }
                        if (BAY_Phase == "5")
                        {
                            if (PhaseTimeDelta > maxPhase5Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                            }
                            else
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        }
                        if (BAY_Phase == "6")
                        {
                            if (PhaseTimeDelta > maxPhase6Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                            }
                            else
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        }
                        if (BAY_Phase == "7")
                        {
                            if (PhaseTimeDelta > maxPhase7Time)
                            {
                                ColorPosition(BAY_Position, "PROBLEM", BAY_SNO);
                            }
                            else
                                ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                        }
                    }
                }
                else
                {
                    ColorPosition(BAY_Position, BAY_Status, BAY_SNO);
                }

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
                    else { }
                }
                else
                {
                    MarkPosition(BAY_Position, "Mark_NoHipot_NoMVS", BAY_SNO);
                }
            }
        }

        public void Clear()
        {
            List<string> Bays_Buttons = new List<string>();
            Bays_Buttons = _alpha.ListBaysButtons;
            foreach (string bay_button in Bays_Buttons)
            {
                var control = Controls.Find(bay_button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackColor = Color.FromArgb(255, 255, 255);
                    control.BackgroundImage = null;
                    control.Text = "";
                    //control.Update();
                }
            }
        }

        public void ColorPosition(string position, string status, string SN)
        {
            string button = "button" + position;
            if (status == "55F")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackColor = Color.FromArgb(255, 0, 0);
                    control.Text = SN;
                    control.Update();
                }
            }
            if (status == "PROBLEM")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackColor = Color.FromArgb(255, 191, 0);
                    control.Text = SN;
                    control.Update();
                }
            }
            if (status == "55S")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackColor = Color.FromArgb(255, 255, 0);
                    control.Text = SN;
                    control.Update();
                }
            }
            if (status == "INI")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackColor = Color.FromArgb(238, 204, 255);
                    control.Text = SN;
                    control.Update();
                }
            }
            if (status == "55P")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackColor = Color.FromArgb(204, 255, 102);
                    control.Text = SN;
                    control.Update();
                }
            }
            if (status == "ADL")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackColor = Color.FromArgb(0, 204, 0);
                    control.Text = SN;
                    control.Update();
                }
            }
            if (status == "55O")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackColor = Color.FromArgb(0, 191, 255);
                    control.Text = SN;
                    control.Update();
                }
            }
            if (status == "BOOTING")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackColor = Color.FromArgb(204, 238, 255);
                    control.Text = SN;
                    control.Update();
                }
            }
            if (status == "DEAD")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackColor = Color.FromArgb(89, 89, 89);
                    control.Text = SN;
                    control.Update();
                }
            }
        }
        public void MarkPosition(string position, string status, string SN)
        {
            string button = "button" + position;

            if (status == "Mark_NoHipot_NoMVS")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackgroundImage = TestTrack.Properties.Resources.NoHipot_PCK;
                    control.Text = SN;
                    control.Update();
                }
            }

            if (status == "Mark_CDIN")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackgroundImage = TestTrack.Properties.Resources.InsertCD2_PCK;
                    control.Text = SN;
                    control.Update();
                }
            }

            if (status == "Mark_CDOUT")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackgroundImage = TestTrack.Properties.Resources.RemoveCD2_PCK;
                    control.Text = SN;
                    control.Update();
                }
            }

            if (status == "Mark_AudioTest")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackgroundImage = TestTrack.Properties.Resources.AudioTest2_PCK;
                    control.Text = SN;
                    control.Update();
                }
            }

            if (status == "Mark_BiosFlash")
            {
                var control = Controls.Find(button, true).FirstOrDefault();
                if (control != null)
                {
                    control.BackgroundImage = TestTrack.Properties.Resources.BiosFlash_PCK;
                    control.Text = SN;
                    control.Update();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SQL.InsertAction(_userName, _accessLVL, "Bayview Tracker Refresh", "OK");
            ManualRefresh();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
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
            if (textBoxPW.Text == "startloop")
            {
                int timer = 240000;
                if (comboBoxRate.SelectedIndex == 0)
                {
                    timer = 30000;
                }
                if (comboBoxRate.SelectedIndex == 1)
                {
                    timer = 60000;
                }
                if (comboBoxRate.SelectedIndex == 2)
                {
                    timer = 120000;
                }
                SQL.InsertAction(_userName, _accessLVL, "Tried BV auto-refresh", "OK");
                AutoRefresh(timer);
            }
            else
            {
                StatusUpdate("Wrong password. Thread loops are evil.");
                SQL.InsertAction(_userName, _accessLVL, "Tried BV auto-refresh", "Fail");
            }
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
    }
}
