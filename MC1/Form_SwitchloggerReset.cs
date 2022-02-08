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

namespace MC1
{
    public partial class Form_SwitchloggerReset : Form
    {
        private ListNDictiomary _alpha;
        private string _userName;
        private string _accessLVL;
        private SQL_Tracker SQL;
        private List<string> lineheads = new List<string>();
        private List<Label> ListLabelsAll = new List<Label>();
        TimeSpan timerMax = new TimeSpan(0, 0, 5, 1, 0);
        TimeSpan timerZero = new TimeSpan(0, 0, 0, 0, 0);
        public Form_SwitchloggerReset(string user, string lvl, ListNDictiomary compositedAlpha)
        {
            InitializeComponent();
            Defaults();
            this._alpha = compositedAlpha;
            this._userName = user;
            this._accessLVL = lvl;
            this.SQL = new SQL_Tracker();

            ListLabelsAll.Add(labelL1Var);
            ListLabelsAll.Add(labelL2Var);
            ListLabelsAll.Add(labelL3Var);
            ListLabelsAll.Add(labelL4Var);
            ListLabelsAll.Add(labelL5Var);
            ListLabelsAll.Add(labelL6Var);
        }   

        private void Defaults()
        {
            comboBoxServers.SelectedIndex = 0;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            buttonReset.Enabled = false;
            buttonReset.Update();
            var index = comboBoxServers.SelectedIndex;

            string action = "Restarted switchlogger with index: " + comboBoxServers.Text;
            SQL.InsertAction(_userName, _accessLVL, action, "OK");

            if (index == 0)
            {
                lineheads = _alpha.ListServers;
            }
            else
            {
                lineheads.Add(_alpha.ListServers[index - 1]);
            }

            foreach (string linehead in lineheads)
            {
                string path = _alpha.DictServerIPs[linehead] + @"\SwitchLoggerReboot\restart.txt";
                string labelName = "label" + linehead + "Var";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                try
                {
                    File.WriteAllText(path, "restart");

                    if (File.Exists(path))
                    {
                        var control = this.Controls.Find(labelName, true).FirstOrDefault();
                        if (control != null)
                        {
                            control.Text = "Request sent..";
                            control.ForeColor = Color.FromArgb(255, 153, 0);
                            control.Update();
                        }
                    }
                }
                catch
                {
                    var control = this.Controls.Find(labelName, true).FirstOrDefault();
                    if (control != null)
                    {
                        control.Text = "Can't send request!";
                        control.ForeColor = Color.FromArgb(255, 51, 0);
                        control.Update();
                    }
                }
            }

            labelStatus.Text = "Switchlogger restarted on all servers. Changes will be visible in 5 mins.";
            Checker();

            if (index == 0)
            {
                labelStatus.Text = "Switchlogger restart requests sent to " + comboBoxServers.Text + " lines. See request status below.";
            }
            else
            {
                labelStatus.Text = "Switchlogger restart requests sent to " + comboBoxServers.Text + ". See request status below.";
            }
            pictureBoxLoading.Visible = true;
        }


        async void Checker()
        {
            DateTime checkerStart = DateTime.Now;
            DateTime chekcerProgress;
            while (true)
            {
                await Task.Delay(1000);
                chekcerProgress = DateTime.Now;
                TimeSpan stopWatchFromClick = (chekcerProgress - checkerStart);
                TimeSpan delta = timerMax - (stopWatchFromClick);
                labelTimer.Text = delta.ToString(@"mm\:ss");
                if (stopWatchFromClick >= timerMax)
                {
                    foreach (Label l in ListLabelsAll)
                    {
                        string path = _alpha.DictServerIPs[_alpha.ListServers[ListLabelsAll.IndexOf(l)]] + @"\SwitchLoggerReboot\restart.txt";
                        if (l.Text == "Request sent..")
                        {
                            l.Text = "restart.txt not generated. Contact TE";
                            l.ForeColor = Color.FromArgb(255, 0, 0);
                            l.Update();
                            labelStatus.Text = "SwitchLogger restart failed: restart.txt was not generated on server. Contact TE";
                            labelStatus.ForeColor = Color.FromArgb(255, 0, 0);
                            labelStatus.Update();

                        }
                        else if (l.Text == "Request received..")
                        {
                            l.Text = "restart.txt not collected. Contact TE";
                            l.ForeColor = Color.FromArgb(255, 0, 0);
                            l.Update();
                            labelStatus.Text = "SwitchLogger restart failed: restart.txt was not collected by server event task. Contact TE";
                            labelStatus.ForeColor = Color.FromArgb(255, 0, 0);
                            labelStatus.Update();
                        }
                    }
                    if (labelStatus.ForeColor != Color.FromArgb(255, 0, 0))
                    {
                        labelStatus.Text = "SwitchLogger restarted successfully.";
                        labelStatus.ForeColor = Color.FromArgb(51, 204, 51);
                        labelStatus.Update();
                    }
                    pictureBoxLoading.Visible = false;
                    break;
                }

                foreach (Label l in ListLabelsAll)
                {
                    string path = _alpha.DictServerIPs[_alpha.ListServers[ListLabelsAll.IndexOf(l)]] + @"\SwitchLoggerReboot\restart.txt";
                    if (l.Text == "Request sent..")
                    {
                        if (File.Exists(path))
                        {
                            l.Text = "Request received..";
                            l.ForeColor = Color.FromArgb(255, 153, 0);
                            l.Update();
                        }
                    }
                    if (l.Text == "Request received..")
                    {
                        if (!File.Exists(path))
                        {
                            l.Text = "SwitchLogger reset succesfull";
                            l.ForeColor = Color.FromArgb(51, 204, 51);
                            l.Update();
                        }
                    }
                }
            }
        }

        //OMG:
        //async void InfiniteLoop()
        //{
        //    while (true)
        //    {
        //        await Task.Delay(100);
        //        label2.Text = DateTime.Now.ToString();
        //    }
        //}
    }
}
