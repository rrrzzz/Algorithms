using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Algorithms.Stanford.Graphs;
using Algorithms.Stanford.Misc;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public class TwoSatSolver
    {
        // In this assignment you will implement one or more algorithms for the 2SAT problem. Here are 6 different 2SAT instances:
        // Your task is to determine which of the 6 instances are satisfiable, and which are unsatisfiable. In the box below,
        // enter a 6-bit string, where the ith bit should be 1 if the ith instance is satisfiable, and 0 otherwise. For example,
        // if you think that the first 3 instances are satisfiable and the last 3 are not, then you should enter the string 111000 in the box below.
        // Answer:

        private const string Link0 = "https://lagunita.stanford.edu/assets/courseware/v1/3b06ac260bfdf1f6b9b2c740f64aa767/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/2sat1.txt";
        private const string Link1 = "https://lagunita.stanford.edu/assets/courseware/v1/00f3826732c3bf38ce375da9b8890b16/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/2sat2.txt";
        private const string Link2 = "https://lagunita.stanford.edu/assets/courseware/v1/3a99efb07b6d4e79e80b1b7cc4409a47/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/2sat3.txt";
        private const string Link3 = "https://lagunita.stanford.edu/assets/courseware/v1/c91f372b2e97093f55924c6915854451/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/2sat4.txt";
        private const string Link4 = "https://s3-us-west-1.amazonaws.com/prod-edx/Algo2/Files/2sat5.txt";
        private const string Link5 = "https://s3-us-west-1.amazonaws.com/prod-edx/Algo2/Files/2sat6.txt";

        private List<string> _links = new List<string>{Link0, Link1, Link2, Link3, Link4, Link5};

        public string SolveTwoSatPapadimitrous()
        {
            var res = "";
            foreach (var link in _links)
            {
                var conditionsStrings = UtilityMethods.GetParsedStringArrayFromWeb(link, '\n').Skip(1).ToArray();
                var count = conditionsStrings.Length;
                var conds = new int[count][];

                for (int i = 0; i < count; i++)
                {
                    var parsed = conditionsStrings[i].Split(' ').Select(int.Parse).ToArray();
                    conds[i] = new[] {parsed[0], parsed[1]};
                }
                
                res += new PapadimitrousTwoSat().GetConditionsSatisfiability(conds) ? 1 : 0;
            }

            return res;
        }
        
        public string SolveTwoSatScc()
        {
            var result = "";

            foreach (var link in _links)
            {
                result += DetermineSatisfiabilityScc(link) ? 1 : 0;
            }
            
            return result;
        }
        
        bool DetermineSatisfiabilityScc(string link)
        {
            var graph = CreateImplicationGraph(link);
            var gSearch = new GraphSearchForTwoSat(graph.Count);
            var sccs = gSearch.KasarajuFindSccs(graph);

            foreach (var scc in sccs)
            {
                var set = new HashSet<int>();
                foreach (var node in scc)
                {
                    if (set.Contains(-node)) return false;
                    set.Add(node);
                }
            }

            return true;
        }
        
        
        Dictionary<int, List<int>> CreateImplicationGraph(string link)
        {
            var conditions = UtilityMethods.GetParsedStringArrayFromWeb(link, '\n').Skip(1);
            var implicationGraph = new Dictionary<int, List<int>>();

            foreach (var condition in conditions.Select(x => x.Split(' ')))
            {
                var a = int.Parse(condition[0]);
                var b = int.Parse(condition[1]);
                var isA = a > 0;
                var isB = b > 0;

                a = Math.Abs(a);
                b = Math.Abs(b);

                if (isA)
                {
                    if (isB)
                    {
                        AddKeyIfNotPresent(implicationGraph, -a);
                        AddKeyIfNotPresent(implicationGraph, -b);
                        
                        implicationGraph[-a].Add(b);
                        implicationGraph[-b].Add(a);
                    }
                    else
                    {
                        AddKeyIfNotPresent(implicationGraph, -a);
                        AddKeyIfNotPresent(implicationGraph, b);
                        
                        implicationGraph[-a].Add(-b);
                        implicationGraph[b].Add(a);
                    }
                }
                else
                {
                    if (isB)
                    {
                        AddKeyIfNotPresent(implicationGraph, a);
                        AddKeyIfNotPresent(implicationGraph, -b);
                        
                        implicationGraph[a].Add(b);
                        implicationGraph[-b].Add(-a);
                    }
                    else
                    {
                        AddKeyIfNotPresent(implicationGraph, a);
                        AddKeyIfNotPresent(implicationGraph, b);
                        
                        implicationGraph[a].Add(-b);
                        implicationGraph[b].Add(-a);
                    }
                }
            }

            return implicationGraph;
        }

        void AddKeyIfNotPresent(Dictionary<int, List<int>> d, int key)
        {
            if (d.ContainsKey(key)) return;
            d.Add(key, new List<int>());
        }
    }
}