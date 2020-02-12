using System.Collections.Generic;

namespace Algorithms.Stanford.Sorting
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

        public int[] MergeSortEffective(int[] toSort)
        {
            var array = (int[])toSort.Clone();
            DivideAndSort(0, array.Length - 1, array);
            return array;
        }

        private void DivideAndSort(int l, int r, int[] toSort)
        {
            if (l < r)
            {
                var m = (l + r) / 2;

                DivideAndSort(l, m, toSort);
                DivideAndSort(m + 1, r, toSort);
                MergeNew(l, m, r, toSort);
            }
        }

        private void MergeNew(int l, int m, int r, int[] toSort)
        {
            var arrL = new int[m - l + 1];
            var arrR = new int[r - m];

            for (int i = 0; i < arrL.Length; i++)
            {
                arrL[i] = toSort[i + l];
            }

            for (int i = 0; i < arrR.Length; i++)
            {
                arrR[i] = toSort[m + 1 + i];
            }

            int leftIndex = 0, rightIndex = 0;

            while(leftIndex < arrL.Length && rightIndex < arrR.Length)
            {
                if (arrL[leftIndex] < arrR[rightIndex])
                {
                    toSort[l++] = arrL[leftIndex++];
                }
                else
                {
                    toSort[l++] = arrR[rightIndex++];
                }
            }

            while (leftIndex < arrL.Length)
            {
                toSort[l++] = arrL[leftIndex++];
            }

            while (rightIndex < arrR.Length)
            {
                toSort[l++] = arrR[rightIndex++];
            }
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
                    newList[indexCompare + 1] = newList[indexCompare];
                    indexCompare--;
                }

                newList[indexCompare + 1] = currentElement;
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
            var length = newList.Count;
            bool noSwaps;

            do
            {
                noSwaps = true;
                for (int i = 0; i < length - 1; i++)
                {
                    if (newList[i] > newList[i + 1])
                    {
                        var temp = newList[i + 1];
                        newList[i + 1] = newList[i];
                        newList[i] = temp;
                        noSwaps = false;
                    }
                }

                length--;
            } while (!noSwaps);

            return newList;
        }
    }
}