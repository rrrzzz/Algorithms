using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Stanford.Dynamic_Programming
{
    public class TravellingSalesman
    {
        /* Pseudocode
         *
         * S - all possible subsets of verts {1,...,n} including 1; S[s_1,j] - optimal paths for all subsets of size 1 from 1 to j including 1 and j;
         * base case: S[s_1;1] = 0; S[S; 1] = null;
         * 
         * for m = 2,..n:
         *     for all subsets of length m:
         *         for all j in S != 1:                 
         *             S[s_m, j] = min through all k from S k != j (S[s - {j}, k] + C_kj)
         *
         * tuple <hashset, int>
         *
         * subsets - dict size:subsets. Subset - hashset? Check equality
         */
       
        public int[] GetOptimalTour(Vector2[] coords)
        {
            var sizesToSubsets = GenerateSubsets(coords.Length);
            var s = new Dictionary<Tuple<HashSet<int>, int>, object[]>(new TupleKeysComparer());
            var startIndex = 0;

            //add base cases
            foreach (var subset in sizesToSubsets[0])
            {
                var element = subset.First();
                var tup = new Tuple<HashSet<int>, int>(subset, element);
                if (element == startIndex)
                {
                    s[tup] = new object[]{(float?)0, 0};
                }
                else s[tup] = null;
            }

            CalculateMinPathForAllDestinations();
            //return GetMinTourLength();
            return GetMinTourPath();
            
            void CalculateMinPathForAllDestinations()
            {
                foreach (var setSize in Enumerable.Range(1, coords.Length - 1))
                {
                    Console.WriteLine("Current set size is " + setSize);
                    foreach (var subset in sizesToSubsets[setSize])
                    {
                        foreach (var element in subset.Where(x => x != startIndex))
                        {
                            float? min = null;
                            var minPredecessor = -1;
                            var elementKey = new Tuple<HashSet<int>, int>(subset, element);

                            foreach (var i in subset.Where(x => x != element))
                            {
                                var smallerSet = new HashSet<int>(subset);
                                smallerSet.Remove(element);
                                
                                if (i == startIndex && smallerSet.Count > 1) continue;
                                
                                var key = new Tuple<HashSet<int>, int>(smallerSet, i);
                                var pathLength = (float?)s[key]?[0];
                                if (!pathLength.HasValue) continue;
                                if (!min.HasValue) min = float.MaxValue;
                                var finalLength = pathLength.Value + GetDistance(coords[element], coords[i]);
                                min = Math.Min(min.Value, finalLength);
                                if (min.Value == finalLength) minPredecessor = i;
                            }

                            if (!min.HasValue) s[elementKey] = null;
                            else s[elementKey] = new object[]{min,minPredecessor};
                        }
                    }
                }
            }

            float GetMinTourLength()
            {
                float? minTourLength = null;
            
                var superset = sizesToSubsets[coords.Length - 1][0];

                foreach (var i in superset)
                {
                    if (i == startIndex) continue;
                    var key = new Tuple<HashSet<int>, int>(superset, i);
                    var pathLength = (float?)s[key]?[0];
                    if (!pathLength.HasValue) continue;
                    if (!minTourLength.HasValue) minTourLength = float.MaxValue;
                    var finalLength = GetDistance(coords[i], coords[startIndex]) + pathLength.Value;
                    //var finalLength = pathLength.Value;
                    minTourLength = Math.Min(minTourLength.Value, finalLength);
                }
                return minTourLength ?? float.MaxValue;
            }
            
            int[] GetMinTourPath()
            {
                float? minTourLength = null;

                var superset = sizesToSubsets[coords.Length - 1][0];
                var bestFinish = -1;
                foreach (var i in superset)
                {
                    if (i == startIndex) continue;
                    var key = new Tuple<HashSet<int>, int>(superset, i);
                    var pathLength = (float?)s[key]?[0];
                    if (!pathLength.HasValue) continue;
                    if (!minTourLength.HasValue) minTourLength = float.MaxValue;
                    
                    var finalLength = pathLength.Value;
                    minTourLength = Math.Min(minTourLength.Value, finalLength);
                    if (minTourLength == finalLength) bestFinish = i;
                }

                if (bestFinish == -1) return null;

                var path = new int [coords.Length];
                path[coords.Length - 1] = bestFinish;
                var bestPathNode = bestFinish;
                var currentSet = new HashSet<int>(superset);
                
                for (int i = coords.Length - 1; i > 0; i--)
                {
                    Console.WriteLine(bestPathNode);
                    var currentKey = new Tuple<HashSet<int>, int>(currentSet,bestPathNode);
                    var temp = bestPathNode;
                    Console.WriteLine($"{currentKey.Item2} and Hashset: {string.Join(", ", currentKey.Item1)}");
                    try
                    {
                        bestPathNode = (int)s[currentKey][1];
                    }
                    catch (KeyNotFoundException e)
                    {
                        Console.WriteLine($"{currentKey.Item2} and Hashset: {string.Join(", ", currentKey.Item1)}");
                        throw;
                    }
                    
                    currentSet.Remove(temp);
                    path[i-1] = bestPathNode;
                }
                return path;
            }
        }

        public int[] GetOptimalTourTest(int[] coords)
        {
            var sizesToSubsets = GenerateSubsets(coords.Length);
            var s = new Dictionary<Tuple<HashSet<int>, int>, float?>(new TupleKeysComparer());
            var startIndex = 0;

            var pathCache = new int [coords.Length, coords.Length];
            var minLengthsCache = new float [coords.Length, coords.Length];
            for (int i = 0; i < coords.Length; i++)
            {
                for (int j = 0; j < coords.Length; j++)
                {
                    minLengthsCache[i, j] = float.MaxValue;
                }
            }

            //add base cases
            foreach (var subset in sizesToSubsets[0])
            {
                var element = subset.First();
                var tup = new Tuple<HashSet<int>, int>(subset, element);
                if (element == startIndex) s[tup] = 0;
                else s[tup] = null;
            }

            CalculateMinPathForAllDestinations();
            return GetMinTourPath();

            void CalculateMinPathForAllDestinations()
            {
                foreach (var setSize in Enumerable.Range(1, coords.Length - 1))
                {
                    Console.WriteLine("Current set size is " + setSize);
                    foreach (var subset in sizesToSubsets[setSize])
                    {
                        foreach (var element in subset.Where(x => x != startIndex))
                        {
                            float? min = null;
                            var elementKey = new Tuple<HashSet<int>, int>(subset, element);

                            foreach (var i in subset.Where(x => x != element))
                            {
                                var smallerSet = new HashSet<int>(subset);
                                smallerSet.Remove(element);

                                if (i == startIndex && smallerSet.Count > 1) continue;

                                var key = new Tuple<HashSet<int>, int>(smallerSet, i);
                                var pathLength = s[key];
                                if (!pathLength.HasValue) continue;
                                if (!min.HasValue) min = float.MaxValue;
                                var finalLength = pathLength.Value + GetDistanceTest(element, i);
                                min = Math.Min(min.Value, finalLength);
                                if (min == finalLength && minLengthsCache[setSize, element] > min)
                                {
                                    pathCache[setSize, element] = i;
                                    minLengthsCache[setSize, element] = min.Value;
                                }
                            }

                            if (!min.HasValue) s[elementKey] = null;
                            else s[elementKey] = min;
                        }
                    }
                }
            }

            float GetMinTourLength()
            {
                float? minTourLength = null;

                var superset = sizesToSubsets[coords.Length - 1][0];

                foreach (var i in superset)
                {
                    if (i == startIndex) continue;
                    var key = new Tuple<HashSet<int>, int>(superset, i);
                    var pathLength = s[key];
                    if (!pathLength.HasValue) continue;
                    if (!minTourLength.HasValue) minTourLength = float.MaxValue;
                    var finalLength = GetDistanceTest(i, startIndex) + pathLength.Value;
                    //var finalLength = pathLength.Value;
                    minTourLength = Math.Min(minTourLength.Value, finalLength);
                }

                return minTourLength ?? float.MaxValue;
            }

            int[] GetMinTourPath()
            {
                float? minTourLength = null;

                var superset = sizesToSubsets[coords.Length - 1][0];
                var bestFinish = -1;
                foreach (var i in superset)
                {
                    if (i == startIndex) continue;
                    var key = new Tuple<HashSet<int>, int>(superset, i);
                    var pathLength = s[key];
                    if (!pathLength.HasValue) continue;
                    if (!minTourLength.HasValue) minTourLength = float.MaxValue;
                    
                    var finalLength = pathLength.Value;
                    minTourLength = Math.Min(minTourLength.Value, finalLength);
                    if (minTourLength == finalLength) bestFinish = i;
                }

                if (bestFinish == -1) return null;

                var path = new int [coords.Length];
                path[coords.Length - 1] = bestFinish;
                for (int i = coords.Length - 1; i > 0; i--)
                {
                    var previousNode = pathCache[i, bestFinish];
                    path[i-1] = previousNode;
                    bestFinish = previousNode;
                }

                return path;
            }
        }

        private float GetDistance(Vector2 s, Vector2 t)
        {
            var x = s.X - t.X;
            var y = s.Y - t.Y;
            return (float)Math.Sqrt(x * x + y * y);
        }
        
        private float GetDistanceTest(int s, int t)
        {
            var vals = new [,]
            {
                {0,2,1,4},
                {2,0,3,5},
                {1,3,0,6},
                {4,5,6,0}
            };
           
            return vals[s,t];
        }

        private List<HashSet<int>>[] GenerateSubsets(int setSize)
        {
            var powerSet = new List<HashSet<int>>[setSize];

            foreach (var bitSet in Enumerable.Range(1, (1 << setSize) - 1))
            {
                if (bitSet % 1000000 == 0)
                {
                    Console.WriteLine("Set number " + bitSet);
                }
                
                var hashSet = new HashSet<int>();
                foreach (var itemIndex in Enumerable.Range(0, setSize))
                {
                    if ((bitSet & (1 << itemIndex)) != 0) hashSet.Add(itemIndex);
                }

                var subsetSize = hashSet.Count - 1;
                if (powerSet[subsetSize] == null) powerSet[subsetSize] = new List<HashSet<int>> {hashSet};
                else powerSet[subsetSize].Add(hashSet);
            }
            return powerSet;
        }
    }

    internal class TupleKeysComparer : IEqualityComparer<Tuple<HashSet<int>, int>>
    {
        public bool Equals(Tuple<HashSet<int>, int> x, Tuple<HashSet<int>, int> y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.Item1.SetEquals(y.Item1) && x.Item2 == y.Item2;
        }

        public int GetHashCode(Tuple<HashSet<int>, int> obj)
        {
            var (hashSet, item2) = obj;
            if (hashSet == null) return 0;
            var h = 0x14345843 + hashSet.Sum(elem => hashSet.Comparer.GetHashCode(elem));
            h = (h * 7) + item2.GetHashCode();
            return h;
        }
    }
}