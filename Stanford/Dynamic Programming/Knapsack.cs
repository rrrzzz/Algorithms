using System;
using System.Linq;

namespace Algorithms.Stanford.Dynamic_Programming
{
    public class Knapsack
    {
        private int _size;
        private int _weight;
        private int[,] _items;

        public Knapsack(int weight, int[,] items)
        {
            _size = items.GetLength(0);
            _weight = weight;
            _items = items;
        }

        public float GetOptimalSackValue(float[,] cache = null)
        {
            cache = cache ?? new float[_size + 1, _weight + 1];

            for (int i = 0; i <= _weight; i++)
            {
                cache[0, i] = 0;
            }

            for (int w = 0; w <= _weight; w++)
            {
                for (int i = 1; i <= _size; i++)
                {
                    var wDif = w - _items[i - 1,0];
                    var prevValue = cache[i - 1, w];
                    cache[i, w] = wDif < 0 ? prevValue : Math.Max(prevValue, cache[i - 1, wDif] + _items[i - 1, 1]);
                }
            }

            return cache[_size, _weight];
        }

        public int[] GetOptimalSackItems()
        {
            var cache = new float[_size + 1, _weight + 1];
            GetOptimalSackValue(cache);
            var optItems = new int[_size];
            var index = 0;
            var itemId = _size;
            var weight = _weight;

            while (itemId > 0)
            {
                var currentVal = cache[itemId, weight];
                if (Math.Abs(cache[--itemId, weight] - currentVal) < 0.0001) continue;
                optItems[index++] = itemId + 1;
                weight -= _items[itemId, 0];
            }

            return optItems.Take(index).ToArray();
        }
    }
}