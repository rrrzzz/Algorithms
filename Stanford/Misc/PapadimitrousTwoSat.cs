using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Algorithms.Stanford.Misc
{
    public class PapadimitrousTwoSat
    {
        //&&
        public bool GetConditionsSatisfiability(int[][] conditions)
        {
            var condCount = conditions.Length;
            var variables = new Dictionary<int, bool>();
            var rnd = new Random(DateTime.Now.Millisecond);
            
            foreach (var condition in conditions)
            {
                if (!variables.ContainsKey(Math.Abs(condition[0])))
                {
                    variables.Add(Math.Abs(condition[0]), true);
                }

                if (!variables.ContainsKey(Math.Abs(condition[1])))
                {
                    variables.Add(Math.Abs(condition[1]), true);
                }
            }

            var varCount = variables.Count;

            var trialsNum = (int)Math.Log(varCount, 2);

            for (int i = 0; i < trialsNum; i++)
            {
                for (int z = 0; z < 2 * varCount * varCount; z++)
                {
                    var satconds = 0;

                    while (satconds != condCount)
                    {
                        var rndIndex = rnd.Next(condCount);
                        var first = conditions[rndIndex][0];
                        var second = conditions[rndIndex][1];
                        var isFirstTrue = first > 0;
                        var isSecondTrue = second > 0;
                        first = Math.Abs(first);
                        second = Math.Abs(second);
                        var isFirstSat = variables[first] == isFirstTrue;
                        var isSecondSat = variables[second] == isSecondTrue;
                        if (!(isFirstSat || isSecondSat))
                        {
                            satconds = 0;
                            var choice = rnd.Next(2);
                            var toChange = choice == 0 ? first : second;
                            variables[toChange] = !variables[toChange];
                            break;
                        }
                        
                        satconds++;
                    }

                    if (satconds == condCount) return true;
                }
            }

            return false;
        }
    }
}