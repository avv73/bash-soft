using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    class RepositorySorters
    {

        public static void OrderAndTake(Dictionary<string, List<int>> wantedData, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {
                OrderAndTake(wantedData, CompareInOrder, studentsToTake);
            }
            else if (comparison == "descending")
            {
                OrderAndTake(wantedData, CompareDescendingOrder, studentsToTake);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private static Dictionary<string, List<int>> GetSortedStudents(Dictionary<string, List<int>> studentsWanted, int takeCount,
            Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> comparisonFunc)
        {
            int valuesTaken = 0;
            Dictionary<string, List<int>> studentsSorted = new Dictionary<string, List<int>>();
            KeyValuePair<string, List<int>> nextInOrder = new KeyValuePair<string, List<int>>();

            bool isSorted = false;

            while (valuesTaken < takeCount)
            {
                isSorted = true;

                foreach (KeyValuePair<string, List<int>> studentWithScore in studentsWanted)
                {
                    if (!String.IsNullOrEmpty(nextInOrder.Key))
                    {
                        int comparisonResult = comparisonFunc(studentWithScore, nextInOrder);
                        if (comparisonResult >= 0 && !studentsSorted.ContainsKey(studentWithScore.Key))
                        {
                            nextInOrder = studentWithScore;
                            isSorted = false;
                        }
                    }
                    else
                    {
                        if (!studentsSorted.ContainsKey(studentWithScore.Key))
                        {
                            nextInOrder = studentWithScore;
                            isSorted = false;
                        }
                    }
                }

                if (!isSorted)
                {
                    studentsSorted.Add(nextInOrder.Key, nextInOrder.Value);
                    valuesTaken++;
                    nextInOrder = new KeyValuePair<string, List<int>>();
                }
            }

            return studentsSorted;
        }

        private static void OrderAndTake(Dictionary<string, List<int>> wantedData, 
            Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> comparisonFunc, int studentsToTake)
        {
            Dictionary<string, List<int>> studentsSorted = GetSortedStudents(wantedData, studentsToTake, comparisonFunc);
            foreach (KeyValuePair<string, List<int>> student in studentsSorted)
            {
                OutputWriter.PrintStudent(student);
            }
        }

        private static int CompareInOrder(KeyValuePair<string, List<int>> firstValue, KeyValuePair<string, List<int>> secondValue)
        {
            int totalOfFirstMarks = 0;
            foreach (int mark in firstValue.Value)
            {
                totalOfFirstMarks += mark;
            }

            int totalOfSecondMarks = 0;
            foreach (int mark in secondValue.Value)
            {
                totalOfSecondMarks += mark;
            }

            return totalOfSecondMarks.CompareTo(totalOfFirstMarks);
        }

        private static int CompareDescendingOrder(KeyValuePair<string, List<int>> firstValue, KeyValuePair<string, List<int>> secondValue)
        {
            int totalOfFirstMarks = 0;
            foreach (int mark in firstValue.Value)
            {
                totalOfFirstMarks += mark;
            }

            int totalOfSecondMarks = 0;
            foreach (int mark in secondValue.Value)
            {
                totalOfSecondMarks += mark;
            }

            return totalOfFirstMarks.CompareTo(totalOfSecondMarks);
        }

    }
}
