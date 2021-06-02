using System;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET_Class_Library_Ex
{
    public class ColorDBAccessLayer
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-M6HVHICA\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=true");

        public string AddColorRecord(Color color)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Color_Add", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nm", color.nm);
                cmd.Parameters.AddWithValue("@hex", color.hex);
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return "Data Saved Succesfully";
            }
            catch (SqlException ex)
            {
                if(sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                return ex.Message.ToString();
            }
        }
    }
}
