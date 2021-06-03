﻿using System;
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

                //adp.FillSchema(ds, SchemaType.Source, "Color");
                adp.Fill(ds, "Color");

                DataSet extraDS = new DataSet();
                extraDS.ReadXml(@"C:\Users\NosyT\source\repos\Preparation\ADO.NET Class Library Ex\Color.xml", XmlReadMode.InferSchema); //Should default to this anyway
                //extraDS.AcceptChanges();
                
                ds.Merge(extraDS, true/*, MissingSchemaAction.Ignore*/);

                

                //string debug = $"{ds.Tables["Color"].Rows[5].RowState}, {ds.Tables["Color"].Rows[5]["name", DataRowVersion.Original]}, {ds.Tables["Color"].Rows[5]["name", DataRowVersion.Current]}" +
                //    $" {ds.Tables["Color"].Rows[12].RowState}, {ds.Tables["Color"].Rows[12]["name", DataRowVersion.Original]}, {ds.Tables["Color"].Rows[12]["name", DataRowVersion.Original]}";
            }
            return ds;
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
                foreach (var pair in procParameters)
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
                }
                if(cn.State == ConnectionState.Open)
                    cn.Close();
            }
            return rc;
        }
    }
}
