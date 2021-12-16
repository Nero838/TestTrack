using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC1
{
    public class Alpha_Tech : ListNDictiomary
    {

        private SQL_Basic basicSQL;
        public Alpha_Tech(string user, string lvl, string version) : base(user, lvl, version) { }

        public override void GetLineheadConnectionData()
        {
            basicSQL = new SQL_Basic();
            serverUserName = "TestTech";
            lineheadLoginPasswords.Add("L1", basicSQL.GetLineaheadPassword("10.13.82.2", "TestTech"));
            lineheadLoginPasswords.Add("L2", basicSQL.GetLineaheadPassword("10.13.82.3", "TestTech"));
            lineheadLoginPasswords.Add("L3", basicSQL.GetLineaheadPassword("10.13.82.4", "TestTech"));
            lineheadLoginPasswords.Add("LAB", basicSQL.GetLineaheadPassword("10.13.82.200", "TestTech"));
            lineheadLoginPasswords.Add("L8", basicSQL.GetLineaheadPassword("10.13.82.8", "TestTech"));
            lineheadLoginPasswords.Add("L10", basicSQL.GetLineaheadPassword("10.13.82.10", "TestTech"));
            lineheadLoginPasswords.Add("Storage", basicSQL.GetLineaheadPassword("10.13.82.201", "TestTech"));
        }
    }
}
