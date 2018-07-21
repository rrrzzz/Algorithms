using System.ComponentModel;

namespace Algorithms.Stanford.Quicksort
{
    public class QuicksortComparesCounter
    {
        public PivotSelect SelectPivotMethod { get; set; }
        private readonly int[] _array;
        private int _comparesCounter;

        public QuicksortComparesCounter(int[] array, PivotSelect selectPivotMethod)
        {
            _array = array;
            SelectPivotMethod = selectPivotMethod;
            QuickSort();
        }

        public void QuickSort()
        {
            if (_array.Length < 2)
            {
                return;
            }

            QuickSortRecursive(0, _array.Length - 1);
        }

        public int GetComparisons()
        {
            return _comparesCounter;
        }

        private void QuickSortRecursive(int startIndex, int endIndex)
        {
            if (endIndex - startIndex < 1)
            {
                return;
            }

            var pivot = Partition(startIndex, endIndex);
            QuickSortRecursive(startIndex, pivot - 1);
            QuickSortRecursive(pivot + 1, endIndex);
        }

        private int Partition(int startIndex, int endIndex)
        {
            _comparesCounter += endIndex - startIndex;

            var pivotIndex = ChoosePivot(startIndex, endIndex);
            UtilityMethods.SwapValues(_array, pivotIndex, startIndex);
            pivotIndex = startIndex;

            var smallPartitionBorderIndex = startIndex + 1;

            for (int currentPosition = startIndex + 1; currentPosition <= endIndex; currentPosition++)
            {
                if (_array[currentPosition] < _array[pivotIndex])
                {
                    UtilityMethods.SwapValues(_array, smallPartitionBorderIndex++, currentPosition);
                }
            }

            var finalPivotIndex = smallPartitionBorderIndex - 1;
            UtilityMethods.SwapValues(_array, finalPivotIndex, pivotIndex);

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

            var start = _array[startIndex];
            var middle = _array[middleIndex];
            var end = _array[endIndex];

            int x = start - middle;
            int y = middle - end;
            int z = start - end;

            if (x * y > 0) return middleIndex;
            if (x * z > 0) return endIndex;
            return startIndex;
        }
    }
}