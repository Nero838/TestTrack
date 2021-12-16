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
    public partial class Form_PasswordTable : Form
    {
        private ListNDictiomary alpha;
        public Form_PasswordTable(ListNDictiomary compositedAlpha)
        {
            InitializeComponent();
            alpha = compositedAlpha;
        }

        private void Form_PasswordTable_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = alpha.GetStatusTable_RemoteLineaheadPWs();
            labelUser.Text = alpha.userName;
        }
    }
}
