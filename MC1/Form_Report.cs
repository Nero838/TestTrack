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
    public partial class Form_Report : Form
    {
        public SQL_Tracker SQL = new SQL_Tracker();
        public string userName;
        public string accessLVL;
        public Form_Report(string user, string lvl)
        {
            InitializeComponent();
            userName = user;
            accessLVL = lvl;

        }

        private void buttonLeave1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonLeave2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonSend1_Click(object sender, EventArgs e)
        {
            string type = "Bug/Error";
            string message = textBox1.Text;
            SQL.InsertReport(userName, accessLVL, type, message);

        }

        private void buttonSend2_Click(object sender, EventArgs e)
        {
            string type = "Suggestion/Idea";
            string message = textBox2.Text;
            SQL.InsertReport(userName, accessLVL, type, message);
        }
    }
}
