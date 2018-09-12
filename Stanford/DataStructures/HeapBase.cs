using System;

namespace Algorithms.Stanford.DataStructures
{
    public abstract class HeapBase<T>
    {
        protected int HeapSize;
        protected T[] HeapArray;

        protected HeapBase()
        {
            HeapArray = new T[0];
        }

        protected HeapBase(T[] heap)
        {
            HeapArray = heap;
            HeapSize = HeapArray.Length;
        }

        public T this[int index]
        {
            get => HeapArray[index - 1];
            set => HeapArray[index - 1] = value;
        }

        public int GetHeapSize()
        {
            return HeapSize;
        }

        protected int GetParent(int i)
        {
            return i >> 1;
        }

        protected int GetLeftChild(int i)
        {
            return i << 1;
        }

        protected int GetRightChild(int i)
        {
            return (i << 1) + 1;
        }

        protected void ResizeHeapArray()
        {
            Array.Resize(ref HeapArray, (HeapSize + 1) * 2);
        }

        protected virtual void SwapHeapElements(int firstIndex, int secondIndex)
        {
            var temp = this[firstIndex];
            this[firstIndex] = this[secondIndex];
            this[secondIndex] = temp;
        }
    }
}
