using Algorithms.Stanford.Sorting.PivotPartition;

namespace Algorithms.Stanford.Sorting.Quicksort
{
    public class QuicksortComparesCounter
    {
        private int _comparesCounter;
        private PivotPartitioner Partitioner { get; }

        public QuicksortComparesCounter(int[] array, PivotSelect pivotSelectionMethod)
        {
            Partitioner = new PivotPartitioner(pivotSelectionMethod);
            QuickSort(array);
        }

        public void QuickSort(int[] array)
        {
            if (array.Length < 2)
            {
                return;
            }

            QuickSortRecursive(array, 0, array.Length - 1);
        }

        public int GetComparisons()
        {
            return _comparesCounter;
        }

        private void QuickSortRecursive(int[] array, int startIndex, int endIndex)
        {
            if (endIndex - startIndex < 1)
            {
                return;
            }

            var pivot = PartitionWithCounting(array, startIndex, endIndex);
            QuickSortRecursive(array, startIndex, pivot - 1);
            QuickSortRecursive(array, pivot + 1, endIndex);
        }

        private int PartitionWithCounting(int[] array, int startIndex, int endIndex)
        {
            _comparesCounter += endIndex - startIndex;
            var pivot = Partitioner.Partition(array, startIndex, endIndex);

            return pivot;
        }
    }
}