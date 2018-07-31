using System;
using System.Collections.Generic;

namespace Algorithms.Stanford.GraphStructures
{
    public static class GraphGenerator
    {
        public static Dictionary<int, List<int>> CreateAdjacencyList(int[][] graphArray)
        {
            var adjacencyList = new Dictionary<int, List<int>>();

            InitializeAdjacencyList(adjacencyList, graphArray.Length);

            foreach (var vertexArray in graphArray)
            {
                var vertex = vertexArray[0] - 1;

                for (int j = 1; j < vertexArray.Length; j++)
                {
                    adjacencyList[vertex].Add(vertexArray[j] - 1);
                }
            }

            return adjacencyList;
        }

        public static List<int[]> CreateEdgesList(int[][] graphArray)
        {
            var edgesList = new List<int[]>();

            foreach (var vertexArray in graphArray)
            {
                var vertex = vertexArray[0] - 1;
                for (int j = 1; j < vertexArray.Length; j++)
                {
                    var adjacentVertex = vertexArray[j] - 1;
                    if (vertex > adjacentVertex) continue;

                    edgesList.Add(new[] { vertex, adjacentVertex });
                }
            }

            return edgesList;
        }

        public static void InitializeAdjacencyList<T>(Dictionary<int, List<T>> adjacencyList, int vertexCount)
        {
            for (int i = 0; i < vertexCount; i++)
            {
                adjacencyList.Add(i, new List<T>());
            }
        }
    }
}