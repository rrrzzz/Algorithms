using System.Collections.Generic;
using System.Text;

namespace Algorithms.HackerRank
{
    public static class VideoConference
    {
        //https://www.hackerrank.com/contests/hourrank-30/challenges/video-conference/problem

        public static List<string> Solve(List<string> names)
        {
            var result = new List<string>();
            var dict = new Dictionary<string, int>();
            foreach (var name in names)
            {
                if (dict.ContainsKey(name) && dict[name] != 0)
                {
                    var t = name + " " + dict[name];
                    result.Add(t);
                    dict[name] += 1;
                    continue;
                }

                if (dict.ContainsKey(name))
                {
                    result.Add(name);
                    dict[name] = 2;
                    continue;
                }

                var partName = new StringBuilder();
                var found = false;
                foreach (var character in name)
                {
                    partName.Append(character);
                    if (!found && !dict.ContainsKey(partName.ToString()))
                    {
                        dict.Add(partName.ToString(), 0);
                        result.Add(partName.ToString());
                        found = true;
                        continue;
                    }

                    if (found)
                    {
                        dict.Add(partName.ToString(), 0);
                    }
                }

                if (dict[name] == 0)
                {
                    dict[name] = 2;
                }
            }

            return result;
        }
    }
}