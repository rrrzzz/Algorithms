namespace Algorithms.Stanford.DataStructures
{
    public class MaxHeap : HeapBase<int>
    {
        public MaxHeap(){}

        public MaxHeap(int[] heap) : base(heap)
        {
            BuildMaxHeap();
        }

        public int GetMaxElement()
        {
            return this[1];
        }

        public int ExtractMaxElement()
        {
            var output = GetMaxElement();

            this[1] = this[HeapSize--];

            MaxHeapify(1);

            return output;
        }

        public void InsertElement(int value)
        {
            HeapSize++;
            if (HeapArray.Length < HeapSize)
            {
                ResizeHeapArray();
            }

            this[HeapSize] = value - 1;

            IncreaseKey(HeapSize, value);
        }

        public void IncreaseKey(int index, int value)
        {
            if (this[index] >= value) return;

            var parentIndex = GetParent(index);

            while (index > 1 && this[parentIndex] < value)
            {
                this[index] = this[parentIndex];
                index = parentIndex;
                parentIndex = GetParent(index);
            }

            this[index] = value;
        }

        private void MaxHeapify(int index)
        {
            var largest = index;

            while (true)
            {
                var leftChildIndex = GetLeftChild(index);
                var rightChildIndex = GetRightChild(index);

                if (leftChildIndex <= HeapSize && this[leftChildIndex] > this[largest])
                {
                    largest = leftChildIndex;
                }

                if (rightChildIndex <= HeapSize && this[rightChildIndex] > this[largest])
                {
                    largest = rightChildIndex;
                }

                if (largest == index) return;

                SwapHeapElements(index, largest);
                index = largest;
            }
        }

        private void BuildMaxHeap()
        {
            var startIndex = HeapSize >> 1;

            while (startIndex != 0) MaxHeapify(startIndex--);
        }
    }
}