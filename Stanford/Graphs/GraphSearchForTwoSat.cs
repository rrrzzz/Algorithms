using System.Collections.Generic;

namespace Algorithms.Stanford.Graphs
{
    public class GraphSearchForTwoSat
	{
		private Dictionary<int, bool> _isNodeVisited;
		private readonly int _nodeCount;

		public GraphSearchForTwoSat(int nodeCount)
		{
			_nodeCount = nodeCount;
			_isNodeVisited = new Dictionary<int, bool>(_nodeCount);
		}
		
		public List<List<int>> KasarajuFindSccs(Dictionary<int, List<int>> graph)
		{
			var sccList = new List<List<int>>();
			var reversedGraph = ReverseEdges(graph);
			ResetNodeVisits(reversedGraph, graph);
			var explorationFinishTimes = DepthFirstSinkSearchOnEachNode(reversedGraph);
			
			ResetNodeVisits(graph, reversedGraph);

			for (int i = explorationFinishTimes.Count - 1; i >= 0; i--)
			{
				var currentNode = explorationFinishTimes[i];
				if(_isNodeVisited[currentNode]) continue;

				var tempList = new List<int>();
				DepthFirstSinkSearch(graph, currentNode, tempList);
				sccList.Add(tempList);
			}

			return sccList;
		}

		private void DepthFirstSinkSearchStack(Dictionary<int, List<int>> graph, int start, List<int> finishedNodes)
		{
			var nodeStack = new Stack<int>();
			nodeStack.Push(start);

			while (nodeStack.Count != 0)
			{
				var currentNode = nodeStack.Peek();
				
				_isNodeVisited[currentNode] = true;

				var hasNeighbours = false;

				if (!graph.ContainsKey(currentNode))
				{
					finishedNodes.Add(nodeStack.Pop());
					continue;
				}

				foreach (var node in graph[currentNode])
				{
					if (_isNodeVisited[node]) continue;

					hasNeighbours = true;
					nodeStack.Push(node);
					break;
				}

				if (hasNeighbours) continue;

				finishedNodes.Add(nodeStack.Pop());
			}
		}

		private void DepthFirstSinkSearchRecursive(Dictionary<int, List<int>> graph, int start, List<int> finishedNodes)
		{
			_isNodeVisited[start] = true;

			if (!graph.ContainsKey(start))
			{
				finishedNodes.Add(start);
				return;
			}

			var currentNodeList = graph[start];
			foreach (var node in currentNodeList)
			{
				if (!_isNodeVisited[node])
				{
					DepthFirstSinkSearchRecursive(graph, node, finishedNodes);
				} 
			}

			finishedNodes.Add(start);
		}

		private void ResetNodeVisits(Dictionary<int, List<int>> graph, Dictionary<int, List<int>> graph2)
		{
			foreach (var node in graph.Keys)
			{
				_isNodeVisited[node] = false;
			}
			
			foreach (var node in graph2.Keys)
			{
				_isNodeVisited[node] = false;
			}
		}
		
		private Dictionary<int, List<int>> ReverseEdges(Dictionary<int, List<int>> graph)
		{
			var reversedGraph = new Dictionary<int, List<int>>(_nodeCount);

			foreach (var node in graph.Keys)
			{
				var tailNode = node;

				foreach (var headNode in graph[tailNode])
				{
					if (!reversedGraph.ContainsKey(headNode)) reversedGraph.Add(headNode, new List<int>());
					
					reversedGraph[headNode].Add(tailNode);
				}
			}

			return reversedGraph;
		}

		private List<int> DepthFirstSinkSearchOnEachNode(Dictionary<int, List<int>> graph)
		{
			var visitedNodes = new List<int>();
			foreach (var node in graph.Keys)
			{
				{
					if (_isNodeVisited[node]) continue;

					DepthFirstSinkSearch(graph, node, visitedNodes);
				}
			}

			return visitedNodes;
		}

		private void DepthFirstSinkSearch(Dictionary<int, List<int>> graph, int start, List<int> finishedNodes)
		{
			if (_nodeCount < 100000)
			{
				DepthFirstSinkSearchRecursive(graph, start, finishedNodes);
				return;
			}

			DepthFirstSinkSearchStack(graph, start, finishedNodes);
		}
	}
}