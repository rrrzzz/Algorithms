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
            var solver = new SortingClassics();

            var ls = new List<int> {1, 1, 3, 5, 6, 2, 4, 8, 7};
            var sorted = solver.MergeSort(ls);

            Console.WriteLine(String.Join(", ", sorted));
            Console.ReadLine();
        }
    }
}