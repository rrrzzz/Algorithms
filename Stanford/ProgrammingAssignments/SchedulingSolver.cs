using System.Linq;
using System.Net;
using System.Numerics;

namespace Algorithms.Stanford.ProgrammingAssignments
{
	public static class SchedulingSolver
    {

        //In this programming problem and the next you'll code up the greedy algorithms from lecture for minimizing the weighted sum of completion times.

        //You should NOT assume that edge weights or lengths are distinct.

        //    Your task in this problem is to run the greedy algorithm that schedules jobs in decreasing order of the difference (weight - length).

        //Recall from lecture that this algorithm is not always optimal.IMPORTANT: if two jobs have equal difference(weight - length), you should schedule the job with higher weight first.Beware: if you break ties in a different way, you are likely to get the wrong answer.You should report the sum of weighted completion times of the resulting schedule --- a positive integer --- in the box below.
        //Answer: 69119377652

        //For this problem, use the same data set as in the previous problem.

        //    Your task now is to run the greedy algorithm that schedules jobs (optimally) in decreasing order of the ratio (weight/length).

        //In this algorithm, it does not matter how you break ties. You should report the sum of weighted completion times of the resulting schedule --- a positive integer --- in the box below.
        //Answer: 67311454237

        private const string Link = @"https://lagunita.stanford.edu/assets/courseware/v1/85f7268f796f7014abab35a19999783c/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/jobs.txt";

		public static BigInteger GetWeightedSumScheduledByDifference()
		{
			var jobs = ParseJobsArrayFromWeb();
		    InsertionSortByDifference(jobs);
			return GetWeightedLengthSum(jobs);
		}

		public static BigInteger GetWeightedSumScheduledByQuotient()
		{
			var jobs = ParseJobsArrayFromWeb();
		    jobs = jobs.OrderByDescending(x => x.RankQuotient).ToArray();

            return GetWeightedLengthSum(jobs);
		}
        
		private static Job[] ParseJobsArrayFromWeb()
		{
			string jobsString;
			using (var client = new WebClient())
			{
				jobsString = client.DownloadString(Link);
			}

			var jobsArray = jobsString.Split('\n');
			var jobsCount = int.Parse(jobsArray[0]);
			var output = new Job[jobsCount];
			for (int i = 1; i <= jobsCount; i++)
			{
				var temp = jobsArray[i].Split(' ').Select(int.Parse).ToArray();
				output[i - 1] = new Job(temp[0], temp[1]);
			}

			return output;
		}

	    private static void InsertionSortByDifference(Job[] jobs)
	    {
	        for(var start = 1; start < jobs.Length; start++)
	        {
	            var current = start;
	            var element = jobs[start];

	            while (true)
	            {
	                if (current == 0 || element.RankDifference < jobs[current - 1].RankDifference)
	                {
	                    jobs[current] = element;
	                    break;
                    }

                    if (element.RankDifference == jobs[current - 1].RankDifference)
	                {
	                    if (element.Weight > jobs[current - 1].Weight)
	                    {
                            jobs[current] = jobs[--current];
	                    }
	                    else
	                    {
	                        jobs[current] = element;
	                        break;
	                    }
	                }
	                else
	                {
	                    jobs[current] = jobs[--current];
                    }
	            }
	        }
	    }

		private static BigInteger GetWeightedLengthSum(Job[] jobs)
		{
			var totalLength = 0;
			BigInteger sum = 0;

			foreach (var job in jobs)
			{
				totalLength += job.Length;
				sum += job.Weight * totalLength;
			}

			return sum;
		}
	}

	public struct Job
	{
		public Job(int weight, int length)
		{
			Weight = weight;
			Length = length;
			RankDifference = weight - length;
			RankQuotient = (float) weight / length;
		}

		public int Weight { get; set; }
		public int Length { get; set; }
		public int RankDifference { get; set; }
		public float RankQuotient { get; set; }
	}
}