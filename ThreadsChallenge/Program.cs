using System;
using System.Collections.Concurrent;
using System.Numerics;
using System.Threading.Tasks;

namespace ThreadsChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            var data = new ConcurrentQueue<int>();
            for (int i=0; i<1000; i++)
            {
                data.Enqueue(r.Next(1, 10000)); 
            }

            var watchParallel = System.Diagnostics.Stopwatch.StartNew();

            var options = new ParallelOptions { MaxDegreeOfParallelism = 4 };
            Parallel.ForEach(data, options, (num) => 
            {
                if (num % 2 == 0) Console.WriteLine($"Even found: {num} SQRT: {Math.Sqrt(num)}");
                else
                {
                    BigInteger mult = 1;
                    for (int i = 1; i <= num; i++) mult *= i;
                    Console.WriteLine($"Odd found: {num} Factorial: {mult}");
                }
            }
            );
            watchParallel.Stop();


            var watchNoParallel = System.Diagnostics.Stopwatch.StartNew();
            foreach (int num in data)
            {
                if (num % 2 == 0) Console.WriteLine($"Even found: {num} SQRT: {Math.Sqrt(num)}");
                else
                {
                    BigInteger mult = 1;
                    for (int i = 1; i <= num; i++) mult *= i;
                    Console.WriteLine($"Odd found: {num} Factorial: {mult}");
                }
            }
            watchNoParallel.Stop();
            Console.WriteLine($"Time parallel: {watchParallel.ElapsedMilliseconds} ms");
            Console.WriteLine($"Time No parallel: {watchNoParallel.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }
    }
}
