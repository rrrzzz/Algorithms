using System;
using System.Linq;
using Algorithms.Corman;
using Algorithms.Corman.HeapStructure;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new[] {16, 4, 10, 14, 7, 9, 3, 2, 8, 1 };

            Sorting.HeapSort(array);
            array.ToList().ForEach(Console.WriteLine);
            Console.ReadLine();
        }
    }
}