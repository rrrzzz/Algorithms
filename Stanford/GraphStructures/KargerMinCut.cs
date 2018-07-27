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
        private int _seed;

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

        public int FindMinCutKargerStein()
        {
            _edgesList = GraphGenerator.CreateEdgesList(_graphArray);
            var graph = new UnionFind(_verticesCount);
            
            return FindMinCutKargerSteinRecursive(graph, _verticesCount);
        }

        private int FindMinCutUnionOnce(Random generator)
        {
            var graph = new UnionFind(_verticesCount);
            var vertexLimit = 2;

            ContractGraph(generator, graph, _verticesCount, vertexLimit);

            return CountCuts(graph);
        }

        private int CountCuts(UnionFind graph)
        {
            var cutCount = 0;

            foreach (var edge in _edgesList)
            {
                var firstRoot = graph.FindRoot(edge[0]);
                var secondRoot = graph.FindRoot(edge[1]);

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

        private int FindMinCutKargerSteinRecursive(UnionFind graph, int verticesCount)
        {
            int vertexLimit;
            var random = new Random(++_seed);
            
            if (verticesCount <= 6)
            {
                vertexLimit = 2;

                ContractGraph(random, graph, verticesCount, vertexLimit);

                return CountCuts(graph);
            }

            vertexLimit = (int) Math.Ceiling(1 + verticesCount / Math.Sqrt(2));

            var graph1 = UtilityMethods.CopyUnionFind(graph);
            var graph2 = UtilityMethods.CopyUnionFind(graph);

            ContractGraph(new Random(++_seed), graph1, verticesCount, vertexLimit);
            ContractGraph(new Random(++_seed), graph2, verticesCount, vertexLimit);

            var firstMinCut = FindMinCutKargerSteinRecursive(graph1, vertexLimit);
            var secondMinCut = FindMinCutKargerSteinRecursive(graph2, vertexLimit);

            return secondMinCut < firstMinCut ? secondMinCut : firstMinCut;
        }

        private void ContractGraph(Random generator, UnionFind graph, int verticesCount, int vertexLimit)
        {
            while (verticesCount != vertexLimit)
            {
                var randomEdge = _edgesList[generator.Next(_edgesList.Count)];

                var firstRoot = graph.FindRoot(randomEdge[0]);
                var secondRoot = graph.FindRoot(randomEdge[1]);

                if (firstRoot == secondRoot) continue;

                graph.Union(firstRoot, secondRoot);
                verticesCount--;
            }
        }
    }
}