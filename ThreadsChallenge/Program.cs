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
            for (int i = 0; i < 100; i++)
            {
                var random = r.Next(1, 10000);
                ListData.Add(random);
                ConcurrentQueueData.Enqueue(random);
            }

            using (var scope = Container.BeginLifetimeScope())
            {
                var run = scope.Resolve<ExcecutionTime>();
                Console.WriteLine(run.GetRunTime(ListData, numThreads, ThreadsTypes.NoParallel));
                Console.WriteLine(run.GetRunTime(ConcurrentQueueData, numThreads, ThreadsTypes.NoParallel));

            }                  
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
