using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    //The goal of this problem is to implement the "Median Maintenance" algorithm (covered in the Week 5 lecture on heap applications). 
    //The text file contains a list of the integers from 1 to 10000 in unsorted order; you should treat this as a stream of numbers, arriving one by one.
    //The goal is to repeatedly compute median of array adding integers to it one by one. 
    // Answer is 1213

    public static class MedianMaintenance
    {
        public static int Solve()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var integers = ParseIntegersFromWeb();
            var medians = new int[integers.Length];
            
            var rightHalfMinHeap = new MinHeap();
            var leftHalfMaxHeap = new MaxHeap();

            var arraySize = 0;

            foreach (var number in integers)
            {
                InsertNumberIntoHeap(number, rightHalfMinHeap, leftHalfMaxHeap);
                MakeHeapSizesEqual(rightHalfMinHeap, leftHalfMaxHeap);
                arraySize++;
                var medianOrder = (arraySize + 1) / 2;

                if (leftHalfMaxHeap.GetHeapSize() == medianOrder)
                {
                    medians[arraySize - 1] = leftHalfMaxHeap.GetMaxElement();
                    continue;
                }

                medians[arraySize - 1] = rightHalfMinHeap.GetMinElement();
            }

            Console.WriteLine($"It took {stopwatch.Elapsed} to finish");
            return medians.Sum() % 10000;
        }

        private static void InsertNumberIntoHeap(int number, MinHeap rightHalfMinHeap, MaxHeap leftHalfMaxHeap)
        {
            if (rightHalfMinHeap.GetHeapSize() == 0)
            {
                leftHalfMaxHeap.InsertElement(number);
                return;
            }

            var leftMax = leftHalfMaxHeap.GetMaxElement();
            var rightMin = rightHalfMinHeap.GetMinElement();

            if (number > leftMax && number < rightMin || number <= leftMax)
            {
                leftHalfMaxHeap.InsertElement(number);
            }
            else
            {
                rightHalfMinHeap.InsertElement(number);
            }
        }

        private static void MakeHeapSizesEqual(MinHeap rightHalfMinHeap, MaxHeap leftHalfMaxHeap)
        {
            var rightSize = rightHalfMinHeap.GetHeapSize();
            var leftSize = leftHalfMaxHeap.GetHeapSize();

            if (Math.Abs(rightSize - leftSize) <= 1) return;

            if (rightSize > leftSize)
            {
                var temp = rightHalfMinHeap.ExtractMinElement();
                leftHalfMaxHeap.InsertElement(temp);
            }
            else
            {
                var temp = leftHalfMaxHeap.ExtractMaxElement();
                rightHalfMinHeap.InsertElement(temp);
            }
        }

        private static int[] ParseIntegersFromWeb()
        {
            const string link = "https://d3c33hcgiwev3.cloudfront.net/_6ec67df2804ff4b58ab21c12edcb21f8_Median.txt?Expires=1584057600&Signature=iq2kwWo6i7w-2ClOog1elVKbel7bH7IQUGI53iLS3lE81qHzaBhrwAbklxzy1-iELA~WRcCce61qHwhQcB31nWSxqomD9MR9Eh4WkVksz0lS-dLah3KoFGxOHenzx5rKEjgUmvqQbNy8THdgnWms78Y2bU7ex3AfHpLsqzMxoWQ_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";

            return UtilityMethods.GetParsedStringArrayFromWeb(link, Environment.NewLine)
                .Select(int.Parse)
                .ToArray();
        }
    }
}
