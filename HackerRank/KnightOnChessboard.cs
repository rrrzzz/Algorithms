using System;
using System.Collections.Generic;

namespace Algorithms.HackerRank
{
    public class KnightOnChessboard
    {
        //Problem description: https://www.hackerrank.com/contests/rookierank-2/challenges/knightl-on-chessboard

        private Dictionary<Tuple<int, int>, int> Solutions { get; } = new Dictionary<Tuple<int, int>, int>();

        public void Solve(int n)
        {
            var nMinus = n - 1;
            for (int i = 1; i < n; i++)
            {
                var answer = "";

                for (int j = 1; j < n; j++)
                {
                    int moves;

                    if (i > j)
                    {
                        var tuple = new Tuple<int, int>(j, i);
                        answer += Solutions[tuple] + " ";
                    }
                    else if (i == j)
                    {
                        moves = nMinus % j == 0 ? nMinus / j : -1;
                        answer += moves + " ";
                    }
                    else
                    {
                        var graph = new GraphKnight(nMinus, i, j);
                        moves = graph.FindShortestPath();
                        answer += moves + " ";
                        Solutions.Add(new Tuple<int, int>(i, j), moves);
                    }
                }

                Console.WriteLine(answer.Trim());
            }
        }
    }

    public class NodeMove
    {
        public bool IsNodeVisited { get; set; }
        public Tuple<int, int> Id { get; set; }
        public List<NodeMove> Parents { get; set; } = new List<NodeMove>();

        public NodeMove(Tuple<int, int> tuple)
        {
            Id = tuple;
        }
    }

    public class GraphKnight
    {
        private bool IsCornerVisited { get; set; }
        private int NMinus { get; }
        private int KnightRangeA { get; }
        private int KnightRangeB { get; }
        private Dictionary<Tuple<int, int>, NodeMove> Graph { get; } = new Dictionary<Tuple<int, int>, NodeMove>();
        private NodeMove Endpoint { get; set; }
        private NodeMove Root { get; } = new NodeMove(new Tuple<int, int>(0, 0));

        public GraphKnight(int nMinus, int a, int b)
        {
            NMinus = nMinus;
            KnightRangeA = a;
            KnightRangeB = b;
        }

        public int FindShortestPath()
        {
            var initialMoves = GetInitialMoves();

            foreach (var initialMove in initialMoves)
            {
                TraceAllMoves(initialMove, Root);
            }

            if (!IsCornerVisited)
            {
                return -1;
            }

            var nodes = Endpoint.Parents;
            var count = 1;

            return FindRoot(nodes, count);
        }

        private List<NodeMove> GetInitialMoves()
        {
            var moves = new List<NodeMove>();
            var tuplesMoves = GetPossibleMoves(Root);
            foreach (var newPosition in tuplesMoves)
            {
                var newNode = new NodeMove(newPosition);
                AddNode(Root, newNode, newPosition);
                moves.Add(newNode);
            }

            return moves;
        }

        private int FindRoot(List<NodeMove> nodes, int count)
        {
            while (true)
            {
                var parents = new List<NodeMove>();
                foreach (var node in nodes)
                {
                    if (node == Root)
                    {
                        return count;
                    }

                    if (!node.IsNodeVisited)
                    {
                        parents.AddRange(node.Parents);
                        node.IsNodeVisited = true;
                    }
                }
                count++;
                nodes = parents;
            }
        }

        private void TraceAllMoves(NodeMove currentPosition, NodeMove previousPosition)
        {
            var previousCoordinates = new Tuple<int, int>(previousPosition.Id.Item1, previousPosition.Id.Item2);
            var tuplesMoves = GetPossibleMoves(currentPosition);

            tuplesMoves.RemoveWhere(x => x.Equals(previousCoordinates));

            foreach (var newPosition in tuplesMoves)
            {
                var nodeToAdd = new NodeMove(newPosition);

                if (AddNode(currentPosition, nodeToAdd, newPosition))
                {
                    TraceAllMoves(nodeToAdd, currentPosition);
                }
            }
        }

        private HashSet<Tuple<int, int>> GetPossibleMoves(NodeMove node)
        {
            var possibleMoves = new HashSet<Tuple<int, int>>();

            var x1 = node.Id.Item1;
            var y1 = node.Id.Item2;

            if (x1 + KnightRangeA <= NMinus)
            {
                if (y1 + KnightRangeB <= NMinus)
                {
                    possibleMoves.Add(new Tuple<int, int>(x1 + KnightRangeA, y1 + KnightRangeB));
                    possibleMoves.Add(new Tuple<int, int>(y1 + KnightRangeB, x1 + KnightRangeA));
                }

                if (y1 - KnightRangeB >= 0)
                {
                    possibleMoves.Add(new Tuple<int, int>(x1 + KnightRangeA, y1 - KnightRangeB));
                    possibleMoves.Add(new Tuple<int, int>(y1 - KnightRangeB, x1 + KnightRangeA));
                }
            }

            if (x1 - KnightRangeA >= 0)
            {
                if (y1 + KnightRangeB <= NMinus)
                {
                    possibleMoves.Add(new Tuple<int, int>(x1 - KnightRangeA, y1 + KnightRangeB));
                    possibleMoves.Add(new Tuple<int, int>(y1 + KnightRangeB, x1 - KnightRangeA));
                }

                if (y1 - KnightRangeB >= 0)
                {
                    possibleMoves.Add(new Tuple<int, int>(x1 - KnightRangeA, y1 - KnightRangeB));
                    possibleMoves.Add(new Tuple<int, int>(y1 - KnightRangeB, x1 - KnightRangeA));
                }
            }

            return possibleMoves;
        }

        private bool AddNode(NodeMove parentNode, NodeMove nodeToAdd, Tuple<int, int> coordinates)
        {
            if (Graph.ContainsKey(coordinates))
            {
                var existingNode = Graph[coordinates];
                parentNode.Parents.Add(existingNode);
                existingNode.Parents.Add(parentNode);

                return false;
            }

            nodeToAdd.Parents.Add(parentNode);
            parentNode.Parents.Add(nodeToAdd);
            Graph.Add(coordinates, nodeToAdd);

            if (coordinates.Item1 == coordinates.Item2 && coordinates.Item2 == NMinus)
            {
                IsCornerVisited = true;
                Endpoint = nodeToAdd;
                return false;
            }

            return true;
        }
    }
}
