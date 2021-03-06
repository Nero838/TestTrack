using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC1
{
    public class Alpha_Guest : ListNDictiomary
    {

        private SQL_Basic basicSQL;
        public Alpha_Guest(string user, string lvl, string version) : base(user, lvl, version) { }

        public override void GetLineheadConnectionData()
        {
            basicSQL = new SQL_Basic();
            serverUserName = "SwapTech";
            lineheadLoginPasswords.Add("L1", basicSQL.GetLineaheadPassword("10.13.82.1", "SwapTech"));
            lineheadLoginPasswords.Add("L2", basicSQL.GetLineaheadPassword("10.13.82.2", "SwapTech"));
            lineheadLoginPasswords.Add("L3", basicSQL.GetLineaheadPassword("10.13.82.3", "SwapTech"));
            lineheadLoginPasswords.Add("L4", basicSQL.GetLineaheadPassword("10.13.82.4", "SwapTech"));
            lineheadLoginPasswords.Add("L5", basicSQL.GetLineaheadPassword("10.13.82.5", "SwapTech"));
            lineheadLoginPasswords.Add("L6", basicSQL.GetLineaheadPassword("10.13.82.6", "SwapTech"));
            lineheadLoginPasswords.Add("Storage", basicSQL.GetLineaheadPassword("10.13.82.201", "SwapTech"));
        }
    }
}
