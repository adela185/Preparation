using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlayField
{
    public class TaskParallelism
    {
        public static void Print10()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
        }

        public static void Print10(object state)
        {
            for (int i = 0; i < 10 /*Convert.ToInt32(n)*/; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
        }

        public static void UnparameterizedThreadViaThreadClass()
        {
            System.Threading.Thread thread;
            
            thread = new System.Threading.Thread(new System.Threading.ThreadStart(Print10));
            thread.Start();
        }

        public static void ParameterizedThreadStart()
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Print10));
            thread.Start(10);
        }

        public static void CreateThreadUsingThreadPool()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Print10));
        }

        public static void BackgroundWorkerEx()
        {
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += SimulateServiceCall;
            backgroundWorker.ProgressChanged += ProgressChanged;
            backgroundWorker.RunWorkerCompleted += RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
            Console.WriteLine("To Cancel, Press 'c'.");
            while (backgroundWorker.IsBusy)
            {
                if (Console.ReadKey(true).KeyChar == 'c')
                {
                    backgroundWorker.CancelAsync();
                }
            }
        }

        private static void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                Console.WriteLine(e.Error.Message);
            }
            else
            {
                Console.WriteLine($"Result from service {e.Result}");
            }
        }

        private static void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine($"{e.ProgressPercentage}% completed");
        }

        private static void SimulateServiceCall(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            StringBuilder data = new StringBuilder();
            for (int i = 1; i <= 100; i++)
            {
                if (!worker.CancellationPending)
                {
                    data.Append(i);
                    worker.ReportProgress(i);
                    Thread.Sleep(100);
                    //Try to uncomment to throw error and see
                    //throw new Exception("Some Error had occurred");
                }
                else
                {
                    worker.CancelAsync();
                }
            }

            e.Result = data;
        }

        public static void GettingResultsFromTask()
        {
            var sumTaskViaTaskOfInt = new Task<int>(() => sum());
            sumTaskViaTaskOfInt.Start();
            Console.WriteLine($"Result From sumTask is: {sumTaskViaTaskOfInt.Result}");

            var sumTaskViaFactory = Task.Factory.StartNew<int>(()=> sum());
            Console.WriteLine($"Result From sumTask is: {sumTaskViaFactory.Result}");

            var sumTaskViaTaskRun = Task.Run<int>(() => sum());
            Console.WriteLine($"Result From sumTask is: {sumTaskViaTaskRun.Result}");

            var sumTaskViaTaskResult = Task.FromResult<int>(sum());
            Console.WriteLine($"Result From sumTask is: {sumTaskViaTaskResult.Result}");
        }

        public static void CancellationViaCallBack()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            DownloadFileWithToken(cts.Token);

            Task.Delay(2000);
            cts.Cancel();
        }

        private static void DownloadFileWithToken(CancellationToken token)
        {
            WebClient client = new WebClient();
            token.Register(() => client.CancelAsync());
            client.DownloadStringAsync(new Uri("http://www.google.com"));
            client.DownloadStringCompleted += (sender, e) =>
            {
                Task.Delay(3000);
                if (e.Cancelled)
                {
                    Console.WriteLine("Download Cancelled.");
                }
                else
                {
                    Console.WriteLine("Download Complete.");
                }
            };
        }

        public static void AggregateException()
        {
            try
            {
                Task task = Task.Factory.StartNew(() =>
                    {
                        int num = 0;
                        int num2 = 25;
                        int result = num2 / num;
                    });
                task.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"Exception Caught: {ex.InnerException.Message}");
            }

            Task task1 = Task.Run(() => throw new DivideByZeroException());
            Task task2 = Task.Run(() => throw new ArithmeticException());
            Task task3 = Task.Run(() => throw new NullReferenceException());

            try
            {
                Task.WaitAll(task1, task2, task3);
            }
            catch (AggregateException ex)
            {
                foreach (Exception innerException in ex.InnerExceptions)
                {
                    Console.WriteLine(innerException.Message);
                }
            }
        }

        public static void FileReadUsingAPMNoCallback()
        {
            string path = "C:/Users/NosyT/source/repos/Preparation/PlayField/FileIOEx/Text.txt";
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous))
            {
                byte[] b = new byte[1024];
                UTF8Encoding encoder = new UTF8Encoding(true);
                IAsyncResult result = fs.BeginRead(b, 0, b.Length, null/*Async CallBack*/, null);
                Console.WriteLine("Do Something Here");
                int numBytes = fs.EndRead(result);
                fs.Close();
                Console.WriteLine(encoder.GetString(b));
            }
        }

        public static void FileReadAPM2Task()
        {
            string path = "C:/Users/NosyT/source/repos/Preparation/PlayField/FileIOEx/Text.txt";
            using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous))
            {
                byte[] b = new byte[1024];
                UTF8Encoding encoder = new UTF8Encoding(true);
                var task = Task<int>.Factory.FromAsync(fs.BeginRead, fs.EndRead, b, 0, b.Length, null);
                Console.WriteLine("Do Something While File Is Read Asynchronously.");
                task.Wait();
                Console.WriteLine(encoder.GetString(b));
            }
        }

        public static Task<string> EAP2Task()
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            var webClient = new WebClient();

            webClient.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error != null)
                    taskCompletionSource.TrySetException(e.Error);
                else if (e.Cancelled)
                    taskCompletionSource.TrySetCanceled();
                else
                    taskCompletionSource.TrySetResult(e.Result);
            };

            webClient.DownloadStringAsync(new Uri("http://www.google.com"));

            return taskCompletionSource.Task;
        }

        public static DataTable FetchData()
        {
            DataTable table = new DataTable("Person");

            table.Columns.Add("ID");
            table.Columns.Add("Name");
            table.Rows.Add(1, "John");

            return table;
        }

        public static void TaskContinuation()
        {
            var task = Task.Factory.StartNew<DataTable>(() =>
            {
                Console.WriteLine("Fetching Data...");
                return FetchData();
            }).ContinueWith((e) =>
            {
                var row = e.Result.Rows[0];
                Console.WriteLine($"{row["Name"]}'s ID is {row["ID"]}.");
            });
        }

        public async static void TaskContinueWhenAll()
        {
            int a = 2, b = 3;
            Task<int> t1 = Task.Factory.StartNew(() => a * a);
            Task<int> t2 = Task.Factory.StartNew<int>(() => b * b); //generic types seem to be infered
            Task<int> t3 = Task.Factory.StartNew(() => 2 * a * b);
            var sum = await Task.Factory.ContinueWhenAll/*<int>*/(new Task[] { t1, t2, t3 }, (tasks) => tasks.Sum(t => ((Task<int>)t).Result));

            Console.WriteLine(sum);
        }

        public static void TaskContineAny()
        {
            int number  = 13;
            Task<bool> t1 = Task.Factory.StartNew<bool>(() => number / 2 == 0);
            Task<bool> t2 = Task.Factory.StartNew(() => (number / 2) * 2 != number);
            Task<bool> t3 = Task.Factory.StartNew(() => (number & 1) != 0);
            Task.Factory.ContinueWhenAny<bool>(new Task<bool>[] { t1, t2, t3 }, (task) => Console.WriteLine( ((Task<bool>)task).Result) );
        }

        public static void TaskDetachmentDemo()
        {
            Task parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent Task Started");
                Task childTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Child Task Started");
                    Task.Delay(1000);
                    Console.WriteLine("Child Task Finished");
                });
                Console.WriteLine("Parent Task Finsihed");
            });
            parentTask.Wait();
            Console.WriteLine("Work Finished");
        }

        public static void TaskAttachmentDemo()
        {
            Task parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent Task Started");
                Task childTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Child Task Started");
                    Task.Delay(1000);
                    Console.WriteLine("Child Task Finished");
                }, TaskCreationOptions.AttachedToParent);
                Console.WriteLine("Parent Task Finsihed");
            });
            //parentTask.Wait();
            Console.WriteLine("Work Finished");
        }

        public static void Main(string[] args)
        {   
            Console.WriteLine("Start:");
            //UnparameterizedThreadViaThreadClass();
            //ParameterizedThreadStart();
            //CreateThreadUsingThreadPool();

            //Using Task Class:
            //Task task = new Task(() => Print10());
            //task.Start();
            //or
            //Task task = new Task(new Action(Print10));
            //task.Start();
            //or
            //Task task = new Task(delegate { Print10(); });
            //task.Start();

            //Using Task.Factory:
            //Task.Factory.StartNew(new Action(Print10));
            //Task.Factory.StartNew(delegate { Print10(); });

            //Using Task.Run():
            //Task.Run(() => Print10());
            //Task.Run(delegate { Print10(); });

            //Use this to wait asychronously:
            //Task.Delay(2000);

            //GettingResultsFromTask();

            //CancellationViaCallBack();

            //AggregateException();

            //FileReadUsingAPMNoCallback();
            //FileReadAPM2Task();
            //var task = EAP2Task();
            //task.Wait();

            //TaskContinuation();
            //TaskContinueWhenAll();
            //TaskContineAny();

            //TaskDetachmentDemo();
            TaskAttachmentDemo();

            Console.WriteLine("End");

            //BackgroundWorkerEx();

            Console.ReadLine();
        }

        public static async void TaskYield()
        {
            for (int i = 0; i < 100000; i++)
            {
                Console.WriteLine(i);
                if(i % 1000 == 0)
                    await Task.Yield();
            }
        }

        public static int sum()
        {
            int i = 5;
            return i;
        }

        public async static void Things()
        {
            //Same as Task.Yield():
             await Task.Factory.StartNew(() => { },
                 CancellationToken.None,
                 TaskCreationOptions.None,
                 SynchronizationContext.Current != null ?
                 TaskScheduler.FromCurrentSynchronizationContext() :
                 TaskScheduler.Current);

            Task<int> taskresult = Task.FromResult(sum());
            Console.WriteLine(taskresult.Result);

            //return Task.FromException<long>(new System.IO.FileNotFoundException("Invalid File name."));

            CancellationTokenSource source = new CancellationTokenSource();
            var token = source.Token;
            source.Cancel(); //Cancel Token first before using from canceled task generation
            Task task = Task.FromCanceled(token);
            Task<int> canceledTask = Task.FromCanceled<int>(token);

            //var task1 = Task.Factory.StartNew<int>(() => GetData())
            //.ContinueWith((i) => GetMoreData(i.Result))
            //.ContinueWith((j) => DisplayData(j.Result));


        }
    }
}
