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
    public partial class Form_Tool_CUZImageCheck : Form
    {
        private ListNDictiomary _alpha;
        private string _userName;
        private string _accessLVL;
        private SQL_Tracker SQL;

        public Form_Tool_CUZImageCheck(string user, string lvl, ListNDictiomary compositedAlpha)
        {
            InitializeComponent();
            _alpha = compositedAlpha;
            _userName = user;
            _accessLVL = lvl;
            this.SQL = new SQL_Tracker();
            labelStatus.Text = "Enter CUZ project number or copy from CUZ project list on right side.";
            dataGridViewCUZ.DataSource = SQL.Get_ImageCUZList();
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            Clear();
            string input = textBoxInput.Text;
            if (input.Length > 10)
            {
                if (input.Substring(0, 3) != "CUZ")
                {
                    labelStatus.Text = "Enter project code you want to check.\nExample: CUZ:1012V500-HKM01";
                }
                else
                {
                    DataTable dtBaseunitImagePnData = SQL.Get_BASEUNIT_IMAGE_PN_DATA(input);
                    string SFTDummyPn = dtBaseunitImagePnData.Rows[0].Field<string>("SFTDummyPn");
                    string BimSN = dtBaseunitImagePnData.Rows[0].Field<string>("BIM_SN_01");
                    string Boot1 = dtBaseunitImagePnData.Rows[0].Field<string>("Boot1");
                    string Active = dtBaseunitImagePnData.Rows[0].Field<string>("Active");
                    string editor = dtBaseunitImagePnData.Rows[0].Field<string>("Editor");
                    string date = dtBaseunitImagePnData.Rows[0].Field<DateTime>("Udt").ToString();


                    labelVarSTF.Text = SFTDummyPn;
                    labelVarBIM.Text = BimSN;
                    string imageName = "WAT" + BimSN.Substring(9, 2) + BimSN.Substring(13, 3);
                    labelVarImage.Text = imageName;
                    labelVarName.Text = editor;
                    labelVarDate.Text = date;

                    if (Boot1 == "Y")
                    {
                        labelVarBoot.Text = "OK";
                        labelVarBoot.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        labelVarBoot.Text = "FAIL";
                        labelVarBoot.ForeColor = System.Drawing.Color.Red;
                        labelStatus.Text = "FAIL: Boot not set.";
                    }

                    if (Active == "Y")
                    {
                        labelVarActive.Text = "OK";
                        labelVarActive.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        labelVarActive.Text = "FAIL";
                        labelVarActive.ForeColor = System.Drawing.Color.Red;
                        labelStatus.Text = "FAIL: Image not active.";
                    }

                    List<string> imageMissingOn = CheckImageOnServers(imageName);
                    if (imageMissingOn.Count == 0)
                    {
                        labelVarLineheads.Text = "OK";
                        labelVarLineheads.ForeColor = System.Drawing.Color.Green;
                        labelVarLineheads.Update();
                    }
                    else
                    {
                        labelVarLineheads.Text = "FAIL";
                        labelVarLineheads.ForeColor = System.Drawing.Color.Red;
                        labelVarLineheads.Update();

                        string statusText = imageName + " is missing on server: " + String.Join(", ", imageMissingOn.ToArray());
                        labelStatus.Text = statusText;
                    }
                    if (labelVarActive.Text == "OK" && labelVarBoot.Text == "OK" && labelVarLineheads.Text == "OK")
                    {
                        labelStatus.Text = "Image " + imageName + " for project " + input + " is active and present on all servers";
                    }
                    else
                    {
                        //labelStatus.Text = "Something wrong :(";
                    }
                }
            }
            else
            {
                labelStatus.Text = "Enter project code you want to check.\nExample: CUZ:1012V500-HKM01";
            }

        }
        private List<string> CheckImageOnServers(string imageName)
        {
            List<string> imageMissingOn = new List<string>();
            foreach (string server in _alpha.ListServers)
            {

                string filePath = _alpha.DictServerIPs[server] + @"comm_uut\onlineloadimages\" + imageName + ".DIA";
                string labelName = "label" + server + "Health";
                if (File.Exists(filePath) == false)
                {
                    imageMissingOn.Add(server);
                }

                else
                {

                }
            }
            return imageMissingOn;
        }

        private void Clear()
        {
            labelVarSTF.Text = "";
            labelVarSTF.Update();
            labelVarBIM.Text = "";
            labelVarBIM.Update();
            labelVarImage.Text = "";
            labelVarImage.Update();
            labelVarName.Text = "";
            labelVarName.Update();
            labelVarDate.Text = "";
            labelVarDate.Update();
            labelVarBoot.Text = "";
            labelVarBoot.Update();
            labelVarLineheads.Text = "";
            labelVarLineheads.Update();
            labelVarBoot.ForeColor = System.Drawing.Color.Black;
            labelVarBoot.Update();
            labelVarActive.Text = "";
            labelVarActive.ForeColor = System.Drawing.Color.Black;
            labelVarActive.Update();
            labelVarLineheads.Text = "";
            labelVarLineheads.ForeColor = System.Drawing.Color.Black;
            labelVarLineheads.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonBIM_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.204/CtoNew/Maintain/FCCL/BIMAssignment.aspx");
            SQL.InsertAction(_userName, _accessLVL, "Opened FIS/BIM_Assigment", "OK");
        }
    }
}
