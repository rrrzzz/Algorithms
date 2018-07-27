using System;
using System.ComponentModel;
using Algorithms.Stanford.Sorting.PivotPartition;

namespace Algorithms.Stanford.PivotPartition
{
    public class PivotPartitioner
    {
        protected int[] Array { get; }
        protected PivotSelect SelectPivotMethod { get; set; }

        public PivotPartitioner(int[] array, PivotSelect pivotSelectMethod)
        {
            Array = array;
            SelectPivotMethod = pivotSelectMethod;
        }

        protected int Partition(int startIndex, int endIndex)
        {
            var pivotIndex = ChoosePivot(startIndex, endIndex);
            UtilityMethods.SwapValues(Array, pivotIndex, startIndex);
            pivotIndex = startIndex;

            var smallPartitionBorderIndex = startIndex + 1;

            for (int currentPosition = startIndex + 1; currentPosition <= endIndex; currentPosition++)
            {
                if (Array[currentPosition] < Array[pivotIndex])
                {
                    UtilityMethods.SwapValues(Array, smallPartitionBorderIndex++, currentPosition);
                }
            }

            var finalPivotIndex = smallPartitionBorderIndex - 1;
            UtilityMethods.SwapValues(Array, finalPivotIndex, pivotIndex);

            return finalPivotIndex;
        }

        private int ChoosePivot(int startIndex, int endIndex)
        {
            switch (SelectPivotMethod)
            {
                case PivotSelect.First:
                    return startIndex;
                case PivotSelect.Last:
                    return endIndex;
                case PivotSelect.Median:
                    return GetMedianElement(startIndex, endIndex);
                case PivotSelect.Random:
                    var randomizer = new Random();
                    return randomizer.Next(startIndex, endIndex);
                default:
                    throw new InvalidEnumArgumentException($"There is no such selection type as {SelectPivotMethod}");
            }
        }

        private int GetMedianElement(int startIndex, int endIndex)
        {
            if (endIndex - startIndex == 1)
            {
                return startIndex;
            }

            var middleIndex = (startIndex + endIndex) / 2;

            var start = Array[startIndex];
            var middle = Array[middleIndex];
            var end = Array[endIndex];

            int x = start - middle;
            int y = middle - end;
            int z = start - end;

            if (x * y > 0) return middleIndex;
            if (x * z > 0) return endIndex;
            return startIndex;
        }
    }
}
