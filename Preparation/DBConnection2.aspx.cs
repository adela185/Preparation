using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class DBConnection2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB2"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Select id, [name] from test_users", connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //while (reader.Read())
                        //    Response.Write(reader["name"].ToString() + "<br />");
                        ddlUsers.DataSource = reader;
                        ddlUsers.DataBind();
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                Response.Write("An Error Has Occured: " + ex.Message);
            }
        }
    }
}