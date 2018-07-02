using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using BashSoft.Models;
using BashSoft.IO;
using BashSoft.Exceptions;
using BashSoft.StaticData;
using BashSoft.Contracts;

namespace BashSoft.Repository
{
    public class StudentsRepository : IDatabase
    {
        private bool isDataInitialized = false;
        private Dictionary<string, ICourse> courses;
        private Dictionary<string, IStudent> students;  

        private IDataFilter filter;
        private IDataSorter sorter;

        public StudentsRepository(IDataSorter sorter, IDataFilter filter)
        {
            this.filter = filter;
            this.sorter = sorter;
        }

        public void LoadData(string fileName)
        {
            if (isDataInitialized)
            {
                throw new InvalidOperationException(ExceptionMessages.DataAlreadyInitializedException);
            }

            courses = new Dictionary<string, ICourse>();
            students = new Dictionary<string, IStudent>();
            OutputWriter.WriteMessageOnNewLine("Reading data...");
            ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!isDataInitialized)
            {
                throw new InvalidOperationException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            students = null;
            courses = null;
            isDataInitialized = false;

        }

        private void ReadData(string fileName)
        {
            string path = SessionData.currentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                string pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                Regex rgx = new Regex(pattern);
                string[] allInputLines = File.ReadAllLines(path);

                for (int line = 0; line < allInputLines.Length; line++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
                    {
                        Match currentMatch = rgx.Match(allInputLines[line]);
                        string courseName = currentMatch.Groups[1].Value;
                        string username = currentMatch.Groups[2].Value;
                        string scoresStr = currentMatch.Groups[3].Value;

                        try
                        {
                            int[] scores = scoresStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();

                            if (scores.Any(x => x > 100 | x < 0))
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                                continue; // ??
                            }

                            if (scores.Length > SoftUniCourse.NumberOfTasksOnExam)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            if (!students.ContainsKey(username))
                            {
                                students.Add(username, new SoftUniStudent(username));
                            }

                            if (!courses.ContainsKey(courseName))
                            {
                                courses.Add(courseName, new SoftUniCourse(courseName));
                            }

                            ICourse course = courses[courseName];
                            IStudent student = students[username];

                            student.EnrollInCourse(course);
                            student.SetMarkOnCourse(courseName, scores);

                            course.EnrollStudent(student);
                        }
                        catch (FormatException fex)
                        {
                            OutputWriter.DisplayException(fex.Message + $"at line : {line}");
                        }
                    }
                }

                isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                OutputWriter.DisplayException(InvalidPathException.InvalidPath);
            }

        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (courses.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (IsQueryForCoursePossible(courseName) && courses[courseName].StudentsByName.ContainsKey(studentUserName))
            {
                return true;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingStudentInDataBase);
            }

            return false;
        }

        public void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossible(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(username, courses[courseName].StudentsByName[username].MarksByCourseName[courseName]));
            }
        }
            
        public void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (KeyValuePair<string, IStudent> studentMarksEntry in courses[courseName].StudentsByName)
                {
                    GetStudentScoresFromCourse(courseName, studentMarksEntry.Key);
                }
            }
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = courses[courseName].StudentsByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

                filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        public void OrderAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = courses[courseName].StudentsByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

                sorter.OrderAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }
    } 
}

