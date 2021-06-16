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
    public class SqlDatabaseUtility
    {
        public SqlConnection GetConnection(string connectionName)
        {
            string cnstr = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            SqlConnection cn = new SqlConnection(cnstr);
            cn.Open();
            return cn;
        }

        public DataSet MergeEx()
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDBConnectionString"].ConnectionString))
            {
                SqlDataAdapter adp = new SqlDataAdapter("Select * From Color", con);
                con.Open();

                adp.InsertCommand = new SqlCommand();
                adp.InsertCommand.Connection = con;
                adp.InsertCommand.CommandText = "sp_Color_AddWithIdentity";
                adp.InsertCommand.CommandType = CommandType.StoredProcedure;
                adp.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "name"));
                adp.InsertCommand.Parameters.Add(new SqlParameter("@hex", SqlDbType.NChar, 6, "hex"));
                SqlParameter parameter = adp.InsertCommand.Parameters.Add("@Identity", SqlDbType.Int, 0, "colorId");
                parameter.Direction = ParameterDirection.Output;
                adp.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                adp.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                //adp.FillSchema(ds, SchemaType.Source, "Color");
                adp.Fill(ds, "Color");

                adp.RowUpdated += Adp_RowUpdated;

                DataSet extraDS = new DataSet();
                extraDS.ReadXml(@"C:\Users\NosyT\source\repos\Preparation\ADO.NET Class Library Ex\Color.xml", XmlReadMode.InferSchema);
                adp.Update(extraDS.Tables[0]);

                ds.Merge(extraDS, true, MissingSchemaAction.AddWithKey);

                

                //string debug = $"{ds.Tables["Color"].Rows[5].RowState}, {ds.Tables["Color"].Rows[5]["name", DataRowVersion.Original]}, {ds.Tables["Color"].Rows[5]["name", DataRowVersion.Current]}" +
                //    $" {ds.Tables["Color"].Rows[12].RowState}, {ds.Tables["Color"].Rows[12]["name", DataRowVersion.Original]}, {ds.Tables["Color"].Rows[12]["name", DataRowVersion.Original]}";
            }
            return ds;
        }

        private void Adp_RowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            // If this is an insert, then skip this row.
            if (e.StatementType == StatementType.Insert)
            {
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }

        public DataSet ExecuteQuery(string connectionName, string storedProcName, Dictionary<string, SqlParameter> procParameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = GetConnection(connectionName))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcName;
                    foreach (var procParameter in procParameters)
                    {
                        cmd.Parameters.Add(procParameter.Value);
                    }
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }

                if(cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return ds;
        }

        public int ExecuteCommandWithIdentity(string connectionName, Dictionary<string, SqlParameter> procParameters, string storedProcName = "sp_Color_AddWithIdentity")
        {
            int rc;
            using (SqlConnection cn = GetConnection(connectionName))
            {
                SqlDataAdapter adpt = new SqlDataAdapter("Select * From Color", cn);
                adpt.InsertCommand = new SqlCommand(storedProcName, cn);
                adpt.InsertCommand.CommandType = CommandType.StoredProcedure;

                adpt.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "name"));
                adpt.InsertCommand.Parameters.Add(new SqlParameter("@hex", SqlDbType.NChar, 6, "hex"));
                SqlParameter parameter = adpt.InsertCommand.Parameters.Add("@Identity", SqlDbType.Int, 0, "colorId");
                parameter.Direction = ParameterDirection.Output;

                DataTable table = new DataTable();
                adpt.Fill(table);
                    
                DataRow row = table.NewRow();
                foreach (KeyValuePair<string, SqlParameter> pair in procParameters)
                {
                    row[$"{pair.Key}"] = $"{pair.Value.Value}";
                }
                table.Rows.Add(row);

                rc = adpt.Update(table);

                adpt.Dispose();
            }
            return rc;
        }

        public int ExecuteCommand(string connectionName, string storedProcName, Dictionary<string, SqlParameter> procParameters)
        {
            int rc;
            using (SqlConnection cn = GetConnection(connectionName))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcName;
                    foreach (var procParameter in procParameters)
                    {
                        cmd.Parameters.Add(procParameter.Value);
                    }
                    rc = cmd.ExecuteNonQuery();

                    SqlParameter idParam = new SqlParameter();
                    bool status = procParameters.TryGetValue("colorId", out idParam);
                    if(status)
                        rc = (int)idParam.Value;
                }
                if(cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return rc;
        }
    }
}
