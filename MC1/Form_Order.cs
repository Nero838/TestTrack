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

namespace MC1
{
    public partial class Form_Order : Form
    {
        //public SQL_Tracker SQL;
        public string userName;
        public string accessLVL;
        private ListNDictiomary _alpha;
        public Form_Order(string user, string lvl, ListNDictiomary alpha)
        {
            //SQL = new SQL_Tracker();
            userName = user;
            accessLVL = lvl;
            _alpha = alpha;
            InitializeComponent();
        }

        private void Form_Order_Load(object sender, EventArgs e)
        {
            dataGridViewModel.DataSource = _alpha.ModelsDataTable;

            dataGridViewModel.Columns[0].Width = (int)(dataGridViewModel.Width * 0.10);
            dataGridViewModel.Columns[1].Width = (int)(dataGridViewModel.Width * 0.4);
            dataGridViewModel.Columns[2].Width = (int)(dataGridViewModel.Width * 0.10);
            dataGridViewModel.Columns[3].Width = (int)(dataGridViewModel.Width * 0.15);
            //dataGridViewModel.Columns[4].Width = (int)(dataGridViewModel.Width * 0.15);
            // also may be a good idea to set FILL for the last column
            // to accomodate the round up in conversions
            dataGridViewModel.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void buttonGetData_Click(object sender, EventArgs e)
        {
            string tableType = "Order";
            string orderNumber = textBoxOrder.Text;
            _alpha.InsertSearchDataGrid(userName, accessLVL, tableType, orderNumber);
            dataGridViewOrder.DataSource = _alpha.GetOrderDataTable(orderNumber);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonGetDataPO_Click(object sender, EventArgs e)
        {
            string POnum = textBoxPO.Text;
            string tableType = "PO";
            _alpha.InsertSearchDataGrid(userName, accessLVL, tableType, POnum);
            dataGridViewPO.DataSource = _alpha.GetPODataTable(POnum);
        }

        private void buttonExitPO_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonGetSkuStatus_Click(object sender, EventArgs e)
        {
            _alpha.InsertAction(userName, accessLVL, "Got TT hourly SKU report", "OK");
            List<string> SKUs = GetSkusFromTextBox();
            string listSKUs = String.Join("', '", SKUs.ToArray());
            DataTable dtSKUs = _alpha.GetStatusTable_Sku(listSKUs);
            dataGridViewSkuStat.DataSource = dtSKUs;
        }

        private List<string> GetSkusFromTextBox()
        {
            //List<string> output = new List<string>();
            Regex rx = new Regex(@"(WG)\d{10}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string Pattern = @"(WG)\d{10}";
            string Content = textBoxSkuStat.Text;
            MatchCollection matchList = Regex.Matches(Content, Pattern);
            var output = matchList.Cast<Match>().Select(match => match.Value).ToList();
            return output;
        }

        //private void buttontest_Click(object sender, EventArgs e)
        //{
        //    Regex reg = new Regex(textBox2.Text);
        //    string contentTostring = textBoxtest1.Text;
        //    bool result = reg.IsMatch(contentTostring);
        //    labeltest.Text = result.ToString();
        //    textBoxtest2.Text = contentTostring;
        //}

        private void buttonExitTestStat_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
