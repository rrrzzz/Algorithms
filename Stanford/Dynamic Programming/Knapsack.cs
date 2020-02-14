using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Stanford.Dynamic_Programming
{
    public class Knapsack
    {
        private int _size;
        private int _weight;
        private int[,] _items;
        private Dictionary<int, int>[] _cache;

        private readonly Dictionary<int, int> _weightToValue = new Dictionary<int, int>();
        private int _tempV;

        public Knapsack(int weight, int[,] items)
        {
            _size = items.GetLength(0);
            _weight = weight;
            _items = items;
            _cache = new Dictionary<int, int>[_size + 1];
            for (var i = 0; i < _size + 1; i++)
            {
                _cache[i] = new Dictionary<int, int>();
            }
        }

        public float GetOptimalSackValue(float[,] cache = null)
        {
            cache = cache ?? new float[_size + 1, _weight + 1];
            
            for (int w = 0; w <= _weight; w++)
            {
                for (int i = 1; i <= _size; i++)
                {
                    var wDif = w - _items[i - 1,0];
                    var prevValue = i - 1 == 0 ? 0 : cache[i - 1, w];
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
        
        public int GetOptimalSackValueBottomUp()
        {
            var i = 1;
            while (i <= _size)
            {
                var w = _items[i - 1, 0];
                var v = _items[i - 1, 1];

                i++;
                var maxDif = _weight - w;
                if (maxDif < 0) continue;

                var keys = _weightToValue.Keys.ToArray();
                var processedKeys = new Dictionary<int,int>();

                foreach (var cw in keys)
                {
                    var wDif = cw - w;
                    if (wDif < 0) continue;

                    int newVal;
                    if (processedKeys.TryGetValue(cw, out _tempV)) newVal = _tempV + v;
                    else newVal = _weightToValue[cw] + v;

                    if (!_weightToValue.TryGetValue(wDif, out _tempV)) _weightToValue.Add(wDif, newVal);
                    else if (_tempV >= newVal) ;
                    else
                    {
                        processedKeys.Add(wDif, _tempV);
                        _weightToValue[wDif] = newVal;
                    }
                }

                if (!_weightToValue.TryGetValue(maxDif, out _tempV)) _weightToValue.Add(maxDif, v);
                else if (_tempV < v) _weightToValue[maxDif] = v;
            }
            return _weightToValue.Values.Max();
        }
    }
}