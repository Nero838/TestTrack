using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using LiveCharts;
namespace MC1
{
    class BayProportionGraph
    {
        public DataTable BayProportionTable;

        public BayProportionGraph()
        {
            MakeTable();
        }

        private void MakeTable()
        {
            BayProportionTable = new DataTable("BayProportion");
            DataColumn column;
            //DataRow row;

            //column = new DataColumn();
            //column.DataType = System.Type.GetType("System.Int32");
            //column.ColumnName = "ID";
            //column.ReadOnly = true;
            //column.Unique = false;
            //BayLoadTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Time";
            column.ReadOnly = true;
            column.Unique = false;
            BayProportionTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Booting";
            column.ReadOnly = true;
            column.Unique = false;
            BayProportionTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Ini";
            column.ReadOnly = true;
            column.Unique = false;
            BayProportionTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "RunIn";
            column.ReadOnly = true;
            column.Unique = false;
            BayProportionTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Problem";
            column.ReadOnly = true;
            column.Unique = false;
            BayProportionTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Fail";
            column.ReadOnly = true;
            column.Unique = false;
            BayProportionTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Pass";
            column.ReadOnly = true;
            column.Unique = false;
            BayProportionTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Adler";
            column.ReadOnly = true;
            column.Unique = false;
            BayProportionTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Online";
            column.ReadOnly = true;
            column.Unique = false;
            BayProportionTable.Columns.Add(column);

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = BayProportionTable.Columns["ID"];
            BayProportionTable.PrimaryKey = PrimaryKeyColumns;

            //test 

            //row = BayLoadTable.NewRow();
            //row["ID"] = 1;
            //row["Time"] = "test1";
            //row["Load"] = 25;
            //BayLoadTable.Rows.Add(row);

            //row = BayLoadTable.NewRow();
            //row["ID"] = 2;
            //row["Time"] = "test2";
            //row["Load"] = 30;
            //BayLoadTable.Rows.Add(row);

            //row = BayLoadTable.NewRow();
            //row["ID"] = 3;
            //row["Time"] = "test3";
            //row["Load"] = 28;
            //BayLoadTable.Rows.Add(row);

        }

        public void AddRow(string time, int booting, int ini, int runin, int problem, int fail, int pass, int adler, int online)
        {

            DataRow row = BayProportionTable.NewRow();
            //row["ID"] = id;
            row["Time"] = time;
            row["Booting"] = booting;
            row["Ini"] = ini;
            row["RunIn"] = runin;
            row["Problem"] = problem;
            row["Fail"] = fail;
            row["Pass"] = pass;
            row["Adler"] = adler;
            row["Online"] = online;
            BayProportionTable.Rows.Add(row);
        }
    }
}
