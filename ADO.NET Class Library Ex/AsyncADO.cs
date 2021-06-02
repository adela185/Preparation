using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Class_Library_Ex
{
    public class AsyncADO
    {
        private async Task<SqlConnection> GetConnectionAsync(string constr)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[constr].ConnectionString);
            await con.OpenAsync();
            return con;
        }

        public async Task<DataSet> GetDataAsync(string constr, Dictionary<string, SqlParameter> parmeters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = await GetConnectionAsync(constr))
            {
                using(SqlCommand cmd = new SqlCommand("sp_Color_Get", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter param in parmeters.Values)
                    {
                        cmd.Parameters.Add(param);
                    }

                    await Task.Run(() =>
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    });
                }

                //con.Close(); Don't Know If I Should Close Async Connections 
            }
            return ds;
        }

        public async Task<string> InsertColorUsingProviderModel(string constr, Dictionary<string, SqlParameter> parameters)
        {
            int rowsModded = 0;

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings[constr].ConnectionString;
                await connection.OpenAsync();

                using (DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Color_Add";
                    foreach (SqlParameter param in parameters.Values)
                    {
                        cmd.Parameters.Add(param);
                    }

                    rowsModded = await cmd.ExecuteNonQueryAsync();
                }
                //Might be useful:
                //using (DbDataReader reader = await command.ExecuteReaderAsync())
                //{
                //    while (await reader.ReadAsync())
                //    {
                //        for (int i = 0; i < reader.FieldCount; i++)
                //        {
                //            // Process each column as appropriate
                //            object obj = await reader.GetFieldValueAsync<object>(i);
                //            Console.WriteLine(obj);
                //        }
                //    }
                //}

                //connection.Close() I'm not sure as to doing this is correct for asynchronous open connections. Should read the theory more
            }

            return $"Modified Rows: {rowsModded}";
        }
    }
}
