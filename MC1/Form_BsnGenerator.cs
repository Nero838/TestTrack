using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace MC1
{
    public partial class Form_BsnGenerator : Form
    {
        private ListNDictiomary _alpha;
        private string unitSN;
        private string MbPN;
        private string MbSN;

        public Form_BsnGenerator(ListNDictiomary compositedAlpha)
        {
            InitializeComponent();
            _alpha = compositedAlpha;
        }

        private void buttonSetUnitSn_Click(object sender, EventArgs e)
        {
            bool tempCheck = Regex.IsMatch(textBoxUnitSN.Text, @"(EI)\w{2}\d{6}");
            if (tempCheck == true)
            {
                unitSN = textBoxUnitSN.Text;
                textBoxMbPn.Enabled = true;
                buttonSetMbPn.Enabled = true;
                string firstFour = textBoxUnitSN.Text.Substring(0, 4);
                if (_alpha.DictSnMB.ContainsKey(firstFour))
                {
                    textBoxMbPn.Text = _alpha.DictSnMB[firstFour];
                    textBoxMbPn.ForeColor = Color.FromArgb(0, 0, 0);
                }

                textBoxUnitSN.Enabled = false;
                textBoxUnitSN.BackColor = Color.FromArgb(204, 255, 102);
                buttonSetUnitSn.Enabled = false;
            }
            else
            {
                textBoxUnitSN.BackColor = Color.FromArgb(255, 153, 128);
                labelStatus.Text = "Incorrect unit SN input!";
            }

        }

        private void buttonSetMbPn_Click(object sender, EventArgs e)
        {
            bool tempCheck = Regex.IsMatch(textBoxMbPn.Text, @"(D)\d{4}\w{1}");
            if (tempCheck == true)
            {
                MbPN = textBoxMbPn.Text;
                textBoxMbSn.Enabled = true;
                buttonSetMbSn.Enabled = true;

                textBoxMbPn.Enabled = false;
                textBoxMbPn.BackColor = Color.FromArgb(204, 255, 102);
                buttonSetMbPn.Enabled = false;
            }
            else
            {
                textBoxUnitSN.BackColor = Color.FromArgb(255, 153, 128);
                labelStatus.Text = "Incorrect MB PN input!";
            }
        }

        private void buttonSetMbSn_Click(object sender, EventArgs e)
        {
            bool tempCheck = Regex.IsMatch(textBoxMbSn.Text, @"\d{8}");
            if (tempCheck == true)
            {
                MbSN = textBoxMbSn.Text;
                buttonGenerate.Enabled = true;

                textBoxMbSn.Enabled = false;
                textBoxMbSn.BackColor = Color.FromArgb(204, 255, 102);
                buttonSetMbSn.Enabled = false;
            }
            else
            {
                textBoxMbSn.BackColor = Color.FromArgb(255, 153, 128);
                labelStatus.Text = "Incorrect MB SN input!";
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            buttonGenerate.Enabled = false;
            if(string.IsNullOrEmpty(MbPN) || string.IsNullOrEmpty(MbSN) || string.IsNullOrEmpty(unitSN))
            {
                labelStatus.Text = String.Format("Error: Some of the varialbes are null:\nMbPN = {0}\nMbSN = {1}\nunitSN = {2}", MbPN, MbSN, unitSN);
            }
            else
            {
                string path = @"\\10.13.82.8\comm_uut\ORDERDATABSN\" + MbPN + @"\" + MbSN + ".txt";
                labelStatus.Text = string.Format("Generating {0} in {1}.txt inside {2} folder", unitSN, MbSN, MbPN);
                if (Directory.Exists(@"\\10.13.82.8\comm_uut\ORDERDATABSN\" + MbPN))
                {
                    using (var streamWriter = new StreamWriter(path, false))
                    {
                        streamWriter.WriteLine(unitSN);
                    }

                }
                else
                {
                    Directory.CreateDirectory(@"\\10.13.82.8\comm_uut\ORDERDATABSN\" + MbPN);
                    if (Directory.Exists(@"\\10.13.82.8\comm_uut\ORDERDATABSN\" + MbPN))
                    {
                        using (var streamWriter = new StreamWriter(path, false))
                        {
                            streamWriter.WriteLine(unitSN);
                        }

                    }
                    else
                    {
                        throw new Exception(String.Format("Unable to create new folder {0}, call TE", MbPN));
                    }
                }
                Wait(5000);
                labelStatus.Text = "Verifying if new " + path + " was created successfully";
                if (File.Exists(path))
                {
                    labelStatus.Text = "New OrderData BSN:\n" + path + "\nwas generated successfully.";
                    labelStatus.BackColor = Color.FromArgb(204, 255, 102);
                    _alpha.InsertAction(_alpha.userName, _alpha.accessLevel, String.Format("Generating OrderDataBSN({0},{1},{2})", unitSN, MbPN, MbSN), "OK");
                }
                else
                {
                    labelStatus.Text = "OrderData BSN:\n" + path + "\nwas not generated successfully.\n\nTry again or call TEs.";
                    labelStatus.BackColor = Color.FromArgb(255, 153, 128);
                    _alpha.InsertAction(_alpha.userName, _alpha.accessLevel, String.Format("Generating OrderDataBSN({0},{1},{2})",unitSN,MbPN,MbSN), "FAIL");
                }
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {

            textBoxUnitSN.Text = "";
            textBoxUnitSN.ForeColor = Color.FromArgb(0, 0, 0);
            textBoxUnitSN.BackColor = Color.FromArgb(255, 255, 255);
            textBoxUnitSN.Enabled = true;
            buttonSetUnitSn.Enabled = true;
            textBoxMbPn.Text = "";
            textBoxMbPn.ForeColor = Color.FromArgb(0, 0, 0);
            textBoxMbPn.BackColor = Color.FromArgb(255, 255, 255);
            textBoxMbPn.Enabled = false;
            buttonSetMbPn.Enabled = false;
            textBoxMbSn.Text = "";
            textBoxMbSn.ForeColor = Color.FromArgb(0, 0, 0);
            textBoxMbSn.BackColor = Color.FromArgb(255, 255, 255);
            textBoxMbSn.Enabled = false;
            buttonSetMbPn.Enabled = false;
            labelStatus.BackColor = Color.WhiteSmoke;
            labelStatus.Text = "Enter unit serial number.";
            buttonGenerate.Enabled = false;
        }

        private void textBoxUnitSN_Click(object sender, EventArgs e)
        {
            if (textBoxUnitSN.Enabled == true)
            {
                textBoxUnitSN.Text = "";
                textBoxUnitSN.ForeColor = Color.FromArgb(0, 0, 0);
                textBoxUnitSN.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void textBoxMbPn_Click(object sender, EventArgs e)
        {
            if (textBoxMbPn.Enabled == true)
            {
                textBoxMbPn.Text = "";
                textBoxMbPn.ForeColor = Color.FromArgb(0, 0, 0);
                textBoxMbPn.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void textBoxMbSn_Click(object sender, EventArgs e)
        {
            if (textBoxMbSn.Enabled == true)
            {
                textBoxMbSn.Text = "";
                textBoxMbSn.ForeColor = Color.FromArgb(0, 0, 0);
                textBoxMbSn.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void Wait(int milliseconds)
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
    }
}
