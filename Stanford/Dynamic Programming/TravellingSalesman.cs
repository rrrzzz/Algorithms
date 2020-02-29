using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Instrumentation;
using System.Numerics;
using System.Threading;
using Algorithms.Stanford.Misc;

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
        private const int StartIndex = 0;

        public Tuple<float, int[]> GetTourPathLengthToLastNode(Vector2[] coords)
        {
            var s = CalculateMinPathForAllDestinations(coords);
            float? minTourLength = null;

            var superset = new HashSet<int>(Enumerable.Range(0, coords.Length));
            var bestFinish = -1;
            foreach (var i in superset)
            {
                if (i == StartIndex) continue;
                var key = new Tuple<HashSet<int>, int>(superset, i);
                var pathLength = (float?) s[key]?[0];
                if (!pathLength.HasValue) continue;
                if (!minTourLength.HasValue) minTourLength = float.MaxValue;

                var finalLength = GetDistance(coords[i], coords[StartIndex]) + pathLength.Value;
                minTourLength = Math.Min(minTourLength.Value, finalLength);
                if (Math.Abs(minTourLength.Value - finalLength) < 0.001) bestFinish = i;
            }

            if (bestFinish == -1 || !minTourLength.HasValue) return null;

            var path = new int [coords.Length];
            path[coords.Length - 1] = bestFinish;
            var bestPathNode = bestFinish;
            var currentSet = new HashSet<int>(superset);

            for (int i = coords.Length - 1; i > 0; i--)
            {
                var currentKey = new Tuple<HashSet<int>, int>(currentSet, bestPathNode);
                var temp = bestPathNode;
                bestPathNode = (int) s[currentKey][1];
                currentSet.Remove(temp);
                path[i - 1] = bestPathNode;
            }

            return new Tuple<float, int[]>(minTourLength.Value, path);
        }

        private Dictionary<Tuple<HashSet<int>, int>, object[]> CalculateMinPathForAllDestinations(Vector2[] coords)
        {
            var s = new Dictionary<Tuple<HashSet<int>, int>, object[]>(new TupleKeysComparer());
            var sizesToSubsets = GenerateSubsets(coords.Length);

            foreach (var subset in Enumerable.Range(0, coords.Length).Select(x => new HashSet<int> {x}))
            {
                var element = subset.First();
                var tup = new Tuple<HashSet<int>, int>(subset, element);
                if (element == StartIndex)
                {
                    s[tup] = new object[] {(float?) 0, 0};
                }
                else s[tup] = null;
            }

            foreach (var setSize in Enumerable.Range(1, coords.Length - 1))
            {
                foreach (var subset in sizesToSubsets[setSize])
                {
                    foreach (var element in subset.Where(x => x != StartIndex))
                    {
                        float? min = null;
                        var minPredecessor = -1;
                        var elementKey = new Tuple<HashSet<int>, int>(subset, element);

                        foreach (var i in subset.Where(x => x != element))
                        {
                            var smallerSet = new HashSet<int>(subset);
                            smallerSet.Remove(element);

                            if (i == StartIndex && smallerSet.Count > 1) continue;

                            var key = new Tuple<HashSet<int>, int>(smallerSet, i);
                            var pathLength = (float?) s[key]?[0];
                            if (!pathLength.HasValue) continue;
                            if (!min.HasValue) min = float.MaxValue;

                            var finalLength = pathLength.Value + GetDistance(coords[element], coords[i]);

                            min = Math.Min(min.Value, finalLength);

                            if (Math.Abs(min.Value - finalLength) < 0.001) minPredecessor = i;
                        }

                        if (!min.HasValue) s[elementKey] = null;
                        else s[elementKey] = new object[] {min, minPredecessor};
                    }
                }
            }

            return s;
        }

        private float GetDistance(Vector2 s, Vector2 t)
        {
            var x = s.X - t.X;
            var y = s.Y - t.Y;
            return (float) Math.Sqrt(x * x + y * y);
        }

        private List<HashSet<int>>[] GenerateSubsets(int setSize)
        {
            var powerSet = new List<HashSet<int>>[setSize];

            foreach (var bitSet in Enumerable.Range(1, (1 << setSize) - 1))
            {
                var hashSet = new HashSet<int>();
                if ((bitSet & 1 << StartIndex) == 0) continue;

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