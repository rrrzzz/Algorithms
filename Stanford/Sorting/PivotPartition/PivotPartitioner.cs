using System;
using System.ComponentModel;

namespace Algorithms.Stanford.Sorting.PivotPartition
{
    public class PivotPartitioner
    {
        private PivotSelect PivotSelectionMethod { get; }
        private readonly Random _randomizer;

        public PivotPartitioner( PivotSelect pivotSelectionMethod)
        {
            PivotSelectionMethod = pivotSelectionMethod;
            if (pivotSelectionMethod == PivotSelect.Random)
            {
                _randomizer = new Random();
            }
        }

        public int Partition(int[] array, int startIndex, int endIndex)
        {
            var pivotIndex = ChoosePivot(array, startIndex, endIndex);
            UtilityMethods.SwapValues(array, pivotIndex, startIndex);
            pivotIndex = startIndex;

            var smallPartitionBorderIndex = startIndex + 1;

            for (int currentPosition = startIndex + 1; currentPosition <= endIndex; currentPosition++)
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

        private int ChoosePivot(int[] array, int startIndex, int endIndex)
        {
            switch (PivotSelectionMethod)
            {
                case PivotSelect.First:
                    return startIndex;
                case PivotSelect.Last:
                    return endIndex;
                case PivotSelect.Median:
                    return GetMedianElement(array, startIndex, endIndex);
                case PivotSelect.Random:
                    return _randomizer.Next(startIndex, endIndex);
                default:
                    throw new InvalidEnumArgumentException($"There is no such selection type as {PivotSelectionMethod}");
            }
        }

        private int GetMedianElement(int[] array, int startIndex, int endIndex)
        {
            if (endIndex - startIndex == 1)
            {
                return startIndex;
            }

            var middleIndex = (startIndex + endIndex) / 2;

            var start = array[startIndex];
            var middle = array[middleIndex];
            var end = array[endIndex];

            var x = start - middle;
            var y = middle - end;
            var z = start - end;

            if (x * y > 0) return middleIndex;
            if (x * z > 0) return endIndex;
            return startIndex;
        }
    }
}