using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayField
{
    public class PLINQPlaying
    {
        public static void ParallelQuery()
        {
            IEnumerable<int> range = Enumerable.Range(1, 100000);

            //Sequential
            Stopwatch watch = Stopwatch.StartNew();
            List<int> resultList = range.Where(i => i % 3 == 0).ToList();
            watch.Stop();
            Console.WriteLine($"Sequantial: Total Items Are {resultList.Count}. Total Time - {watch.Elapsed}");

            //Parallel
            watch = Stopwatch.StartNew();
            List<int> resultListP = range.AsParallel<int>().Where(i => i % 3 == 0).ToList();
            watch.Stop();
            Console.WriteLine($"Parallel: Total Items Are {resultListP.Count}. Total Time - {watch.Elapsed}");
            //OR
            watch = Stopwatch.StartNew();
            List<int> resultListP2 = (from i in range.AsParallel()
                                      where i % 3 == 0
                                      select i).ToList();
            watch.Stop();
            Console.WriteLine($"Parallel: Total Items Are {resultListP2.Count}. Total Time - {watch.Elapsed}");

            Console.WriteLine();
            range = Enumerable.Range(1, 10);
            watch = Stopwatch.StartNew();
            range.ToList().ForEach(i => Console.Write(i + "-"));
            watch.Stop();
            Console.WriteLine(watch.Elapsed);

            watch = Stopwatch.StartNew();
            var unordered = range.AsParallel().Select(i => i).ToList();
            unordered.ForEach(i => Console.Write(i + "-"));
            watch.Stop();
            Console.WriteLine(watch.Elapsed);

            watch = Stopwatch.StartNew();
            var ordered = range.AsParallel().AsOrdered().Select(i => i).ToList();
            ordered.ForEach(i => Console.Write(i + "-"));
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }
    }
}
