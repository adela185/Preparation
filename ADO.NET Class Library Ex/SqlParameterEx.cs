using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Class_Library_Ex
{
    public class SqlParameterEx
    {
        static string constr = ConfigurationManager.ConnectionStrings["TestDBConnectionString"].ConnectionString;

        public static void AnotherAdd()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Color Values(@testName, @testHex)", con);

                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@testName";
                nameParam.SqlDbType = SqlDbType.NVarChar;
                nameParam.Size = 50;
                nameParam.Direction = ParameterDirection.Input;

                SqlParameter hexParam = new SqlParameter("@testHex", SqlDbType.NChar, 6, null);
                hexParam.Direction = ParameterDirection.Input;

                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(hexParam);

                nameParam.Value = "Test";
                hexParam.Value = "000000";

                cmd.ExecuteNonQuery();
            }
        }

        public static void AnotherGet()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select @testName=[name], @testHex=hex From Color Where [name] = @testName", con);

                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@testName";
                nameParam.SqlDbType = SqlDbType.NVarChar;
                nameParam.Size = 50;
                nameParam.Direction = ParameterDirection.Input;

                SqlParameter hexParam = new SqlParameter("@testHex", SqlDbType.NChar, 6, null);
                hexParam.Direction = ParameterDirection.Output; //This is returned

                cmd.Parameters.Add(nameParam);
                cmd.Parameters.Add(hexParam);

                nameParam.Value = "Blue";
                hexParam.Value = "000000";

                cmd.ExecuteNonQuery();

                string hex = hexParam.Value.ToString();
            }

        }
    }
}
