using System;
using System.Linq;
using System.Net;

namespace Algorithms.Stanford.GraphStructures
{
    public class MinCutSolver
    {
        /* The file contains the adjacency list representation of a simple undirected graph.There are 200 vertices labeled 1 to 200. The first column in the file represents the vertex label, and the particular row (other entries except the first column) tells all the vertices that the vertex is adjacent to.So for example, the row looks like : "6	155	56	52	120	......". This just means that the vertex with label 6 is adjacent to (i.e., shares an edge with) the vertices with labels 155,56,52,120,......, etc

         Your task is to code up and run the randomized contraction algorithm for the min cut problem and use it on the above graph to compute the min cut. (HINT: Note that you'll have to figure out an implementation of edge contractions. Initially, you might want to do this naively, creating a new graph from the old every time there's an edge contraction. But you should also think about more efficient implementations.) (WARNING: As per the video lectures, please make sure to run the algorithm many times with different random seeds, and remember the smallest cut that you ever find.) Write your numeric answer in the space provided.So e.g., if your answer is 5, just type 5 in the space provided.

        https://lagunita.stanford.edu/assets/courseware/v1/d81819849ab16ea07f97e4814a7f76d0/asset-v1:Engineering+Algorithms1+SelfPaced+type@asset+block/kargerMinCut.txt */
        //answer is 17

        private readonly int[][] _graphArray;
        

        public MinCutSolver(int[][] graphArray)
        {
            _graphArray = graphArray;
        }

        public MinCutSolver(string link)
        {
            _graphArray = ParseGraphArrayStanfordWebSource(link);
        }

        public int FindMinCutByTrialsUnion()
        {
            var minCut = new KargerMinCut(_graphArray);
            return minCut.FindMinCutUnion();
        }

        public int FindMinCutByTrialsSlow()
        {
            var minCut = new KargerMinCut(_graphArray);
            return minCut.FindMinCutAdjacencyList();
        }

        public int FindMinCutByKargerStein()
        {
            var minCut = new KargerMinCut(_graphArray);
            return minCut.FindMinCutKargerStein();
        }

        private int[][] ParseGraphArrayStanfordWebSource(string link)
        {
            string graphStringTable;
            using (var webClinet = new WebClient())
            {
                graphStringTable = webClinet.DownloadString(link);
            }

            var graphStringArray = graphStringTable.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var graphFinalArray = new int[graphStringArray.Length][];

            for (int i = 0; i < graphStringArray.Length; i++)
            {
                var temp = graphStringArray[i].Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                graphFinalArray[i] = temp.Select(int.Parse).ToArray();
            }

            return graphFinalArray;
        }
    }
}