using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace ADO.NET_Class_Library_Ex
{
    /*
     * Let's Talk Isolation Levels (Degrading Performace):
     * Read Uncomitted: Dirty Reads, Lost Update, Non Repeatable Reads, Phantom Reads
     * Read Commited:   Lost Update, Non Repeatable Reads, Phantom Reads
     * Repeatable Read: Phantom Reads
     * Snapshot:        None
     * Serializable:    None
     * 
     * Concurrency Issues:
     * Dirty Reads          - Reading uncommited data after a change in that data (dirty data non-existent)
     * Lost Upadate         - Updating data with incorrect/stale values due to other transaction's update. Rows need to be locked for writes.
     * Non Repeatable Reads - Read data, then read it again after some time only to see it has been changed by another transaction
     * Phantom Reads        - Executing a query, then executing it again some time later only to see another transaction modified some data that matched the WHERE clause of the query
     * 
     * Serializable would range lock the transaction returns on the WHERE clause, so that another transaction can't insert based on WHERE
     * Snapshot doesn't range lock data returns, instead opting for versioning in a tempdb system database so that more concurrent transactions can run
     */
    public class TransactionEx
    {
        SqlTransaction _transaction = null;
        int rowsModded;

        public async Task<DataSet> DistributedGetDataAsync(string constr, string constr2, Dictionary<string, SqlParameter> parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings[constr].ConnectionString))
                using (SqlConnection _con2 = new SqlConnection(ConfigurationManager.ConnectionStrings[constr2].ConnectionString))
                {
                    using (CommittableTransaction transaction = new CommittableTransaction())
                    {
                        await _con.OpenAsync();
                        _con.EnlistTransaction(transaction);

                        await _con2.OpenAsync();
                        _con2.EnlistTransaction(transaction);

                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_Color_Get", _con))
                            using (SqlCommand cmd2 = new SqlCommand("Insert into Color values('Testy2', '000010')", _con2))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                foreach (SqlParameter param in parameters.Values)
                                {
                                    cmd.Parameters.Add(param);
                                }

                                await cmd2.ExecuteNonQueryAsync();
                                await Task.Run(() =>
                                {
                                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                    {
                                        da.Fill(ds);
                                    }
                                });

                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                ds.Tables.Add(ErrorTable($"{ex2.ToString()}: {ex2.Message}\nStack Trace: {ex2.StackTrace}"));
                                return ds;
                            }

                            ds.Tables.Add(ErrorTable($"{ex.ToString()}: {ex.Message}\nStack Trace: {ex.StackTrace}"));
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ds.Tables.Add(ErrorTable($"{ex.ToString()}: {ex.Message}\nStack Trace: {ex.StackTrace}"));
                return ds;
            }
            return ds;
        }

        private DataTable ErrorTable(string errorString)
        {
            DataTable erT = new DataTable("Error");
            DataColumn detC = new DataColumn("Details", Type.GetType("System.String"));

            detC.ReadOnly = true;
            erT.Columns.Add(detC);

            DataRow row = erT.NewRow();
            row["Details"] = errorString;
            erT.Rows.Add(row);              //erT.Rows.Add(errorString); or this works too I think

            return erT;
        }

        public async Task DeleteDBAsync(string contr, string id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[contr].ConnectionString))
            {
                await con.OpenAsync();
                using(SqlCommand cmd = new SqlCommand($"Delete From Color Where colorId = {id}", con))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateViaAdapter (string constr)
        {
            using (SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings[constr].ConnectionString))
            {
                await _con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("Select * From Color", _con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        //SqlCommandBuilder builder = new SqlCommandBuilder(da);
                        //string r = builder.GetInsertCommand().CommandText;
                        //"INSERT INTO [Color] ([name], [hex]) VALUES (@p1, @p2)"

                        //The CommandBuilder will build all that for every type of command, and probrably alot better too so just use that, this is just for learning purposes
                        da.InsertCommand = new SqlCommand("INSERT INTO [Color] ([name], [hex]) VALUES (@p1, @p2)", _con);
                        da.InsertCommand.Parameters.Add("@p1", SqlDbType.NVarChar, 50, "name");
                        da.InsertCommand.Parameters.Add("@p2", SqlDbType.NChar, 6, "hex");

                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        
                        ds.Tables[0].Rows.Add("This Color", "Adaptr");

                        //Can specify the order of updates, insert, deleting processing by:
                        DataRow[] result = ds.Tables[0].Select(null, null, DataViewRowState.Added); //The DataViewRowState defaults to CurrentRows; debug for these values

                        da.Update(ds.Tables[0]);
                    }
                }
            }

        }

        public async Task<string> InsertDBAsync(string constr, Dictionary<string, SqlParameter> parameters, CancellationToken cancellationToken)
        {
            using (SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings[constr].ConnectionString))
            {
                await _con.OpenAsync();
                _transaction = await Task.Run<SqlTransaction>(() => _con.BeginTransaction("InsertAsync"));

                using(SqlCommand _cmd = new SqlCommand("sp_Color_Add", _con, _transaction))
                {
                    _cmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter param in parameters.Values)
                    {
                        _cmd.Parameters.Add(param);
                    }

                    try
                    {
                        rowsModded = await _cmd.ExecuteNonQueryAsync();

                        await Task.Delay(10000);
                        cancellationToken.ThrowIfCancellationRequested();

                        _cmd.CommandText = "Insert into Color values ('Testy', '000005')";
                        _cmd.CommandType = CommandType.Text;
                        rowsModded += await _cmd.ExecuteNonQueryAsync();

                        await Task.Run(() => _transaction.Commit());
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            _transaction.Rollback("InsertColor");
                        }
                        catch (Exception ex2)
                        {
                            return ex2.ToString() + ": " + ex2.Message + "\nStack Trace: " + ex2.StackTrace;
                        }
                        return ex.ToString() + ": " + ex.Message + "\nStack Trace: " + ex.StackTrace;
                    }
                }
            }

            return $"Rows Modified: {rowsModded}";
        }

        public string InsertDB (string connectionString, Dictionary<string, SqlParameter> parameters)
        {
            using(SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
            {
                _con.Open();
                using(SqlCommand _cmd = new SqlCommand("sp_Color_Add", _con))
                {
                    try
                    {
                        //Isolation level defaults to readcommited, meaning shared locks held while data is read; data can still be changed before end of transaction.
                        _transaction = _con.BeginTransaction("InsertColor");    
                        _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        foreach (SqlParameter param in parameters.Values)
                        {
                            _cmd.Parameters.Add(param);
                        }
                        _cmd.Transaction = _transaction;
                        rowsModded = _cmd.ExecuteNonQuery();
                        _transaction.Commit();
                        _transaction.Dispose();
                    }
                    catch (Exception ex)
                    {
                        _transaction.Rollback("InsertColor");
                        if (_con.State == ConnectionState.Open)
                            _con.Close();
                        return ex.ToString() + ": " + ex.Message + "\nStack Trace: " + ex.StackTrace;
                    }
                }
                if (_con.State == ConnectionState.Open)
                    _con.Close();
            }

            return $"Rows Modified: {rowsModded}";
        }

        public DataSet GetData(string connectionString, Dictionary<string, SqlParameter> parameters)
        {
            DataSet ds = new DataSet();
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
            {
                con.Open();
                _transaction = con.BeginTransaction("GetData");
                using(SqlCommand cmd = new SqlCommand("sp_Color_Get", con, _transaction))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (SqlParameter param in parameters.Values)
                        {
                            cmd.Parameters.Add(param);
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                    catch (Exception ex)
                    {
                        _transaction.Rollback();
                        con.Close();
                        return null;
                    }
                }
                _transaction.Commit();
                _transaction.Dispose();
                con.Close();
            }
            return ds;
        }

        public void CopyOver2DataBase (string constr, string constr2)
        {
            constr2 = ConfigurationManager.ConnectionStrings[constr2].ConnectionString;
            using (SqlConnection sourceCon = new SqlConnection(ConfigurationManager.ConnectionStrings[constr].ConnectionString))
            {
                sourceCon.Open();

                SqlCommand cmd = new SqlCommand("Select * From Departments", sourceCon);
                
                using(SqlDataReader rdr = cmd.ExecuteReader()) 
                { 
                    using(SqlConnection destinationCon = new SqlConnection(constr2))
                    {
                        using(SqlBulkCopy bc = new SqlBulkCopy(destinationCon))
                        {
                            bc.DestinationTableName = "Departments";
                            destinationCon.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * From Employees", sourceCon);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    using (SqlConnection destinationCon = new SqlConnection(constr2))
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destinationCon))
                        {
                            bc.DestinationTableName = "Employees";
                            destinationCon.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }
            }
        }

        public void UpdateUsingSqlBulkCopy(string connectionString, HttpServerUtility server)
        {
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
            {
                con.Open();

                DataSet ds = new DataSet();
                ds.ReadXml(server.MapPath("~/Data.xml"));

                DataTable dtDept = ds.Tables["Department"];
                DataTable dtEmp = ds.Tables["Employee"];

                using (SqlBulkCopy bc = new SqlBulkCopy(con))
                {
                    bc.DestinationTableName = "Departments";
                    bc.ColumnMappings.Add("ID", "ID");
                    bc.ColumnMappings.Add("Name", "Name");
                    bc.ColumnMappings.Add("Location", "Location");
                    bc.WriteToServer(dtDept);
                }

                using (SqlBulkCopy bc = new SqlBulkCopy(con))
                {
                    bc.DestinationTableName = "Employees";
                    bc.ColumnMappings.Add("ID", "ID");
                    bc.ColumnMappings.Add("Name", "Name");
                    bc.ColumnMappings.Add("Gender", "Gender");
                    bc.ColumnMappings.Add("DepartmentId", "DepartmentId");
                    bc.WriteToServer(dtEmp);
                }
            }
        }

        public void UpdateUsingSqlBulkCopy2(string connectionString, HttpServerUtility server)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
            {
                con.Open();

                DataSet ds = new DataSet();
                ds.ReadXml(@"C:\Users\NosyT\source\repos\Preparation\ADO.NET Class Library Ex\Data.xml");

                DataTable dtDept = ds.Tables["Department"];
                DataTable dtEmp = ds.Tables["Employee"];

                using (SqlBulkCopy bc = new SqlBulkCopy(con))
                {
                    bc.DestinationTableName = "Departments";
                    bc.ColumnMappings.Add("ID", "ID");
                    bc.ColumnMappings.Add("Name", "Name");
                    bc.ColumnMappings.Add("Location", "Location");
                    bc.WriteToServer(dtDept);
                }

                using (SqlBulkCopy bc = new SqlBulkCopy(con))
                {
                    bc.DestinationTableName = "Employees";
                    bc.ColumnMappings.Add("ID", "ID");
                    bc.ColumnMappings.Add("Name", "Name");
                    bc.ColumnMappings.Add("Gender", "Gender");
                    bc.ColumnMappings.Add("DepartmentId", "DepartmentId");
                    bc.WriteToServer(dtEmp);
                }
            }
        }

        //public void UpdateUsingSqlBulkCopyNoXML(string connectionString, DataTable table)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
        //    {
        //        con.Open();

        //        DataTable dtColor = table;

        //        using (SqlBulkCopy bc = new SqlBulkCopy(con))
        //        {
        //            bc.DestinationTableName = "Color";
        //            bc.ColumnMappings.Add("name", "color");
        //            bc.ColumnMappings.Add("hex", "hex");
        //            bc.ColumnMappings.Add("colorId", "colorID");
        //            bc.WriteToServer(dtColor);
        //        }
        //    }
        //}
    }
}
