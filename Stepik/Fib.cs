using System;
using System.Collections.Generic;

namespace Algorithms.Stepik
{
    class Fib
    {
        public void CalculateFibSmall()
        {
            var n = int.Parse(Console.ReadLine());

            var fibList = new List<int>(n + 1) { 0, 1 };

            for (int i = 2; i < n + 1; i++)
            {
                var temp = fibList[i - 1] + fibList[i - 2];
                fibList.Add(temp);
            }

            Console.WriteLine(fibList[n]);
        }

        public void CalculateLastFibDigit()
        {
            var n = int.Parse(Console.ReadLine());

            var fibList = new List<int>(n + 1) { 0, 1 };

            for (int i = 2; i < n + 1; i++)
            {
                var temp = (fibList[i - 1] + fibList[i - 2]) % 10;
                fibList.Add(temp);
            }

            Console.WriteLine(fibList[n]);
        }

        public void CalculateFibHard()
        {
            var input = Console.ReadLine().Split(' ');

            var n = long.Parse(input[0]);
            var m = int.Parse(input[1]);

            var fibList = new List<int> { 0, 1 };

            for (int i = 2; i < n + 1; i++)
            {
                var newFib = (fibList[0] + fibList[1]) % m;

                fibList[0] = fibList[1];
                fibList[1] = newFib;
            }

            Console.WriteLine(fibList[1]);
        }

        public void CalculateFibCheat()
        {
            var input = Console.ReadLine().Split(' ');

            var n = long.Parse(input[0]);
            var m = int.Parse(input[1]);

            var tempFib = Math.Pow(1.618, n);
        }
    }
}