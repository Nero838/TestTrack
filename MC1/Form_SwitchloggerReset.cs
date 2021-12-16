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
        public Form_SwitchloggerReset(string user, string lvl, ListNDictiomary compositedAlpha)
        {
            InitializeComponent();
            Defaults();
            this._alpha = compositedAlpha;
            this._userName = user;
            this._accessLVL = lvl;
            this.SQL = new SQL_Tracker();
        }

        private void Defaults()
        {
            comboBoxServers.SelectedIndex = 0;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            var index = comboBoxServers.SelectedIndex;

            string action = "Restarted switchlogger with index: " + comboBoxServers.Text;
            SQL.InsertAction(_userName, _accessLVL, action, "OK");

            if (index == 0)
            {
                foreach (string server in _alpha.ListServers)
                {
                    string path = _alpha.DictServerIPs[server] + @"\SwitchLoggerReboot\restart.txt";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    File.WriteAllText(path, "restart");
                }
                labelStatus.Text = "Switchlogger restarted on all servers. Changes will be visible in 5 mins.";
            }
            else
            {
                string path = _alpha.DictServerIPs[_alpha.ListServers[index - 1]] + @"\SwitchLoggerReboot\restart.txt";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.WriteAllText(path, "restart");
            }
            labelStatus.Text = "Switchlogger restarted on " + comboBoxServers.Text + " lines. Changes will be visible in 5 mins.";
            pictureBoxLoading.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
