using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayField
{
    public class SqlBulkCopyBatchEx
    {
        public static void BulkCopyByBatch()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * From Products_Source", con);
                using(SqlDataReader rdr = cmd.ExecuteReader())
                {
                    using(SqlConnection conDestination = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(conDestination))
                        {
                            bc.DestinationTableName = "Products_Destination";
                            bc.BatchSize = 10000;
                            bc.NotifyAfter = 5000;
                            bc.SqlRowsCopied += new SqlRowsCopiedEventHandler(bc_notified);
                            conDestination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }
            }
        }

        private static void bc_notified(object sender, SqlRowsCopiedEventArgs e)
        {
            Console.WriteLine($"{e.RowsCopied} ...Loaded");
        }
    }
}
