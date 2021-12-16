using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MC1
{
    public class SQL_Tracker
    {
        public string connectionString;
        //public SqlConnection myConnection;
        public SQL_Tracker()
        {
            connectionString = "Data Source=icz3sql;Initial Catalog=CCD;Persist Security Info=True;User ID=fisread;Password=readfis";
        }

        public async Task<string> GetOrder(string SN)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Model FROM CCD..SNO WHERE Sno='" + SN + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT Model FROM CCD_History..SNO WHERE Sno='" + SN + "'", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetString(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }


        //public async Task<int> GetTotalBaysCount()
        //{
        //    int output = 0;
        //    try
        //    {
        //        using (SqlConnection myConnection = new SqlConnection(connectionString))
        //        using (SqlCommand Command_Get = new SqlCommand("SELECT COUNT(Bay) FROM ICZ_LOCAL..BayView_MainView", myConnection))
        //        {
        //            myConnection.Open();
        //            SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
        //            while (Reader_Get.Read())
        //            {
        //                output = Reader_Get.GetInt32(0);
        //                myConnection.Close();
        //                return output;
        //            }
        //            return output;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return output;
        //    }
        //}

        public DataTable GetOrderDataTable(string order)
        {
            DataTable dtOrder = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("WITH data AS (SELECT Sno, Model, Po AS PO, NextWc, Wc, Family, CCD..SNO.Udt as Updated, Description AS CurrentStation FROM CCD..SNO INNER JOIN CCD..WC ON CCD..SNO.Wc = CCD..WC.WC WHERE Model = '" + order + "') SELECT Sno, Model, PO, Family, CurrentStation,\"NextStation\" = case when Description is null then 'Out Of Inventec' else Description end, Updated FROM data LEFT JOIN CCD..WC ON data.NextWc = CCD..WC.WC", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    while (Reader_Get.Read())
                    {
                        dtOrder.Load(Reader_Get);
                        myConnection.Close();
                        return dtOrder;
                    }
                }
            }
            catch (Exception)
            {
                return dtOrder;
            }


            return dtOrder;
        }

        public DataTable GetPODataTable(string PO)
        {
            DataTable dtOrder = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("WITH data AS (SELECT Sno, Model, Po AS PO, NextWc, Wc, Family, CCD..SNO.Udt as Updated, Description AS CurrentStation FROM CCD..SNO INNER JOIN CCD..WC ON CCD..SNO.Wc = CCD..WC.WC WHERE Po = '" + PO + "') SELECT Sno, Model, PO, Family, CurrentStation,\"NextStation\" = case when Description is null then 'Out Of Inventec' else Description end, Updated FROM data LEFT JOIN CCD..WC ON data.NextWc = CCD..WC.WC", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    while (Reader_Get.Read())
                    {
                        dtOrder.Load(Reader_Get);
                        myConnection.Close();
                        return dtOrder;
                    }
                }
            }
            catch (Exception)
            {
                return dtOrder;
            }


            return dtOrder;
        }

        public DataTable GetBVDataTable()
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("WITH DATA_LastStatus AS(SELECT Sno AS Sno_A, MAX(Cdt) AS LatestStatusCdt FROM ICZ_LOCAL..BayViewStatusLog GROUP BY Sno), DATA_BayViewStatusLog_LastStatus AS (SELECT Sno_A, LatestStatusCdt, Sno AS Sno_B, Status, Attribute, Data, Cdt AS Cdt_B FROM DATA_LastStatus INNER JOIN ICZ_LOCAL..BayViewStatusLog ON DATA_LastStatus.LatestStatusCdt = ICZ_LOCAL..BayViewStatusLog.Cdt), data_A AS(SELECT ICZ_LOCAL..BayView_IPConfig.Sno AS SNO, ICZ_LOCAL..BayView_IPConfig.IP AS IP, MLBSN, Line, Type, Bay, Info, ICZ_LOCAL..BayView_MainView.MAC AS MAC FROM ICZ_LOCAL..BayView_MainView INNER JOIN ICZ_LOCAL..BayView_IPConfig ON ICZ_LOCAL..BayView_MainView.MAC = ICZ_LOCAL..BayView_IPConfig.Mac), DATA_AB AS (SELECT SNO, IP, MLBSN, CONCAT(Line,'_',Type,'_',Bay) AS Position, Info, MAC, Status AS LatestFile, LatestStatusCdt, Attribute, Data FROM data_A LEFT JOIN DATA_BayViewStatusLog_LastStatus ON data_A.SNO = DATA_BayViewStatusLog_LastStatus.Sno_B), DATA_C AS (SELECT Sno AS Sno_C, MAX(Cdt) AS LatestPartCdt FROM ICZ_LOCAL..BayViewPartsLog GROUP BY Sno), DATA_CD AS( SELECT Sno_C, LatestPartCdt, Part, SubPart, Status AS PartStatus FROM DATA_C INNER JOIN ICZ_LOCAL..BayViewPartsLog ON DATA_C.LatestPartCdt = ICZ_LOCAL..BayViewPartsLog.Cdt) SELECT SNO, IP, MLBSN, Position, Info, MAC, \"LatestFileAll\" = case when LatestFile is null then 'INI' else LatestFile end, LatestStatusCdt AS LatestFileCdt, Attribute, Data, LatestPartCdt, Part, SubPart, PartStatus FROM DATA_AB INNER JOIN DATA_CD ON DATA_AB.SNO = DATA_CD.Sno_C", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    while (Reader_Get.Read())
                    {
                        dtBV.Load(Reader_Get);
                        myConnection.Close();
                        return dtBV;
                    }
                }
            }
            catch (Exception)
            {
                return dtBV;
            }


            return dtBV;
        }

        public DataTable GetBVDataTable_All()
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("WITH DATA_LastStatus AS(SELECT Sno AS Sno_A, MAX(Cdt) AS LatestStatusCdt FROM ICZ_LOCAL..BayViewStatusLog GROUP BY Sno), DATA_BayViewStatusLog_LastStatus AS (SELECT Sno_A, LatestStatusCdt, Sno AS Sno_B, Status, Attribute, Data, Cdt AS Cdt_B FROM DATA_LastStatus INNER JOIN ICZ_LOCAL..BayViewStatusLog ON DATA_LastStatus.LatestStatusCdt = ICZ_LOCAL..BayViewStatusLog.Cdt), DATA_C AS (SELECT Sno AS Sno_C, MAX(Cdt) AS LatestPartCdt FROM ICZ_LOCAL..BayViewPartsLog GROUP BY Sno), DATA_CD AS(SELECT Sno_C, LatestPartCdt, Part, SubPart, Status AS PartStatus FROM DATA_C INNER JOIN ICZ_LOCAL..BayViewPartsLog ON DATA_C.LatestPartCdt = ICZ_LOCAL..BayViewPartsLog.Cdt), DATA_MAIN AS (SELECT \"SNO\" = case when BayView_IPConfig.Sno is null then 'EMPTY' else BayView_IPConfig.Sno end, CONCAT(Line,'_',Type,'_',Bay) AS Position, Status AS Unit_Connected, BayView_MainView.Cdt AS Connection_Time, \"IP_add\" = case when BayView_IPConfig.IP is null then 'EMPTY' else BayView_IPConfig.IP end, \"MAC_add\" = case when BayView_IPConfig.Mac is null then 'EMPTY' else BayView_IPConfig.Mac end, \"MLB_SN\" = case when BayView_IPConfig.MLBSN is null then 'EMPTY' else BayView_IPConfig.MLBSN end, Reboot, SwitchName, SwitchPort, TT FROM ICZ_LOCAL..BayView_MainView LEFT JOIN ICZ_LOCAL..BayView_IPConfig ON ICZ_LOCAL..BayView_MainView.MAC = ICZ_LOCAL..BayView_IPConfig.Mac), DATA_MAIN_PLUS_STATUS AS (SELECT SNO, Position, Unit_Connected, Connection_Time, IP_add, MAC_add, MLB_SN, Reboot, SwitchName, SwitchPort, TT, \"Status_Final\" = case when Status is null and Unit_Connected = 0 then 'EMPTY' when Status is null and Unit_Connected = 1 and MAC_add = 'EMPTY' then 'BOOTING' when Status is null and Unit_Connected = 1 and MAC_add != 'EMPTY' then 'INI' else Status end, LatestStatusCdt, Attribute, Data FROM DATA_MAIN LEFT JOIN DATA_BayViewStatusLog_LastStatus ON DATA_MAIN.SNO = DATA_BayViewStatusLog_LastStatus.Sno_B) SELECT SNO, Position, Unit_Connected, Connection_Time, IP_add, MAC_add, MLB_SN, Reboot, SwitchName, SwitchPort, TT, Status_Final, Part AS Phase, SubPart, PartStatus, LatestStatusCdt, Attribute, Data FROM DATA_MAIN_PLUS_STATUS LEFT JOIN DATA_CD ON DATA_MAIN_PLUS_STATUS.SNO = DATA_CD.Sno_C", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                    // !!!!!!!!!!!!!!!!!!!! removing Reader_Get.Read() - this passing the reader already advanced with one position !!!!!!!
                    //while (Reader_Get.Read())
                    //{
                    //    dtBV.Load(Reader_Get);
                    //    myConnection.Close();
                    //    return dtBV;
                    //}
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
            //return dtBV;
        }

        public async Task<DataTable> GetBVDataTable_Important()
        {
            var rand = new Random();
            string randomNumber = rand.Next(100000, 999999).ToString();
            string tempTable_1A = "1A_" + randomNumber;
            string tempTable_1B = "1B_" + randomNumber;
            string tempTable_MainPlus = "MainPlus_" + randomNumber;

            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                //using (SqlCommand Command_Get = new SqlCommand("WITH DATA_LastStatus AS(SELECT Sno AS Sno_A, MAX(Cdt) AS LatestStatus_Cdt FROM ICZ_LOCAL..BayViewStatusLog GROUP BY Sno), DATA_BayViewStatusLog_LastStatus AS (SELECT Sno_A, LatestStatus_Cdt, Sno AS Sno_B, Status, Attribute, Data, Cdt AS Cdt_B FROM DATA_LastStatus INNER JOIN ICZ_LOCAL..BayViewStatusLog ON DATA_LastStatus.LatestStatus_Cdt = ICZ_LOCAL..BayViewStatusLog.Cdt), DATA_C AS (SELECT Sno AS Sno_C, MAX(Cdt) AS LatestPartCdt FROM ICZ_LOCAL..BayViewPartsLog GROUP BY Sno), DATA_CD AS(SELECT Sno_C, LatestPartCdt, Part, SubPart, Status AS PartStatus, Cdt AS LatestSubPart_Cdt FROM DATA_C INNER JOIN ICZ_LOCAL..BayViewPartsLog ON DATA_C.LatestPartCdt = ICZ_LOCAL..BayViewPartsLog.Cdt), DATA_MAIN AS (SELECT \"SNO\" = case when BayView_IPConfig.Sno is null then 'EMPTY' else BayView_IPConfig.Sno end, CONCAT(Line,'_',Type,'_',Bay) AS Position, Status AS Unit_Connected, BayView_MainView.Cdt AS Connection_Time, \"IP_add\" = case when BayView_IPConfig.IP is null then 'EMPTY' else BayView_IPConfig.IP end, \"MAC_add\" = case when BayView_IPConfig.Mac is null then 'EMPTY' else BayView_IPConfig.Mac end, BayView_IPConfig.Cdt AS IP_Add_Assigned, \"MLB_SN\" = case when BayView_IPConfig.MLBSN is null then 'EMPTY' else BayView_IPConfig.MLBSN end, Reboot, SwitchName, SwitchPort, TT FROM ICZ_LOCAL..BayView_MainView LEFT JOIN ICZ_LOCAL..BayView_IPConfig ON ICZ_LOCAL..BayView_MainView.MAC = ICZ_LOCAL..BayView_IPConfig.Mac WHERE Status = 1), DATA_MAIN_PLUS_STATUS AS (SELECT SNO, Position, Unit_Connected, Connection_Time, IP_add, MAC_add, IP_Add_Assigned, MLB_SN, Reboot, SwitchName, SwitchPort, TT, \"Status_Final\" = case when Status is null and Unit_Connected = 0 then 'EMPTY' when Status is null and Unit_Connected = 1 and MAC_add = 'EMPTY' then 'BOOTING' when Status is null and Unit_Connected = 1 and MAC_add != 'EMPTY' then 'INI' else Status end, LatestStatus_Cdt, Attribute, Data FROM DATA_MAIN LEFT JOIN DATA_BayViewStatusLog_LastStatus ON DATA_MAIN.SNO = DATA_BayViewStatusLog_LastStatus.Sno_B), DATA_SEMIFINAL AS (SELECT Position, SNO, Connection_Time, IP_add, MAC_add, IP_Add_Assigned, MLB_SN, Reboot, Status_Final, LatestStatus_Cdt, Part AS Test_Phase, SubPart, PartStatus, LatestSubPart_Cdt, Attribute FROM DATA_MAIN_PLUS_STATUS LEFT JOIN DATA_CD ON DATA_MAIN_PLUS_STATUS.SNO = DATA_CD.Sno_C) SELECT Position, SNO, \"NextWc\" = case when CCD..SNO.NextWc is null then 'EMPTY' else CCD..SNO.NextWc end,Connection_Time, IP_add, MAC_add, IP_Add_Assigned, MLB_SN, Reboot, Status_Final, LatestStatus_Cdt, Test_Phase, SubPart, PartStatus, LatestSubPart_Cdt, Attribute FROM DATA_SEMIFINAL LEFT JOIN CCD..SNO ON DATA_SEMIFINAL.SNO = CCD..SNO.Sno", myConnection))
                using (SqlCommand Command_Get = new SqlCommand("WITH DATA_LastStatus AS (SELECT Sno AS Sno_A, MAX(Cdt) AS LatestStatus_Cdt FROM ICZ_LOCAL..BayViewStatusLog WITH(NOLOCK) GROUP BY Sno) SELECT Sno_A, LatestStatus_Cdt, Sno AS Sno_B, Status, Attribute, Data, Cdt AS Cdt_B INTO #" + tempTable_1A + " FROM DATA_LastStatus INNER JOIN ICZ_LOCAL..BayViewStatusLog WITH(NOLOCK) ON DATA_LastStatus.LatestStatus_Cdt = ICZ_LOCAL..BayViewStatusLog.Cdt; WITH DATA_C AS (SELECT Sno AS Sno_C, MAX(Cdt) AS LatestPartCdt FROM ICZ_LOCAL..BayViewPartsLog WITH(NOLOCK) GROUP BY Sno) SELECT Sno_C, LatestPartCdt, Part, SubPart, Status AS PartStatus, Cdt AS LatestSubPart_Cdt INTO #" + tempTable_1B + " FROM DATA_C INNER JOIN ICZ_LOCAL..BayViewPartsLog WITH(NOLOCK) ON DATA_C.LatestPartCdt = ICZ_LOCAL..BayViewPartsLog.Cdt; WITH DATA_MAIN AS (SELECT \"SNO\" = case when BayView_IPConfig.Sno is null then 'EMPTY' else BayView_IPConfig.Sno end, CONCAT(Line,'_',Type,'_',Bay) AS Position, Status AS Unit_Connected, BayView_MainView.Cdt AS Connection_Time, \"IP_add\" = case when BayView_IPConfig.IP is null then 'EMPTY' else BayView_IPConfig.IP end, \"MAC_add\" = case when BayView_IPConfig.Mac is null then 'EMPTY' else BayView_IPConfig.Mac end, BayView_IPConfig.Cdt AS IP_Add_Assigned, \"MLB_SN\" = case when BayView_IPConfig.MLBSN is null then 'EMPTY' else BayView_IPConfig.MLBSN end, Reboot, SwitchName, SwitchPort, TT FROM ICZ_LOCAL..BayView_MainView WITH(NOLOCK) LEFT JOIN ICZ_LOCAL..BayView_IPConfig WITH(NOLOCK) ON ICZ_LOCAL..BayView_MainView.MAC = ICZ_LOCAL..BayView_IPConfig.Mac WHERE Status = 1) SELECT SNO, Position, Unit_Connected, Connection_Time, IP_add, MAC_add, IP_Add_Assigned, MLB_SN, Reboot, SwitchName, SwitchPort, TT, \"Status_Final\" = case when Status is null and Unit_Connected = 0 then 'EMPTY' when Status is null and Unit_Connected = 1 and MAC_add = 'EMPTY' then 'BOOTING' when Status is null and Unit_Connected = 1 and MAC_add != 'EMPTY' then 'INI' else Status end, LatestStatus_Cdt, Attribute, Data INTO #" + tempTable_MainPlus + " FROM DATA_MAIN LEFT JOIN #" + tempTable_1A + " ON DATA_MAIN.SNO = #" + tempTable_1A + ".Sno_B; WITH DATA_FINAL AS(SELECT Position, SNO, Connection_Time, IP_add, MAC_add, IP_Add_Assigned, MLB_SN, Reboot, Status_Final, LatestStatus_Cdt, Part AS Test_Phase, SubPart, PartStatus, LatestSubPart_Cdt, Attribute FROM #" + tempTable_MainPlus + " LEFT JOIN #" + tempTable_1B + " ON #" + tempTable_MainPlus + ".SNO = #" + tempTable_1B + ".Sno_C) SELECT Position, SNO, \"NextWc\" = case when CCD..SNO.NextWc is null then 'EMPTY' else CCD..SNO.NextWc end,Connection_Time, IP_add, MAC_add, IP_Add_Assigned, MLB_SN, Reboot, Status_Final, LatestStatus_Cdt, Test_Phase, SubPart, PartStatus, LatestSubPart_Cdt, Attribute FROM DATA_FINAL LEFT JOIN CCD..SNO ON DATA_FINAL.SNO = CCD..SNO.Sno; DROP TABLE #" + tempTable_1A + "; DROP TABLE #" + tempTable_1B + "; DROP TABLE #" + tempTable_MainPlus + ";", myConnection))

                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public async Task<DataTable> GetBVDataTable_Operator_Notif()
        {
            var rand = new Random();
            string randomNumber = rand.Next(100000, 999999).ToString();
            string tempTable_1A = "1A_" + randomNumber;
            string tempTable_1B = "1B_" + randomNumber;
            string tempTable_MainPlus = "MainPlus_" + randomNumber;
            string tempTable_Final = "Final_" + randomNumber;

            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("WITH DATA_LastStatus AS(SELECT Sno AS Sno_A, MAX(Cdt) AS LatestStatus_Cdt FROM ICZ_LOCAL..BayViewStatusLog WITH(NOLOCK) GROUP BY Sno) SELECT Sno_A, LatestStatus_Cdt, Sno AS Sno_B, Status, Attribute, Data, Cdt AS Cdt_B INTO #" + tempTable_1A + " FROM DATA_LastStatus INNER JOIN ICZ_LOCAL..BayViewStatusLog WITH(NOLOCK) ON DATA_LastStatus.LatestStatus_Cdt = ICZ_LOCAL..BayViewStatusLog.Cdt; WITH DATA_C AS (SELECT Sno AS Sno_C, MAX(Cdt) AS LatestPartCdt FROM ICZ_LOCAL..BayViewPartsLog WITH(NOLOCK) GROUP BY Sno) SELECT Sno_C, LatestPartCdt, Part, SubPart, Status AS PartStatus, Cdt AS LatestSubPart_Cdt INTO #" + tempTable_1B + " FROM DATA_C INNER JOIN ICZ_LOCAL..BayViewPartsLog WITH(NOLOCK) ON DATA_C.LatestPartCdt = ICZ_LOCAL..BayViewPartsLog.Cdt; WITH DATA_MAIN AS (SELECT \"SNO\" = case when BayView_IPConfig.Sno is null then 'EMPTY' else BayView_IPConfig.Sno end, CONCAT(Line,' - ',Bay) AS Position, Status AS Unit_Connected, BayView_MainView.Cdt AS Connection_Time, \"IP_add\" = case when BayView_IPConfig.IP is null then 'EMPTY' else BayView_IPConfig.IP end, \"MAC_add\" = case when BayView_IPConfig.Mac is null then 'EMPTY' else BayView_IPConfig.Mac end, BayView_IPConfig.Cdt AS IP_Add_Assigned, \"MLB_SN\" = case when BayView_IPConfig.MLBSN is null then 'EMPTY' else BayView_IPConfig.MLBSN end, Reboot, SwitchName, SwitchPort, TT FROM ICZ_LOCAL..BayView_MainView WITH(NOLOCK) LEFT JOIN ICZ_LOCAL..BayView_IPConfig WITH(NOLOCK) ON ICZ_LOCAL..BayView_MainView.MAC = ICZ_LOCAL..BayView_IPConfig.Mac WHERE Status = 1) SELECT SNO, Position, Unit_Connected, Connection_Time, IP_add, MAC_add, IP_Add_Assigned, MLB_SN, Reboot, SwitchName, SwitchPort, TT, \"Status_Final\" = case when Status is null and Unit_Connected = 0 then 'EMPTY' when Status is null and Unit_Connected = 1 and MAC_add = 'EMPTY' then 'BOOTING' when Status is null and Unit_Connected = 1 and MAC_add != 'EMPTY' then 'INI' else Status end, LatestStatus_Cdt, Attribute, Data INTO #" + tempTable_MainPlus + " FROM DATA_MAIN LEFT JOIN #" + tempTable_1A + " ON DATA_MAIN.SNO = #" + tempTable_1A + ".Sno_B; WITH DATA_FINAL AS (SELECT Position, SNO, Connection_Time, IP_add, MAC_add, IP_Add_Assigned, MLB_SN, Reboot, Status_Final, LatestStatus_Cdt, Part AS Test_Phase, SubPart, PartStatus, LatestSubPart_Cdt, Attribute FROM #" + tempTable_MainPlus + " LEFT JOIN #" + tempTable_1B + " ON #" + tempTable_MainPlus + ".SNO = #" + tempTable_1B + ".Sno_C) SELECT Position, SNO, \"NextWc\" = case when CCD..SNO.NextWc is null then 'EMPTY' else CCD..SNO.NextWc end,Connection_Time, IP_add, MAC_add, IP_Add_Assigned, MLB_SN, Reboot, Status_Final, LatestStatus_Cdt, Test_Phase, SubPart, PartStatus, LatestSubPart_Cdt, Attribute INTO #" + tempTable_Final + " FROM DATA_FINAL LEFT JOIN CCD..SNO ON DATA_FINAL.SNO = CCD..SNO.Sno; SELECT Position, SNO, \"Status\" = case when Status_Final = '55F' then 'FAILED' when Status_Final = '55P' then 'PASSED' when Status_Final = '55S' AND SubPart = 'ODD_Raus' then 'Odeber CD' when Status_Final = '55S' AND SubPart = 'ODD_Retry' then 'Vloz CD' when Status_Final = '55S' AND SubPart = 'Speaker_test' then 'Speaker test' when NextWc < '50' then 'Nema HIPOT/MVS' else Status_Final END FROM #" + tempTable_Final + " WHERE (Status_Final in ('55F','55P') OR (Status_Final = '55S' AND SubPart in ('ODD_Raus', 'ODD_Retry', 'Speaker_test')) OR (NextWc < '50')) AND Position is NOT NULL;DROP TABLE #" + tempTable_1A + ";DROP TABLE #" + tempTable_1B + ";DROP TABLE #" + tempTable_MainPlus + ";DROP TABLE #" + tempTable_Final + ";", myConnection))

                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public async Task<DataTable> GetBVDataTable_Fails()
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("WITH DATA_LastStatus AS(SELECT Sno AS Sno_A, MAX(Cdt) AS LatestStatusCdt FROM ICZ_LOCAL..BayViewStatusLog WITH(NOLOCK) GROUP BY Sno), DATA_BayViewStatusLog_LastStatus AS (SELECT Sno_A, LatestStatusCdt, Sno AS Sno_B, Status, Attribute, Data, Cdt AS Cdt_B FROM DATA_LastStatus WITH(NOLOCK) INNER JOIN ICZ_LOCAL..BayViewStatusLog WITH(NOLOCK) ON DATA_LastStatus.LatestStatusCdt = ICZ_LOCAL..BayViewStatusLog.Cdt), data_A AS(SELECT ICZ_LOCAL..BayView_IPConfig.Sno AS SNO, ICZ_LOCAL..BayView_IPConfig.IP AS IP, BayView_IPConfig.Cdt AS IP_Cdt, MLBSN, Line, Type, Bay, Info, ICZ_LOCAL..BayView_MainView.MAC AS MAC FROM ICZ_LOCAL..BayView_MainView WITH(NOLOCK) INNER JOIN ICZ_LOCAL..BayView_IPConfig WITH(NOLOCK) ON ICZ_LOCAL..BayView_MainView.MAC = ICZ_LOCAL..BayView_IPConfig.Mac), DATA_AB AS (SELECT SNO, IP, IP_Cdt, MLBSN, CONCAT(Line,'_',Type,'_',Bay) AS Position, Info, MAC, Status AS LatestFile, LatestStatusCdt, Attribute, Data FROM data_A LEFT JOIN DATA_BayViewStatusLog_LastStatus ON data_A.SNO = DATA_BayViewStatusLog_LastStatus.Sno_B), DATA_C AS (SELECT Sno AS Sno_C, MAX(Cdt) AS LatestPartCdt FROM ICZ_LOCAL..BayViewPartsLog WITH(NOLOCK) GROUP BY Sno), DATA_CD AS(SELECT Sno_C, LatestPartCdt, Part, SubPart, Status AS PartStatus FROM DATA_C INNER JOIN ICZ_LOCAL..BayViewPartsLog WITH(NOLOCK) ON DATA_C.LatestPartCdt = ICZ_LOCAL..BayViewPartsLog.Cdt) SELECT SNO, IP, MLBSN, Position, MAC, LatestFile AS Status, LatestStatusCdt AS Fail_Time, Attribute, Part, SubPart, PartStatus, LatestPartCdt FROM DATA_AB INNER JOIN DATA_CD ON DATA_AB.SNO = DATA_CD.Sno_C WHERE LatestFile = '55F' ORDER BY Position DESC", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public async Task<DataTable> GetTDSDataTable_SNOInfo(string SN)
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("WITH DATA_LastStatus AS(SELECT Sno AS Sno_A, MAX(Cdt) AS LatestStatus_Cdt FROM ICZ_LOCAL..BayViewStatusLog GROUP BY Sno), DATA_BayViewStatusLog_LastStatus AS (SELECT Sno_A, LatestStatus_Cdt, Sno AS Sno_B, Status, Attribute, Data, Cdt AS Cdt_B FROM DATA_LastStatus INNER JOIN ICZ_LOCAL..BayViewStatusLog ON DATA_LastStatus.LatestStatus_Cdt = ICZ_LOCAL..BayViewStatusLog.Cdt), DATA_C AS (SELECT Sno AS Sno_C, MAX(Cdt) AS LatestPartCdt FROM ICZ_LOCAL..BayViewPartsLog GROUP BY Sno), DATA_CD AS(SELECT Sno_C, LatestPartCdt, Part, SubPart, Status AS PartStatus, Cdt AS LatestSubPart_Cdt FROM DATA_C INNER JOIN ICZ_LOCAL..BayViewPartsLog ON DATA_C.LatestPartCdt = ICZ_LOCAL..BayViewPartsLog.Cdt WHERE Sno_C = '" + SN + "'), DATA_ABCD AS(SELECT Sno_C AS SNO, Status, Part AS Phase, SubPart AS Part, Attribute FROM DATA_CD INNER JOIN DATA_BayViewStatusLog_LastStatus ON DATA_CD.Sno_C = DATA_BayViewStatusLog_LastStatus.Sno_A), DATA_ABCD_IP AS(SELECT SNO, Status, Phase, Part, Attribute, Mac AS MAC, IP FROM DATA_ABCD INNER JOIN  ICZ_LOCAL..BayView_IPConfig ON DATA_ABCD.SNO = ICZ_LOCAL..BayView_IPConfig.Sno)SELECT SNO, DATA_ABCD_IP.Status, Phase, Part, Attribute, DATA_ABCD_IP.MAC, DATA_ABCD_IP.IP, Line, CONCAT(Line,' ',Type,' - Bay ',Bay) AS Position FROM DATA_ABCD_IP INNER JOIN ICZ_LOCAL..BayView_MainView ON DATA_ABCD_IP.MAC = ICZ_LOCAL..BayView_MainView.MAC", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public async Task<DataTable> GetTDSDataTable_TDS_Progress(string SN)
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                //using (SqlCommand Command_Get = new SqlCommand("SELECT Sno, MAC, Part AS Phase, SubPart AS Part, Status, Cdt FROM ICZ_LOCAL..BayViewPartsLog WHERE Sno = '" + SN + "'  ORDER BY Cdt DESC", myConnection))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Sno, MAC, Part AS Phase, SubPart AS Part, \"Status\" = case when Status = 1 then 'Script started' when Status = 2 then 'Script ended' else Status end, Cdt FROM ICZ_LOCAL..BayViewPartsLog WHERE Sno = '" + SN + "' ORDER BY Cdt DESC", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public async Task<DataTable> GetBVDataTable_DeadBays()
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT * FROM ICZ_LOCAL..TestTrack_BV_DeadBays", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public async Task<DataTable> GetBVDataTable_Pending(string minutes)
        {
            var rand = new Random();
            string randomNumber = rand.Next(100000, 999999).ToString();
            string tempTable_1A = "1A_" + randomNumber;
            string tempTable_1B = "1B_" + randomNumber;

            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Sno as Sno_A, Wc, NextWc, Udt, Status AS Status_Sno, Po INTO #" + tempTable_1A + " FROM CCD..SNO(nolock) Where Wc in ('40','49') AND NextWc = '55' AND Status = 'W' AND Po LIKE '1%'; SELECT TOP(20000) Sno as Sno_B, Status, Cdt INTO #" + tempTable_1B + " FROM ICZ_LOCAL..BayViewStatusLog_History WITH(NOLOCK) WHERE Status = '55S' Order by Cdt DESC; WITH DATA_AB AS (SELECT *,datediff(minute, #" + tempTable_1A + ".Udt,getdate()) as 'Delta_Minutes' FROM #" + tempTable_1A + " LEFT JOIN #" + tempTable_1B + " ON #" + tempTable_1A + ".Sno_A = #" + tempTable_1B + ".Sno_B), DATA_FINAL AS (SELECT *, CASE when Delta_Minutes >= " + minutes + " AND Status IS NULL then 'Need_Check' else 'OK' end as 'Result' FROM DATA_AB)SELECT Sno_A AS SNO, Wc, NextWc, Udt, Delta_Minutes FROM DATA_FINAL WHERE Result = 'Need_Check' AND Sno_A NOT IN (SELECT Sno FROM ICZ_LOCAL..TestTrack_BV_IgnoredSnoWarning WITH(NOLOCK));DROP TABLE #" + tempTable_1A + ";DROP TABLE #" + tempTable_1B + ";", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public async Task<DataTable> GetDataTable_InternalDisks(string SN)
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT DISTINCT(ScanSn),CustPn FROM CCD..SNO_PARTS WHERE (CustPn LIKE 'UGS%' OR  CustPn LIKE 'SGT%' OR CustPn LIKE 'WDC%' OR CustPn LIKE 'MOI%' OR CustPn LIKE 'TOS%') AND Sno = '" + SN + "'; ", myConnection))

                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        dtBV.Load(Reader_Get);
                        myConnection.Close();
                        return dtBV;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT DISTINCT(ScanSn),CustPn FROM CCD_History..SNO_PARTS WHERE (CustPn LIKE 'UGS%' OR  CustPn LIKE 'SGT%' OR CustPn LIKE 'WDC%' OR CustPn LIKE 'MOI%' OR CustPn LIKE 'TOS%') AND Sno = '" + SN + "'; ", myConnection))

                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            dtBV.Load(Reader_Get2);
                            myConnection.Close();
                            return dtBV;
                        }
                        return dtBV;
                    }
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }


        public DataTable GetStatusTable_Sku(string SKUs)
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("WITH data_main AS(SELECT Model, COUNT(Sno) AS Total FROM CCD..SNO where Model IN ('" + SKUs + "') GROUP BY Model), data_MVS AS (Select Model, COUNT(Sno) AS to_MVS from CCD..SNO where Wc < '50' AND NOT Wc = '49' AND Wc = '' GROUP BY Model), data_Test AS (Select Model, COUNT(Sno) AS TEST from CCD..SNO WHERE NextWc = '55' GROUP BY Model ), data_SWAP AS (Select Model, COUNT(Sno) AS SWAP from CCD..SNO WHERE (Wc = '55' AND NextWc = 'FF') OR (Wc = 'FF' AND NextWc = '49') GROUP BY Model) SELECT data_main.Model, \"MVS\" = case when to_MVS is null then 0 else to_MVS end, \"TEST\" = case when TEST is null then 0 else TEST end, \"SWAP\" = case when SWAP is null then 0 else SWAP end FROM data_main LEFT JOIN data_MVS ON data_main.Model = data_MVS.Model LEFT JOIN data_Test ON data_main.Model = data_Test.Model LEFT JOIN data_SWAP ON data_main.Model = data_SWAP.Model ORDER BY Model DESC", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public DataTable GetStatusTable_RemoteLineaheadPWs()
        {
            DataTable dtBV = new DataTable();
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Category, Host, UserName, Password FROM ICZ_LOCAL..FILE_STOCK_LOCAL ORDER BY Category", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public DataTable Get_BASEUNIT_IMAGE_PN_DATA(string baseUnit)
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT BaseUnit,SFTDummyPn,BIM_SN_01,Boot1,Active,Editor,Udt from CCD..BASEUNIT_IMAGE_PN_DATA WHERE BaseUnit = '" + baseUnit + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public DataTable Get_ImageCUZList()
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("select BaseUnit from CCD..BASEUNIT_IMAGE_PN_DATA WHERE BaseUnit Like 'CUZ:%'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public DataTable GetTT_Savers()
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT TT_ICZ FROM ICZ_LOCAL..TestTrack_TT_List WHERE Saver = 'Y'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public DataTable GetActiveLineheadServers()
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Server_ShortName FROM ICZ_LOCAL..TestTrack_TestServers WHERE Active = 'Y'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public DataTable GetModelData()
        {
            DataTable dtBV = new DataTable();

            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Prefix,ModelName,MB_SN,Category,Manufactory FROM ICZ_LOCAL..TestTrack_Models", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public DataTable GetBayUsageHeatMap()
        {
            DataTable dtBV = new DataTable();
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Bay, Usage FROM ICZ_LOCAL..TestTrack_BV_BayUsageHeatmap", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public DataTable GetSearchHistory(string userName)
        {
            DataTable dtBV = new DataTable();
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT TOP(9) SN FROM ICZ_LOCAL..TestTrack_SearchLog WHERE UserName = '" + userName + "' ORDER BY DateTime DESC", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }

        public DataTable GetSavedSN(string userName)
        {
            DataTable dtBV = new DataTable();
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT SN, Note, DelSN, Time FROM ICZ_LOCAL..TestTrack_SavedSn WHERE UserName = '" + userName + "' ORDER BY Time DESC", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    dtBV.Load(Reader_Get);
                    myConnection.Close();
                    return dtBV;
                }
            }
            catch (Exception)
            {
                return dtBV;
            }
        }



        public async Task<string> GetPO(string SN)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Po FROM CCD..SNO WHERE Sno='" + SN + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT Po FROM CCD_History..SNO WHERE Sno='" + SN + "'", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetString(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }

        }

        public async Task<string> GetDn(string SN)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Dn FROM CCD..SNO WHERE Sno='" + SN + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT Dn FROM CCD_History..SNO WHERE Sno='" + SN + "'", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetString(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }

        }


        public async Task<int> GetOrderUnitCount(string SN)
        {
            int output = 0;
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("Select Qty from CCD..PO A left join CCD..SNO B ON A.Model=B.Model where Sno='" + SN + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetInt32(0);
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("Select Qty from CCD_History..PO A left join CCD_History..SNO B ON A.Model=B.Model where Sno='" + SN + "'", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetInt32(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<int> GetOrderUnitTested(string SN)
        {
            int output = 0;
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("Select Count(Sno) from CCD..SNO where Model in(Select Model from CCD..SNO where Sno='" + SN + "') and (NextWc > '55' or NextWc = '53' or NextWc = '')", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetInt32(0);
                        if (output == 0)
                        {
                            myConnection.Close();
                            using (SqlCommand Command_Get2 = new SqlCommand("Select Count(Sno) from CCD_History..SNO where Model in(Select Model from CCD_History..SNO where Sno='" + SN + "') and (NextWc > '55' or NextWc = '53' or NextWc = '')", myConnection))
                            {
                                myConnection.Open();
                                SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                                while (Reader_Get2.Read())
                                {
                                    output = Reader_Get2.GetInt32(0);
                                    myConnection.Close();
                                    return output;
                                }
                                output = 0;
                                return output;
                            }
                        }
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("Select Count(Sno) from CCD_History..SNO where Model in(Select Model from CCD_History..SNO where Sno='" + SN + "') and (NextWc > '55' or NextWc = '53' or NextWc = '')", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetInt32(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<string> GetFamily(string SN)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Family FROM CCD..SNO WHERE Sno='" + SN + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT Family FROM CCD_History..SNO WHERE Sno='" + SN + "'", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetString(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<string> GetTimeCreated(string SN)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Cdt FROM CCD..SNO WHERE Sno='" + SN + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        var outputDate = Reader_Get.GetDateTime(0);
                        output = outputDate.ToString();
                        try
                        {
                            output = output.Substring(0, 19);
                        }
                        catch (Exception) { }
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT Cdt FROM CCD_History..SNO WHERE Sno='" + SN + "'", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            var outputDate = Reader_Get2.GetDateTime(0);
                            output = outputDate.ToString();
                            try
                            {
                                output = output.Substring(0, 19);
                            }
                            catch (Exception) { }
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<string> GetTimeUpdated(string SN)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Udt FROM CCD..SNO WHERE Sno='" + SN + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        var outputDate = Reader_Get.GetDateTime(0);
                        output = outputDate.ToString();
                        try
                        {
                            output = output.Substring(0, 19);
                        }
                        catch (Exception) { }
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT Udt FROM CCD_History..SNO WHERE Sno='" + SN + "'", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            var outputDate = Reader_Get2.GetDateTime(0);
                            output = outputDate.ToString();
                            try
                            {
                                output = output.Substring(0, 19);
                            }
                            catch (Exception) { }
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<string> GetLocation(string SN)
        {
            string output = "No record";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("WITH data AS (SELECT ROW_NUMBER() over(ORDER BY Cdt DESC) AS Id, Switch_Name, Switch_Port FROM ICZ_LOCAL..BayView_MacHistory WHERE MAC IN(SELECT bvi.Mac FROM ICZ_LOCAL..BayView_IPConfig bvi WHERE  bvi.Sno = '" + SN + "')) SELECT CONCAT(Line,' ',Type,' - Bay ',Bay) AS ss FROM data d LEFT JOIN ICZ_LOCAL..BayView_MainView bvmv ON d.Switch_Name = bvmv.SwitchName AND d.Switch_Port = bvmv.SwitchPort WHERE d.Id = '1'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    return output;
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<string> GetStation(string SN)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Description FROM CCD..WC WHERE WC=(SELECT Wc FROM CCD..SNO WHERE Sno='" + SN + "')", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT Description FROM CCD..WC WHERE WC=(SELECT Wc FROM CCD_History..SNO WHERE Sno='" + SN + "')", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetString(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<string> GetNextStationName(string SN)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Description FROM CCD..WC WHERE WC=(SELECT NextWc FROM CCD..SNO WHERE Sno='" + SN + "')", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT Description FROM CCD..WC WHERE WC=(SELECT NextWc FROM CCD_History..SNO WHERE Sno='" + SN + "')", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetString(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }


        public async Task<string> GetLastStation(string SN)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Wc FROM CCD..SNO WHERE Sno='" + SN + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT Wc FROM CCD_History..SNO WHERE Sno='" + SN + "'", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetString(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }


        public async Task<string> GetNextStation(string SN)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT NextWc FROM CCD..SNO WHERE Sno='" + SN + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("SELECT NextWc FROM CCD_History..SNO WHERE Sno='" + SN + "'", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetString(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<string> GetTDS_MAC_IP(string SN)
        {
            string output = "Nothing";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Mac FROM ICZ_LOCAL..BayView_IPConfig WHERE Sno = '" + SN + "' ORDER BY Cdt DESC", myConnection))
                    // pridat ORDER BY DESC - aby funkce returnovala pouze posledni MAC, pokud byla menena deska ***
                //using (SqlCommand Command_Get = new SqlCommand("SELECT * FROM ICZ_LOCAL..BayView_MainView WHERE MAC = (SELECT Mac FROM ICZ_LOCAL..BayView_IPConfig WHERE Sno = '" + SN + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    return output;
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<string> GetTDS_MAC_Main(string MAC)
        {
            string output = "Nothing";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT MAC FROM ICZ_LOCAL..BayView_MainView WHERE MAC = '" + MAC + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    return output;
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<string> GetM4U(string SN)
        {
            string output = "No customization";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("with data as (Select distinct A.Sno,FCCLPo from CCD..SNO A join CCD..SAP_BOM_DATA B ON A.Model=B.Model where A.Sno='" + SN + "') Select distinct Dispatch_code from data C left join CCD..FCCL_ORDER_M4Y D on C.FCCLPo=D.FCCL_PO_No", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    myConnection.Close();
                    using (SqlCommand Command_Get2 = new SqlCommand("with data as (Select distinct A.Sno,FCCLPo from CCD_History..SNO A join CCD..SAP_BOM_DATA B ON A.Model=B.Model where A.Sno='" + SN + "') Select distinct Dispatch_code from data C left join CCD..FCCL_ORDER_M4Y D on C.FCCLPo=D.FCCL_PO_No", myConnection))
                    {
                        myConnection.Open();
                        SqlDataReader Reader_Get2 = await Command_Get2.ExecuteReaderAsync();
                        while (Reader_Get2.Read())
                        {
                            output = Reader_Get2.GetString(0);
                            myConnection.Close();
                            return output;
                        }
                        return output;
                    }
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public string GetLineaheadPassword(string server, string userName)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Password FROM ICZ_LOCAL..FILE_STOCK_LOCAL WHERE Host = '" + server + "' AND UserName = '" + userName + "'", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    return output;
                }
            }
            catch (Exception)
            {
                return output;
            }

        }

        public string GetBayView_LastStatus(string line, string bay)
        {
            string output = "Empty";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT TOP 1 Status FROM ICZ_LOCAL..BayViewStatusLog bvsl WHERE Sno=(SELECT TOP 1 Sno FROM ICZ_LOCAL..BayViewPartsLog WHERE MAC=(SELECT TOP 1 MAC FROM ICZ_LOCAL..BayView_MainView WHERE Line='" + line + "' AND Bay='" + bay + "' ORDER BY Udt DESC) ORDER BY Cdt DESC) ORDER BY Cdt DESC", myConnection))
                using (SqlCommand comm = new SqlCommand("SET ARITHABORT ON", myConnection))
                {
                    myConnection.Open();
                    comm.ExecuteNonQuery();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    return output;
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public async Task<int> GetTotalBaysCount()
        {
            int output = 0;
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT COUNT(Bay) FROM ICZ_LOCAL..BayView_MainView", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = await Command_Get.ExecuteReaderAsync();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetInt32(0);
                        myConnection.Close();
                        return output;
                    }
                    return output;
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public int GetDeadBaysCount()
        {
            int output = 0;
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT DISTINCT COUNT(Bay) FROM ICZ_LOCAL..TestTrack_BV_DeadBays;", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetInt32(0);
                        myConnection.Close();
                        return output;
                    }
                    return output;
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public string GetMicCheck()
        {
            string output = "Error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Verification FROM ICZ_LOCAL..TestTrack_Root", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    return output;
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public string GetReqVersion()
        {
            string output = "Error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Version FROM ICZ_LOCAL..TestTrack_Root", myConnection))
                {
                    myConnection.Open();
                    SqlDataReader Reader_Get = Command_Get.ExecuteReader();
                    while (Reader_Get.Read())
                    {
                        output = Reader_Get.GetString(0);
                        myConnection.Close();
                        return output;
                    }
                    return output;
                }
            }
            catch (Exception)
            {
                return output;
            }
        }

        public void InsertAction(string userName, string Lvl, string action, string result)
        {
            //string output = "Empty";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command = new SqlCommand("INSERT INTO ICZ_LOCAL..TestTrack_Activity select '" + userName + "','" + Lvl + "','" + action + "','" + result + "',getdate()", myConnection))
                {
                    myConnection.Open();
                    Command.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public void InsertSnoIgnore(string sno)
        {
            //string output = "Empty";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command = new SqlCommand("INSERT INTO ICZ_LOCAL..TestTrack_BV_IgnoredSnoWarning select '" + sno + "',getdate();", myConnection))
                {
                    myConnection.Open();
                    Command.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public void InsertBayUsage(string date, string usage, string failUsage)
        {
            //string output = "Empty";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command = new SqlCommand("INSERT INTO ICZ_LOCAL..TestTrack_BayUsage select '" + date + "','" + usage + "','" + failUsage + "',getdate()", myConnection))
                {
                    myConnection.Open();
                    Command.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
            }
        }


        public void InsertSearch(string userName, string SN)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command = new SqlCommand("INSERT INTO ICZ_LOCAL..TestTrack_SearchLog select '" + userName + "','" + SN + "',getdate()", myConnection))
                {
                    myConnection.Open();
                    Command.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public void InsertSearchDataGrid(string userName, string lvl, string tableType, string number)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command = new SqlCommand("INSERT INTO ICZ_LOCAL..TestTrack_SearchLog_DataGrid select '" + userName + "', '" + lvl + "','" + tableType + "','" + number + "',getdate();", myConnection))
                {
                    myConnection.Open();
                    Command.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public void InsertLogin(string userName, string lvl, string result)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command = new SqlCommand("INSERT INTO ICZ_LOCAL..TestTrack_Login select '" + userName + "','" + lvl + "','" + result + "',getdate()", myConnection))
                {
                    myConnection.Open();
                    Command.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
            }
        }
        public void InsertLogRecovery(string userName, string lvl, string SN, string testLog)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command = new SqlCommand("INSERT INTO ICZ_LOCAL..TestTrack_LogRecovery select '" + userName + "','" + lvl + "','" + SN + "','" + testLog + "',getdate()", myConnection))
                {
                    myConnection.Open();
                    Command.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public void InsertReport(string userName, string lvl, string type, string message)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command = new SqlCommand("INSERT INTO ICZ_LOCAL..TestTrack_Report select '" + userName + "','" + lvl + "','" + type + "','" + message + "',getdate()", myConnection))
                {
                    myConnection.Open();
                    Command.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public void InsertBayPositionUsage(string date, Dictionary<string,string> BayPositionUsage)
        {
            //string output = "Empty";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command = new SqlCommand("INSERT INTO ICZ_LOCAL..TestTrack_BayPositionUsage select '" + date + "','" + BayPositionUsage["LINE2_B_25"] + "','" + BayPositionUsage["LINE2_B_26"] + "','" + BayPositionUsage["LINE2_B_27"] + "','" + BayPositionUsage["LINE2_B_28"] + "','" + BayPositionUsage["LINE2_B_29"] + "','" + BayPositionUsage["LINE2_B_30"] + "','" + BayPositionUsage["LINE2_B_31"] + "','" + BayPositionUsage["LINE2_B_32"] + "','" + BayPositionUsage["LINE2_B_33"] + "','" + BayPositionUsage["LINE2_B_34"] + "','" + BayPositionUsage["LINE2_B_35"] + "','" + BayPositionUsage["LINE2_B_36"] + "','" + BayPositionUsage["LINE3_Lower_02"] + "','" + BayPositionUsage["LINE3_Lower_04"] + "','" + BayPositionUsage["LINE3_Lower_06"] + "','" + BayPositionUsage["LINE3_Lower_08"] + "','" + BayPositionUsage["LINE3_Lower_10"] + "','" + BayPositionUsage["LINE3_Lower_12"] + "','" + BayPositionUsage["LINE3_Lower_14"] + "','" + BayPositionUsage["LINE3_Lower_16"] + "','" + BayPositionUsage["LINE3_Lower_18"] + "','" + BayPositionUsage["LINE3_Lower_20"] + "','" + BayPositionUsage["LINE3_Lower_22"] + "','" + BayPositionUsage["LINE3_Lower_24"] + "','" + BayPositionUsage["LINE3_Lower_26"] + "','" + BayPositionUsage["LINE3_Lower_28"] + "','" + BayPositionUsage["LINE3_Lower_30"] + "','" + BayPositionUsage["LINE3_Lower_32"] + "','" + BayPositionUsage["LINE3_Lower_34"] + "','" + BayPositionUsage["LINE3_Lower_36"] + "','" + BayPositionUsage["LINE3_Lower_38"] + "','" + BayPositionUsage["LINE3_Lower_40"] + "','" + BayPositionUsage["LINE3_Lower_42"] + "','" + BayPositionUsage["LINE3_Lower_44"] + "','" + BayPositionUsage["LINE3_Lower_46"] + "','" + BayPositionUsage["LINE3_Lower_48"] + "','" + BayPositionUsage["LINE3_Lower_50"] + "','" + BayPositionUsage["LINE3_Lower_52"] + "','" + BayPositionUsage["LINE3_Lower_54"] + "','" + BayPositionUsage["LINE3_Lower_56"] + "','" + BayPositionUsage["LINE3_Lower_58"] + "','" + BayPositionUsage["LINE3_Lower_60"] + "','" + BayPositionUsage["LINE3_Lower_62"] + "','" + BayPositionUsage["LINE3_Lower_64"] + "','" + BayPositionUsage["LINE3_Lower_66"] + "','" + BayPositionUsage["LINE3_Lower_68"] + "','" + BayPositionUsage["LINE3_Lower_70"] + "','" + BayPositionUsage["LINE3_Lower_72"] + "','" + BayPositionUsage["LINE3_Upper_01"] + "','" + BayPositionUsage["LINE3_Upper_03"] + "','" + BayPositionUsage["LINE3_Upper_05"] + "','" + BayPositionUsage["LINE3_Upper_07"] + "','" + BayPositionUsage["LINE3_Upper_09"] + "','" + BayPositionUsage["LINE3_Upper_11"] + "','" + BayPositionUsage["LINE3_Upper_13"] + "','" + BayPositionUsage["LINE3_Upper_15"] + "','" + BayPositionUsage["LINE3_Upper_17"] + "','" + BayPositionUsage["LINE3_Upper_19"] + "','" + BayPositionUsage["LINE3_Upper_21"] + "','" + BayPositionUsage["LINE3_Upper_23"] + "','" + BayPositionUsage["LINE3_Upper_25"] + "','" + BayPositionUsage["LINE3_Upper_27"] + "','" + BayPositionUsage["LINE3_Upper_29"] + "','" + BayPositionUsage["LINE3_Upper_31"] + "','" + BayPositionUsage["LINE3_Upper_33"] + "','" + BayPositionUsage["LINE3_Upper_35"] + "','" + BayPositionUsage["LINE3_Upper_37"] + "','" + BayPositionUsage["LINE3_Upper_39"] + "','" + BayPositionUsage["LINE3_Upper_41"] + "','" + BayPositionUsage["LINE3_Upper_43"] + "','" + BayPositionUsage["LINE3_Upper_45"] + "','" + BayPositionUsage["LINE3_Upper_47"] + "','" + BayPositionUsage["LINE3_Upper_49"] + "','" + BayPositionUsage["LINE3_Upper_51"] + "','" + BayPositionUsage["LINE3_Upper_53"] + "','" + BayPositionUsage["LINE3_Upper_55"] + "','" + BayPositionUsage["LINE3_Upper_57"] + "','" + BayPositionUsage["LINE3_Upper_59"] + "','" + BayPositionUsage["LINE3_Upper_61"] + "','" + BayPositionUsage["LINE3_Upper_63"] + "','" + BayPositionUsage["LINE3_Upper_65"] + "','" + BayPositionUsage["LINE3_Upper_67"] + "','" + BayPositionUsage["LINE3_Upper_69"] + "','" + BayPositionUsage["LINE3_Upper_71"] + "','" + BayPositionUsage["LINE4_Lower_02"] + "','" + BayPositionUsage["LINE4_Lower_04"] + "','" + BayPositionUsage["LINE4_Lower_06"] + "','" + BayPositionUsage["LINE4_Lower_08"] + "','" + BayPositionUsage["LINE4_Lower_10"] + "','" + BayPositionUsage["LINE4_Lower_12"] + "','" + BayPositionUsage["LINE4_Lower_14"] + "','" + BayPositionUsage["LINE4_Lower_16"] + "','" + BayPositionUsage["LINE4_Lower_18"] + "','" + BayPositionUsage["LINE4_Lower_20"] + "','" + BayPositionUsage["LINE4_Lower_22"] + "','" + BayPositionUsage["LINE4_Lower_24"] + "','" + BayPositionUsage["LINE4_Lower_26"] + "','" + BayPositionUsage["LINE4_Lower_28"] + "','" + BayPositionUsage["LINE4_Lower_30"] + "','" + BayPositionUsage["LINE4_Lower_32"] + "','" + BayPositionUsage["LINE4_Lower_34"] + "','" + BayPositionUsage["LINE4_Lower_36"] + "','" + BayPositionUsage["LINE4_Lower_38"] + "','" + BayPositionUsage["LINE4_Lower_40"] + "','" + BayPositionUsage["LINE4_Lower_42"] + "','" + BayPositionUsage["LINE4_Lower_44"] + "','" + BayPositionUsage["LINE4_Lower_46"] + "','" + BayPositionUsage["LINE4_Lower_48"] + "','" + BayPositionUsage["LINE4_Lower_50"] + "','" + BayPositionUsage["LINE4_Lower_52"] + "','" + BayPositionUsage["LINE4_Lower_54"] + "','" + BayPositionUsage["LINE4_Lower_56"] + "','" + BayPositionUsage["LINE4_Lower_58"] + "','" + BayPositionUsage["LINE4_Lower_60"] + "','" + BayPositionUsage["LINE4_Lower_62"] + "','" + BayPositionUsage["LINE4_Lower_64"] + "','" + BayPositionUsage["LINE4_Lower_66"] + "','" + BayPositionUsage["LINE4_Lower_68"] + "','" + BayPositionUsage["LINE4_Lower_70"] + "','" + BayPositionUsage["LINE4_Lower_72"] + "','" + BayPositionUsage["LINE4_Upper_01"] + "','" + BayPositionUsage["LINE4_Upper_03"] + "','" + BayPositionUsage["LINE4_Upper_05"] + "','" + BayPositionUsage["LINE4_Upper_07"] + "','" + BayPositionUsage["LINE4_Upper_09"] + "','" + BayPositionUsage["LINE4_Upper_11"] + "','" + BayPositionUsage["LINE4_Upper_13"] + "','" + BayPositionUsage["LINE4_Upper_15"] + "','" + BayPositionUsage["LINE4_Upper_17"] + "','" + BayPositionUsage["LINE4_Upper_19"] + "','" + BayPositionUsage["LINE4_Upper_21"] + "','" + BayPositionUsage["LINE4_Upper_23"] + "','" + BayPositionUsage["LINE4_Upper_25"] + "','" + BayPositionUsage["LINE4_Upper_27"] + "','" + BayPositionUsage["LINE4_Upper_29"] + "','" + BayPositionUsage["LINE4_Upper_31"] + "','" + BayPositionUsage["LINE4_Upper_33"] + "','" + BayPositionUsage["LINE4_Upper_35"] + "','" + BayPositionUsage["LINE4_Upper_37"] + "','" + BayPositionUsage["LINE4_Upper_39"] + "','" + BayPositionUsage["LINE4_Upper_41"] + "','" + BayPositionUsage["LINE4_Upper_43"] + "','" + BayPositionUsage["LINE4_Upper_45"] + "','" + BayPositionUsage["LINE4_Upper_47"] + "','" + BayPositionUsage["LINE4_Upper_49"] + "','" + BayPositionUsage["LINE4_Upper_51"] + "','" + BayPositionUsage["LINE4_Upper_53"] + "','" + BayPositionUsage["LINE4_Upper_55"] + "','" + BayPositionUsage["LINE4_Upper_57"] + "','" + BayPositionUsage["LINE4_Upper_59"] + "','" + BayPositionUsage["LINE4_Upper_61"] + "','" + BayPositionUsage["LINE4_Upper_63"] + "','" + BayPositionUsage["LINE4_Upper_65"] + "','" + BayPositionUsage["LINE4_Upper_67"] + "','" + BayPositionUsage["LINE4_Upper_69"] + "','" + BayPositionUsage["LINE4_Upper_71"] + "','" + BayPositionUsage["LINE5_Lower_02"] + "','" + BayPositionUsage["LINE5_Lower_04"] + "','" + BayPositionUsage["LINE5_Lower_06"] + "','" + BayPositionUsage["LINE5_Lower_08"] + "','" + BayPositionUsage["LINE5_Lower_10"] + "','" + BayPositionUsage["LINE5_Lower_12"] + "','" + BayPositionUsage["LINE5_Lower_14"] + "','" + BayPositionUsage["LINE5_Lower_16"] + "','" + BayPositionUsage["LINE5_Lower_18"] + "','" + BayPositionUsage["LINE5_Lower_20"] + "','" + BayPositionUsage["LINE5_Lower_22"] + "','" + BayPositionUsage["LINE5_Lower_24"] + "','" + BayPositionUsage["LINE5_Lower_26"] + "','" + BayPositionUsage["LINE5_Lower_28"] + "','" + BayPositionUsage["LINE5_Lower_30"] + "','" + BayPositionUsage["LINE5_Lower_32"] + "','" + BayPositionUsage["LINE5_Lower_34"] + "','" + BayPositionUsage["LINE5_Lower_36"] + "','" + BayPositionUsage["LINE5_Lower_38"] + "','" + BayPositionUsage["LINE5_Lower_40"] + "','" + BayPositionUsage["LINE5_Lower_42"] + "','" + BayPositionUsage["LINE5_Lower_44"] + "','" + BayPositionUsage["LINE5_Lower_46"] + "','" + BayPositionUsage["LINE5_Lower_48"] + "','" + BayPositionUsage["LINE5_Lower_50"] + "','" + BayPositionUsage["LINE5_Lower_52"] + "','" + BayPositionUsage["LINE5_Lower_54"] + "','" + BayPositionUsage["LINE5_Lower_56"] + "','" + BayPositionUsage["LINE5_Lower_58"] + "','" + BayPositionUsage["LINE5_Lower_60"] + "','" + BayPositionUsage["LINE5_Lower_62"] + "','" + BayPositionUsage["LINE5_Lower_64"] + "','" + BayPositionUsage["LINE5_Lower_66"] + "','" + BayPositionUsage["LINE5_Lower_68"] + "','" + BayPositionUsage["LINE5_Lower_70"] + "','" + BayPositionUsage["LINE5_Lower_72"] + "','" + BayPositionUsage["LINE5_Lower_74"] + "','" + BayPositionUsage["LINE5_Lower_76"] + "','" + BayPositionUsage["LINE5_Lower_78"] + "','" + BayPositionUsage["LINE5_Lower_80"] + "','" + BayPositionUsage["LINE5_Upper_01"] + "','" + BayPositionUsage["LINE5_Upper_03"] + "','" + BayPositionUsage["LINE5_Upper_05"] + "','" + BayPositionUsage["LINE5_Upper_07"] + "','" + BayPositionUsage["LINE5_Upper_09"] + "','" + BayPositionUsage["LINE5_Upper_11"] + "','" + BayPositionUsage["LINE5_Upper_13"] + "','" + BayPositionUsage["LINE5_Upper_15"] + "','" + BayPositionUsage["LINE5_Upper_17"] + "','" + BayPositionUsage["LINE5_Upper_19"] + "','" + BayPositionUsage["LINE5_Upper_21"] + "','" + BayPositionUsage["LINE5_Upper_23"] + "','" + BayPositionUsage["LINE5_Upper_25"] + "','" + BayPositionUsage["LINE5_Upper_27"] + "','" + BayPositionUsage["LINE5_Upper_29"] + "','" + BayPositionUsage["LINE5_Upper_31"] + "','" + BayPositionUsage["LINE5_Upper_33"] + "','" + BayPositionUsage["LINE5_Upper_35"] + "','" + BayPositionUsage["LINE5_Upper_37"] + "','" + BayPositionUsage["LINE5_Upper_39"] + "','" + BayPositionUsage["LINE5_Upper_41"] + "','" + BayPositionUsage["LINE5_Upper_43"] + "','" + BayPositionUsage["LINE5_Upper_45"] + "','" + BayPositionUsage["LINE5_Upper_47"] + "','" + BayPositionUsage["LINE5_Upper_49"] + "','" + BayPositionUsage["LINE5_Upper_51"] + "','" + BayPositionUsage["LINE5_Upper_53"] + "','" + BayPositionUsage["LINE5_Upper_55"] + "','" + BayPositionUsage["LINE5_Upper_57"] + "','" + BayPositionUsage["LINE5_Upper_59"] + "','" + BayPositionUsage["LINE5_Upper_61"] + "','" + BayPositionUsage["LINE5_Upper_63"] + "','" + BayPositionUsage["LINE5_Upper_65"] + "','" + BayPositionUsage["LINE5_Upper_67"] + "','" + BayPositionUsage["LINE5_Upper_69"] + "','" + BayPositionUsage["LINE5_Upper_71"] + "','" + BayPositionUsage["LINE5_Upper_73"] + "','" + BayPositionUsage["LINE5_Upper_75"] + "','" + BayPositionUsage["LINE5_Upper_77"] + "','" + BayPositionUsage["LINE5_Upper_79"] + "','" + BayPositionUsage["LINE6_Lower_02"] + "','" + BayPositionUsage["LINE6_Lower_04"] + "','" + BayPositionUsage["LINE6_Lower_06"] + "','" + BayPositionUsage["LINE6_Lower_08"] + "','" + BayPositionUsage["LINE6_Lower_10"] + "','" + BayPositionUsage["LINE6_Lower_12"] + "','" + BayPositionUsage["LINE6_Lower_14"] + "','" + BayPositionUsage["LINE6_Lower_16"] + "','" + BayPositionUsage["LINE6_Lower_18"] + "','" + BayPositionUsage["LINE6_Lower_20"] + "','" + BayPositionUsage["LINE6_Lower_22"] + "','" + BayPositionUsage["LINE6_Lower_24"] + "','" + BayPositionUsage["LINE6_Lower_26"] + "','" + BayPositionUsage["LINE6_Lower_28"] + "','" + BayPositionUsage["LINE6_Lower_30"] + "','" + BayPositionUsage["LINE6_Lower_32"] + "','" + BayPositionUsage["LINE6_Lower_34"] + "','" + BayPositionUsage["LINE6_Lower_36"] + "','" + BayPositionUsage["LINE6_Lower_38"] + "','" + BayPositionUsage["LINE6_Lower_40"] + "','" + BayPositionUsage["LINE6_Upper_01"] + "','" + BayPositionUsage["LINE6_Upper_03"] + "','" + BayPositionUsage["LINE6_Upper_05"] + "','" + BayPositionUsage["LINE6_Upper_07"] + "','" + BayPositionUsage["LINE6_Upper_09"] + "','" + BayPositionUsage["LINE6_Upper_11"] + "','" + BayPositionUsage["LINE6_Upper_13"] + "','" + BayPositionUsage["LINE6_Upper_15"] + "','" + BayPositionUsage["LINE6_Upper_17"] + "','" + BayPositionUsage["LINE6_Upper_19"] + "','" + BayPositionUsage["LINE6_Upper_21"] + "','" + BayPositionUsage["LINE6_Upper_23"] + "','" + BayPositionUsage["LINE6_Upper_25"] + "','" + BayPositionUsage["LINE6_Upper_27"] + "','" + BayPositionUsage["LINE6_Upper_29"] + "','" + BayPositionUsage["LINE6_Upper_31"] + "','" + BayPositionUsage["LINE6_Upper_33"] + "','" + BayPositionUsage["LINE6_Upper_35"] + "','" + BayPositionUsage["LINE6_Upper_37"] + "','" + BayPositionUsage["LINE6_Upper_39"] + "','" + BayPositionUsage["LINE7_Lower_02"] + "','" + BayPositionUsage["LINE7_Lower_04"] + "','" + BayPositionUsage["LINE7_Lower_06"] + "','" + BayPositionUsage["LINE7_Lower_08"] + "','" + BayPositionUsage["LINE7_Lower_10"] + "','" + BayPositionUsage["LINE7_Lower_12"] + "','" + BayPositionUsage["LINE7_Lower_14"] + "','" + BayPositionUsage["LINE7_Lower_16"] + "','" + BayPositionUsage["LINE7_Lower_18"] + "','" + BayPositionUsage["LINE7_Lower_20"] + "','" + BayPositionUsage["LINE7_Lower_22"] + "','" + BayPositionUsage["LINE7_Lower_24"] + "','" + BayPositionUsage["LINE7_Lower_26"] + "','" + BayPositionUsage["LINE7_Lower_28"] + "','" + BayPositionUsage["LINE7_Lower_30"] + "','" + BayPositionUsage["LINE7_Lower_32"] + "','" + BayPositionUsage["LINE7_Lower_34"] + "','" + BayPositionUsage["LINE7_Lower_36"] + "','" + BayPositionUsage["LINE7_Lower_38"] + "','" + BayPositionUsage["LINE7_Lower_40"] + "','" + BayPositionUsage["LINE7_Lower_50"] + "','" + BayPositionUsage["LINE7_Lower_52"] + "','" + BayPositionUsage["LINE7_Lower_54"] + "','" + BayPositionUsage["LINE7_Lower_56"] + "','" + BayPositionUsage["LINE7_Lower_58"] + "','" + BayPositionUsage["LINE7_Lower_60"] + "','" + BayPositionUsage["LINE7_Lower_62"] + "','" + BayPositionUsage["LINE7_Lower_64"] + "','" + BayPositionUsage["LINE7_Lower_66"] + "','" + BayPositionUsage["LINE7_Lower_68"] + "','" + BayPositionUsage["LINE7_Lower_70"] + "','" + BayPositionUsage["LINE7_Lower_72"] + "','" + BayPositionUsage["LINE7_Upper_01"] + "','" + BayPositionUsage["LINE7_Upper_03"] + "','" + BayPositionUsage["LINE7_Upper_05"] + "','" + BayPositionUsage["LINE7_Upper_07"] + "','" + BayPositionUsage["LINE7_Upper_09"] + "','" + BayPositionUsage["LINE7_Upper_11"] + "','" + BayPositionUsage["LINE7_Upper_13"] + "','" + BayPositionUsage["LINE7_Upper_15"] + "','" + BayPositionUsage["LINE7_Upper_17"] + "','" + BayPositionUsage["LINE7_Upper_19"] + "','" + BayPositionUsage["LINE7_Upper_21"] + "','" + BayPositionUsage["LINE7_Upper_23"] + "','" + BayPositionUsage["LINE7_Upper_25"] + "','" + BayPositionUsage["LINE7_Upper_27"] + "','" + BayPositionUsage["LINE7_Upper_29"] + "','" + BayPositionUsage["LINE7_Upper_31"] + "','" + BayPositionUsage["LINE7_Upper_33"] + "','" + BayPositionUsage["LINE7_Upper_35"] + "','" + BayPositionUsage["LINE7_Upper_37"] + "','" + BayPositionUsage["LINE7_Upper_39"] + "','" + BayPositionUsage["LINE7_Upper_49"] + "','" + BayPositionUsage["LINE7_Upper_51"] + "','" + BayPositionUsage["LINE7_Upper_53"] + "','" + BayPositionUsage["LINE7_Upper_55"] + "','" + BayPositionUsage["LINE7_Upper_57"] + "','" + BayPositionUsage["LINE7_Upper_59"] + "','" + BayPositionUsage["LINE7_Upper_61"] + "','" + BayPositionUsage["LINE7_Upper_63"] + "','" + BayPositionUsage["LINE7_Upper_65"] + "','" + BayPositionUsage["LINE7_Upper_67"] + "','" + BayPositionUsage["LINE7_Upper_69"] + "','" + BayPositionUsage["LINE7_Upper_71"] + "','" + BayPositionUsage["LINE8_Lower_02"] + "','" + BayPositionUsage["LINE8_Lower_04"] + "','" + BayPositionUsage["LINE8_Lower_06"] + "','" + BayPositionUsage["LINE8_Lower_08"] + "','" + BayPositionUsage["LINE8_Lower_10"] + "','" + BayPositionUsage["LINE8_Lower_12"] + "','" + BayPositionUsage["LINE8_Lower_14"] + "','" + BayPositionUsage["LINE8_Lower_16"] + "','" + BayPositionUsage["LINE8_Lower_18"] + "','" + BayPositionUsage["LINE8_Lower_20"] + "','" + BayPositionUsage["LINE8_Lower_22"] + "','" + BayPositionUsage["LINE8_Lower_24"] + "','" + BayPositionUsage["LINE8_Lower_26"] + "','" + BayPositionUsage["LINE8_Lower_28"] + "','" + BayPositionUsage["LINE8_Lower_30"] + "','" + BayPositionUsage["LINE8_Lower_32"] + "','" + BayPositionUsage["LINE8_Lower_34"] + "','" + BayPositionUsage["LINE8_Lower_36"] + "','" + BayPositionUsage["LINE8_Lower_38"] + "','" + BayPositionUsage["LINE8_Lower_40"] + "','" + BayPositionUsage["LINE8_Lower_50"] + "','" + BayPositionUsage["LINE8_Lower_52"] + "','" + BayPositionUsage["LINE8_Lower_54"] + "','" + BayPositionUsage["LINE8_Lower_56"] + "','" + BayPositionUsage["LINE8_Lower_58"] + "','" + BayPositionUsage["LINE8_Lower_60"] + "','" + BayPositionUsage["LINE8_Lower_62"] + "','" + BayPositionUsage["LINE8_Lower_64"] + "','" + BayPositionUsage["LINE8_Lower_66"] + "','" + BayPositionUsage["LINE8_Lower_68"] + "','" + BayPositionUsage["LINE8_Lower_70"] + "','" + BayPositionUsage["LINE8_Lower_72"] + "','" + BayPositionUsage["LINE8_Upper_01"] + "','" + BayPositionUsage["LINE8_Upper_03"] + "','" + BayPositionUsage["LINE8_Upper_05"] + "','" + BayPositionUsage["LINE8_Upper_07"] + "','" + BayPositionUsage["LINE8_Upper_09"] + "','" + BayPositionUsage["LINE8_Upper_11"] + "','" + BayPositionUsage["LINE8_Upper_13"] + "','" + BayPositionUsage["LINE8_Upper_15"] + "','" + BayPositionUsage["LINE8_Upper_17"] + "','" + BayPositionUsage["LINE8_Upper_19"] + "','" + BayPositionUsage["LINE8_Upper_21"] + "','" + BayPositionUsage["LINE8_Upper_23"] + "','" + BayPositionUsage["LINE8_Upper_25"] + "','" + BayPositionUsage["LINE8_Upper_27"] + "','" + BayPositionUsage["LINE8_Upper_29"] + "','" + BayPositionUsage["LINE8_Upper_31"] + "','" + BayPositionUsage["LINE8_Upper_33"] + "','" + BayPositionUsage["LINE8_Upper_35"] + "','" + BayPositionUsage["LINE8_Upper_37"] + "','" + BayPositionUsage["LINE8_Upper_39"] + "','" + BayPositionUsage["LINE8_Upper_49"] + "','" + BayPositionUsage["LINE8_Upper_51"] + "','" + BayPositionUsage["LINE8_Upper_53"] + "','" + BayPositionUsage["LINE8_Upper_55"] + "','" + BayPositionUsage["LINE8_Upper_57"] + "','" + BayPositionUsage["LINE8_Upper_59"] + "','" + BayPositionUsage["LINE8_Upper_61"] + "','" + BayPositionUsage["LINE8_Upper_63"] + "','" + BayPositionUsage["LINE8_Upper_65"] + "','" + BayPositionUsage["LINE8_Upper_67"] + "','" + BayPositionUsage["LINE8_Upper_69"] + "','" + BayPositionUsage["LINE8_Upper_71"] + "','" + BayPositionUsage["LINEDMR_Lower_02"] + "','" + BayPositionUsage["LINEDMR_Lower_04"] + "','" + BayPositionUsage["LINEDMR_Lower_06"] + "','" + BayPositionUsage["LINEDMR_Lower_08"] + "','" + BayPositionUsage["LINEDMR_Lower_10"] + "','" + BayPositionUsage["LINEDMR_Lower_12"] + "','" + BayPositionUsage["LINEDMR_Lower_14"] + "','" + BayPositionUsage["LINEDMR_Lower_16"] + "','" + BayPositionUsage["LINEDMR_Upper_01"] + "','" + BayPositionUsage["LINEDMR_Upper_03"] + "','" + BayPositionUsage["LINEDMR_Upper_05"] + "','" + BayPositionUsage["LINEDMR_Upper_07"] + "','" + BayPositionUsage["LINEOBA_Lower_02"] + "','" + BayPositionUsage["LINEOBA_Lower_04"] + "','" + BayPositionUsage["LINEOBA_Lower_06"] + "','" + BayPositionUsage["LINEOBA_Lower_08"] + "','" + BayPositionUsage["LINEOBA_Lower_10"] + "','" + BayPositionUsage["LINEOBA_Lower_12"] + "','" + BayPositionUsage["LINEOBA_Lower_14"] + "','" + BayPositionUsage["LINEOBA_Lower_16"] + "','" + BayPositionUsage["LINEOBA_Upper_01"] + "','" + BayPositionUsage["LINEOBA_Upper_03"] + "','" + BayPositionUsage["LINEOBA_Upper_05"] + "','" + BayPositionUsage["LINEOBA_Upper_07"] + "','" + BayPositionUsage["LINEOBA_Upper_09"] + "','" + BayPositionUsage["LINEOBA_Upper_11"] + "','" + BayPositionUsage["LINEOBA_Upper_13"] + "','" + BayPositionUsage["LINEOBA_Upper_15"] + "','" + BayPositionUsage["LINEPRI_Lower_10"] + "','" + BayPositionUsage["LINEPRI_Lower_12"] + "','" + BayPositionUsage["LINEPRI_Lower_14"] + "','" + BayPositionUsage["LINEPRI_Lower_16"] + "','" + BayPositionUsage["LINEPRI_Lower_18"] + "','" + BayPositionUsage["LINEPRI_Lower_20"] + "','" + BayPositionUsage["LINEPRI_Lower_22"] + "','" + BayPositionUsage["LINEPRI_Lower_24"] + "','" + BayPositionUsage["LINEPRI_Upper_09"] + "','" + BayPositionUsage["LINEPRI_Upper_11"] + "','" + BayPositionUsage["LINEPRI_Upper_13"] + "','" + BayPositionUsage["LINEPRI_Upper_15"] + "','" + BayPositionUsage["LINEPRI_Upper_17"] + "','" + BayPositionUsage["LINEPRI_Upper_19"] + "','" + BayPositionUsage["LINEPRI_Upper_21"] + "','" + BayPositionUsage["LINEPRI_Upper_23"] + "',getdate()", myConnection))
                {
                    myConnection.Open();
                    Command.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public void InsertSaveSN(string userName, string sno, string note)
        {
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command = new SqlCommand("insert into ICZ_LOCAL..TestTrack_SavedSn select '" + userName + "','" + sno + "','" + note + "','Delete " + sno + "',getdate()", myConnection))
                {
                    myConnection.Open();
                    Command.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
