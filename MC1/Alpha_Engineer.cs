using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC1
{
    public class Alpha_Engineer : ListNDictiomary
    {

        private SQL_Basic basicSQL;
        public Alpha_Engineer(string user, string lvl, string version) : base(user, lvl, version) { }

        public override void GetLineheadConnectionData()
        {
            basicSQL = new SQL_Basic();
            serverUserName = "Administrator";
            lineheadLoginPasswords.Add("L1", basicSQL.GetLineaheadPassword("10.13.82.1", "Administrator"));
            lineheadLoginPasswords.Add("L2", basicSQL.GetLineaheadPassword("10.13.82.2", "Administrator"));
            lineheadLoginPasswords.Add("L3", basicSQL.GetLineaheadPassword("10.13.82.3", "Administrator"));
            lineheadLoginPasswords.Add("L4", basicSQL.GetLineaheadPassword("10.13.82.4", "Administrator"));
            lineheadLoginPasswords.Add("L5", basicSQL.GetLineaheadPassword("10.13.82.5", "Administrator"));
            lineheadLoginPasswords.Add("L6", basicSQL.GetLineaheadPassword("10.13.82.6", "Administrator"));
            lineheadLoginPasswords.Add("Storage", basicSQL.GetLineaheadPassword("10.13.82.201", "Administrator"));
        }
    }

}
