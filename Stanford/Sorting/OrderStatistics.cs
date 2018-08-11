using System;
using System.Linq;
using Algorithms.Stanford.Sorting.PivotPartition;

namespace Algorithms.Stanford.Sorting
{
    public static class OrderStatistics
    {
        public static int FindOrderStatistics(int[] array, int i)
        {
            if (i < 1 || i > array.Length)
            {
                throw new ArgumentOutOfRangeException($"There is no {i}th smallest index in the array");
            }

            var pivotPartitioner = new PivotPartitioner(PivotSelect.Random);

            i--;
            var startIndex = 0;
            var endIndex = array.Length - 1;

            var pivot = pivotPartitioner.Partition(array, startIndex, endIndex);
            while (pivot != i)
            {
                if (pivot > i)
                {
                    endIndex = pivot - 1;
                    pivot = pivotPartitioner.Partition(array, startIndex, endIndex);
                }
                else
                {
                    startIndex = pivot + 1;
                    pivot = pivotPartitioner.Partition(array, startIndex, endIndex);
                }
            }

            return array[pivot];
        }

        public static int FindOrderStatisticsRecursive(int[] array, int i)
        {
            if (i < 1 || i > array.Length)
            {
                throw new ArgumentOutOfRangeException($"There is no {i}th smallest index in the array");
            }
            if (array.Length == 1)
            {
                return array[0];
            }

            var pivotPartitioner = new PivotPartitioner(PivotSelect.Random);

            i--;

            var pivot = pivotPartitioner.Partition(array, 0, array.Length - 1);
            if (pivot == i)
            {
                return array[i];
            }
            if (pivot < i)
            {
                var rightArray = array.Skip(pivot + 1).ToArray();
                return FindOrderStatisticsRecursive(rightArray, i - pivot);
            }

            var leftArray = array.Take(pivot + 1).ToArray();
            return FindOrderStatisticsRecursive(leftArray, i + 1);
        }
    }
}