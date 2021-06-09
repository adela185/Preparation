using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

            Console.WriteLine();
            range = Enumerable.Range(100, 10000);
            ordered = range.AsParallel().AsOrdered().Take(100).AsUnordered().Select(i => i * i).ToList();
        }

        public static void Merges()
        {
            var range = ParallelEnumerable.Range(1, 100);
            Stopwatch watch = null;
            ParallelQuery<int> notBufferedQuery =
            range.WithMergeOptions(ParallelMergeOptions.NotBuffered)
             .Where(i => i % 10 == 0)
             .Select(x => {
                 Thread.SpinWait(1000);
                 return x;
             });
            watch = Stopwatch.StartNew();
            foreach (var item in notBufferedQuery)
            {
                Console.WriteLine($"{item}:{watch.ElapsedMilliseconds}");
            }
            Console.WriteLine($"\nNotBuffered Full Result returned in { watch.ElapsedMilliseconds } ms");
            watch.Stop();

            ParallelQuery<int> query = 
                range.WithMergeOptions(ParallelMergeOptions.AutoBuffered)
                .Where(i => i % 10 == 0)
                .Select(x => 
                { 
                    Thread.SpinWait(1000); 
                    return x; 
                });
            watch = Stopwatch.StartNew();
            foreach (var item in query)
            {
                Console.WriteLine($"{item}:{watch.ElapsedMilliseconds}");
            }
            Console.WriteLine($"\nAutoBuffered Full Result returned in { watch.ElapsedMilliseconds} ms");
            watch.Stop();

            ParallelQuery<int> fullyBufferedQuery =
                range.WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                .Where(i => i % 10 == 0)
                .Select(x =>
                {
                    Thread.SpinWait(1000);
                    return x;
                });
            watch = Stopwatch.StartNew();
            foreach (var item in fullyBufferedQuery)
            {
                Console.WriteLine($"{item}:{watch.ElapsedMilliseconds}");
            }
            Console.WriteLine($"\nFully Buffered Full Result returned in { watch.ElapsedMilliseconds} ms");
            watch.Stop();
        }
    }
}
