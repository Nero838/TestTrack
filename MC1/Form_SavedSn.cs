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
    public partial class Form_SavedSn : Form
    {
        private ListNDictiomary alpha;
        public Form_SavedSn(ListNDictiomary compositedAlpha)
        {
            InitializeComponent();
            alpha = compositedAlpha;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string input = textBoxSN.Text;
            string note = textBoxNote.Text;
            if (input.Length == 10)
            {
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Saved SN", "Denied");
                alpha.InsertSaveSN(alpha.userName, input, note);
            }
        }
    }
}
