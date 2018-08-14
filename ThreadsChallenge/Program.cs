using Autofac;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ThreadsChallenge
{
    class Program
    {

        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            BindingDependcies();
            int numThreads = 20;
            Random r = new Random();
            var ListData = new List<int>();
            ConcurrentQueue<int> ConcurrentQueueData = new ConcurrentQueue<int>();
            ConcurrentQueue<int> ConcurrentQueueData2 = new ConcurrentQueue<int>();
            ConcurrentQueue<int> ConcurrentQueueData3 = new ConcurrentQueue<int>();
            for (int i = 0; i < 100; i++)
            {
                var random = r.Next(1, 10000);
                ListData.Add(random);
                ConcurrentQueueData.Enqueue(random);
                ConcurrentQueueData2.Enqueue(random);
                ConcurrentQueueData3.Enqueue(random);
            }

            using (var scope = Container.BeginLifetimeScope())
            {
                //var parallelForEach = scope.Resolve<RunTimeParallelForEach>();
                //Console.WriteLine(parallelForEach.GetRunTimeConqurrentQueue(ConcurrentQueueData, numThreads));
                //Console.WriteLine(parallelForEach.GetRunTimeList(ListData, numThreads));

                var LinQ = scope.Resolve<ExcecutionTime>();
                
                Console.WriteLine(LinQ.GetRunTimeConqurrentQueue(ConcurrentQueueData, numThreads, ThreadsTypes.ParallelForeach));
                Console.WriteLine(LinQ.GetRunTimeList(ListData, numThreads, ThreadsTypes.LinQ));
                Console.WriteLine(LinQ.GetRunTimeConqurrentQueue(ConcurrentQueueData, numThreads, ThreadsTypes.NoParallel));
                Console.WriteLine(LinQ.GetRunTimeList(ListData, numThreads, ThreadsTypes.Task));

            }

            //RunMode runMode = new RunMode(numThreads);            

            //var watchParallel = System.Diagnostics.Stopwatch.StartNew();
            //runMode.ParallelForeach(ListData); //Parallel ForEach List
            //watchParallel.Stop();
            //Console.WriteLine($"Parallel ForEach List: {watchParallel.ElapsedMilliseconds} ms");

            //watchParallel = System.Diagnostics.Stopwatch.StartNew();
            //runMode.ParallelForeach(ConcurrentQueueData); //Parallel ForEach ConcurrentQueueData
            //watchParallel.Stop();
            //Console.WriteLine($"Parallel ForEach ConcurrentQueueData: {watchParallel.ElapsedMilliseconds} ms");            

            //watchParallel = System.Diagnostics.Stopwatch.StartNew();
            //runMode.Tasks(ListData); //Parallel Tasks List
            //watchParallel.Stop();
            //Console.WriteLine($"Parallel Task List: {watchParallel.ElapsedMilliseconds} ms");            

            //watchParallel = System.Diagnostics.Stopwatch.StartNew();
            //runMode.Tasks(ConcurrentQueueData2); //Parallel Tasks ConcurrentQueueData
            //watchParallel.Stop();
            //Console.WriteLine($"Parallel Task ConcurrentQueueData: {watchParallel.ElapsedMilliseconds} ms");   

            //watchParallel = System.Diagnostics.Stopwatch.StartNew();
            //runMode.AsParallel(ListData); //AsParallel Parallel List
            //watchParallel.Stop();
            //Console.WriteLine($"Linq Parallel List: {watchParallel.ElapsedMilliseconds} ms");

            //watchParallel = System.Diagnostics.Stopwatch.StartNew();
            //runMode.AsParallel(ConcurrentQueueData3); //AsParallel Parallel ConcurrentQueueData
            //watchParallel.Stop();
            //Console.WriteLine($"Linq Parallel ConcurrentQueueData: {watchParallel.ElapsedMilliseconds} ms");

            //var watchNoParallel = System.Diagnostics.Stopwatch.StartNew();
            //runMode.NoParallel(ListData); //No Parallel
            //watchParallel.Stop();
            //Console.WriteLine($"No Parallel: {watchNoParallel.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }

        private static void BindingDependcies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ExcecutionTime>();
            builder.RegisterType<Calculus>().As<ICalculus>();
            builder.RegisterType<ForeachParallel>().Keyed<IRunMode>(ThreadsTypes.ParallelForeach);
            builder.RegisterType<LinQParallel>().Keyed<IRunMode>(ThreadsTypes.LinQ);
            builder.RegisterType<NoParallel>().Keyed<IRunMode>(ThreadsTypes.NoParallel);
            builder.RegisterType<TaskParallel>().Keyed<IRunMode>(ThreadsTypes.Task);
            Container = builder.Build();
        }

    }
}
