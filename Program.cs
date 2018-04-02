using System;
using System.Collections.Generic;
using System.Threading;
using Algorithms.Stanford;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var solver = new InversionCounter();

            var arr = new[] {1, 3, 5, 6, 2, 4, 8, 7};
            var count = solver.CountInversions(arr);

            Console.WriteLine(count);
            Console.ReadLine();
        }
    }
}