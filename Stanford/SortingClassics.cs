using System.Collections.Generic;

namespace Algorithms.Stanford
{
    class SortingClassics
    {
        public List<int> MergeSort(List<int> toSort)
        {
            var listLength = toSort.Count;

            if (listLength == 1)
            {
                return toSort;
            }

            var halfListLength = listLength / 2;
            var firstHalfLength = listLength % 2 == 0 ? halfListLength : halfListLength + 1;

            var firstHalfSorted = MergeSort(toSort.GetRange(0, firstHalfLength));
            var secondHalfSorted = MergeSort(toSort.GetRange(firstHalfLength, halfListLength));

            return Merge(firstHalfSorted, secondHalfSorted);
        }

        private List<int> Merge(List<int> firstList, List<int> secondList)
        {
            var sorted = new List<int>(firstList.Count + secondList.Count);
            var indexFirst = 0; 
            var indexSecond = 0;

            while (indexSecond != secondList.Count && indexFirst != firstList.Count)
            {
                if (firstList[indexFirst] < secondList[indexSecond])
                {
                    sorted.Add(firstList[indexFirst]);
                    indexFirst++;
                }
                else
                {
                    sorted.Add(secondList[indexSecond]);
                    indexSecond++;
                }
            }

            if (indexSecond == secondList.Count)
            {
                for (int i = indexFirst; i < firstList.Count; i++)
                {
                    sorted.Add(firstList[i]);
                }
            }
            else
            {
                for (int i = indexSecond; i < secondList.Count; i++)
                {
                    sorted.Add(secondList[i]);
                }
            }

            return sorted;
        }

        public List<int> InsertionSort(List<int> toSort)
        {
            var newList = toSort.ConvertAll(x => x);

            for (int i = 1; i < newList.Count; i++)
            {
                var indexCompare = i - 1;
                var currentElement = newList[i];
                while (indexCompare != -1 && currentElement < newList[indexCompare])
                {
                    indexCompare--;
                }

                var indexToInsert = indexCompare + 1;
                if (indexToInsert == i) continue;
                
                var temp = newList[indexToInsert];
                newList[indexToInsert] = newList[i];

                for (int j = indexToInsert + 1; j <= i; j++)
                {
                    var newTemp = newList[j];
                    newList[j] = temp;
                    temp = newTemp;
                }
            }

            return newList;
        }

        public List<int> SelectionSort(List<int> toSort)
        {
            var newList = toSort.ConvertAll(x => x);

            for (int currentPosition = 0; currentPosition < newList.Count - 1; currentPosition++)
            {
                var currentMin = currentPosition;
                
                for (int j = currentPosition + 1; j < newList.Count; j++)
                {
                    if (newList[j] < newList[currentMin])
                    {
                        currentMin = j;
                    }
                }

                if (currentMin != currentPosition)
                {
                    var temp = newList[currentMin];
                    newList[currentMin] = newList[currentPosition];
                    newList[currentPosition] = temp;
                }
            }
            return newList;
        }

        public List<int> BubbleSort(List<int> toSort)
        {
            var newList = toSort.ConvertAll(x => x);
            bool noSwaps;

            do
            {
                noSwaps = true;
                for (int i = 0; i < newList.Count - 1; i++)
                {
                    if (newList[i] > newList[i + 1])
                    {
                        var temp = newList[i + 1];
                        newList[i + 1] = newList[i];
                        newList[i] = temp;
                        noSwaps = false;
                    }
                }
            } while (!noSwaps);

            return newList;
        }
    }
}
