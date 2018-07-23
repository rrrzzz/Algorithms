using System;

namespace Algorithms.Corman.HeapStructure
{
    public class PriorityQueue : Heap
    {
        public PriorityQueue(int[] array) : base(array)
        {
        }

        public int GetHeapMax() => HeapArray[1];

        public int ExtractHeapMax()
        {
            if (HeapSize == 1) throw new InvalidOperationException("The heap is empty");

            var max = GetHeapMax();
            this[1] = this[HeapSize--];
            MaxHeapify(1);

            return max;
        }

        public void IncreaseHeapKey(int index, int value)
        {
            if (value < this[index])
                throw new ArgumentException($"The new value: {value} is smaller than the present value: {this[index]}");

            while (index > 1 && value > this[GetParentIndex(index)])
            {
                this[index] = this[GetParentIndex(index)];
                index = GetParentIndex(index);
            }

            this[index] = value;
        }

        public void MaxHeapInsertKey(int value)
        {
            HeapSize++;

            if (HeapArray.Length < HeapSize)
            {
                Array.Resize(ref HeapArray, HeapSize);
            }

            IncreaseHeapKey(HeapSize, value);
        }
    }
}