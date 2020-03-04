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
            var satconds = 0;
            var condBool = new bool[condCount];
            
            for (int i = 0; i < trialsNum; i++)
            {
                Console.WriteLine($"current trial: {i} out of {trialsNum}");
                for (int z = 0; z < 2 * varCount * varCount; z++)
                {
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
                            condBool = new bool[condCount];
                            condBool[rndIndex] = true;
                            satconds = 1;
                            var choice = rnd.Next(2);
                            var toChange = choice == 0 ? first : second;
                            variables[toChange] = !variables[toChange];
                            break;
                        }

                        if (condBool[rndIndex]) continue;
                        condBool[rndIndex] = true;
                        satconds++;
                    }

                    if (satconds == condCount) return true;
                }
            }

            return false;
        }
    }
}