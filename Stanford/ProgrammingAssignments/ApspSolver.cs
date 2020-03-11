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
        // Answer: -19

        private const string Link1 = "https://d3c33hcgiwev3.cloudfront.net/_6ff856efca965e8774eb18584754fd65_g1.txt?Expires=1584057600&Signature=Ig6aHCazWLeXeHzAKuuLB5ugWPpWd5A66STZ6mMlH6oYjAQmqMMgA77wGUYlv-MUV~QLwA-Z~IQB7KqzlbA8K3p-XYYCip96cvPIsVMpZGeJCdX9pWzCwhhMm5h3s~AAo6zwqyYYB~QbayO2kAZAxTR-xZqSAQuG5i1Og~GafIc_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        private const string Link2 = "https://d3c33hcgiwev3.cloudfront.net/_6ff856efca965e8774eb18584754fd65_g2.txt?Expires=1584057600&Signature=B9F2CWthoqiC6iZ9-mGhITfuClhJi5rmyBTqJOUA8XgV68MfctphmRxGrCojnRbbu1wqF6jnnCghXuzgUasIkpEmflV5Qu8Dfzj1fc7K0FwXYlPPRuupkTD1XlT-AMjJk4jWTYvSmQgU8fiLsOeP7bF4wv3B~CN9DfgaW0dvyLc_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        private const string Link3 = "https://d3c33hcgiwev3.cloudfront.net/_6ff856efca965e8774eb18584754fd65_g3.txt?Expires=1584057600&Signature=Lh8r25G3gs0FTt0URG89FGmlD0aP1yZ--cUtu7znXQyrORhVTEnAjz3UptxEVK~2ERfn5GsHKSRWY9N-EuU5BjTNRfPcPigjlwnkKAUDl5wfh-MhFJEbT2Y7bQEHtAxPIJw2WgKvB5ad5yYdWhcwwLjoQNg7z5FTzBhEEp-CiM8_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        private const string Link4 = "https://d3c33hcgiwev3.cloudfront.net/_919f8ed0c52d8b5926aa7e3fdecc2d32_large.txt?Expires=1584057600&Signature=EpO4kNU74nTGw0laFhm3pD8IVMNVJinPFw~mmQePsPrf4NZpT~izgKIJUoLfs6tol-DZg~dx~nt29KGJNb0v2tbKdeWF6v0~EUPVzmr-T2fqbqpH2c2oFWjFH~~-AbniCOCxr5UvYF04zKvMUY3b9VcfvU60wLswPqd1Zi5rPEY_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";

        public IEnumerable<int?> SolveFirstLink()
        {
            var g = ParseWeightedGraphFromWeb(Link1);
            return new BellmanFord().GetShortestPaths(g, 1);
        }
        
        public int SolveSecondLink()
        {
            var g = ParseWeightedGraphFromWeb(Link2);
            var reweightedGraph = new JohnsonReweighting().ReweightEdges(g); 
            return new DijkstraShortestPath().GetMinAllPairsShortestPaths(reweightedGraph);
        }
        
        public int SolveThirdLink()
        {
            var g = ParseWeightedGraphFromWeb(Link3);
            
            var reweightedGraph = new JohnsonReweighting().ReweightEdges(g); 
            var res = new DijkstraShortestPath().GetMinAllPairsShortestPaths(reweightedGraph);

            return res;
        }public int SolveFourthLink()
        {
            var g = ParseWeightedGraphFromWeb(Link4);
            
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
                case 4:
                    link = Link4;
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