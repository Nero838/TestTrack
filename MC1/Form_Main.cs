using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MC1
{

    public partial class Form_Main : Form
    {
        private ListNDictiomary alpha;
        private List<string> _history = new List<string>();
        private int searchCount = 0;
        private int pikacheck = 0;
        private string Index_Clear = "....";
        private string Index_NotFound = "✕";
        private List<string> possibleReMFI = new List<string>();
        Stopwatch stopWatch = new Stopwatch();
        public Form_Main(ListNDictiomary compositedPolymorphAlpha)
        {
            this.alpha = compositedPolymorphAlpha;
            //this._history = new List<string>() { "", "", "", "", "", "", "" };
            //DataTable dtHistory = alpha.GetSearchHistory(alpha.userName);
            //GetHistory();
            InitializeComponent();
            CustomizeDesign();
            AccessCustomize(alpha.accessLevel);
            Defaults();
            Mapping();
            SetUserName();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            Check_Updater_data();
            GetHistory();
            StatusUpdate("Ready, Enter SN..");
        }

        private void AccessCustomize(string LVL)
        {
            if (LVL == "Guest")
            {
                labelLVL.Text = "Guest";
                checkBoxSaver.Checked = false;
                checkBoxSaver.Enabled = false;
                buttonData.Enabled = false;
                buttonBvReset.Enabled = false;
                buttonRegen.Enabled = false;
            }
            else if (LVL == "Technician")
            {
                labelLVL.Text = "Technician";
                buttonData.Enabled = false;
            }
            else if (LVL == "Engineer")
            {
                labelLVL.Text = "Engineer";
            }
            else
            {
                labelLVL.Text = "Chyba";
            }
        }

        private void Defaults()
        {
            comboBoxODs.SelectedIndex = 3;
            tabControlOrderData.SelectedTab = LH2;
        }
        private void GetHistory()
        {
            DataTable dtHistory = alpha.GetSearchHistory(alpha.userName);
            foreach (DataRow row in dtHistory.Rows)
            {
                _history.Add(row["SN"].ToString());
            }

            int historyCount = _history.Count;
            if (historyCount > 0)
            {
                for (int i = 0; i < historyCount; i++)
                {
                    string history_label_name = "labelHistory" + (i + 1).ToString();
                    var control = this.Controls.Find(history_label_name, true).FirstOrDefault();
                    if (control != null)
                    {
                        control.Text = _history[i];
                        control.Update();

                    }
                    else
                    {
                    }
                }
            }
           
        }
        private void CustomizeDesign()
        {
            //panelSideSub1.Visible = false;
            labelAppVer.Text = alpha.appVersion;
            panelSideSub2.Visible = false;
            panelSideSubBV.Visible = false;
        }

        public void SetUserName()
        {
            labelUser.Text = alpha.userName;
        }

        public void Mapping()
        {
            foreach (string server in alpha.ListServers)
            {
                string status = "Mapping " + alpha.DictServerNames[server] + ", if there is issue with connectiong this could take longer.";
                StatusUpdate(status);

                //TEST THIS after netsh winsock reset
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                string strCmdText = "/C C:/Windows/System32/net.exe use " + alpha.DictServerIPs[server] + @"comm_uut" + " /persistent:no /user:" + alpha.serverUserName + " \"" + alpha.lineheadLoginPasswords[server] + "\"";
                startInfo.Arguments = strCmdText;
                process.StartInfo = startInfo;
                process.Start();
            }
            StatusUpdate("Servers mapped..");

        }

        private void LineHealthCheck()
        {
            if (!alpha.ListServers.Contains("L1"))
            {
                labelL1Health.Text = "● offline";
                labelL1Health.ForeColor = System.Drawing.Color.Red;
                labelL1Health.Update();
            }

            if (!alpha.ListServers.Contains("L2"))
            {
                labelL2Health.Text = "● offline";
                labelL2Health.ForeColor = System.Drawing.Color.Red;
                labelL2Health.Update();
            }

            if (!alpha.ListServers.Contains("L3"))
            {
                labelL3Health.Text = "● offline";
                labelL3Health.ForeColor = System.Drawing.Color.Red;
                labelL3Health.Update();
            }

            if (!alpha.ListServers.Contains("LAB"))
            {
                labelLABHealth.Text = "● offline";
                labelLABHealth.ForeColor = System.Drawing.Color.Red;
                labelLABHealth.Update();
            }

            if (!alpha.ListServers.Contains("L8"))
            {
                labelL8Health.Text = "● offline";
                labelL8Health.ForeColor = System.Drawing.Color.Red;
                labelL8Health.Update();
            }

            if (!alpha.ListServers.Contains("L10"))
            {
                labelL10Health.Text = "● offline";
                labelL10Health.ForeColor = System.Drawing.Color.Red;
                labelL10Health.Update();
            }

            foreach (string server in alpha.ListServers)
            {

                string checkPath = alpha.DictServerIPs[server] + @"comm_uut\Testresults";
                string labelName = "label" + server + "Health";
                if (Directory.Exists(checkPath))
                {
                    var control = this.Controls.Find(labelName, true).FirstOrDefault();
                    if (control != null)
                    {
                        control.Text = "● online";
                        control.ForeColor = System.Drawing.Color.Green;
                        control.Update();

                    }
                    else { }
                }
                else
                {
                    var control = this.Controls.Find(labelName, true).FirstOrDefault();
                    if (control != null)
                    {
                        control.Text = "● offline";
                        control.ForeColor = System.Drawing.Color.Red;
                        control.Update();

                    }
                    else { }
                }
            }
        }

        private void ClearAuto()
        {
            alpha.ListResultPaths.Clear();
            //alpha.ListServersWorking.Clear();
            textBoxModel.Text = "";
            textBoxModel.Update();
            textBoxMBSN.Text = "";
            textBoxMBSN.Update();
            textBoxLoc.Text = "";
            textBoxLoc.Update();
            textBoxStation.Text = "";
            textBoxStation.Update();
            textBoxUpdated.Text = "";
            textBoxUpdated.Update();
            textBoxCreated.Text = "";
            textBoxCreated.Update();
            textBoxOrder.Text = "";
            textBoxOrder.Update();
            textBoxPO.Text = "";
            textBoxPO.Update();
            textBoxDn.Text = "";
            textBoxDn.Update();
            textBoxFamily.Text = "";
            textBoxFamily.Update();
            textBoxM4U.Text = "";
            textBoxM4U.Update();
            textBoxPassToAll.Text = "";
            textBoxPassToAll.Update();
            progressBarProgress.Value = 0;
            progressBarProgress.Update();
            textBoxTDS_Status.Text = "";
            textBoxTDS_Status.Update();
            textBoxTDS_Location.Text = "";
            textBoxTDS_Location.Update();
            textBoxTDS_Status.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            textBoxTDS_Status.Update();
            textBoxTDS_IP.Text = "";
            textBoxTDS_IP.Update();
            textBoxTDS_MAC.Text = "";
            textBoxTDS_MAC.Update();
            textBoxTDS_Progress.Text = "";
            textBoxTDS_Progress.Update();
            progressBarTDS.Value = 0;
            progressBarTDS.Update();
            labelTDS_Attribute.Text = "";
            labelTDS_Attribute.Update();
            //buttonTDS_Restart.Visible = false;
            buttonTDS_Restart.Text = "Restart TDS";
            buttonTDS_Restart.Enabled = false;
            buttonTDS_Restart.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            buttonTDS_Restart.Update();
            buttonTDSHistory.Visible = false;
            //administrator/Iczpdc1911
            possibleReMFI.Clear();
            buttonReMFI.Enabled = false;
            buttonReMFI.BackColor = Color.FromArgb(255, 255, 255);

            textBoxNextStation.Text = "";
            textBoxNextStation.BackColor = Color.FromArgb(240, 240, 240);
            textBoxNextStation.Update();



            inputBox2.ReadOnly = false;
            foreach (string server in alpha.ListServers)
            {
                foreach (string area in alpha.ListAreas)
                {
                    foreach (string type in alpha.ListLogTypes)
                    {
                        string labelName = "label" + server + area + type;
                        string status = Index_Clear;
                        LabelUpdate(labelName, status);

                        if (area == "Test" && (type == "Adler" || type == "OA3" || type == "60T"))
                        {
                            string BackupPicName = "pictureBoxBackup" + server + type;
                            var control2 = this.Controls.Find(BackupPicName, true).FirstOrDefault();
                            if (control2 != null)
                            {
                                control2.Visible = false;
                                control2.Update();

                            }
                            else
                            {
                            }
                        }
                    }
                }
                foreach (string ODtype in alpha.ListTypeOD)
                {
                    string labelName = "label" + server + ODtype;
                    string status = Index_Clear;
                    LabelUpdate(labelName, status);
                }
            }
        }

        private List<string> Get_WrokingLineaheadsList()
        {
            List<string> WrokingLineaheadsList = new List<string>();
            foreach (string server in alpha.ListServers)
            {
                string labelName = "label" + server + "Health";
                var control = this.Controls.Find(labelName, true).FirstOrDefault();
                if (control != null)
                {
                    if (control.Text == "● online")
                    {
                        WrokingLineaheadsList.Add(server);
                    }
                    control.Update();
                }
                else
                {
                    continue;
                }
            }
            return WrokingLineaheadsList;
        }

        private void StatusUpdate(string labelText)
        {
            labelVarStatus.Text = labelText;
            labelVarStatus.Update();
        }

        private void LabelUpdate(string labelName, string labelText)
        {
            var control = this.Controls.Find(labelName, true).FirstOrDefault();
            if (control != null)
            {
                control.Text = labelText;
                control.Update();
            }
            else
            {
            }
        }

        private void HideSub()
        {
            if (panelSideSubBV.Visible == true)
                panelSideSubBV.Visible = false;
            if (panelSideSub2.Visible == true)
                panelSideSub2.Visible = false;

        }
        private void ShowSub(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSub();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //    ShowSub(panelSideSub1);
        }

        private void buttonTools_Click(object sender, EventArgs e)
        {
            ShowSub(panelSideSub2);
        }

        private void buttonResults_Click(object sender, EventArgs e)
        {
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened Results", "OK");
            foreach (string path in alpha.ListResultPaths)
                Process.Start(path);
        }

        private void buttonOpenODs_Click(object sender, EventArgs e)
        {

            string input = inputBox2.Text;
            int index = comboBoxODs.SelectedIndex;
            string action = "Opened OrderData - " + input + " with index: " + index.ToString();
            alpha.InsertAction(alpha.userName, alpha.accessLevel, action, "OK");
            OpenODs(input, index);
        }


        private void OpenODs(string input, int index)
        {
            if (index == 0)
            {
                foreach (string server in alpha.ListServers)
                {
                    string path = alpha.DictServerIPs[server] + @"comm_uut\ORDERDATA\" + input;
                    Process.Start(path);
                }
            }
            else
            {
                string path = alpha.DictServerIPs[alpha.ListServers[index - 1]] + @"comm_uut\ORDERDATA\" + input; //haha
                Process.Start(path);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HideSub();
        }

        public void buttonSearch_Click(object sender, EventArgs e)
        {
            SearchAsync();
        }

        private async void SearchAsync()
        {
            if (inputBox2.TextLength == 10)
            {
                stopWatch.Start();
                string input = inputBox2.Text;
                StatusUpdate("Collecting " + input + " unit log data, please wait..");
                var unitDatabaseData = new Async_Main_Search_DatabaseData();

                //first
                var task_MacAdressHistory = alpha.GetTDS_MAC_IP(input);
                //--- SNO info
                var task_unitOrderNumber = alpha.GetOrder(input);
                var task_unitPoNumber = alpha.GetPO(input);
                var task_unitDnNumber = alpha.GetDn(input);
                var task_orderUnitCount = alpha.GetOrderUnitCount(input);
                var task_orderTestedUnits = alpha.GetOrderUnitTested(input);
                var task_unitFamily = alpha.GetFamily(input);
                var task_unitTimeCreated = alpha.GetTimeCreated(input);
                var task_unitTimeUpdated = alpha.GetTimeUpdated(input);
                var task_unitLocation = alpha.GetLocation(input);
                var task_unitLastStation = alpha.GetStation(input);
                var task_unitM4U_code = alpha.GetM4U(input);
                var task_NextStationName = alpha.GetNextStationName(input);
                //var task_dtInternalDisks = alpha.GetDataTable_InternalDisks(input);
                //----------------- Test Status
                var task_NextWC = alpha.GetNextStation(input);
                var task_LastWC = alpha.GetLastStation(input);
                var task_TDS_Progress = alpha.GetTDSDataTable_TDS_Progress(input);
                var task_dbSnoInfo = alpha.GetTDSDataTable_SNOInfo(input);

                searchCount = searchCount + 1;
                if (searchCount % 5 == 0)
                {
                    string version = alpha.GetReqVersion();
                    if (alpha.appVersion != version)
                    {
                        labelUpdateRequestVar.Text = version;
                        panelRequestUpdate.Visible = true;
                    }
                }
                StatusUpdate("Clearing UI..");
                ClearAuto();
                StatusUpdate("Linehead health check..");
                LineHealthCheck();
                // could be changed
                List<string> AreaList = ReadTrackBar();
                //readSN(unitDatabaseData);
                List<string> ListWorkingServers = Get_WrokingLineaheadsList();
                alpha.InsertSearch(alpha.userName, input);
                inputBox2.ReadOnly = true;
                inputBox2.Update();

                buttonClear.BackColor = Color.FromArgb(255, 255, 255);
                StatusUpdate("Collecting " + input + " unit log data, please wait..");
                foreach (var area in AreaList)
                {
                    foreach (var server in ListWorkingServers)
                    {
                        string pathToArea = alpha.DictServerIPs[server] + alpha.DictAreaPaths[area];
                        string pathToFolder = alpha.DictServerIPs[server] + alpha.DictAreaPaths[area] + input;
                        if (Directory.Exists(pathToFolder) == true)
                        {
                            alpha.ListResultPaths.Add(pathToFolder);
                            foreach (var logType in alpha.ListLogTypes)
                            {
                                string pathToFile = pathToFolder + "\\" + input + alpha.DictLogTypeSuffix[logType];
                                if (File.Exists(pathToFile) == true)
                                {
                                    fileFound(area, server, logType, pathToFile);
                                    if (area == "Test" && ((logType == "60S" || logType == "60F")))
                                    {
                                        if (!possibleReMFI.Contains(server))
                                        {
                                            possibleReMFI.Add(server);
                                        }
                                    }
                                }
                                else
                                {
                                    fileNotFound(area, server, logType);
                                    string passLabel = "label" + server + area + "Pass";
                                    var control = this.Controls.Find(passLabel, true).FirstOrDefault();
                                    if (control != null)
                                    {
                                        if (control.Text != Index_Clear)
                                        {
                                            if (area == "Test" && (logType == "Adler" || logType == "OA3") && checkBoxSaver.Checked == true)
                                            {
                                                string pathLogBackupFolder = alpha.DictServerIPs[server] + @"comm_uut\ADLOA3";
                                                string pathLogBackupFile = pathLogBackupFolder + @"\" + input + alpha.DictLogTypeSuffix[logType];

                                                if (File.Exists(pathLogBackupFile))
                                                {
                                                    string BackupPicName = "pictureBoxBackup" + server + logType;
                                                    var control2 = this.Controls.Find(BackupPicName, true).FirstOrDefault();
                                                    if (control2 != null)
                                                    {
                                                        control2.Visible = true;
                                                        control2.Update();
                                                    }
                                                    else
                                                    {
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                    }

                                    string passLabelMFI = "label" + server + area + "60P";
                                    var controlMFI = this.Controls.Find(passLabelMFI, true).FirstOrDefault();
                                    if (controlMFI != null)
                                    {
                                        if (controlMFI.Text != Index_Clear && controlMFI.Text != Index_NotFound)
                                        {
                                            if (area == "Test" && logType == "60T" && checkBoxSaver.Checked == true)
                                            {
                                                string BackupPicName = "pictureBoxBackup" + server + logType;
                                                var controlMFI2 = this.Controls.Find(BackupPicName, true).FirstOrDefault();
                                                if (controlMFI2 != null)
                                                {
                                                    controlMFI2.Visible = true;
                                                    controlMFI2.Update();
                                                }
                                                else
                                                {
                                                }
                                             
                                            }

                                        }
                                    }
                                    else
                                    {
                                    }

                                }
                            }
                        }

                        else
                        {
                            folderNotFound(area, server, alpha.ListLogTypes);
                        }
                    }
                }
                if (possibleReMFI.Count != 0 && (alpha.accessLevel == "Engineer" || alpha.accessLevel == "Technician"))
                {
                    buttonReMFI.Enabled = true;
                    buttonReMFI.BackColor = Color.FromArgb(240, 235, 255);
                }
                unitDatabaseData.MacAdressHistory = await task_MacAdressHistory;
                var task_MacAdressCurrent = alpha.GetTDS_MAC_Main(unitDatabaseData.MacAdressHistory);
                StatusUpdate("Collecting OrderData..");
                OrderDataSearch();
                StatusUpdate("Awaiting data from database..");
                unitDatabaseData.unitOrderNumber = await task_unitOrderNumber;
                unitDatabaseData.unitPoNumber = await task_unitPoNumber;
                unitDatabaseData.unitDnNumber = await task_unitDnNumber;
                unitDatabaseData.orderUnitCount = await task_orderUnitCount;
                unitDatabaseData.orderTestedUnits = await task_orderTestedUnits;
                unitDatabaseData.unitFamily = await task_unitFamily;
                unitDatabaseData.unitTimeCreated = await task_unitTimeCreated;
                unitDatabaseData.unitTimeUpdated = await task_unitTimeUpdated;
                unitDatabaseData.unitLocation = await task_unitLocation;
                unitDatabaseData.unitLastStation = await task_unitLastStation;
                if (unitDatabaseData.unitLastStation.Contains(" "))
                {
                    unitDatabaseData.unitLastStation = unitDatabaseData.unitLastStation.Trim(' ');
                }
                unitDatabaseData.unitM4U_code = await task_unitM4U_code;
                unitDatabaseData.unitNextStationName = await task_NextStationName;
                if (unitDatabaseData.unitNextStationName.Contains(" "))
                {
                    unitDatabaseData.unitNextStationName = unitDatabaseData.unitNextStationName.Trim(' ');
                }
                //unitDatabaseData.dtInternalDisks = await task_dtInternalDisks;

                //-----------------
                unitDatabaseData.NextWC = await task_NextWC;
                unitDatabaseData.LastWC = await task_LastWC;
                unitDatabaseData.TDS_Progress = await task_TDS_Progress;
                unitDatabaseData.dbSnoInfo = await task_dbSnoInfo;

                unitDatabaseData.MacAdressCurrent = await task_MacAdressCurrent;

                StatusUpdate("Collecting FIS and Test data..");
                readSN(unitDatabaseData);
                
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00} seconds.",ts.Seconds, ts.Milliseconds / 10);
                StatusUpdate("Job done. Collection time: " + elapsedTime);
                stopWatch.Reset();
            }
            else
            {
                StatusUpdate("Incorrect Serial Number lenght!");
            }
        }

        private void folderNotFound(string area, string server, List<string> types)
        {
            foreach (string type in types)
            {
                string labelName = "label" + server + area + type; //creating button name to string
                string labelText = Index_NotFound;
                LabelUpdate(labelName, labelText);
            }
        }

        private void folderNotFound_OD(string server, List<string> types)
        {
            foreach (string type in types)
            {
                string labelName = "label" + server + type; //creating button name to string
                string labelText = Index_NotFound;
                LabelUpdate(labelName, labelText);
            }
        }

        private void fileFound(string area, string server, string type, string filePath)
        {
            string labelName = "label" + server + area + type;

            var control = this.Controls.Find(labelName, true).FirstOrDefault();
            if (control != null)
            {
                if (File.Exists(filePath))
                {
                    //DateTime creationTime = File.GetCreationTime(filePath); // Read oritinal file creation time.
                    DateTime modification = File.GetLastWriteTime(filePath); // Read oritinal file creation time.
                    string creationTimeString = modification.ToString("dd/MM/yyyy HH:mm:ss"); // Display creation time.
                    control.Text = creationTimeString;
                    //control.Click += delegate { Process.Start(filePath); };
                    control.Update();
                }
            }
            else { }
        }

        private void fileFound_OD(string server, string type, string filePath)
        {
            string labelName = "label" + server + type;

            var control = this.Controls.Find(labelName, true).FirstOrDefault();
            if (control != null)
            {
                if (File.Exists(filePath))
                {
                    //DateTime creationTime = File.GetCreationTime(filePath); // Read oritinal file creation time.
                    DateTime modification = File.GetLastWriteTime(filePath); // Read oritinal file creation time.
                    string creationTimeString = modification.ToString("dd/MM/yyyy HH:mm:ss"); // Display creation time.
                    control.Text = creationTimeString;
                    //control.Click += delegate { Process.Start(filePath); };
                    control.Update();
                }
            }
            else { }
        }

        private void fileNotFound(string area, string server, string type)
        {
            string labelName = "label" + server + area + type;
            string labelText = Index_NotFound;
            LabelUpdate(labelName, labelText);

        }

        private void fileNotFound_OD(string server, string type)
        {
            string labelName = "label" + server + type;
            string labelText = Index_NotFound;
            LabelUpdate(labelName, labelText);

        }

        private void OrderDataSearch()
        {
            List<string> ListWorkingServers = Get_WrokingLineaheadsList();
            string input = inputBox2.Text;

            foreach (var server in ListWorkingServers)
            {
                foreach (var ODType in alpha.ListTypeOD)
                {
                    string pathToFolder = alpha.DictServerIPs[server] + @"comm_uut\ORDERDATA\" + input;
                    if (Directory.Exists(pathToFolder) == true)
                    {
                        if (ODType == "SWL")
                        {

                            string[] files = System.IO.Directory.GetFiles(pathToFolder, "*.SWL");
                            try
                            {
                                if (files[0] != null)
                                {
                                    string pathToSWL = files[0];
                                    fileFound_OD(server, ODType, pathToSWL);
                                }
                                else
                                    fileNotFound_OD(server, ODType);
                            }
                            catch
                            {
                                fileNotFound_OD(server, ODType);
                            }
                        }
                        else
                        {
                            string pathToODfile = alpha.DictServerIPs[server] + @"comm_uut\ORDERDATA\" + input + @"\" + alpha.DictODTypeWhole[ODType];
                            if (File.Exists(pathToODfile))
                            {
                                fileFound_OD(server, ODType, pathToODfile);
                            }
                            else
                                fileNotFound_OD(server, ODType);
                        }
                    }
                    else
                    {
                        folderNotFound_OD(server, alpha.ListTypeOD);
                    }
                }
            }
        }

        private List<string> ReadTrackBar()
        {
            List<string> AreaList = new List<string>();
            int valueTrackBar = trackBar1.Value;
            if (valueTrackBar == 0)
                AreaList.Add("Test");
            else if (valueTrackBar == 1)
            {
                AreaList.Add("Test");
                AreaList.Add("Logs");
            }
            else if (valueTrackBar == 2)
            {
                AreaList.Add("Test");
                AreaList.Add("Logs");
                AreaList.Add("Arch");
            }
            else { }
            return AreaList;
        }
        private void readSN(Async_Main_Search_DatabaseData unitDatabaseData)
        {
            string SN = inputBox2.Text;
            _history.Add(SN);
            labelHistory1.Text = _history[_history.Count - 1];
            labelHistory2.Text = _history[_history.Count - 2];
            labelHistory3.Text = _history[_history.Count - 3];
            labelHistory4.Text = _history[_history.Count - 4];
            labelHistory5.Text = _history[_history.Count - 5];
            labelHistory6.Text = _history[_history.Count - 6];
            labelHistory7.Text = _history[_history.Count - 7];
            labelHistory8.Text = _history[_history.Count - 8];
            labelHistory9.Text = _history[_history.Count - 9];
            if (checkBoxInfo.Checked == true)
            {
                //UpdateTextBoxMenu(SN);
                UpdateTextBoxMenu2(unitDatabaseData);
            }
            if (checkBoxTDS.Checked == true)
            {
                TestInfo(SN, unitDatabaseData);

            }

            string firstFour = SN.Substring(0, 4);
            if (alpha.DictSnModel.ContainsKey(firstFour))
            {
                string model = alpha.DictSnModel[firstFour];
                textBoxModel.Text = model;
                textBoxModel.Update();
            }
            else
            {
                textBoxModel.Text = "Unknown Model";
            }

            if (alpha.DictSnMB.ContainsKey(firstFour))
            {
                string MBSN = alpha.DictSnMB[firstFour];
                textBoxMBSN.Text = MBSN;
                textBoxMBSN.Update();
            }
            else
            {
                textBoxMBSN.Text = "Unknown SN";
            }

            bool underP4 = false;
            string progress = textBoxTDS_Progress.Text;
            if (progress.Contains("Phase 0") || progress.Contains("Phase 1") || progress.Contains("Phase 2") || progress.Contains("Phase 3"))
            {
                underP4 = true;
            }

            if (textBoxTDS_IP.Text != "" && textBoxLoc.Text != "" && underP4 == true && alpha.accessLevel == "Engineer")
            {
                buttonTDS_Restart.Enabled = true;
                buttonTDS_Restart.BackColor = System.Drawing.Color.FromArgb(255, 255, 170);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            buttonClear.BackColor = Color.FromArgb(232, 232, 232);
            ClearAuto();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void labelL1IP_Click(object sender, EventArgs e)
        {
            string filePath = @"\\10.13.82.2\comm_uut";
            if (Directory.Exists(filePath))
            {
                Process.Start(filePath);
            }
            else
            {
                string status = "Cannot access Linehead 1";
                StatusUpdate(status);
            }
        }

        private void labelL2IP_Click(object sender, EventArgs e)
        {
            string filePath = @"\\10.13.82.3\comm_uut";
            if (Directory.Exists(filePath))
            {
                Process.Start(filePath);
            }
            else
            {
                string status = "Cannot access Linehead 2";
                StatusUpdate(status);
            }
        }

        private void labelL3IP_Click(object sender, EventArgs e)
        {
            string filePath = @"\\10.13.82.4\comm_uut";
            if (Directory.Exists(filePath))
            {
                Process.Start(filePath);
            }
            else
            {
                string status = "Cannot access Linehead 3";
                StatusUpdate(status);
            }
        }

        private void labelL8IP_Click(object sender, EventArgs e)
        {
            string filePath = @"\\10.13.82.8\comm_uut";
            if (Directory.Exists(filePath))
            {
                Process.Start(filePath);
            }
            else
            {
                string status = "Cannot access Linehead 8";
                StatusUpdate(status);
            }
        }

        private void labelLABIP_Click(object sender, EventArgs e)
        {
            string filePath = @"\\10.13.82.200\comm_uut";
            if (Directory.Exists(filePath))
            {
                Process.Start(filePath);
            }
            else
            {
                string status = "Cannot access Linehead 200";
                StatusUpdate(status);
            }
        }

        private void labelL10IP_Click(object sender, EventArgs e)
        {
            string filePath = @"\\10.13.82.10\comm_uut";
            if (Directory.Exists(filePath))
            {
                Process.Start(filePath);
            }
            else
            {
                string status = "Cannot access Lineahead 10";
                StatusUpdate(status);
            }
        }

        private void LabelClick(string labelName, string filePath)
        {
            var control = this.Controls.Find(labelName, true).FirstOrDefault();
            if (control != null)
            {
                if (control.Text != Index_Clear && control.Text != Index_NotFound)

                {
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            Process.Start(filePath);
                        }
                        catch
                        {
                            Process.Start("notepad.exe", filePath);
                        }
                    }
                    else
                    {
                        string status = "Cannot access log file. It might have moved to Logs, try Search again.";
                        StatusUpdate(status);
                    }
                }
                else { }
            }
            else
            {
                string status = labelName + " label is null, contact tool developer";
                StatusUpdate(status);
            }
        }

        private void labelStatusLog_Click(object sender, EventArgs e)
        {
            string serverPath;
            string folderPath;
            string suffix;
            string input = inputBox2.Text;
            var label = (Label)sender;
            string labelName = label.Name;

            //serverPath
            if (labelName.Contains("L10"))
            {
                serverPath = @"\\10.13.82.10\comm_uut\";
            }
            else if (labelName.Contains("L2"))
            {
                serverPath = @"\\10.13.82.3\comm_uut\";
            }
            else if (labelName.Contains("L3"))
            {
                serverPath = @"\\10.13.82.4\comm_uut\";
            }
            else if (labelName.Contains("LAB"))
            {
                serverPath = @"\\10.13.82.200\comm_uut\";
            }
            else if (labelName.Contains("L8"))
            {
                serverPath = @"\\10.13.82.8\comm_uut\";
            }
            // odsunuto pod L10 (L10 contains L1)
            else if (labelName.Contains("L1"))
            {
                serverPath = @"\\10.13.82.2\comm_uut\";
            }
            else
            {
                throw new Exception("New server not impemented, inform the developer");
            }

            //folderPath
            if (labelName.Contains("Test"))
            {
                folderPath = @"Testresults\";
            }
            else if (labelName.Contains("Logs"))
            {
                folderPath = @"Testresults\LOG\";
            }
            else if (labelName.Contains("Arch"))
            {
                folderPath = @"Archivefolder_orderdata\";
            }
            else
            {
                throw new Exception("New server folder path not impemented, inform the developer");
            }

            //suffix
            if (labelName.Contains("Start"))
            {
                suffix = ".55S";
            }
            else if (labelName.Contains("Fail"))
            {
                suffix = ".55F";
            }
            else if (labelName.Contains("Pass"))
            {
                suffix = ".55P";
            }
            else if (labelName.Contains("OA3"))
            {
                suffix = "-OA30.xml";
            }
            else if (labelName.Contains("Adler"))
            {
                suffix = ".ADL";
            }
            else if (labelName.Contains("TDS"))
            {
                suffix = ".TDS";
            }
            else if (labelName.Contains("60S"))
            {
                suffix = ".60S";
            }
            else if (labelName.Contains("60F"))
            {
                suffix = ".60F";
            }
            else if (labelName.Contains("60P"))
            {
                suffix = ".60P";
            }
            else if (labelName.Contains("60T"))
            {
                suffix = ".60T";
            }
            else
            {
                throw new Exception("New log type not impemented, inform the developer");
            }

            //filePath Generation
            string filePath = serverPath + folderPath + input + @"\" + input + suffix;

            LabelClick(labelName, filePath);
        }

        

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void SaveIt(string input, string file, string pathMissingLog, string pathToFolder, string pathLogBackupFile, object sender, EventArgs e)
        {
            //if (alpha.accessLevel == "Engineer" || alpha.ListSaverTechs.Contains(alpha.userName))
            if (alpha.accessLevel == "Engineer" || alpha.accessLevel == "Technician")
            {
                if (alpha.fistSaveWarning == true)
                {
                    msgBx();
                    alpha.fistSaveWarning = false;
                }
                else { }

                if (File.Exists(pathMissingLog) == false && Directory.Exists(pathToFolder) == true)
                {
                    File.Copy(pathLogBackupFile, pathMissingLog);
                    alpha.InsertLogRecovery(alpha.userName, alpha.accessLevel, input, file);
                    //string timeNow = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    //string record = timeNow + @" / " + input + @" / " + file + @" / " + labelUser.Text;
                    //string recordFilePath = @"\\10.13.82.200\comm_uut\TestTrack\SavedLogsRecord.txt";
                    //File.AppendAllText(recordFilePath, record + Environment.NewLine);
                    buttonSearch_Click(sender, e);
                }
                else
                {
                    buttonSearch_Click(sender, e);
                }
            }
            else
            {
                string actionn = "Tried to recover " + file;
                alpha.InsertAction(alpha.userName, alpha.accessLevel, actionn, "Denied");
                msgBxSavePerm();
                //string timeNow = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                //string record = "Denied to save log: " + timeNow + @" / " + input + @" / " + file + @" / " + alpha.userName;
                //string recordFilePath = @"\\10.13.82.200\comm_uut\TestTrack\Denied.txt";
                //File.AppendAllText(recordFilePath, record + Environment.NewLine);
            }

        }

        private void msgBx()
        {
            {
                string message = "Please make sure that unit power button is working properly.";
                string caption = "Warning";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, icon);
            }
        }

        //private void msgBx_OpenLogIssue()
        //{
        //    {
        //        string message = "Problem appears when opening log file:\n- File might moved to different folder. Try Seach again.";
        //        string caption = "Warning";
        //        MessageBoxButtons buttons = MessageBoxButtons.OK;
        //        MessageBoxIcon icon = MessageBoxIcon.Warning;
        //        DialogResult result;
        //        result = MessageBox.Show(message, caption, buttons, icon);
        //    }
        //}

        private void msgBx_Restart()
        {
            {
                string message = "Remote TDS restart will works only if the unit is booted to Windows and have all TDS data loaded.";
                string caption = "Warning";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, icon);
            }
        }

        private void msgBxLVL()
        {
            {
                string message = "You have no permission to enter.";
                string caption = "Warning";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, icon);
            }
        }
        private void msgBxSavePerm()
        {
            {
                string message = "Pravomoce pro doplnění logů mají pouze TEs a TTs";
                string caption = "Warning";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, icon);
            }
        }

        private void pictureBoxSaveLogBackup_Click(object sender, EventArgs e)
        {
            //if (alpha.accessLevel == "Engineer" || alpha.ListSaverTechs.Contains(alpha.userName))
            if (alpha.accessLevel == "Engineer" || alpha.accessLevel == "Technician")
            {
                if (alpha.fistSaveWarning == true)
                {
                    msgBx();
                    alpha.fistSaveWarning = false;
                }
                else { }

                string serverPath;
                string pathLogBackupFolder;
                string input = inputBox2.Text;
                var pictureBox = (PictureBox)sender;
                string pictureBoxName = pictureBox.Name;

                //serverPath
                if (pictureBoxName.Contains("L10"))
                {
                    serverPath = @"\\10.13.82.10\comm_uut\Testresults\";
                    pathLogBackupFolder = @"\\10.13.82.10\comm_uut\ADLOA3\";
                }
                else if (pictureBoxName.Contains("L2"))
                {
                    serverPath = @"\\10.13.82.3\comm_uut\Testresults\";
                    pathLogBackupFolder = @"\\10.13.82.3\comm_uut\ADLOA3\";
                }
                else if (pictureBoxName.Contains("L3"))
                {
                    serverPath = @"\\10.13.82.4\comm_uut\Testresults\";
                    pathLogBackupFolder = @"\\10.13.82.4\comm_uut\ADLOA3\";
                }
                else if (pictureBoxName.Contains("LAB"))
                {
                    serverPath = @"\\10.13.82.200\comm_uut\Testresults\";
                    pathLogBackupFolder = @"\\10.13.82.200\comm_uut\ADLOA3\";
                }
                else if (pictureBoxName.Contains("L8"))
                {
                    serverPath = @"\\10.13.82.8\comm_uut\Testresults\";
                    pathLogBackupFolder = @"\\10.13.82.8\comm_uut\ADLOA3\";
                }
                // odsunuto pod L10 (L10 contains L1)
                else if (pictureBoxName.Contains("L1"))
                {
                    serverPath = @"\\10.13.82.2\comm_uut\Testresults\";
                    pathLogBackupFolder = @"\\10.13.82.2\comm_uut\ADLOA3\";
                }
                else
                {
                    throw new Exception("New server not impemented, inform the developer");
                }

                if (pictureBoxName.Contains("60T"))
                {
                    string file = input + ".60T";
                    string pathToLog60T = serverPath + input + @"\" + file;

                    if (!File.Exists(pathToLog60T))
                    {
                        string logContent = "Log created manually by " + alpha.userName + " at: " + DateTime.Now.ToString("yyyy'-'MM'-'dd'/'HH':'mm':'ss");
                        File.WriteAllText(pathToLog60T, logContent);
                        string actionn = "Recovered LOG: " + file;
                        alpha.InsertAction(alpha.userName, alpha.accessLevel, actionn, "OK");
                        alpha.InsertLogRecovery(alpha.userName, alpha.accessLevel, input, file);
                        buttonSearch_Click(sender, e);
                    }
                }
                else if (pictureBoxName.Contains("Adler"))
                {
                    string file = input + ".ADL";
                    string pathToFolder = serverPath + input;
                    string pathMissingLog = serverPath + input + @"\" + file;
                    string pathLogBackupFile = pathLogBackupFolder + file;

                    if (File.Exists(pathMissingLog) == false && Directory.Exists(pathToFolder) == true)
                    {
                        File.Copy(pathLogBackupFile, pathMissingLog);
                        string actionn = "Recovered LOG: " + file;
                        alpha.InsertAction(alpha.userName, alpha.accessLevel, actionn, "OK");
                        alpha.InsertLogRecovery(alpha.userName, alpha.accessLevel, input, file);
                        buttonSearch_Click(sender, e);
                    }
                    else
                    {
                        buttonSearch_Click(sender, e);
                    }
                }

                else if (pictureBoxName.Contains("OA3"))
                {
                    string file = input + "-OA30.xml";
                    string pathToFolder = serverPath + input;
                    string pathMissingLog = serverPath + input + @"\" + file;
                    string pathLogBackupFile = pathLogBackupFolder + file;

                    if (File.Exists(pathMissingLog) == false && Directory.Exists(pathToFolder) == true)
                    {
                        File.Copy(pathLogBackupFile, pathMissingLog);
                        string actionn = "Recovered LOG: " + file;
                        alpha.InsertAction(alpha.userName, alpha.accessLevel, actionn, "OK");
                        alpha.InsertLogRecovery(alpha.userName, alpha.accessLevel, input, file);
                        buttonSearch_Click(sender, e);
                    }
                    else
                    {
                        buttonSearch_Click(sender, e);
                    }
                }

                else
                {
                    throw new Exception("New type of backup not impemented, inform the developer");
                }
            }
            else
            {
                string actionn = "Tried to recover log file for SN: " + inputBox2.Text;
                alpha.InsertAction(alpha.userName, alpha.accessLevel, actionn, "Denied");
                msgBxSavePerm();
                //string timeNow = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                //string record = "Denied to save log: " + timeNow + @" / " + input + @" / " + file + @" / " + alpha.userName;
                //string recordFilePath = @"\\10.13.82.200\comm_uut\TestTrack\Denied.txt";
                //File.AppendAllText(recordFilePath, record + Environment.NewLine);
            }
        }

        //private void pictureBackupL1OA3_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + "-OA30.xml";
        //    string pathMissingLog = @"\\10.13.82.2\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.2\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.2\comm_uut\ADLOA3\" + input + "-OA30.xml";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupL1Adler_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + ".ADL";
        //    string pathMissingLog = @"\\10.13.82.2\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.2\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.2\comm_uut\ADLOA3\" + input + ".ADL";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupL2OA3_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + "-OA30.xml";
        //    string pathMissingLog = @"\\10.13.82.3\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.3\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.3\comm_uut\ADLOA3\" + input + "-OA30.xml";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupL2Adler_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + ".ADL";
        //    string pathMissingLog = @"\\10.13.82.3\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.3\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.3\comm_uut\ADLOA3\" + input + ".ADL";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupL3OA3_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + "-OA30.xml";
        //    string pathMissingLog = @"\\10.13.82.4\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.4\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.4\comm_uut\ADLOA3\" + input + "-OA30.xml";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupL3Adler_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + ".ADL";
        //    string pathMissingLog = @"\\10.13.82.4\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.4\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.4\comm_uut\ADLOA3\" + input + ".ADL";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupL8OA3_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + "-OA30.xml";
        //    string pathMissingLog = @"\\10.13.82.8\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.8\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.8\comm_uut\ADLOA3\" + input + "-OA30.xml";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupL8Adler_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + ".ADL";
        //    string pathMissingLog = @"\\10.13.82.8\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.8\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.8\comm_uut\ADLOA3\" + input + ".ADL";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupLABOA3_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + "-OA30.xml";
        //    string pathMissingLog = @"\\10.13.82.200\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.200\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.200\comm_uut\ADLOA3\" + input + "-OA30.xml";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupLABAdler_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + ".ADL";
        //    string pathMissingLog = @"\\10.13.82.200\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.200\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.200\comm_uut\ADLOA3\" + input + ".ADL";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupL10OA3_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + "-OA30.xml";
        //    string pathMissingLog = @"\\10.13.82.200\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.200\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.200\comm_uut\ADLOA3\" + input + "-OA30.xml";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //private void pictureBackupL10Adler_Click(object sender, EventArgs e)
        //{
        //    string input = inputBox2.Text;
        //    string file = input + ".ADL";
        //    string pathMissingLog = @"\\10.13.82.10\comm_uut\Testresults\" + input + @"\" + file;
        //    string pathToFolder = @"\\10.13.82.10\comm_uut\Testresults\" + input;
        //    string pathLogBackupFile = @"\\10.13.82.10\comm_uut\ADLOA3\" + input + ".ADL";
        //    SaveIt(input, file, pathMissingLog, pathToFolder, pathLogBackupFile, sender, e);
        //}

        //ORDERDATA label open

        private void labelOrderData_Click(object sender, EventArgs e)
        {
            string serverPath;
            string orderDataType;
            string input = inputBox2.Text;
            var label = (Label)sender;
            string labelName = label.Name;

            //serverPath
            if (labelName.Contains("L10"))
            {
                serverPath = @"\\10.13.82.10\comm_uut\ORDERDATA\";
            }
            else if (labelName.Contains("L2"))
            {
                serverPath = @"\\10.13.82.3\comm_uut\ORDERDATA\";
            }
            else if (labelName.Contains("L3"))
            {
                serverPath = @"\\10.13.82.4\comm_uut\ORDERDATA\";
            }
            else if (labelName.Contains("LAB"))
            {
                serverPath = @"\\10.13.82.200\comm_uut\ORDERDATA\";
            }
            else if (labelName.Contains("L8"))
            {
                serverPath = @"\\10.13.82.8\comm_uut\ORDERDATA\";
            }
            // odsunuto pod L10 (L10 contains L1)
            else if (labelName.Contains("L1"))
            {
                serverPath = @"\\10.13.82.2\comm_uut\ORDERDATA\";
            }
            else
            {
                throw new Exception("New server not impemented, inform the developer");
            }

            string pathToFolder = serverPath + input;

            //OD_name
            if (labelName.Contains("UUT"))
            {
                orderDataType = "UUTScan.scd";
            }
            else if (labelName.Contains("PDT"))
            {
                orderDataType = "PDTORDER.PDT";
            }
            else if (labelName.Contains("SWL"))
            {
                string[] files = System.IO.Directory.GetFiles(pathToFolder, "*.SWL");
                orderDataType = files[0];
            }
            else if (labelName.Contains("OA30"))
            {
                orderDataType = "OA30_key.xml";
            }
            else if (labelName.Contains("Head"))
            {
                orderDataType = "Header.scd";
            }
            else if (labelName.Contains("Json"))
            {
                orderDataType = "HDDSerNr.json";
            }
            else if (labelName.Contains("MFIs"))
            {
                orderDataType = "MFIShipmentSetting.cmd";
            }
            else
            {
                throw new Exception("New log type not impemented, inform the developer");
            }

            //string[] files = System.IO.Directory.GetFiles(pathToFolder, "*.SWL");
            //string filePath = files[0];
            //filePath Generation
            string filePath;

            if (labelName.Contains("SWL"))
            {
                filePath = orderDataType;
                //System.IO.Directory.GetFiles vraci celou path
            }
            else
            {
                filePath = pathToFolder + @"\" + orderDataType;
            }

            LabelClick(labelName, filePath);
        }

        private void pictureBoxRemote_Click(object sender, EventArgs e)
        {
            string pathToRemote = @"C:\Windows\system32\mstsc.exe";
            if (File.Exists(pathToRemote) == true)
            {
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened remote", "OK");
                Process.Start(pathToRemote);
                if (alpha.accessLevel == "Engineer")
                {
                    Form_PasswordTable formPWTable = new Form_PasswordTable(alpha);
                    formPWTable.Show();
                }
            }
            else
            {
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened remote", "Fail");
                string status = "Cant open remote control tool.";
                StatusUpdate(status);
            }
        }

        void pictureBoxRemote_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxRemote.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.remote2));
        }

        void pictureBoxRemote_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxRemote.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.remote2_in));
        }

        private void buttonData_Click(object sender, EventArgs e)
        {
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened Logs Collector tool", "OK");
            Form_Collector form4 = new Form_Collector(alpha);
            form4.Show();
        }

        private void pictureBoxFIS_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore", "http://10.13.54.204/CtoNew/login.aspx");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened FIS", "OK");
        }

        void pictureBoxFIS_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxFIS.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.FIS));
        }

        void pictureBoxFIS_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxFIS.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.FIS_in));
        }

        private void pictureBoxExcel_Click(object sender, EventArgs e)
        {
            if (alpha.accessLevel == "Engineer")
            {
                System.Diagnostics.Process.Start("https://inventeccorp-my.sharepoint.com/:x:/r/personal/jisa_radek_inventec_com/_layouts/15/guestaccess.aspx?e=4%3A66fPDD&at=9&share=EfjIkp8nAadGut7-7tE5-9ABVPFXBwYSjJQC4to7BDfT4g");
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Tried to access Excel Sharepoint", "OK");
            }
            else
            {
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Tried to access Excel Sharepoint", "Denied");
                msgBxLVL();
            }
        }

        void pictureBoxExcel_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxExcel.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.excel));
        }

        void pictureBoxExcel_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxExcel.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.excel_in));
        }

        private void pictureBoxSOP_Click(object sender, EventArgs e)
        {
            string pathToSOP = @"\\10.13.3.15\Iso-document\ISO_documents\Level_3\E00_Engineering\WI-Product\CZ02-C1\TE";
            if (Directory.Exists(pathToSOP) == true)
            {
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened SOP folder", "OK");
                Process.Start(pathToSOP);
            }
            else
            {
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened SOP folder", "Fail");
                string status = "Cant open remote control tool.";
                StatusUpdate(status);
            }
        }

        void pictureBoxSOP_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxSOP.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.SOP));
        }

        void pictureBoxSOP_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxSOP.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.SOP_in));
        }

        private void pictureBoxBIOS_Click(object sender, EventArgs e)
        {
            string loginUserNameB = null;
            if (alpha.accessLevel == "Guest")
                loginUserNameB = "SwapTech";
            else if (alpha.accessLevel == "Technician")
                loginUserNameB = "TestTech";
            else if (alpha.accessLevel == "Engineer")
                loginUserNameB = "Administrator";
            else
                loginUserNameB = "Error";

            if (loginUserNameB != "Error" && loginUserNameB != "Guest")
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                string pwd = alpha.GetLineaheadPassword("10.13.82.201", loginUserNameB);
                string strCmdText;
                strCmdText = @"/C C:/Windows/System32/net.exe use \\10.13.82.201 /persistent:no /user:" + loginUserNameB + " \"" + pwd + "\"";
                startInfo.Arguments = strCmdText;
                process.StartInfo = startInfo;
                process.Start();
                Wait(1000);
                Process.Start(@"\\10.13.82.201\TEFolder\Bios Files");
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened BIOS files", "OK");
            }
            else
            {
                msgBxLVL();
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened BIOS files", "Denied");
            }
        }

        void pictureBoxBIOS_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxBIOS.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.BIOS1));
        }

        void pictureBoxBIOS_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxBIOS.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.BIOS1_in));
        }

        private void Remote(string ip)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            string strCmdText;
            strCmdText = @"/C C:\Windows\system32\mstsc.exe /v:10.13.82.4:60824";
            startInfo.Arguments = strCmdText;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void pictureBox201_Click(object sender, EventArgs e)
        {
            if (alpha.accessLevel == "Engineer")
            {
                string loginUserNameB = "Administrator";
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                string pwd = alpha.GetLineaheadPassword("10.13.82.201", loginUserNameB);
                string strCmdText;
                strCmdText = @"/C C:/Windows/System32/net.exe use \\10.13.82.201 /persistent:no /user:" + loginUserNameB + " \"" + pwd + "\"";
                startInfo.Arguments = strCmdText;
                process.StartInfo = startInfo;
                process.Start();
                Wait(1000);
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened .201", "OK");
                Process.Start(@"\\10.13.82.201");
            }
            else
            {
                msgBxLVL();
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened .201", "Denied");
            }
        }

        void pictureBox201_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox201.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources._201_b));
        }

        void pictureBox201_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox201.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources._201_b_in));
        }

        public void Wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void TestInfo(string SN, Async_Main_Search_DatabaseData unitDatabaseData)
        {
            //string nextWC = alpha.GetNextStation(SN);
            //string lastWC = alpha.GetLastStation(SN);
            //string lastWC = alpha.GetLastStation(SN);
            int nextWC_NUM;
            try
            {
                nextWC_NUM = Int32.Parse(unitDatabaseData.NextWC);
            }
            catch
            {
                nextWC_NUM = 999;
            }

            if (unitDatabaseData.NextWC == "55" || unitDatabaseData.NextWC == "53" || (unitDatabaseData.NextWC == "49" & unitDatabaseData.LastWC == "FF"))
            {
                //string MAC_from_BayView_IPConfig = alpha.GetTDS_MAC_IP(SN);
                if (unitDatabaseData.MacAdressHistory != "" && unitDatabaseData.MacAdressHistory != "Nothing")
                {
                    if (unitDatabaseData.TDS_Progress != null)
                    {
                        buttonTDSHistory.Visible = true;
                    }
                    //string MAC_from_MainView = alpha.GetTDS_MAC_Main(MAC_from_BayView_IPConfig);
                    if (unitDatabaseData.MacAdressCurrent != "" && unitDatabaseData.MacAdressCurrent != "Nothing")
                    {
                        //DataTable dbSnoInfo = alpha.GetTDSDataTable_SNOInfo(SN);
                        if (unitDatabaseData.dbSnoInfo != null && unitDatabaseData.dbSnoInfo.Rows.Count == 1)
                        {

                            string status_TDS = unitDatabaseData.dbSnoInfo.Rows[0]["Status"].ToString();
                            string attribute = unitDatabaseData.dbSnoInfo.Rows[0]["Attribute"].ToString();
                            if (status_TDS == "55S")
                            {
                                textBoxTDS_Status.Text = "Running TDS";
                                textBoxTDS_Status.BackColor = System.Drawing.Color.FromArgb(255, 255, 179);
                            }
                            else if (status_TDS == "55F")
                            {
                                textBoxTDS_Status.Text = "Test failed";
                                textBoxTDS_Status.BackColor = System.Drawing.Color.FromArgb(255, 179, 179);
                                labelTDS_Attribute.Text = attribute;
                            }
                            else if (status_TDS == "55P")
                            {
                                textBoxTDS_Status.Text = "Passed, waiting for shutdown";
                                textBoxTDS_Status.BackColor = System.Drawing.Color.FromArgb(198, 255, 179);
                            }
                            else if (status_TDS == "55O")
                            {
                                textBoxTDS_Status.Text = "Online loading";
                                textBoxTDS_Status.BackColor = System.Drawing.Color.FromArgb(179, 236, 255);
                            }
                            else { };
                            string IP = unitDatabaseData.dbSnoInfo.Rows[0]["IP"].ToString();
                            textBoxTDS_IP.Text = IP;
                            textBoxTDS_MAC.Text = unitDatabaseData.MacAdressCurrent;
                            string phase = unitDatabaseData.dbSnoInfo.Rows[0]["Phase"].ToString();
                            string part = unitDatabaseData.dbSnoInfo.Rows[0]["Part"].ToString();
                            string position = unitDatabaseData.dbSnoInfo.Rows[0]["Position"].ToString();
                            textBoxTDS_Location.Text = position;
                            string progressInfo = "Phase " + phase + ": " + part;
                            textBoxTDS_Progress.Text = progressInfo;
                            progressBarTDS.Value = Int32.Parse(phase);
                        }
                        else
                        {
                            textBoxTDS_Status.Text = "Initialzing TDS";
                            textBoxTDS_MAC.Text = unitDatabaseData.MacAdressCurrent;
                            textBoxTDS_IP.Text = "No IP assigned yet";
                            textBoxTDS_Progress.Text = "Booting/Initialzing..";

                        }
                        //try
                        //{
                        //    if (dbSnoInfo != null)
                        //    {
                        //        textBoxTDS_Status.Text = "In TDS";
                        //        string IP = dbSnoInfo.Rows[0]["IP"].ToString();
                        //        textBoxTDS_IP.Text = IP;
                        //        textBoxTDS_MAC.Text = MAC_from_MainView;
                        //        string phase = dbSnoInfo.Rows[0]["Phase"].ToString();
                        //        string part = dbSnoInfo.Rows[0]["Part"].ToString();
                        //        string progressInfo = "Phase " + phase + ": " + part;
                        //        textBoxTDS_Progress.Text = progressInfo;
                        //        progressBarTDS.Value = Int32.Parse(phase);
                        //    }
                        //    else
                        //    {
                        //        textBoxTDS_Status.Text = "Initialzing TDS";
                        //        textBoxTDS_MAC.Text = MAC_from_MainView;
                        //        textBoxTDS_IP.Text = "No IP assigned yet";
                        //        textBoxTDS_Progress.Text = "Booting/Initialzing..";

                        //    }
                        //}
                        //catch (Exception)
                        //{
                        //    textBoxTDS_Status.Text = "Can't get TDS status";
                        //}

                    }
                    else
                    {
                        textBoxTDS_Status.Text = "TDS stopped. Unit disc.";
                        //DataTable dbSnoInfo = alpha.GetTDSDataTable_SNOInfo(SN);
                        //string IP = dbSnoInfo.Rows[0]["IP"].ToString();
                        //textBoxTDS_IP.Text = IP;
                        //textBoxTDS_MAC.Text = MAC_from_MainView;
                        //string phase = dbSnoInfo.Rows[0]["Phase"].ToString();
                        //string part = dbSnoInfo.Rows[0]["Part"].ToString();
                        //string progressInfo = "Phase " + phase + ": " + part;
                        //textBoxTDS_Progress.Text = progressInfo;
                        //progressBarTDS.Value = Int32.Parse(phase);
                    }
                }
                else
                {
                    textBoxTDS_Status.Text = "Waiting for testing.";
                }
            }
            else if (unitDatabaseData.NextWC == "Database output error")
            {
                textBoxTDS_Status.Text = "SNO does not exist";
            }
            else if (nextWC_NUM > 55 || unitDatabaseData.NextWC == "")
            {
                textBoxTDS_Status.Text = "SNO already passed TDS";
            }
            else
            {
                textBoxTDS_Status.Text = "MVS";
            }
        }

        private void UpdateTextBoxMenu2(Async_Main_Search_DatabaseData unitDatabaseData)
        {
            textBoxOrder.Text = unitDatabaseData.unitOrderNumber;
            textBoxOrder.Update();

            textBoxPO.Text = unitDatabaseData.unitPoNumber;
            textBoxPO.Update();

            textBoxDn.Text = unitDatabaseData.unitDnNumber;
            textBoxDn.Update();

            string testToTextBox = unitDatabaseData.orderTestedUnits.ToString() + " / " + unitDatabaseData.orderUnitCount.ToString();
            textBoxPassToAll.Text = testToTextBox;

            textBoxFamily.Text = unitDatabaseData.unitFamily;
            textBoxFamily.Update();

            textBoxCreated.Text = unitDatabaseData.unitTimeCreated;
            textBoxCreated.Update();

            textBoxUpdated.Text = unitDatabaseData.unitTimeUpdated;
            textBoxUpdated.Update();

            textBoxLoc.Text = unitDatabaseData.unitLocation;
            textBoxLoc.Update();

            textBoxStation.Text = unitDatabaseData.unitLastStation;
            textBoxStation.Update();

            if (unitDatabaseData.unitNextStationName == "Database output error")
            {
                
                if (unitDatabaseData.unitLastStation == "Carton/Pallet Packing")
                {
                    textBoxNextStation.Text = "1.5 Scan";
                }

                else if (unitDatabaseData.unitLastStation == "1.5 Scan")
                {
                    textBoxNextStation.Text = "2nd Scan";
                }
                else if (unitDatabaseData.unitLastStation == "2nd Scan")
                {
                    textBoxNextStation.Text = "Shipping/Shipped";
                    textBoxNextStation.BackColor = Color.FromArgb(204, 255, 153);
                }
                else
                {
                    textBoxNextStation.Text = unitDatabaseData.unitNextStationName;
                }
            }
            else
            {
                textBoxNextStation.Text = unitDatabaseData.unitNextStationName;
            }
            textBoxNextStation.Update();

            textBoxM4U.Text = unitDatabaseData.unitM4U_code;
            textBoxM4U.Update();

            //dataGridViewInternalDisks.DataSource = unitDatabaseData.dtInternalDisks;
            //dataGridViewInternalDisks.Update();
        }


        //HISTORY LOG -------------------------------------------------------------------------------1
        private void labelHistory1_Click(object sender, EventArgs e)
        {
            if (labelHistory1.Text != "")
            {
                string historySN = labelHistory1.Text;
                inputBox2.Text = historySN;
                inputBox2.Update();
                buttonSearch.PerformClick();
            }
            else { }
        }

        private void labelHistory2_Click(object sender, EventArgs e)
        {
            if (labelHistory2.Text != "")
            {
                string historySN = labelHistory2.Text;
                inputBox2.Text = historySN;
                inputBox2.Update();
                buttonSearch.PerformClick();
            }
            else { }
        }

        private void labelHistory3_Click(object sender, EventArgs e)
        {
            if (labelHistory3.Text != "")
            {
                string historySN = labelHistory3.Text;
                inputBox2.Text = historySN;
                inputBox2.Update();
                buttonSearch.PerformClick();
            }
            else { }
        }

        private void labelHistory4_Click(object sender, EventArgs e)
        {
            if (labelHistory4.Text != "")
            {
                string historySN = labelHistory4.Text;
                inputBox2.Text = historySN;
                inputBox2.Update();
                buttonSearch.PerformClick();
            }
            else { }
        }

        private void labelHistory5_Click(object sender, EventArgs e)
        {
            if (labelHistory5.Text != "")
            {
                string historySN = labelHistory5.Text;
                inputBox2.Text = historySN;
                inputBox2.Update();
                buttonSearch.PerformClick();
            }
            else { }
        }

        private void labelHistory6_Click(object sender, EventArgs e)
        {
            if (labelHistory6.Text != "")
            {
                string historySN = labelHistory6.Text;
                inputBox2.Text = historySN;
                inputBox2.Update();
                buttonSearch.PerformClick();
            }
            else { }
        }


        private void labelHistory7_Click(object sender, EventArgs e)
        {
            if (labelHistory7.Text != "")
            {
                string historySN = labelHistory7.Text;
                inputBox2.Text = historySN;
                inputBox2.Update();
                buttonSearch.PerformClick();
            }
            else { }
        }

        private void labelHistory8_Click(object sender, EventArgs e)
        {
            if (labelHistory8.Text != "")
            {
                string historySN = labelHistory8.Text;
                inputBox2.Text = historySN;
                inputBox2.Update();
                buttonSearch.PerformClick();
            }
            else { }
        }

        private void labelHistory9_Click(object sender, EventArgs e)
        {
            if (labelHistory8.Text != "")
            {
                string historySN = labelHistory9.Text;
                inputBox2.Text = historySN;
                inputBox2.Update();
                buttonSearch.PerformClick();
            }
            else { }
        }

        private void buttonBV_Click(object sender, EventArgs e)
        {
            ShowSub(panelSideSubBV);
        }

        private void buttonBugs_Click(object sender, EventArgs e)
        {
            Form_Report formRep = new Form_Report(alpha.userName, alpha.accessLevel);
            //formRep.Closed += (s, args) => Close();
            formRep.Show();
        }

        private void checkBoxInfo_CheckedChanged(object sender, EventArgs e)
        {

            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Info checked/unchecked", "OK");
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            StatusUpdate("Loading Order List...");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened Order List", "OK");
            Form_Order formOrder = new Form_Order(alpha.userName, alpha.accessLevel, alpha);
            //formOrder.Closed += (s, args) => Close();
            formOrder.Show();
            StatusUpdate("Order List loaded.");
        }

        private void pictureBoxTroubleshooting_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.49/Tests/BayView/BayView_Troubleshooting.aspx");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened Troubleshooting", "OK");
        }

        void pictureBoxTroubleshooting_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxTroubleshooting.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.puzzle3));
        }

        void pictureBoxTroubleshooting_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxTroubleshooting.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.puzzle3_in));
        }
        private void buttonBV_Tracker_Click(object sender, EventArgs e)
        {
            //StatusUpdate("Loading BayView...");
            //alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened BayView(Tracker Pocket)", "OK");
            //Form_BayView formBW = new Form_BayView(alpha.userName, alpha.accessLevel, alpha);
            //formBW.Closed += (s, args) => Close();
            //formBW.Form_Main = this;
            //formBW.Show();
            //StatusUpdate("BayView loaded.");

            StatusUpdate("Loading BayView...");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened BayView(Tracker FHD)", "OK");
            Form_BayView_FHD formBW = new Form_BayView_FHD(alpha.userName, alpha.accessLevel, alpha);
            //formBW.Closed += (s, args) => Close();
            formBW.Form_Main = this;
            formBW.Show();
            StatusUpdate("BayView loaded.");
        }

        private void buttonBV_FIS_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.13.54.49/Tests/BayView/BayView_New.aspx");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened BayView(FIS)", "OK");
        }

        private void labelLVL_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxTDS_CheckedChanged(object sender, EventArgs e)
        {
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "TDS info checked/unchecked", "OK");
        }

        private void buttonTDS_Restart_Click(object sender, EventArgs e)
        {
            Check_PSEXEC_data();
            RestartUnit();
        }

        private void Check_PSEXEC_data()
        {

            if (Directory.Exists(@"C:\TestTrack") == false)
            {
                Directory.CreateDirectory(@"C:\TestTrack");
                Wait(1000);
            }
            foreach (var file in alpha.PSEXEC_files)
            {
                string pathToSource = @"\\10.13.82.200\comm_uut\tools\TestTrack\data\" + file;
                string pathToFile = @"C:\TestTrack\" + file;
                if (File.Exists(pathToFile) == false)
                {
                    StatusUpdate("Loading PSEXEC tools data, please wait..");
                    File.Copy(pathToSource, pathToFile, false);
                }
            }
        }

        private void Check_Updater_data()
        {

            if (Directory.Exists(@"C:\TestTrack") == false)
            {
                Directory.CreateDirectory(@"C:\TestTrack");
                Wait(1000);
            }
            foreach (var file in alpha.Updater_files)
            {
                string pathToSource = @"\\10.13.82.200\comm_uut\tools\TestTrack\data\" + file;
                string pathToFile = @"C:\TestTrack\" + file;
                if (File.Exists(pathToFile) == false)
                {
                    StatusUpdate("Loading Updater data, please wait..");
                    File.Copy(pathToSource, pathToFile, false);
                }
            }
        }

        private void RestartUnit()
        {
            //if (alpha.firstRetestWarning == true)
            //{
            //    alpha.firstRetestWarning = false;
            //    msgBx_Restart();
            //}
            labelTDS_Attribute.Text = "Remote TDS restart will works only if the unit is booted to Windows and have all TDS data loaded.";
            labelTDS_Attribute.Update();
            buttonTDS_Restart.Text = "Restarting";
            buttonTDS_Restart.BackColor = System.Drawing.Color.FromArgb(204, 255, 204);
            buttonTDS_Restart.Enabled = false;
            buttonTDS_Restart.Update();
            string tempFolderName = "tempPsExec_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bat";
            //string tempFolderName2 = "tempExec_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bat";
            string IP_unit = textBoxTDS_IP.Text;
            string location = textBoxTDS_Location.Text;
            string IP_server = GetServerIP_byLocation(location);
            string action = "Restarting " + inputBox2.Text + " on " + location;
            alpha.InsertAction(alpha.userName, alpha.accessLevel, action, "OK");
            if (IP_server != "error_Get_IP_by_Location")
            {
                ClearTemp(IP_server);
                PSEXEC1();
                PSEXEC2(IP_unit, tempFolderName);
                Wait(2000);
                PSEXEC3(IP_server, tempFolderName);
                //PSEXEC3(IP_server, tempFolderName, tempFolderName2);
                //string pathh = @"C:\TestTrack\" + tempFolderName2;
                //Wait(5000);
                //System.Diagnostics.Process.Start(pathh);
                StatusUpdate("Restarting TDS on: " + inputBox2.Text);
            }
            else
            {
                StatusUpdate("Restart Fail: Can't get server IP adress by current location");
            }
        }

        private void ClearTemp(string IP_server)
        {

        }

        private void PSEXEC1()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            string strCmdText = @"/C mkdir C:\TestTrack";
            startInfo.Arguments = strCmdText;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void PSEXEC2(string IP_unit, string tempFolderName)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            string strCmdText = @"/C echo psexec -i \\" + IP_unit + " -u Administrator -p \"\" \"C:\\TDS\\restart.cmd\" > C:\\TestTrack\\" + tempFolderName + " -accepteula";
            startInfo.Arguments = strCmdText;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void PSEXEC3(string IP_server, string tempFolderName)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            string pwd = alpha.GetLineaheadPassword(IP_server, "Administrator");
            string strCmdText = @"/C start C:\TestTrack\PsExec.exe -i \\" + IP_server + " -u Administrator -p \"" + pwd + "\" -c C:\\TestTrack\\" + tempFolderName + " -accepteula";
            startInfo.Arguments = strCmdText;
            process.StartInfo = startInfo;
            process.Start();
        }

        //    private void PSEXEC3(string IP_server, string tempFolderName, string tempFolderName2)
        //{
        //    Process process = new Process();
        //    ProcessStartInfo startInfo = new ProcessStartInfo();
        //    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    startInfo.FileName = "cmd.exe";
        //    string pwd = alpha.GetLineaheadPassword(IP_server, "Administrator");
        //    string strCmdText = @"/C echo start C:\TestTrack\psexec.exe -i \\" + IP_server + " -u Administrator -p \"" + pwd + "\" -c C:\\TestTrack\\" + tempFolderName + @" > C:\TestTrack\" + tempFolderName2;
        //    startInfo.Arguments = strCmdText;
        //    process.StartInfo = startInfo;
        //    process.Start();
        //}


        private string GetServerIP_byLocation(string location)
        {
            string output;
            if (location.Contains("LINE1"))
            {
                output = "10.13.82.2";
            }
            else if (location.Contains("LINE2") || location.Contains("LINE3"))
            {
                output = "10.13.82.3";
            }
            else if (location.Contains("LINE4") || location.Contains("LINE7"))
            {
                output = "10.13.82.4";
            }
            else if (location.Contains("LINE5") || location.Contains("LINE6"))
            {
                output = "10.13.82.200";
            }
            else if (location.Contains("LINE8") || location.Contains("LINEDMR") || location.Contains("LINEOBA"))
            {
                output = "10.13.82.8";
            }
            else if (location.Contains("LINEPRI"))
            {
                output = "10.13.82.10";
            }
            else
            {
                output = "error_Get_IP_by_Location";
            }
            return output;
        }

        private void buttonBV_Tracker_FHD_Click(object sender, EventArgs e)
        {
            StatusUpdate("Loading BayView...");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened BayView(Tracker FHD)", "OK");
            Form_BayView_FHD formBW = new Form_BayView_FHD(alpha.userName, alpha.accessLevel, alpha);
            //formBW.Closed += (s, args) => Close();
            formBW.Form_Main = this;
            formBW.Show();
            StatusUpdate("BayView loaded.");
        }

        private void buttonTDSHistory_Click(object sender, EventArgs e)
        {
            StatusUpdate("Loading TDS History...");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened TDS History", "OK");
            Form_TDS_Progress FormTDSProgress = new Form_TDS_Progress(alpha.userName, alpha.accessLevel, inputBox2.Text);
            //FormTDSProgress.Closed += (s, args) => Close();
            FormTDSProgress.Show();
            StatusUpdate("TDS History loaded.");
        }

        private void buttonToolM4U_Click(object sender, EventArgs e)
        {
            StatusUpdate("Loading M4U Image check tool...");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened M4UImageCheck tool", "OK");
            Form_Tool_CUZImageCheck forma = new Form_Tool_CUZImageCheck(alpha.userName, alpha.accessLevel, alpha);
            //forma.Closed += (s, args) => Close();
            forma.Show();
            StatusUpdate("M4U Image check tool loaded.");
        }

        private void buttonBvReset_Click(object sender, EventArgs e)
        {
            StatusUpdate("Loading Switchlogger reset tool...");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened Switchlogger reset", "OK");
            Form_SwitchloggerReset forma = new Form_SwitchloggerReset(alpha.userName, alpha.accessLevel, alpha);
            //forma.Closed += (s, args) => Close();
            forma.Show();
            StatusUpdate("BV Switchlogger reset tool loaded.");
        }

        private void buttonRegen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore", "http://10.13.54.204/CtoNew/Maintain/FCCL/pdT_SWL_Query_Regen.aspx");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened FIS/RegenerateOrderData", "OK");
        }

        private void pictureBoxSOP_M4U_Click(object sender, EventArgs e)
        {
            string pathToSOP = @"\\10.13.3.15\Iso-document\ISO_documents\Level_4\Q00_Quality\OPR_Operation\C1\_M4Y_";
            if (Directory.Exists(pathToSOP) == true)
            {
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened M4U  Instructions folder", "OK");
                Process.Start(pathToSOP);
            }
            else
            {
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened SOP folder", "Fail");
                string status = "Cant open remote control tool.";
                StatusUpdate(status);
            }
        }

        void pictureBoxSOP_M4U_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBoxSOP_M4U.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.M4U));
        }

        void pictureBoxSOP_M4U_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBoxSOP_M4U.Image = ((System.Drawing.Image)(TestTrack.Properties.Resources.M4U_in));
        }

        private void buttonDmrOrderDataBSN_Click(object sender, EventArgs e)
        {
            StatusUpdate("Loading DMR OrderData BSN generator..");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened DMR OrderData BSN generator", "OK");
            Form_BsnGenerator forma = new Form_BsnGenerator(alpha);
            //forma.Closed += (s, args) => Close();
            forma.Show();
            StatusUpdate("DMR OrderData BSN generator loaded.");
        }

        private void buttonBV_Tracker_Simple_Click(object sender, EventArgs e)
        {
            StatusUpdate("Loading BayView...");
            alpha.InsertAction(alpha.userName, alpha.accessLevel, "Opened BayView_OP(Tracker)", "OK");
            Form_BayView_Simple formBW = new Form_BayView_Simple(alpha.userName, alpha.accessLevel, alpha);
            //formBW.Closed += (s, args) => Close();
            formBW.Form_Main = this;
            formBW.Show();
            StatusUpdate("BayView loaded.");
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            Form test = new Form_BayOverview_Report(alpha);
            test.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pikacheck = pikacheck + 1;
            if (pikacheck == 10)
            {
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "PIKAAAAAAA", "OK");
                pictureBox2.Image = TestTrack.Properties.Resources.pika2;
                pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else if (pikacheck > 10)
            {
                pictureBox2.Image = TestTrack.Properties.Resources.LOGOnew2;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void buttonUpdateRequest_Click(object sender, EventArgs e)
        {
            AutoUpdate();
        }

        private void AutoUpdate()
        {
            if (File.Exists(@"\\10.13.82.200\comm_uut\tools\TestTrack\TestTrack.exe"))
            {
                alpha.InsertAction(alpha.userName, alpha.accessLevel, "Automatic update (Main)", "OK");
                string appDomain = AppDomain.CurrentDomain.BaseDirectory;
                System.Diagnostics.Process.Start(@"C:\TestTrack\Updater.exe", appDomain + " " + alpha.appVersion + " " + labelUpdateRequestVar.Text + " " + alpha.serverUserName + " " + alpha.lineheadLoginPasswords["LAB"]);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void buttonReMFI_Click(object sender, EventArgs e)
        {
            if (possibleReMFI.Count != 0)
            {
                foreach (string server in possibleReMFI)
                {
                    alpha.InsertAction(alpha.userName, alpha.accessLevel, "Removing MFI logs for SN: " + inputBox2.Text + " on server: " + server, "OK");
                    string pathToRequestFile = alpha.DictServerIPs[server] + @"comm_uut\Testresults\TestTrack_DeleteLogRequest.txt";
                    File.AppendAllText(pathToRequestFile, inputBox2.Text + Environment.NewLine);
                    buttonReMFI.BackColor = System.Drawing.Color.FromArgb(204, 255, 204);
                    buttonReMFI.Text = "MFI logs removed";
                    buttonReMFI.Update();
                }
            }
        }

        //HISTORY LOG -------------------------------------------------------------------------------0

        //private void validateUserEntry(int forList)
        //{
        //    // Checks the value of the text.

        //    // Initializes the variables to pass to the MessageBox.Show method.
        //    List<string> LineInfo = new List<string>() {"Line 1","Line 2/3","Line 7", "Line 8", };
        //    string message = "Line 1";
        //    string caption = "Info";
        //    MessageBoxButtons buttons = MessageBoxButtons.OK;
        //    DialogResult result;

        //    // Displays the MessageBox.
        //    result = MessageBox.Show(message, caption, buttons);
        //    if (result == System.Windows.Forms.DialogResult.Yes)
        //    {
        //        // Closes the parent form.
        //        this.Close();
        //    }
        //}

    }
    //public class ProgressBarEx : ProgressBar
    //{
    //    private SolidBrush brush = null;

    //    public ProgressBarEx()
    //    {
    //        this.SetStyle(ControlStyles.UserPaint, true);
    //    }

    //    protected override void OnPaint(PaintEventArgs e)
    //    {
    //        if (brush == null || brush.Color != this.ForeColor)
    //            brush = new SolidBrush(this.ForeColor);

    //        Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
    //        if (ProgressBarRenderer.IsSupported)
    //            ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rec);
    //        rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
    //        rec.Height = rec.Height - 4;
    //        e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);
    //    }
    //}
}
