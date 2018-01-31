using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace GuiUserSmartParcking.Data
{
    class Dal
    {
        private string StringConnection;
        private SqlConnection connection;

        public Dal()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["GuiUserSmartParcking.Properties.Settings.ParckingDBConnectionString"].ConnectionString;
            connection = new SqlConnection(StringConnection);
        }

        public DataTable GetDataTable(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds.Tables[0];
        }

        public void DeleteInsertUpdate(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
