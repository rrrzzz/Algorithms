namespace Algorithms.Stanford.DataStructures
{
    public class MinHeapUniversal<T> : HeapBase<T> where T : class, IHeapIndexable
    {
        public MinHeapUniversal(){}

        public MinHeapUniversal(T[] heap) : base(heap)
        {
            BuildMinHeap();
        }

        public T GetMinElement()
        {
            return this[1];
        }

        public T ExtractMinElement()
        {
            var output = GetMinElement();

            this[1] = this[HeapSize--];
            MinHeapify(1);

            return output;
        }

        public bool DecreaseKey(int index, int newKey)
        {
            if (this[index].GetKey() <= newKey) return false;

            var heapObject = this[index];
            var parent = GetParent(index);

            while (index > 1 && this[parent].GetKey() > newKey)
            {
                this[index] = this[parent];
                this[index].HeapIndex = index;
                index = parent;
                parent = GetParent(index);
            }

            this[index] = heapObject;
            this[index].SetKey(newKey);
            this[index].HeapIndex = index;
            return true;
        }

        public void InsertElement(T element)
        {
            HeapSize++;

            if (HeapArray.Length < HeapSize)
            {
                ResizeHeapArray();
            }

            var actualKey = element.GetKey();
            element.SetKey(actualKey + 1);

            this[HeapSize] = element;
            DecreaseKey(HeapSize, actualKey);
        }

        private void BuildMinHeap()
        {
            var lastParentIndex = HeapSize >> 1;
            for (int i = lastParentIndex; i >= 1; i--)
            {
                MinHeapify(i);
            }
        }

        private void MinHeapify(int index)
        {
            var smallestIndex = index;

            while (true)
            {
                var leftChildIndex = GetLeftChild(index);
                var rightChildIndex = GetRightChild(index);

                if (leftChildIndex <= HeapSize && this[leftChildIndex].GetKey() < this[smallestIndex].GetKey())
                {
                    smallestIndex = leftChildIndex;
                }

                if (rightChildIndex <= HeapSize && this[rightChildIndex].GetKey() < this[smallestIndex].GetKey())
                {
                    smallestIndex = rightChildIndex;
                }

                if (smallestIndex == index) return;

                SwapHeapElements(index, smallestIndex);

                index = smallestIndex;
            }
        }

        protected override void SwapHeapElements(int firstIndex, int secondIndex)
        {
            var temp = this[firstIndex];
            this[firstIndex] = this[secondIndex];
            this[secondIndex] = temp;

            this[firstIndex].HeapIndex = firstIndex;
            this[secondIndex].HeapIndex = secondIndex;
        }
    }
}