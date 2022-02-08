using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;

namespace MC1
{
    //[System.Runtime.InteropServices.Guid("95B51032-B0BF-422F-856E-6ADCA13097AA")]
    public partial class Form_Login : Form
    {
        private string _userName;
        private string _accessLVL;
        private bool needUpdate;
        private string reqVersion;
        private string update_acc;
        private string update_acc_PW;
        public List<string> TEs;
        public SQL_Tracker SQL;
        private string _programVersion;

        public Form_Login()
        {
            needUpdate = true;
            this.SQL = new SQL_Tracker();
            this._userName = GetUserName();
            this._programVersion = "2.5.5";
            this.TEs = new List<string>() { "ICZ198022", "ICZ198023", "ICZ208110", "ICZ208054", "ICZ200477", "ICZ160120", "ICZ208427", "ICZ218016" };
            //EnterDungeonTest();
            InitializeComponent();
            labelVerVar.Text = _programVersion;
            labelVerVar.Update();
        }

        private void MapLab(string LVL)
        {
            StatusUpdate("login", "Connecting to server..");
            string userName = null;
            if (LVL == "Guest")
                userName = "SwapTech";
            else if (LVL == "Technician")
                userName = "TestTech";
            else if (LVL == "Engineer")
                userName = "Administrator";
            else
                userName = "Error";

            if (userName != "Error")

            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                string pwd = SQL.GetLineaheadPassword("10.13.82.200", userName);
                update_acc = userName;
                update_acc_PW = pwd;
                string strCmdText;
                strCmdText = @"/C C:/Windows/System32/net.exe use \\10.13.82.200\comm_uut /persistent:no /user:" + userName + " \"" + pwd + "\"";
                startInfo.Arguments = strCmdText;
                process.StartInfo = startInfo;
                process.Start();
            }
            else
            {
                StatusUpdate("login","Mapping issue. Restart the program.");
            }
            Wait(1000);
        }

        public string GetUserName()
        {
            string userName;
            try
            {
                userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            }
            catch
            {
                userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            finally
            {
                userName = Environment.UserName;
            }
            userName = userName.ToUpper();
            return userName;
        }

        private bool CheckConnection()
        {
            StatusUpdate("login", "Checking connection..");
            bool connection = false;
            for (int i = 0; i < 3; i++)
            {
                if (Directory.Exists(@"\\10.13.82.200\comm_uut\TestTrack") == true)
                {
                    connection = true;
                    break;
                }
                else
                {
                    Wait(1000);
                    continue;
                }
            }
            return connection;
        }

        private void CheckVersion()
        {
            StatusUpdate("login", "Checking version..");
            //string programVersion = "2.3.1";
            string currentVersion = _programVersion;
            //string reqVersion = File.ReadAllText(@"\\10.13.82.200\comm_uut\TestTrack\Version.dat");
            reqVersion = SQL.GetReqVersion();
            if (currentVersion == reqVersion)
            {
                labelVerStat.ForeColor = System.Drawing.Color.Green;
                StatusUpdate("version", "Updated");
                needUpdate = false;
            }
            else
            {
                labelVerStat.ForeColor = System.Drawing.Color.Red;
                StatusUpdate("version", "Outdated");
                buttonUpdate.Visible = true;
                //buttonUpdate.PerformClick();
            }
        }

        private void buttonGuest_Click(object sender, EventArgs e)
        {
            _accessLVL = "Guest";
            Proceed();
        }

        private void EnterDungeonTest()
        {
            Hide();
            Form_Main formMain = new Form_Main(new Alpha_Engineer(_userName, "Engineer", _programVersion));
            formMain.Closed += (s, args) => Close();
            formMain.Show();
        }

        private void EnterDungeon()
        {
            SQL.InsertLogin(_userName, _accessLVL, "OK");
            Hide();
            if (_accessLVL == "Engineer")
            {
                Form_Main formMain = new Form_Main(new Alpha_Engineer(_userName, _accessLVL, _programVersion));
                formMain.Closed += (s, args) => Close();
                formMain.Show();
            }
            else if (_accessLVL == "Technician")
            {
                Form_Main formMain = new Form_Main(new Alpha_Tech(_userName, _accessLVL, _programVersion));
                formMain.Closed += (s, args) => Close();
                formMain.Show();
            }
            else if (_accessLVL == "Guest")
            {
                Form_Main formMain = new Form_Main(new Alpha_Guest(_userName, _accessLVL, _programVersion));
                formMain.Closed += (s, args) => Close();
                formMain.Show();
            }
            else
            {
                throw new Exception("You should not get here bruh, id 1");
            }

        }

        private void Proceed()
        {
            pictureBoxLoadingGif.Visible = true;
            pictureBoxLoadingGif.Update();
            string authentification = Authentification();
            if (authentification == "OK")
            {
                labelAuthStatFix.Visible = true;
                labelAuthStatFix.Update();
                labelAuthStat.Text = "OK";
                labelAuthStat.ForeColor = System.Drawing.Color.Green;
                labelAuthStat.Update();
                Wait(500);
                string SQLConn = SQL.GetMicCheck();
                if (SQLConn == "MicCheck")
                {
                    labelSqlConnectionStatFix.Visible = true;
                    labelSqlConnectionStatFix.Update();
                    labelSqlConnectionStat.Text = "OK";
                    labelSqlConnectionStat.ForeColor = System.Drawing.Color.Green;
                    labelSqlConnectionStat.Update();
                    Wait(500);
                    MapLab(_accessLVL);
                    bool connection = CheckConnection();
                    if (connection == true)
                    {
                        labelConnectionStatFix.Visible = true;
                        labelConnectionStatFix.Update();
                        labelConnectionStat.Text = "OK";
                        labelConnectionStat.ForeColor = System.Drawing.Color.Green;
                        labelConnectionStat.Update();
                        Wait(500);
                        CheckVersion();
                        if (needUpdate == false)
                        {
                            labelVerStatFix.Visible = true;
                            labelVerStat.Update();
                            labelVerStat.Text = "OK";
                            labelVerStat.ForeColor = System.Drawing.Color.Green;
                            labelVerStat.Update();
                            Wait(1000);
                            EnterDungeon();
                        }
                        else
                        {
                            labelVerStatFix.Visible = true;
                            labelVerStatFix.Update();
                            labelVerStat.Text = "Outdated";
                            labelVerStat.ForeColor = System.Drawing.Color.Red;
                            labelVerStat.Update();
                            StatusUpdate("login", "Outdated, please download new version with update button.");
                            buttonUpdate.Visible = true;
                            //buttonUpdate.PerformClick();
                            bool autoUpdate = msgBxUpdate();
                            if (autoUpdate == true)
                            {
                                AutoUpdate();
                            }
                            else
                            {
                                ManualUpdate();
                            }
                            ClearProgress();
                        }
                    }
                    else
                    {
                        labelConnectionStatFix.Visible = true;
                        labelConnectionStatFix.Update();
                        labelConnectionStat.Text = "Fail";
                        labelConnectionStat.ForeColor = System.Drawing.Color.Red;
                        labelConnectionStat.Update();
                        StatusUpdate("login", "Can't connect to test server.");
                        msgBx();
                        ClearProgress();
                    }
                }
                else
                {
                    labelSqlConnectionStatFix.Visible = true;
                    labelSqlConnectionStatFix.Update();
                    labelSqlConnectionStat.Text = "Fail";
                    labelSqlConnectionStat.ForeColor = System.Drawing.Color.Red;
                    labelSqlConnectionStat.Update();
                    Wait(500);
                    StatusUpdate("login", "Can't connect to database server.");
                    msgBx();
                    ClearProgress();
                }
            }
            else
            {
                labelAuthStatFix.Visible = true;
                labelAuthStatFix.Update();
                labelAuthStat.Text = "Fail";
                labelAuthStat.ForeColor = System.Drawing.Color.Red;
                labelAuthStat.Update();
                ClearProgress();
            }
        }

        private string Authentification()
        {
            if (_accessLVL == "Technician")
            {
                string input = textBoxPW.Text;
                if (String.IsNullOrEmpty(input))
                {
                    StatusUpdate("login", "Enter password.");
                    return "NOK";
                }
                else if (input == "hadouken" || input == "HADOUKEN")
                {
                    return "OK";
                }
                else
                {
                    _accessLVL = null;
                    StatusUpdate("login", "Incorrect password!");
                    return "NOK";
                }
            }
            else if (_accessLVL == "Engineer")
            {
                if (TEs.Contains(_userName))
                {
                    return "OK";
                }
                else
                {
                    _accessLVL = null;
                    StatusUpdate("login", "You are not TE.");
                    return "NOK";
                }
            }
            else if (_accessLVL == "Guest")
            {
                return "OK";
            }
            else
            {
                return "NOK";
            }
            
        }
        private void buttonTT_Click(object sender, EventArgs e)
        {
            if (textBoxPW.Text == "xxc")
            {
                _accessLVL = "Engineer";
            }
            else
            {
                _accessLVL = "Technician";
            }
            Proceed();
        }

        private void buttonTE_Click(object sender, EventArgs e)
        {
            _accessLVL = "Engineer";
            Proceed();
        }

        private void ClearProgress()
        {
            SQL.InsertLogin(_userName, _accessLVL, "Fail");
            Wait(1000);
            pictureBoxLoadingGif.Visible = false;
            pictureBoxLoadingGif.Update();

            labelAuthStatFix.Visible = false;
            labelAuthStatFix.Update();
            labelAuthStat.Text = "";
            labelAuthStat.ForeColor = System.Drawing.Color.Black;
            labelAuthStat.Update();

            labelSqlConnectionStatFix.Visible = false;
            labelSqlConnectionStatFix.Update();
            labelSqlConnectionStat.Text = "";
            labelSqlConnectionStat.ForeColor = System.Drawing.Color.Black;
            labelSqlConnectionStat.Update();

            labelConnectionStatFix.Visible = false;
            labelConnectionStatFix.Update();
            labelConnectionStat.Text = "";
            labelConnectionStat.ForeColor = System.Drawing.Color.Black;
            labelConnectionStat.Update();

            labelVerStatFix.Visible = false;
            labelVerStatFix.Update();
            labelVerStat.Text = "";
            labelVerStat.ForeColor = System.Drawing.Color.Black;
            labelVerStat.Update();

        }

        private void labelLoginVar_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            SQL.InsertAction(_userName, _accessLVL, "Moved to update folder", "OK");
            string filePath = @"\\10.13.82.200\comm_uut\tools\TestTrack";
            Process.Start(filePath);
        }

        private void AutoUpdate()
        {
            if (File.Exists(@"\\10.13.82.200\comm_uut\tools\TestTrack\TestTrack.exe"))
            {
                SQL.InsertAction(_userName, _accessLVL, "Automatic update (Login)", "OK");
                string appDomain = AppDomain.CurrentDomain.BaseDirectory;
                System.Diagnostics.Process.Start(@"C:\TestTrack\Updater.exe", appDomain + " " + _programVersion + " " + reqVersion + " " + update_acc + " " + update_acc_PW);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private void ManualUpdate()
        {
            SQL.InsertAction(_userName, _accessLVL, "Manual update (Login)", "OK");
            string filePath = @"\\10.13.82.200\comm_uut\tools\TestTrack";
            Process.Start(filePath);
        }

        private void msgBx()
        {
            {
                // Initializes the variables to pass to the MessageBox.Show method.
                string message = "Connection issue.\n\nMake sure you are connected to Inventec Slatina network and try again.";
                string caption = "Warning";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, icon);
            }
        }

        private bool msgBxUpdate()
        {
            {
                // Initializes the variables to pass to the MessageBox.Show method.
                string message = "Application update required. \n\nDo you want to update the TestTrack to ver. " + _programVersion + " automatically?";
                string caption = "Update";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Question;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, icon);
                if (result == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void StatusUpdate(string status, string labelText)
        {
            if (status == "login")
            {
                labelStatusLogin.Text = labelText;
                labelStatusLogin.Update();
            }
            else if (status == "version")
            {
                labelVerStat.Text = labelText;
                labelVerStat.Update();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQL.InsertLogin("adminTE", "Engineer", "OK");
            Hide();
            Form_Main formMain = new Form_Main(new Alpha_Engineer("adminTE", "Engineer", _programVersion));
            formMain.Closed += (s, args) => Close();
            formMain.Show();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            Form test = new Form_ConnectionIssue();
            test.Show();
        }
    }
}
