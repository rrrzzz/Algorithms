using System;

namespace Algorithms.Corman.DataStructures
{
    public class Stack<T>
    {
        private T[] _stackArray;
        private int _topIndex = -1;

        public Stack(int n = 0)
        {
            _stackArray = new T[n];
        }

        public T Peek()
        {
            CheckUnderflow();

            return _stackArray[_topIndex];
        }

        public T Pop()
        {
            CheckUnderflow();

            return _stackArray[_topIndex--];
        }

        public void Push(T item)
        {
            if (++_topIndex == _stackArray.Length) Array.Resize(ref _stackArray, (_stackArray.Length + 1) * 2);

            _stackArray[_topIndex] = item;
        }

        public bool IsStackEmpty()
        {
            return _topIndex == -1;
        }

        private void CheckUnderflow()
        {
            if (IsStackEmpty()) throw new InvalidOperationException("Stack underflow");
        }
    }
}