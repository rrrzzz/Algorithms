using System;
using System.Collections.Generic;
using Algorithms.Corman.HeapStructure;

namespace Algorithms.Corman
{
    public class Sorting
    {
        public void SelectionSort(List<int> ints)
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

        public void HeapSort(int[] arrayToSort)
        {
            var heap = new Heap(arrayToSort);
            heap.BuildHeap();
            
            while (heap.HeapSize > 1)
            {
                heap.SwapHeapValues(1, heap.HeapSize--);
                heap.MaxHeapify(1);
            }
        }

        public void Quicksort(int[] arrayToSort)
        {
            var startIndex = 0;
            var endIndex = arrayToSort.Length - 1;

            QuicksortRecursive(arrayToSort, startIndex, endIndex);
        }

        private void QuicksortRecursive(int[] arrayToSort, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                var pivotIndex = Partition(arrayToSort, startIndex, endIndex);
                QuicksortRecursive(arrayToSort, startIndex, pivotIndex - 1);
                QuicksortRecursive(arrayToSort, pivotIndex + 1, endIndex);

            }
        }

        private int Partition(int[] arrayToSort, int startIndex, int endIndex)
        {
            var pivotIndex = endIndex;
            var pivotValue = arrayToSort[pivotIndex];
            var smallerPartitionEndIndex = startIndex - 1;
            for (int currentPosition = startIndex; currentPosition < pivotIndex; currentPosition++)
            {
                if (arrayToSort[currentPosition] <= pivotValue)
                {
                    UtilityMethods.SwapValues(arrayToSort, ++smallerPartitionEndIndex, currentPosition);
                }
            }

            pivotIndex = smallerPartitionEndIndex + 1;
            UtilityMethods.SwapValues(arrayToSort, pivotIndex, endIndex);

            return pivotIndex;
        }
    }
}