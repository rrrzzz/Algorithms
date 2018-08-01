using System.Collections.Generic;
using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.Graphs
{
    public class DijkstraShortestPath
    {
        public int[] GetShortestPaths(Dictionary<int,Node> graph, int startingVertex)
        {
            var visitedNodesCount = 0;
            var closestUnvisitedNodesHeap = new MinHeap<Node>();
            
            var pathLengths = new int[graph.Count + 1];
            var firstNode = graph[startingVertex];

            firstNode.DijkstraScore = 0;
            pathLengths[startingVertex] = 0;
            visitedNodesCount++;
            firstNode.Visit();

            var lastAddedNode = firstNode;

            while (visitedNodesCount != graph.Count)
            {
                var unvisitedHeadNodes = lastAddedNode.Neighbours.FindAll(x => !x.Item1.IsVisited);

                foreach (var nodeTuple in unvisitedHeadNodes)
                {
                    var currentNode = nodeTuple.Item1;
                    var pathLength = nodeTuple.Item2 + lastAddedNode.DijkstraScore;

                    if (currentNode.HeapIndex != 0)
                    {
                        closestUnvisitedNodesHeap.DecreaseKey(currentNode.HeapIndex, pathLength);
                    }
                    else
                    {
                        currentNode.DijkstraScore = pathLength;
                        closestUnvisitedNodesHeap.InsertElement(currentNode);
                    }
                }

                lastAddedNode = closestUnvisitedNodesHeap.ExtractMinElement();
                lastAddedNode.HeapIndex = 0;
                lastAddedNode.Visit();
                pathLengths[lastAddedNode.Id] = lastAddedNode.DijkstraScore;
                visitedNodesCount++;
            }

            return pathLengths;
        }
    }
}
