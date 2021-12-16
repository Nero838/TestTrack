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
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace MC1
{
    public partial class Form_Collector : Form
    {
        public ListNDictiomary alpha;
        public Form_Collector(ListNDictiomary compositedAplha)
        {
            InitializeComponent();
            Defaults();
            alpha = compositedAplha;
        }

        private void Defaults()
        {
            comboBoxLogType.SelectedIndex = 0;
            comboBoxOutput.SelectedIndex = 0;
        }

        private void StatusUpdate(string labelText)
        {
            labelStatusVar.Text = labelText;
            labelStatusVar.Update();
        }

        private List<string> ServerSelect()
        {
            List<string> servers = new List<string>();
            if (checkBoxL1.Checked)
            {
                servers.Add("L1");
            }
            if (checkBoxL2.Checked)
            {
                servers.Add("L2");
            }
            if (checkBoxL3.Checked)
            {
                servers.Add("L3");
            }
            if (checkBoxL8.Checked)
            {
                servers.Add("L8");
            }
            if (checkBoxLAB.Checked)
            {
                servers.Add("LAB");
            }
            return servers;
        }

        private List<string> FolderSelect()
        {
            List<string> areas = new List<string>();

            if (checkBoxTest.Checked)
            {
                areas.Add("Test");
            }
            if (checkBoxLogs.Checked)
            {
                areas.Add("Logs");
            }
            if (checkBoxArchive.Checked)
            {
                areas.Add("Arch");
            }

            return areas;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var shallWe = CheckSetup();
            if (shallWe == "Ready")
            {
                Gather();
            }
            else
            {
                StatusUpdate(shallWe);
            }
        }

        private string CheckSetup()
        {
            string allOK = "Ready";
            if (textBoxSearch1.Text == "")
            {
                allOK = "Fill the string to search in panel A. box first";
            }
            if (textBoxSN.Text == "")
            {
                allOK = "Fill SN type you want to seach from (panel 3).";
            }
            if (textBoxOutput.Text == "")
            {
                allOK = "Fill the output file (panel 8).";
            }
            List<string> servers = ServerSelect();
            if (servers.Count == 0)
            {
                allOK = "Need at least one server checked in panel 5";
            }
            List<string> areas = FolderSelect();
            if (areas.Count == 0)
            {
                allOK = "Need at least one folder checked in panel 4";
            }
            if (int.TryParse(textBoxOld.Text,out int parss) == false || textBoxOld.Text == "")
            {
                allOK = "Invalid or missing number in panel 6";
            }
            if (int.TryParse(textBoxSeq.Text, out int parss2) == false || textBoxSeq.Text == "")
            {
                allOK = "Invalid or missing number in panel 7";
            }
            return allOK;
        }

        private void Gather()
        {
            progressBar1.Value = 0;
            StatusUpdate("Please wait..");
            int numberFound = 0;
            int numberTotal = 0;
            var StringSearch1 = textBoxSearch1.Text;
            var StringSearch2 = textBoxSearch2.Text;
            var StringSearch3 = textBoxSearch3.Text;
            //var StringSearch = "Gesamttestzeit";
            var SNtypePRE = textBoxSN.Text;
            var SNtype = "EI" + SNtypePRE + "*";
            //var servers = alpha.ListServers;
            List<string> servers = ServerSelect();
            List<string> areas = FolderSelect();
            var sequencing = Int32.Parse(textBoxSeq.Text);
            var notOlderThan = Int32.Parse(textBoxOld.Text);
            var outputPRE = textBoxOutput.Text;
            var output = @"C:\TestTrack\" + outputPRE + ".txt";
            var outputExcel = @"C:\TestTrack\" + outputPRE + ".xlsx";
            var suffix = alpha.ListLogTypesGather[comboBoxLogType.SelectedIndex];

            List<string> Found1 = new List<string>();
            List<string> Found2 = new List<string>();
            List<string> Found3 = new List<string>();
            List<string> Num = new List<string>();
            List<string> listSN = new List<string>();
            List<string> CreationTimes = new List<string>();
            progressBar1.Maximum = servers.Count;

            foreach (var server in servers)
            {
                progressBar1.Increment(1);
                foreach (var area in areas)
                {
                    StatusUpdate("Preparing data on " + alpha.DictServerNames[server] + " - " + area);
                    var folderIn = alpha.DictServerIPs[server] + alpha.DictAreaPathsForGather[area];
                    var folderSNArray = Directory.GetDirectories(folderIn, SNtype, SearchOption.TopDirectoryOnly);
                    foreach (var folderSN in folderSNArray)
                    {
                        numberTotal += 1;
                        if (numberTotal % sequencing == 0)
                        {
                            int firstStringPosition = folderSN.IndexOf("EI");
                            string SN;
                            try
                            {
                                SN = folderSN.Substring(firstStringPosition, 10);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                            var filePath = folderSN + @"\" + SN + suffix;
                            if (File.Exists(filePath) == true)
                            {
                                if (DateTime.UtcNow - File.GetCreationTimeUtc(filePath) < TimeSpan.FromDays(notOlderThan))
                                {
                                    string creationTime = File.GetCreationTimeUtc(filePath).ToString();
                                    numberFound += 1;
                                    Num.Add(numberFound.ToString());
                                    CreationTimes.Add(creationTime);
                                    listSN.Add(SN);
                                    string statusText = "Checking " + numberFound + ". log: " + SN + suffix + " on server " + alpha.DictServerNames[server] + "..";
                                    StatusUpdate(statusText);
                                    var logFileLinesArray = File.ReadAllLines(filePath);
                                    string foundLine1 = "Not Found";

                                    string foundLine2;
                                    if (StringSearch2 == "")
                                    {
                                        foundLine2 = "Not set";
                                    }
                                    else
                                    {
                                        foundLine2 = "Not Found";
                                    }

                                    string foundLine3;
                                    if (StringSearch3 == "")
                                    {
                                        foundLine3 = "Not set";
                                    }
                                    else
                                    {
                                        foundLine3 = "Not Found";
                                    }

                                    //CHECKING LINES in LOG
                                    if (foundLine2 == "Not set")
                                    {
                                        foreach (var line in logFileLinesArray)
                                        {
                                            if (line.Contains(StringSearch1))
                                            {
                                                foundLine1 = line;
                                                continue;
                                            }
                                        }
                                    }
                                    else if (foundLine3 == "Not set")
                                    {
                                        {
                                            foreach (var line in logFileLinesArray)
                                            {
                                                if (line.Contains(StringSearch1))
                                                {
                                                    foundLine1 = line;
                                                    continue;
                                                }
                                                else if (line.Contains(StringSearch2))
                                                {
                                                    foundLine2 = line;
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        {
                                            foreach (var line in logFileLinesArray)
                                            {
                                                if (line.Contains(StringSearch1))
                                                {
                                                    foundLine1 = line;
                                                    continue;
                                                }
                                                else if (line.Contains(StringSearch2))
                                                {
                                                    foundLine2 = line;
                                                    continue;
                                                }
                                                else if (line.Contains(StringSearch3))
                                                {
                                                    foundLine3 = line;
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                    Found1.Add(foundLine1);
                                    Found2.Add(foundLine2);
                                    Found3.Add(foundLine3);
                                }
                            }
                        }
                    }
                }
            }
            //if (!File.Exists(output))
            if (!Directory.Exists(@"C:\TestTrack"))
            {
                Directory.CreateDirectory(@"C:\TestTrack");
            }

            if (Found1.Count() != 0 || Found2.Count() != 0 || Found3.Count() != 0)
            {
                if (comboBoxOutput.SelectedIndex == 0)
                {
                    Excel excel = new Excel(outputExcel, 1);
                    //excel.CreateNewFile();

                    if (Found2[0] == "Not set")
                    {
                        for (var i = 0; i < Num.Count; i++)
                        {
                            excel.WriteToCell(i, 0, Num[i]);
                            excel.WriteToCell(i, 1, CreationTimes[i]);
                            excel.WriteToCell(i, 2, listSN[i]);
                            excel.WriteToCell(i, 3, Found1[i]);
                        }
                    }
                    else if (Found3[0] == "Not set")
                    {
                        for (var i = 0; i < Num.Count; i++)
                        {
                            excel.WriteToCell(i, 0, Num[i]);
                            excel.WriteToCell(i, 1, CreationTimes[i]);
                            excel.WriteToCell(i, 2, listSN[i]);
                            excel.WriteToCell(i, 3, Found1[i]);
                            excel.WriteToCell(i, 4, Found2[i]);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < Num.Count; i++)
                        {
                            excel.WriteToCell(i, 0, Num[i]);
                            excel.WriteToCell(i, 1, CreationTimes[i]);
                            excel.WriteToCell(i, 2, listSN[i]);
                            excel.WriteToCell(i, 3, Found1[i]);
                            excel.WriteToCell(i, 4, Found2[i]);
                            excel.WriteToCell(i, 5, Found3[i]);
                        }
                    }

                    excel.Save();
                    //excel.SaveAs(outputExcel);
                    excel.Close();
                }

                else
                {
                    List<string> Final = new List<string>();
                    if (Found2[1] == "Not set")
                    {
                        for (var i = 0; i < Num.Count; i++)
                        {
                            string finalLine = Num[i] + "/" + CreationTimes[i] + "/" + listSN[i] + "/" + Found1[i];
                            Final.Add(finalLine);
                        }
                    }
                    else if (Found3[1] == "Not set")
                    {
                        for (var i = 0; i < Num.Count; i++)
                        {
                            string finalLine = Num[i] + "/" + CreationTimes[i] + "/" + listSN[i] + "/" + Found1[i] + "/" + Found2[i];
                            Final.Add(finalLine);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < Num.Count; i++)
                        {
                            string finalLine = Num[i] + "/" + CreationTimes[i] + "/" + listSN[i] + "/" + Found1[i] + "/" + Found2[i] + "/" + Found3[i];
                            Final.Add(finalLine);
                        }
                    }

                    File.WriteAllLines(output, Final, Encoding.UTF8);
                }
                StatusUpdate("Done.");
                Process.Start(@"C:\TestTrack");
            }
            else
            {
                StatusUpdate("No results found. Try different setup.");
                msgBxNologs();
            }
        }

        private void msgBxNologs()
        {
            {
                // Initializes the variables to pass to the MessageBox.Show method.
                string message = "No results found. Try different setup.";
                string caption = "Warning";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, icon);
            }
        }

        private void buttonOutputFolder_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@"C:\TestTrack"))
            {
                Directory.CreateDirectory(@"C:\TestTrack");
            }
            Process.Start(@"C:\TestTrack");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
