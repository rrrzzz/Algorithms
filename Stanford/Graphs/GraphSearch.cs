using System.Collections.Generic;

namespace Algorithms.Stanford.Graphs
{
	public class GraphSearch
	{
		private bool[] _isNodeVisited;
		private readonly int _nodeCount;

		public GraphSearch(int nodeCount)
		{
			_nodeCount = nodeCount;
			_isNodeVisited = new bool [_nodeCount];
		}

		public void BreadthFirstSearch(Dictionary<int, List<int>> graph, int start)
		{
			var nodeQueue = new Queue<int>();
			_isNodeVisited[start] = true;

			nodeQueue.Enqueue(start);

			while (nodeQueue.Count != 0)
			{
				var currentNode = nodeQueue.Dequeue();
				foreach (var node in graph[currentNode])
				{
					if (_isNodeVisited[node]) continue;

					_isNodeVisited[node] = true;

					nodeQueue.Enqueue(node);
				}
			}
		}

		public void DepthFirstSinkSearchStack(Dictionary<int, List<int>> graph, int start, List<int> finishedNodes)
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

		public void DepthFirstSinkSearchRecursive(Dictionary<int, List<int>> graph, int start, List<int> finishedNodes)
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

		public List<int> GetTopologicalOrdering(Dictionary<int, List<int>> graph)
		{
			var output = new List<int>();

			DepthFirstSinkSearchOnEachNode(graph, output);

			output.Reverse();

			return output;
		}

		public List<List<int>> KasarajuFindSccs(Dictionary<int, List<int>> graph)
		{
			var sccList = new List<List<int>>();
			var reversedGraph = ReverseEdges(graph);
			var explorationFinishTimes = new List<int>();

			DepthFirstSinkSearchOnEachNode(reversedGraph, explorationFinishTimes);

			ResetNodeVisits();

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

		public void ResetNodeVisits()
		{
			_isNodeVisited = new bool[_nodeCount];
		}

		private Dictionary<int, List<int>> ReverseEdges(Dictionary<int, List<int>> graph)
		{
			var reversedGraph = new Dictionary<int, List<int>>(_nodeCount);
			
			GraphGenerator.InitializeAdjacencyList(reversedGraph, _nodeCount);

			foreach (var node in graph)
			{
				var tailNode = node.Key;

				foreach (var headNode in graph[tailNode])
				{
					reversedGraph[headNode].Add(tailNode);
				}
			}

			return reversedGraph;
		}

		private void DepthFirstSinkSearchOnEachNode(Dictionary<int, List<int>> graph, List<int> finishedNodes)
		{
			foreach (var node in graph)
			{
				var nodeKey = node.Key;
				{
					if (_isNodeVisited[nodeKey]) continue;

					DepthFirstSinkSearch(graph, nodeKey, finishedNodes);
				}
			}
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