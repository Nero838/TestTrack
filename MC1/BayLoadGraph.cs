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
    class BayLoadGraph
    {
        public DataTable BayLoadTable;

        public BayLoadGraph()
        {
            MakeTable();
        }

        private void MakeTable()
        {
            BayLoadTable = new DataTable("BayLoad");
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
            BayLoadTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Load [%]";
            column.ReadOnly = true;
            column.Unique = false;
            BayLoadTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Fails [%]";
            column.ReadOnly = true;
            column.Unique = false;
            BayLoadTable.Columns.Add(column);

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = BayLoadTable.Columns["ID"];
            BayLoadTable.PrimaryKey = PrimaryKeyColumns;

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

        public void AddRow(string time, int load, int fails)
        {
            if (load < 1000 || fails < 1000) 
            {
                DataRow row = BayLoadTable.NewRow();
                //row["ID"] = id;
                row["Time"] = time;
                row["Load [%]"] = load;
                row["Fails [%]"] = fails;
                BayLoadTable.Rows.Add(row);
            }
            else
            {

            }
        }
    }
}
