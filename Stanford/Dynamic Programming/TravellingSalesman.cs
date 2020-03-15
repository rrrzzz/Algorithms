using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Numerics;
using System.Text;
using System.Threading;
using Algorithms.Stanford.DataStructures;
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

        public int GetTourGreedy(VectorDouble[] coords)
        {
            var pathCost = 0d;
            var visitedCities = new List<int>{0};

            var unvisitedCities = new HashSet<int>(Enumerable.Range(1,coords.Length - 1));

            var currentCity = 0;
            
            while (unvisitedCities.Count != 0)
            {
                var min = double.MaxValue;
                var minIndex = -1;

                var currentCoords = coords[currentCity];
                foreach (var city in unvisitedCities)
                {
                    var d = GetDistanceFast(currentCoords, coords[city]);
                    if (d < min)
                    {
                        min = d;
                        minIndex = city;
                    }
                    else if (Math.Abs(d - min) < 0.001f && city < minIndex) minIndex = city;
                }
            
                currentCity = minIndex;
                visitedCities.Add(minIndex);
                unvisitedCities.Remove(minIndex);
            }
            
            for (int i = 0; i < visitedCities.Count-1; i++)
            {
                pathCost += GetDistance(coords[visitedCities[i]], coords[visitedCities[i+1]]);
            }

            var lastCityIndex = visitedCities[visitedCities.Count - 1];
            
            pathCost += GetDistance(coords[StartIndex], coords[lastCityIndex]);

            return (int)pathCost;
        }
        
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
        
        public decimal Sqrt(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }

        private float GetDistance(Vector2 s, Vector2 t)
        {
            var x = s.X - t.X;
            var y = s.Y - t.Y;
            return (float) Math.Sqrt(x * x + y * y);
        }
        
        private double GetDistance(VectorDouble s, VectorDouble t)
        {
            var x = s.X - t.X;
            var y = s.Y - t.Y;
            
            return Math.Sqrt(x * x + y * y);
        }
        
        private double GetDistanceFast(VectorDouble s, VectorDouble t)
        {
            var x = s.X - t.X;
            var y = s.Y - t.Y;
            return x * x + y * y;
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