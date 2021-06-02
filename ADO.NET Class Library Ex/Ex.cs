using System;
using System.Data;
using System.Data.SqlClient;
//using System.Data.Odbc;
using System.Web.UI.WebControls;


namespace ADO.NET_Class_Library_Ex
{
    public class Ex
    {
        private SqlConnection _con = null;
        private SqlCommand _cmd = null;
        private SqlDataReader _dataReader = null;
        private SqlDataAdapter _dataAdapter = null;
        private SqlTransaction _transaction;

        public (DataList, string) GetDBData()
        {
            _con = new SqlConnection(@"Data Source=LAPTOP-M6HVHICA\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True");
            //new OdbcConnection("...");

            _cmd = new SqlCommand("Select colorId, [name], hex From Color", _con);
            //new OdbcCommand

            try
            {
                if(_con.State == ConnectionState.Closed)
                    _con.Open();

                _transaction = _con.BeginTransaction();
                _cmd.Transaction = _transaction;
                _dataReader = _cmd.ExecuteReader();
                _dataAdapter = new SqlDataAdapter(_cmd);
                DataList dataList = new DataList();
                dataList.DataSource = _dataReader;
                dataList.DataBind();

                _dataReader.Close();

                _transaction.Commit();

                return (dataList , "");
            }
            catch (SqlException ex)
            {
                string r = ex.Message;
                _transaction.Rollback();
                return (null, r);
            }
            finally
            {
                if (_con.State == ConnectionState.Open)
                {
                    _con.Close();
                    _con.Dispose();
                }  
            }
        }
    }
}
