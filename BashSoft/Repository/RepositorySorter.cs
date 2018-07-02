using BashSoft.Contracts;
using BashSoft.IO;
using BashSoft.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoft.Repository
{
    public class RepositorySorter : IDataSorter
    {
        public void OrderAndTake(Dictionary<string, double> studentsWithMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {
                PrintStudents(studentsWithMarks.OrderBy(kvp => kvp.Value)
                    .Take(studentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else if (comparison == "descending")
            {
                PrintStudents(studentsWithMarks.OrderByDescending(kvp => kvp.Value)
                   .Take(studentsToTake)
                   .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private void PrintStudents(Dictionary<string, double> studentsSorted)
        {
            foreach (KeyValuePair<string, double> student in studentsSorted)
            {
                OutputWriter.PrintStudent(student);
            }
        }
    } 
}

