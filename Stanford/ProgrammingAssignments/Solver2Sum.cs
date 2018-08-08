using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public static class Solver2Sum
    {
        //The goal of this problem is to implement a variant of the 2-SUM algorithm (covered in the Week 6 lecture on hash table applications).

        //The file contains 1 million integers, both positive and negative (there might be some repetitions!). 

        //    Your task is to compute the number of target values t in the interval[-10000, 10000] (inclusive) such that there are distinct numbers x,y in the input file that satisfy x + y = t. 

        // Answer is 427

        private const string TestInputPath = @"Algorithms.Stanford.TestInput.2sum.txt";
        private const int TotalNumberOfTargets = 20001;
        private const int LowestTarget = -10000;


        public static int SolveHashSet()
        {
            var integers = GetParsedIntegersFromFile();
            var iterationCounter = 0;
            var targetsCounter = 0;
            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var targets = Enumerable.Range(LowestTarget, TotalNumberOfTargets).ToArray();

            foreach (var target in targets)
            {
                stopwatch.Restart();
                Console.WriteLine($"Processing {iterationCounter}th number");

                foreach(var currentNumber in integers)
                {
                    var numberToFind = target - currentNumber;

                    if (numberToFind == currentNumber || !integers.Contains(numberToFind)) continue;
                    targetsCounter++;
                    break;
                }

                Console.WriteLine($"It took {stopwatch.Elapsed} to process current target");
                iterationCounter++;
            }

            Console.WriteLine($"It took {stopwatch2.Elapsed} to finish");
            return targetsCounter;
        }

        private static HashSet<long> GetParsedIntegersFromFile()
        {
            var hashSet = new HashSet<long>();
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(TestInputPath))
            using (var reader = new StreamReader(stream))
            {
                var number = reader.ReadLine();
                while (number != null)
                {
                    var parsedNumber = long.Parse(number);
                    number = reader.ReadLine();

                    if (hashSet.Contains(parsedNumber)) continue;

                    hashSet.Add(parsedNumber);
                }
            }

            return hashSet;
        }


        public static int SolveTwoPointers()
        {
            var stopwatch = new Stopwatch();
            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();

            var counter = 0;
            var index = 0;

            var output = GetParsedIntegersArrayFromFile();
            output.Sort();

            var numsArray = output.ToArray();
            var targets = Enumerable.Range(LowestTarget, TotalNumberOfTargets).ToArray();

            while (index != targets.Length - 1)
            {
                stopwatch.Restart();
                Console.WriteLine($"Processing {index}th target");
                var pointerStart = 0;
                var pointerEnd = numsArray.Length - 1;

                var sum = targets[index];
                while (pointerStart < pointerEnd)
                {
                    if (numsArray[pointerStart] + numsArray[pointerEnd] == sum)
                    {
                        counter++;
                        break;
                    }

                    if (numsArray[pointerStart] + numsArray[pointerEnd] > sum) pointerEnd--;
                    else pointerStart++;
                }

                Console.WriteLine($"It took {stopwatch.Elapsed} to process current number");
                index++;
            }

            Console.WriteLine($"It took {stopwatch2.Elapsed} to finish");
            return counter;
        }

        private static List<long> GetParsedIntegersArrayFromFile()
        {
            var numsList = new List<long>();
            
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(TestInputPath))
            using (var reader = new StreamReader(stream))
            {
                var number = reader.ReadLine();
                while (number != null)
                {
                    numsList.Add(long.Parse(number));
                    number = reader.ReadLine();
                }
            }
            
            return numsList.Distinct().ToList();
        }
    }
}