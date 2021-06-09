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

            LINQ2DataSet.Selection();

            //SqlBulkCopyBatchEx.BulkCopyByBatch();

            //Parallel.For(1, 100, (i) => Console.WriteLine(i));

            //DataParallelism.GetFileCount();
            //DataParallelism.PartitionerEx();
            //DataParallelism.CallingABreak();
            //DataParallelism.ThreadLocals();

            //PLINQPlaying.ParallelQuery();
            //PLINQPlaying.Merges();


            Console.ReadLine();
        }
    }
}
