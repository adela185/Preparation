using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlayField
{
    public static class DataParallelism
    {
        public static void GetFileCount()
        {
            int totalFiles = 0;
            var files = Directory.GetFiles(@"C:\Alex's Laptop");
            Parallel.For(0, files.Length, (i) =>
            {
                FileInfo fileinfo = new FileInfo(files[i]);
                if (fileinfo.CreationTime.Day == DateTime.Now.Day)
                    Interlocked.Increment(ref totalFiles);
            });
            Console.WriteLine($"Total Files: {files.Count()}, with {totalFiles} created today");
        }
        

    }
}
