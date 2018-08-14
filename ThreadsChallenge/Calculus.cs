using System;
using System.Numerics;

namespace ThreadsChallenge
{
    public interface ICalculus
    {
        void Calculate(int num);
    }

    public class Calculus : ICalculus
    {
        public void Calculate(int num)
        {
            double x;
            if (num % 2 == 0) x = Math.Sqrt(num);
            //Console.WriteLine($"Even found: {num} SQRT: {Math.Sqrt(num)}");
            else
            {
                BigInteger mult = 1;
                for (int i = 1; i <= num; i++) mult *= i;
                //Console.WriteLine($"Odd found: {num} Factorial: {mult}");
            }
        }
    }
}
