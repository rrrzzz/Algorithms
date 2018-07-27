using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Stanford.GraphStructures
{
    public class KargerMinCut
    {
        private Dictionary<int, List<int>> _adjacencyList;
        private List<int[]> _edgesList;
        private readonly int _verticesCount;
        private readonly int[][] _graphArray;

        public KargerMinCut(int[][] graphArray)
        {
            _graphArray = graphArray;
            _verticesCount = _graphArray.Length;
        }

        public int FindMinCutAdjacencyList()
        {
            var trialsNumber = GetDefaultTrials();
            var lowestCut = int.MaxValue;

            for (int i = 0; i < trialsNumber; i++)
            {
                var randomGenerator = new Random(i);
                var currentCut = FindMinCutAdjacencyListOnce(randomGenerator);

                if (currentCut > lowestCut) continue;
                lowestCut = currentCut;
            }

            return lowestCut;
        }

        public int FindMinCutUnion()
        {
            _edgesList = GraphGenerator.CreateEdgesList(_graphArray);
            var trialsNumber = GetDefaultTrials();
            var lowestCut = int.MaxValue;

            for (int i = 0; i < trialsNumber; i++)
            {
                var randomGenerator = new Random(i);
                var currentCut = FindMinCutUnionOnce(randomGenerator);

                if (currentCut > lowestCut) continue;
                lowestCut = currentCut;
            }

            return lowestCut;
        }

        private int FindMinCutUnionOnce(Random generator)
        {
            var unionStructure = new UnionFind(_verticesCount);
            var verticesCount = _verticesCount;

            while (verticesCount > 2)
            {
                var randomEdge = _edgesList[generator.Next(_edgesList.Count)];

                var firstRoot = unionStructure.FindRoot(randomEdge[0]);
                var secondRoot = unionStructure.FindRoot(randomEdge[1]);

                if (firstRoot == secondRoot) continue;

                unionStructure.Union(firstRoot, secondRoot);
                verticesCount--;
            }

            return CountCuts(_edgesList, unionStructure);
        }

        private int CountCuts(List<int[]> edgesList, UnionFind unionStructure)
        {
            var cutCount = 0;

            foreach (var edge in edgesList)
            {
                var firstRoot = unionStructure.FindRoot(edge[0]);
                var secondRoot = unionStructure.FindRoot(edge[1]);

                if (firstRoot == secondRoot) continue;
                cutCount++;
            }

            return cutCount;
        }

        private int GetDefaultTrials()
        {
            return _verticesCount * _verticesCount * (int)Math.Ceiling(Math.Log(_verticesCount));
        }

        private int FindMinCutAdjacencyListOnce(Random generator)
        {
            _adjacencyList = GraphGenerator.CreateAdjacencyList(_graphArray);
            while (_adjacencyList.Count > 2)
            {
                var randomNum = generator.Next(_adjacencyList.Count);

                var start = _adjacencyList.Keys.ElementAt(randomNum);
                var startEdges = _adjacencyList[start];

                var finish = startEdges[generator.Next(startEdges.Count)];
                var finishEdges = _adjacencyList[finish];

                startEdges.RemoveAll(x => x == finish);

                foreach (var vertex in finishEdges)
                {
                    if (vertex == start) continue;

                    startEdges.Add(vertex);
                    _adjacencyList[vertex].RemoveAll(x => x == finish);
                    _adjacencyList[vertex].Add(start);
                }

                _adjacencyList.Remove(finish);
            }

            return _adjacencyList[_adjacencyList.Keys.ElementAt(0)].Count;
        }
    }
}