using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        // Answer: 101100

        private const string Link0 = "https://d3c33hcgiwev3.cloudfront.net/_02c1945398be467219866ee1c3294d2d_2sat1.txt?Expires=1584057600&Signature=FOgFxpnXcu-F30MTpu~E2tY0SZ~UlIRnokR5Y1rLHKkKm0HRbTdf2GEy~-0YR1OvSLA9ne4KjS7QU5twYmXTWI3UO~HZRlfdxZARhbJILFs3l4uZ0sTV8w26ZklYJ1A3KG-3jBEe1-kFadwl3IWJmixlVFTHXpjDcUaHzKdE7MY_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        private const string Link1 = "https://d3c33hcgiwev3.cloudfront.net/_02c1945398be467219866ee1c3294d2d_2sat2.txt?Expires=1584057600&Signature=SjZAmRglfSaVTajt3d3LLa7ET3ysTKM99lRK9HxzhFpK3OaW5F28t~GG0OzesKP4aYFKN0CwPRd2TMtoklPakaERnRqS5yQ8JAUYWSkk41sLYkX6SjE6XaKXMIEIo063TYIMP8Ro~UbtB-TWnAb3cGzyRAWWPXEFQFWOFubuqyM_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        private const string Link2 = "https://d3c33hcgiwev3.cloudfront.net/_02c1945398be467219866ee1c3294d2d_2sat3.txt?Expires=1584057600&Signature=Zt9RwoAPY6jH1oTIHkck82xGSSkdNdXJ5VIarKzxckMvTuIaADOXeOV242BVAacLUxin0ZfAwn93spPHjsCV9syyljx~4911VRNHnaBOLPt49dxkhp2VxrP~HMgte7Bi~YquAaBfPuqGEVoiZrSqhT00Ghd6xRoGdI4rAGVFWsY_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        private const string Link3 = "https://d3c33hcgiwev3.cloudfront.net/_02c1945398be467219866ee1c3294d2d_2sat4.txt?Expires=1584057600&Signature=Tjn88gZ-t01rtxpl8HKNQccHSdLe9J-SqN59bSpBzNfEDpZQGAChfNNy4bSp15~PUCBm9SNmKGIL84ob8wOMBV4ChoHM7oDQe1WSjR9C0F8oxmnX4U0jNUw8qUnLOF2pzGjS~OSC6sedEsBMMtq4xT~sZLX1UtDeBF0yfUwQhJA_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        private const string Link4 = "https://d3c33hcgiwev3.cloudfront.net/_02c1945398be467219866ee1c3294d2d_2sat5.txt?Expires=1584057600&Signature=fS26dnU1lVgwmCJUrsFfGTr6Sv0AKbC3AU6dqfLE29q1uxpxHIjtv8layDlVy8qU3-pj8Kp6KFAZPM7q5LKjfkmiuuK~Az0r-w-Voe-OrK1sFDUYzVHsAJPTZRY55UWVwX9iMG00g-0MD4mNaDtpmj657~evLm8tmI-~IfSKZdI_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        private const string Link5 = "https://d3c33hcgiwev3.cloudfront.net/_02c1945398be467219866ee1c3294d2d_2sat6.txt?Expires=1584057600&Signature=dnReXgG5pvcGGLrPP-ppiQIedqjz2AUpBYfbcarzN1rW3hQezHX3YgOd8KnZKM1r0zKsh3dMXuzjGj8WlJU5376~OyWrhzaO6gKw9yZU~1MDxXqDQFLBglnVJRKrxW6tQXKcJq653q0YwtAxJT8B1eMvpQZtL9mbf-KyKQ64Sb0_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";

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
                
                
                res += new PapadimitrousTwoSat().GetConditionsSatisfiability(conds);
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