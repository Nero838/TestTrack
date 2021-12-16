using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MC1
{
    public class SwitchloggerReset
    {
        private ListNDictiomary alpha;
        public SwitchloggerReset(ListNDictiomary compositedAlpha)
        {
            alpha = compositedAlpha;
        }

        public void Reset()
        {

            string action = "BV restarted automatically by switchlogger on all servers";
            alpha.InsertAction(alpha.userName, alpha.accessLevel, action, "OK");


            foreach (string server in alpha.ListServers)
            {
                string pathToCheckConnection = alpha.DictServerIPs[server];
                if (Directory.Exists(pathToCheckConnection))
                {
                    string path = alpha.DictServerIPs[server] + @"\SwitchLoggerReboot\restart.txt";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    File.WriteAllText(path, "restart");
                }
                else
                {
                    action = "Failed to automatically restart switchlogger on " + server;
                    alpha.InsertAction(alpha.userName, alpha.accessLevel, action, "Fail");
                }
            }
        }
    }
}
