using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MC1
{
    public partial class Form_TDS_Progress : Form
    {
        public SQL_Tracker SQL;
        public string userName;
        public string accessLVL;
        public Form_TDS_Progress(string user, string lvl, string Sn)
        {
            this.SQL = new SQL_Tracker();
            this.userName = user;
            this.accessLVL = lvl;

            InitializeComponent();
            textBox1.Text = Sn;
            if (textBox1.Text.Length == 10)
            {
                FillTheTable();
            }
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            dataGridViewTDSProgress.DataSource = null;
            if (textBox1.Text.Length == 10)
            {
                FillTheTable();
            }
            else
            {
                labelStatus.Text = "Enter SNO.";
            }
        }

        private async void FillTheTable()
        {
            labelStatus.Text = "";
            var task_TDS_Progress = SQL.GetTDSDataTable_TDS_Progress(textBox1.Text);
            DataTable TDS_Progress = await task_TDS_Progress;
            dataGridViewTDSProgress.DataSource = TDS_Progress;
            dataGridViewTDSProgress.Update();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
