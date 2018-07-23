using System;
using Algorithms.Stanford;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new[] {16, 4, 10, 14, 7, 9, 3, 2, 8, 1 };

            var order8 = new OrderStatistics(array);

            var answer = order8.FindOrderStatisticsRecursive(5, array);
            Console.WriteLine(answer);
            Console.ReadLine();
        }
    }
}