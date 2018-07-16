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
            var array = new[] {-999, 1, 3, 2, 4, 7, 9, 8, 10, 14, 16};
            var arraySkewed = new[] {-999, 16, 4, 10, 14, 7, 9, 3, 2, 8, 1 };

            Sorting.HeapSort(array);
            array.ToList().ForEach(Console.WriteLine);
            Console.ReadLine();
        }
    }
}