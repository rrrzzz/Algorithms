using Algorithms.Stanford.PivotPartition;
using Algorithms.Stanford.Sorting.PivotPartition;

namespace Algorithms.Stanford.Sorting.Quicksort
{
    public class QuicksortComparesCounter : PivotPartitioner
    {
       
        private int _comparesCounter;

        public QuicksortComparesCounter(int[] array, PivotSelect selectPivotMethod) : base(array, selectPivotMethod)
        {
            QuickSort();
        }

        public void QuickSort()
        {
            if (Array.Length < 2)
            {
                return;
            }

            QuickSortRecursive(0, Array.Length - 1);
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

            var pivot = PartitionWithCounting(startIndex, endIndex);
            QuickSortRecursive(startIndex, pivot - 1);
            QuickSortRecursive(pivot + 1, endIndex);
        }

        private int PartitionWithCounting(int startIndex, int endIndex)
        {
            _comparesCounter += endIndex - startIndex;
            var pivot = Partition(startIndex, endIndex);

            return pivot;
        }
    }
}