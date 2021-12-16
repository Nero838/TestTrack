using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MC1
{
    public class SQL_Basic
    {
        private string connectionString;
        public SQL_Basic()
        {
            connectionString = "Data Source=icz3sql;Initial Catalog=CCD;Persist Security Info=True;User ID=fisread;Password=readfis";
        }

        public string GetLineaheadPassword(string ip, string userName)
        {
            string output = "Database output error";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                using (SqlCommand Command_Get = new SqlCommand("SELECT Password FROM ICZ_LOCAL..FILE_STOCK_LOCAL WHERE Host = '" + ip + "' AND UserName = '" + userName + "'", myConnection))
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
    }
}
