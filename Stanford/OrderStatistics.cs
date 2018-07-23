using System;
using System.Linq;
using Algorithms.Stanford.PivotPartition;

namespace Algorithms.Stanford
{
    public class OrderStatistics : PivotPartitioner
    {
        public OrderStatistics(int[] array) : base(array, PivotSelect.Random)
        {
        }

        public int FindOrderStatistics(int i)
        {
            if (i < 1 || i > Array.Length)
            {
                throw new ArgumentOutOfRangeException($"There is no {i}th smallest index in the array");
            }

            i--;
            var startIndex = 0;
            var endIndex = Array.Length - 1;

            var pivot = Partition(startIndex, endIndex);
            while (pivot != i)
            {
                if (pivot > i)
                {
                    endIndex = pivot - 1;
                    pivot = Partition(startIndex, endIndex);
                }
                else
                {
                    startIndex = pivot + 1;
                    pivot = Partition(startIndex, endIndex);
                }
            }

            return Array[pivot];
        }

        public int FindOrderStatisticsRecursive(int i, int[] array)
        {
            if (i < 1 || i > array.Length)
            {
                throw new ArgumentOutOfRangeException($"There is no {i}th smallest index in the array");
            }
            if (array.Length == 1)
            {
                return array[0];
            }

            i--;

            var pivot = MyPartition(array);
            if (pivot == i)
            {
                return array[i];
            }
            if (pivot < i)
            {
                var rightArray = array.Skip(pivot + 1).ToArray();
                return FindOrderStatisticsRecursive(i - pivot, rightArray);
            }

            var leftArray = array.Take(pivot + 1).ToArray();
            return FindOrderStatisticsRecursive(i + 1, leftArray);
        }

        private int MyPartition(int[] array)
        {
            var endIndex = array.Length - 1;
            var smallPartitionBorderIndex = 1;

            var pivotIndex = new Random().Next(0, endIndex);
            UtilityMethods.SwapValues(array, pivotIndex, 0);

            pivotIndex = 0;

            for (int currentPosition = 1; currentPosition <= endIndex; currentPosition++)
            {
                if (array[currentPosition] < array[pivotIndex])
                {
                    UtilityMethods.SwapValues(array, smallPartitionBorderIndex++, currentPosition);
                }
            }

            var finalPivotIndex = smallPartitionBorderIndex - 1;
            UtilityMethods.SwapValues(array, finalPivotIndex, pivotIndex);

            return finalPivotIndex;
        }
    }
}