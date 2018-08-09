namespace Algorithms.Stanford.DataStructures
{
    public class MinHeap : HeapBase<int>
    {
        public MinHeap(){}

        public MinHeap(int[] heap) : base(heap)
        {
            BuildMinHeap();
        }

        public int GetMinElement()
        {
            return this[1];
        }

        public int ExtractMinElement()
        {
            var output = this[1];

            this[1] = this[HeapSize--];

            MinHeapify(1);

            return output;
        }
        
        public void InsertElement(int value)
        {
            HeapSize++;
            if (HeapArray.Length < HeapSize) ResizeHeapArray();

            this[HeapSize] = value + 1;

            DecreaseKey(HeapSize, value);
        }

        public void DecreaseKey(int index, int value)
        {
            if (this[index] <= value) return;

            var parentIndex = GetParent(index);

            while (index > 1 && this[parentIndex] > value)
            {
                this[index] = this[parentIndex];
                index = parentIndex;
                parentIndex = GetParent(index);
            }

            this[index] = value;
        }

        private void MinHeapify(int index)
        {
            var smallest = index;
            while (true)
            {
                var leftChildIndex = GetLeftChild(index);
                var rightChildIndex = GetRightChild(index);

                if (leftChildIndex <= HeapSize && this[leftChildIndex] < this[smallest])
                {
                    smallest = leftChildIndex;
                }

                if (rightChildIndex <= HeapSize && this[rightChildIndex] < this[smallest])
                {
                    smallest = rightChildIndex;
                }

                if (smallest == index) return;

                SwapHeapElements(index, smallest);
                index = smallest;
            }
        }

        private void BuildMinHeap()
        {
            var startIndex = HeapSize >> 1;

            while (startIndex != 0) MinHeapify(startIndex--);
        }
    }
}