using System.Collections.Generic;
using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.Graphs
{
    public class DijkstraShortestPath
    {
        public int[] GetShortestPaths(Dictionary<int,Node> graph, int startingVertex)
        {
            var visitedNodesCount = 0;
            var closestUnvisitedNodes = new MinHeapUniversal<Node>();
            
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

                foreach (var nodeWeightTuple in unvisitedHeadNodes)
                {
                    var currentNode = nodeWeightTuple.Item1;
                    var pathLength = nodeWeightTuple.Item2 + lastAddedNode.DijkstraScore;
                    var isNodeInHeap = currentNode.HeapIndex != 0;

                    if (isNodeInHeap)
                    {
                        closestUnvisitedNodes.DecreaseKey(currentNode.HeapIndex, pathLength);
                    }
                    else
                    {
                        currentNode.DijkstraScore = pathLength;
                        closestUnvisitedNodes.InsertElement(currentNode);
                    }
                }

                lastAddedNode = closestUnvisitedNodes.ExtractMinElement();
                lastAddedNode.Visit();
                pathLengths[lastAddedNode.Id] = lastAddedNode.DijkstraScore;
                visitedNodesCount++;
            }

            return pathLengths;
        }
    }
}