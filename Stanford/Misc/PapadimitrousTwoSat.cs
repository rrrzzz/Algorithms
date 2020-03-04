using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Stanford.Misc
{
    public class PapadimitrousTwoSat
    {
        public bool GetConditionsSatisfiability(int[][] conditions)
        {
            var condCount = conditions.Length;
            var variables = new Dictionary<int, bool>();
            var varToConds = new Dictionary<int, List<int>>();
            var rnd = new Random(DateTime.Now.Millisecond);
    
            var notSatCond = new HashSet<int>();
            var counter = 0;
            
            foreach (var condition in conditions)
            {
                if (!variables.ContainsKey(Math.Abs(condition[0])))
                {
                    variables.Add(Math.Abs(condition[0]), true);
                    varToConds.Add(Math.Abs(condition[0]), new List<int> {counter});
                }
                else varToConds[Math.Abs(condition[0])].Add(counter);
                if (!variables.ContainsKey(Math.Abs(condition[1])))
                {
                    variables.Add(Math.Abs(condition[1]), true);
                    varToConds.Add(Math.Abs(condition[1]), new List<int>{counter});
                }
                else varToConds[Math.Abs(condition[1])].Add(counter);
                notSatCond.Add(counter++);
            }
    
            var varCount = variables.Count;
    
            var trialsNum = (int)Math.Log(varCount, 2);
            
            
            //
            // var condBool = new bool[condCount];
            var notSatCond2 = new HashSet<int>(notSatCond);
            for (int i = 0; i < trialsNum; i++)
            {
                
                Console.WriteLine($"current trial: {i} out of {trialsNum}");
                for (int z = 0; z < 2 * varCount; z++)
                {
                    while (true)
                    {
                        if (notSatCond2.Count == 0) return true;
                        var rndIndex = rnd.Next(notSatCond2.Count);
                        var index = notSatCond2.Skip(rndIndex).First();
                        var first = conditions[index][0];
                        var second = conditions[index][1];
                        var isFirstTrue = first > 0;
                        var isSecondTrue = second > 0;
                        first = Math.Abs(first);
                        second = Math.Abs(second);
                        var isFirstSat = variables[first] == isFirstTrue;
                        var isSecondSat = variables[second] == isSecondTrue;
                        if (!(isFirstSat || isSecondSat))
                        {
                            notSatCond2.Remove(index);
                            var choice = rnd.Next(2);
                            var toChange = choice == 0 ? first : second;
                            variables[toChange] = !variables[toChange];
                            foreach (var condition in varToConds[toChange].Where(x => x != index))
                            {
                               first = conditions[condition][0];
                               second = conditions[condition][1];
                               isFirstTrue = first > 0;
                               isSecondTrue = second > 0;
                                first = Math.Abs(first);
                                second = Math.Abs(second);
                                isFirstSat = variables[first] == isFirstTrue;
                                isSecondSat = variables[second] == isSecondTrue;
                                if (!(isFirstSat || isSecondSat))
                                {
                                    notSatCond2.Add(condition);
                                }
                                else notSatCond2.Remove(condition);
                            }
                            break;
                        }
                        notSatCond2.Remove(index);
                    }
                }
            }
            return false;
        }
    }
}