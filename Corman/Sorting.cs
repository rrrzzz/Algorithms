using System;
using System.Collections.Generic;
using Algorithms.Corman.HeapStructure;

namespace Algorithms.Corman
{
    public static class Sorting
    {
        public static void SelectionSort(List<int> ints)
        {
            var length = ints.Count;
            for (int i = 0; i < length - 1; i++)
            {
                var currentMin = i;
                for (int j = i + 1; j < length; j++)
                {
                    if (ints[currentMin] > ints[j])
                    {
                        currentMin = j;
                    }
                }

                if (currentMin != i)
                {
                    var temp = ints[i];
                    ints[i] = ints[currentMin];
                    ints[currentMin] = temp;
                }
            }

            Console.WriteLine(string.Join(", ", ints));
            Console.ReadLine();
        }

        public static void HeapSort(int[] arrayToSort)
        {
            var heap = new Heap(arrayToSort);
            heap.BuildHeap();
            
            while (heap.HeapSize > 1)
            {
                heap.SwapValues(1, heap.HeapSize--);
                heap.MaxHeapify(1);
            }
        }
    }
}
