using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Class_Library_Ex
{
    public class SqlDataAdapterTranscationalManagement
    {
        //Things to learn:
        //IsolationLevels in concept and what each really does
        //Learn the use of Reflection and its relation to metadata
        //Bindingflags and what they do
        //A deeper review into DataAdapter???
        public SqlTransaction BeginTransaction(object tableAdapter)
        {
            return this.BeginTransaction(tableAdapter, IsolationLevel.ReadCommitted);
        }

        public SqlTransaction BeginTransaction(object tableAdapter, IsolationLevel isoLevel)
        {
            Type type = tableAdapter.GetType();
            SqlConnection con = this.prvGetConnection(tableAdapter);

            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlTransaction stTransaction = con.BeginTransaction(isoLevel);

            this.prvSetTransaction(tableAdapter, stTransaction);

            return stTransaction;
        }

        public void EnlistInTransaction(object tablaAdapter, SqlTransaction stTrans)
        {
            this.prvSetTransaction(tablaAdapter, stTrans);
        }

        private SqlConnection prvGetConnection(object tableAdapter)
        {
            SqlConnection scResult = null;

            //I know this logic gets information from the meta data, whatever that means in practice
            Type type = tableAdapter.GetType();
            PropertyInfo conProperty = type.GetProperty("Connection", BindingFlags.NonPublic | BindingFlags.Instance);
            scResult = (SqlConnection)conProperty.GetValue(tableAdapter, null);

            return scResult;
        }
        private void prvSetTransaction(object tableAdapter, SqlTransaction stTrans)
        {
            //Meta get for adapter prop
            Type type = tableAdapter.GetType();
            PropertyInfo adapterProperty = type.GetProperty("Adapter", BindingFlags.NonPublic | BindingFlags.Instance);
            SqlDataAdapter sda = (SqlDataAdapter)adapterProperty.GetValue(tableAdapter, null);

            sda.UpdateCommand.Transaction = stTrans;
            sda.InsertCommand.Transaction = stTrans;
            sda.DeleteCommand.Transaction = stTrans;
            sda.AcceptChangesDuringUpdate = false;

            PropertyInfo cmdCollectionProperty = type.GetProperty("Command Collection", BindingFlags.NonPublic | BindingFlags.Instance);
            SqlCommand[] cmds = (SqlCommand[])cmdCollectionProperty.GetValue(tableAdapter, null);

            foreach (SqlCommand cmd in cmds)
            {
                cmd.Transaction = stTrans;
            }

            this.prvSetConnection(tableAdapter, stTrans.Connection);
        }

        private void prvSetConnection(object tableAdapter, SqlConnection connection)
        {
            Type type = tableAdapter.GetType();
            PropertyInfo conProperty = type.GetProperty("Connection", BindingFlags.NonPublic | BindingFlags.Instance);
            conProperty.SetValue(tableAdapter, connection, null);
        }
    }
}
