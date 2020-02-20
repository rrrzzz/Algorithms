using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.Graphs
{
    public class DijkstraShortestPath
    {
        public int[] GetShortestPaths(Dictionary<int,NodeWeighted> graph, int startingVertex)
        {
            var visitedNodesCount = 1;
            var closestUnvisitedNodes = new MinHeapUniversal<NodeWeighted>();
            
            var pathLengths = new int[graph.Count + 1];
            var firstNode = graph[startingVertex];

            firstNode.Value = 0;
            pathLengths[startingVertex] = 0;
            var lastAddedNode = firstNode;
            lastAddedNode.Visit();
            while (visitedNodesCount++ != graph.Count - 1)
            {
                var unvisitedHeadNodes = lastAddedNode.Neighbours.FindAll(x => !x.Item1.IsVisited);
            
                foreach (var nodeWeightTuple in unvisitedHeadNodes)
                {
                    var (neighbour, edgeLength) = nodeWeightTuple;
                    var pathLength = edgeLength + lastAddedNode.Value;
                    var isNodeInHeap = neighbour.HeapIndex != 0;
            
                    if (isNodeInHeap)
                    {
                        closestUnvisitedNodes.TryDecreaseKey(neighbour.HeapIndex, pathLength);
                    }
                    else
                    {
                        neighbour.Value = pathLength;
                        closestUnvisitedNodes.InsertElement(neighbour);
                    }
                }
            
                if (closestUnvisitedNodes.GetHeapSize() != 0)
                {
                    lastAddedNode = closestUnvisitedNodes.ExtractMinElement();
                    lastAddedNode.Visit();
                    lastAddedNode.HeapIndex = 0;
                    pathLengths[lastAddedNode.Id] = lastAddedNode.Value;
                }
            }

            return pathLengths;
        }

        public int GetMinAllPairsShortestPaths(Dictionary<int, NodeWeighted> graph)
        {
            var vertCount = graph.Count;
            var min = int.MaxValue;
            
            foreach (var startingNode in graph)
            {
                var sourceId = startingNode.Key;
                var sourceJohnsonVal = graph[sourceId].JohnsonsValue;
                var visitedNodesCount = 0;
                var closestUnvisitedNodes = new MinHeapUniversal<NodeWeighted>();
            
                var sourceNode = graph[sourceId];
            
                sourceNode.Value = 0;
               
                foreach (var node in graph)
                {
                    closestUnvisitedNodes.InsertElement(node.Value);
                }
                
                while (visitedNodesCount++ != vertCount)
                {
                    var lastAddedNode = closestUnvisitedNodes.ExtractMinElement();
                    lastAddedNode.HeapIndex = 0;

                    if (lastAddedNode.Id != sourceId)
                    {
                        var adjustedPathValue = lastAddedNode.Value + lastAddedNode.JohnsonsValue - sourceJohnsonVal;
                        min = Math.Min(min, adjustedPathValue);
                    }
                    
                    foreach (var nodeWeightTuple in lastAddedNode.Neighbours)
                    {
                        var (neighbour, edgeLength) = nodeWeightTuple;
                        if (neighbour.HeapIndex == 0) continue;
                        
                        var pathLength = edgeLength + lastAddedNode.Value;
                        closestUnvisitedNodes.TryDecreaseKey(neighbour.HeapIndex, pathLength);
                    }
            
                    lastAddedNode.Value = Globals.DefaultDijkstraValue;
                }
            }
            return min;
        }
    }
}