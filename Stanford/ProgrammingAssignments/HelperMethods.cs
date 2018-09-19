using System;
using System.Net;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public static class HelperMethods
    {
        public static string[] GetNodesParsedStringArray(string link, string separator)
        {
            string content;
            using (var client = new WebClient())
            {
                content = client.DownloadString(link);
            }

            var parsedLines = content.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            return parsedLines;
        }

        public static string[] GetNodesParsedStringArray(string link, char separator)
        {
            string content;
            using (var client = new WebClient())
            {
                content = client.DownloadString(link);
            }

            var parsedLines = content.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            return parsedLines;
        }
    }
}