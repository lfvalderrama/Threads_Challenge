﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace ThreadsChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            int numThreads = 4;



            Random r = new Random();
            var ListData = new List<int>();
            ConcurrentQueue<int> ConcurrentQueueData = new ConcurrentQueue<int>();
            ConcurrentQueue<int> ConcurrentQueueData2 = new ConcurrentQueue<int>();
            ConcurrentQueue<int> ConcurrentQueueData3 = new ConcurrentQueue<int>();
            for (int i=0; i<100; i++)
            {
                var random = r.Next(1, 100000);
                ListData.Add(random);
                ConcurrentQueueData.Enqueue(random);
                ConcurrentQueueData2.Enqueue(random);
                ConcurrentQueueData3.Enqueue(random);
            }

            RunMode runMode = new RunMode(4);            
            
            var watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.ParallelForeach(ListData); //Parallel ForEach List
            watchParallel.Stop();
            Console.WriteLine($"Parallel ForEach List: {watchParallel.ElapsedMilliseconds} ms");

            watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.ParallelForeach(ConcurrentQueueData); //Parallel ForEach ConcurrentQueueData
            watchParallel.Stop();
            Console.WriteLine($"Parallel ForEach ConcurrentQueueData: {watchParallel.ElapsedMilliseconds} ms");            
            
            watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.Tasks(ListData); //Parallel Tasks List
            watchParallel.Stop();
            Console.WriteLine($"Parallel Task List: {watchParallel.ElapsedMilliseconds} ms");            
            
            watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.Tasks(ConcurrentQueueData2); //Parallel Tasks ConcurrentQueueData
            watchParallel.Stop();
            Console.WriteLine($"Parallel Task ConcurrentQueueData: {watchParallel.ElapsedMilliseconds} ms");   
           
            watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.AsParallel(ListData); //AsParallel Parallel List
            watchParallel.Stop();
            Console.WriteLine($"Linq Parallel List: {watchParallel.ElapsedMilliseconds} ms");
           
            watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.AsParallel(ConcurrentQueueData3); //AsParallel Parallel ConcurrentQueueData
            watchParallel.Stop();
            Console.WriteLine($"Linq Parallel ConcurrentQueueData: {watchParallel.ElapsedMilliseconds} ms");
            
            watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.NoParallel(ListData); //No Parallel
            watchParallel.Stop();
            Console.WriteLine($"No Parallel: {watchParallel.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }
    }
}
