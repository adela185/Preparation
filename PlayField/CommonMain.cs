using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayField
{
    public class CommonMain
    {
        public static void Main(string[] args)
        {
            //LINQPlaying lq = new LINQPlaying();
            //lq.QueryStringArray();
            //lq.QueryIntArray();
            //lq.QueryArrayList();
            //lq.QueryCollection();
            //lq.QueryAnimalData();

            SqlBulkCopyBatchEx.BulkCopyByBatch();

            Console.ReadLine();
        }
    }
}
