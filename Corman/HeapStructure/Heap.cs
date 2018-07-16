namespace Algorithms.Corman.HeapStructure
{
    public class Heap
    {
        public int[] HeapArray { get; set; }
        
        public int HeapSize { get; set; }

        public Heap(){}

        public Heap(int[] array)
        {
            HeapArray = array;
            HeapSize = array.Length - 1;
        }

        public int GetParentIndex(int index)
        {
            return index >> 1;
        }

        private int GetLeftChildIndex(int index)
        {
            return index << 1;
        }

        private int GetRightChildIndex(int index)
        {
            return (index << 1) + 1;
        }

        public void MaxHeapify(int parentIndex)
        {
            var largestIndex = parentIndex;

            while (true)
            {
                var leftChildIndex = GetLeftChildIndex(parentIndex);
                var rightChildIndex = GetRightChildIndex(parentIndex);

                if (leftChildIndex <= HeapSize && HeapArray[leftChildIndex] > HeapArray[largestIndex])
                {
                    largestIndex = leftChildIndex;
                }

                if (rightChildIndex <= HeapSize && HeapArray[rightChildIndex] > HeapArray[largestIndex])
                {
                    largestIndex = rightChildIndex;
                }

                if (largestIndex != parentIndex)
                {
                    SwapValues(parentIndex, largestIndex);
                    parentIndex = largestIndex;
                }
                else
                {
                    return;
                }
            }
        }

        public void BuildHeap()
        {
            int arrayMiddle = HeapSize / 2;

            for (int i = arrayMiddle; i >= 1; i--)
            {
                MaxHeapify(i);
            }
        }

        public void SwapValues(int firstIndex, int secondIndex)
        {
            var temp = HeapArray[firstIndex];
            HeapArray[firstIndex] = HeapArray[secondIndex];
            HeapArray[secondIndex] = temp;
        }
    }
}