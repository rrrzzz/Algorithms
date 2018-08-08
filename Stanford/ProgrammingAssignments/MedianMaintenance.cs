using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    //The goal of this problem is to implement the "Median Maintenance" algorithm (covered in the Week 5 lecture on heap applications). 
    //The text file contains a list of the integers from 1 to 10000 in unsorted order; you should treat this as a stream of numbers, arriving one by one.
    //The goal is to repeatedly compute median of array adding integers to it one by one. 

    public class MedianMaintenance
    {
        private const string LinkToIntegers = "https://lagunita.stanford.edu/assets/courseware/v1/036a1a01e616390f2554e7e524da9a18/asset-v1:Engineering+Algorithms1+SelfPaced+type@asset+block/Median.txt";
        private const int IntegersCount = 10000;

        private int[] ParseIntegersFromWeb()
        {
            string integersString;

            using (var webClient = new WebClient())
            {
                integersString = webClient.DownloadString(LinkToIntegers);
            }

            var stringArray = integersString.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            return stringArray.Select(int.Parse).ToArray();
        }
    }
}
