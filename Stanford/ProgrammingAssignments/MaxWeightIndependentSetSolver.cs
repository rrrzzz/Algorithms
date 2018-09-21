using System.Text;
using Algorithms.Stanford.Graphs;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    //In this programming problem you'll code up the dynamic programming algorithm for computing a maximum-weight independent set of a path graph.
    //Your task in this problem is to run the dynamic programming algorithm (and the reconstruction procedure) from lecture on this data set. The question is: of the vertices 1, 2, 3, 4, 17, 117, 517, and 997, which ones belong to the maximum-weight independent set? (By "vertex 1" we mean the first vertex of the graph---there is no vertex 0.) In the box below, enter a 8 - bit string, where the ith bit should be 1 if the ith of these 8 vertices is in the maximum-weight independent set, and 0 otherwise.
    // answer: 10100110

    public static class MaxWeightIndependentSetSolver
    {
        public static string CheckIfNodesPresentInWis(int[] nodesToCheck)
        {
            var maxWeightCalculator = new MaxWeightIndependentSetPathGraph();
            var maximumSet = maxWeightCalculator.GetMaxWeightIndependentSet(ParsePathGraphFromWeb());
            var result = new StringBuilder(nodesToCheck.Length);

            foreach (var node in nodesToCheck)
            {
                result.Append(maximumSet.Contains(node) ? '1' : '0');
            }

            return result.ToString();
        }

        private static int[] ParsePathGraphFromWeb()
        {
            const string link = "https://d3c33hcgiwev3.cloudfront.net/_790eb8b186eefb5b63d0bf38b5096873_mwis.txt?Expires=1537401600&Signature=Atom2t7Rv9U4u9XjiqE7CDpC5szM2tDcIl8aVdKe5uKEbVUt5DNFnlwl9M-4823cYdlLHXQPUlDaI75VzLh8T1L~SIducD6JcL3z8E1JyVH5MuEKoooN9U6Zn2Ho5jM9XWGCVeMz0Y0KIUA8xpXhBlPajSt08fOcOHjUmS7x4ds_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";

            var parsedLines = UtilityMethods.GetNodesParsedStringArray(link, '\n');
            var nodeCount = int.Parse(parsedLines[0]);

            var nodesIntArray = new int[nodeCount];

            for (int i = 1; i < nodeCount; i++)
            {
                nodesIntArray[i - 1] = int.Parse(parsedLines[i]);
            }

            return nodesIntArray;
        }
    }
}