using System;

namespace Algorithms.Stanford.DataStructures
{
    public class MinHeap<T> where T : class, IHeapIndexable
    {
        private int _heapSize;

        private T[] _minHeap;

        public T this[int index]
        {
            get => _minHeap[index - 1];
            set => _minHeap[index - 1] = value;
        }

        public MinHeap()
        {
            _minHeap = new T[0];
        }

        public MinHeap(T[] heap)
        {
            _minHeap = heap;
            _heapSize = _minHeap.Length;
            BuildMinHeap();
        }

        public T GetMinElement()
        {
            return this[1];
        }

        public T ExtractMinElement()
        {
            var output = GetMinElement();

            this[1] = this[_heapSize--];
            MinHeapify(1);

            return output;
        }

        public void DecreaseKey(int index, int newKey)
        {
            if (this[index].GetKey() <= newKey)
            {
                return;
            }

            var nodeObject = this[index];
            var parent = GetParent(index);

            while (index > 1 && this[parent].GetKey() > newKey)
            {
                this[index] = this[parent];
                this[index].HeapIndex = index;
                index = parent;
                parent = GetParent(index);
            }

            this[index] = nodeObject;
            this[index].SetKey(newKey);
            this[index].HeapIndex = index;
        }

        public void InsertElement(T element)
        {
            _heapSize++;

            if (_minHeap.Length < _heapSize)
            {
                Array.Resize(ref _minHeap, _heapSize);
            }

            var actualKey = element.GetKey();
            element.SetKey(actualKey + 1);

            this[_heapSize] = element;
            DecreaseKey(_heapSize, actualKey);
        }

        private int GetParent(int i)
        {
            return i >> 1;
        }

        private int GetLeftChild(int i)
        {
            return i << 1;
        }

        private int GetRightChild(int i)
        {
            return (i << 1) + 1;
        }

        private void BuildMinHeap()
        {
            var lastParentIndex = _heapSize >> 1;
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

                if (leftChildIndex <= _heapSize && this[leftChildIndex].GetKey() < this[smallestIndex].GetKey())
                {
                    smallestIndex = leftChildIndex;
                }

                if (rightChildIndex <= _heapSize && this[rightChildIndex].GetKey() < this[smallestIndex].GetKey())
                {
                    smallestIndex = rightChildIndex;
                }

                if (smallestIndex == index){ return;}

                SwapHeapElements(index, smallestIndex);

                index = smallestIndex;
            }
        }

        private void SwapHeapElements(int firstIndex, int secondIndex)
        {
            var temp = this[firstIndex];
            this[firstIndex] = this[secondIndex];
            this[secondIndex] = temp;

            this[firstIndex].HeapIndex = firstIndex;
            this[secondIndex].HeapIndex = secondIndex;
        }
    }
}