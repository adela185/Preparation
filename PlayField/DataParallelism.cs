using System;
using System.Collections.Concurrent;
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
        
        public static void PartitionerEx()
        {
            OrderablePartitioner<Tuple<int, int>> orderablePartitioner = Partitioner.Create(1, 100);
            Parallel.ForEach(orderablePartitioner, new ParallelOptions { MaxDegreeOfParallelism = 7 }, (range, state) =>
            {
                var startIndex = range.Item1;
                var endIndex = range.Item2;
                Console.WriteLine($"Range Execution finished on task {Task.CurrentId} with range {startIndex} - {endIndex}.");
            });
        }

        public static void CallingABreak()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 1000);
            Parallel.ForEach(numbers, (i, parallelLoopState) =>
            {
                Console.WriteLine($"For i={i} LowestBreakPoint = {parallelLoopState.LowestBreakIteration} and Task id = {Task.CurrentId}.");
                if(i >= 10)
                {
                    parallelLoopState.Break();
                }
            });

            Parallel.ForEach(numbers, (i, parallelLoopState) =>
            {
                Console.Write(i + " ");
                if (i % 4 == 0)
                {
                    Console.Write($"Loop Stopped at {i} with Task{Task.CurrentId} ");
                    parallelLoopState.Stop();
                }
            });
            Console.WriteLine();

            CancellationTokenSource cts = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                cts.Cancel();
                Console.WriteLine("Cancellation Called");
            });
            ParallelOptions loopOptions = new ParallelOptions() { CancellationToken = cts.Token };
            try
            {
                Parallel.For(0, Int64.MaxValue, loopOptions, index =>
                    {
                        Thread.Sleep(3000);
                        double result = Math.Sqrt(index);
                        Console.WriteLine($"Index {index}, result {result}");
                    });
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Cancellation Exception Caught!");
            }
        }

        public static void ThreadLocals()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 60);
            long sumOfNumbers = 0;
            Action<long> taskFinishedMethod = (taskResult) =>
           {
               Console.WriteLine($"Sum at the end of all task iterations for this task {Task.CurrentId} is {taskResult}");
               Interlocked.Add(ref sumOfNumbers, taskResult);
           };
            Parallel.For(0, numbers.Count(),
                () => 0, 
                (j, loop, subtotal) =>
                {
                    subtotal += j;
                    return subtotal;
                },
                taskFinishedMethod);
            Console.WriteLine($"The total of 60 numbers is {sumOfNumbers}");

            sumOfNumbers = 0;
            Parallel.ForEach<int, long>(numbers,
                () => 0,
                (j, loop, subtotal) =>
                {
                    subtotal += j;
                    return subtotal;
                },
                taskFinishedMethod);
            Console.WriteLine($"The total of 60 numbers is {sumOfNumbers}");
        }
    }
}
