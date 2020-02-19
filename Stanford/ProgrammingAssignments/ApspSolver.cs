using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Algorithms.Stanford.Dynamic_Programming;
using Algorithms.Stanford.Graphs;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public class ApspSolver
    {
        // In this assignment you will implement one or more algorithms for the all-pairs shortest-path problem. Here are data files describing three graphs:
        //
        // g1.txt 
        // g2.txt
        // g3.txt
        //
        //The first line indicates the number of vertices and edges, respectively. Each subsequent line describes an edge
        //(the first two numbers are its tail and head, respectively) and its length (the third number). NOTE: some of the
        //edge lengths are negative. NOTE: These graphs may or may not have negative-cost cycles.
        //Your task is to compute the shortest shortest path of three
        //

        private const string Link1 = "https://lagunita.stanford.edu/assets/courseware/v1/c614f1fbb78bca7c98067a9038976ffb/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/g1.txt";
        private const string Link2 = "https://lagunita.stanford.edu/assets/courseware/v1/617a8dccb5474bdf76f973f553f83777/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/g2.txt";
        private const string Link3 = "https://lagunita.stanford.edu/assets/courseware/v1/9f0236832060a0f2ccfcb43f07301b7d/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/g3.txt";

        public IEnumerable<int?> SolveFirstLink()
        {
            var g = ParseWeightedGraphFromWeb(Link1);
            return new BellmanFord().GetShortestPaths(g, 1);
        }
        
        public IEnumerable<int?> SolveSecondLink()
        {
            var g = ParseWeightedGraphFromWeb(Link2);
            return new BellmanFord().GetShortestPaths(g, 1);
        }
        
        public int SolveThirdLink()
        {
            var g = ParseWeightedGraphFromWeb(Link3);
            var reweightedGraph = new JohnsonReweighting().ReweightEdges(g); 
            var res = new DijkstraShortestPath().GetMinAllPairsShortestPaths(reweightedGraph);

            return res;
        }

        public Dictionary<int, NodeWeighted> GetGraph(int linkNum)
        {
            string link;
            switch (linkNum)
            {
                case 1:
                    link = Link1;
                    break;
                case 2:
                    link = Link2;
                    break;
                case 3:
                    link = Link3;
                    break;
                default:
                    link = "yo mamas dick";
                    break;
            }
            return ParseWeightedGraphFromWeb(link);;
        }

        private Dictionary<int, NodeWeighted> ParseWeightedGraphFromWeb(string link)
        {
            var parsed = UtilityMethods.GetParsedStringArrayFromWeb(link, '\n');

            return UtilityMethods.GetTailHeadWeightSpaceGraph(parsed.Skip(1));
        }
    }
}