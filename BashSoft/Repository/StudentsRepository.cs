using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

public class StudentsRepository
{
    private bool isDataInitialized = false;
    private Dictionary<string, Course> courses;
    private Dictionary<string, Student> students;

    private RepositoryFilter filter;
    private RepositorySorter sorter;

    public StudentsRepository(RepositorySorter sorter, RepositoryFilter filter)
    {
        this.filter = filter;
        this.sorter = sorter;
    }

    public void LoadData(string fileName)
    {
        if (isDataInitialized)
        {
            OutputWriter.DisplayException(ExceptionMessages.DataAlreadyInitializedException);
            return;
        }

        courses = new Dictionary<string, Course>();
        students = new Dictionary<string, Student>();
        OutputWriter.WriteMessageOnNewLine("Reading data...");
        ReadData(fileName);
    }

    public void UnloadData()
    {
        if (!isDataInitialized)
        {
            OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
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

                        if (scores.Length > Course.NumberOfTasksOnExam)
                        {
                            OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                            continue;
                        }

                        if (!students.ContainsKey(username))
                        {
                            students.Add(username, new Student(username));
                        }

                        if (!courses.ContainsKey(courseName))
                        {
                            courses.Add(courseName, new Course(courseName));
                        }

                        Course course = courses[courseName];
                        Student student = students[username];

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
            OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
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
        if (IsQueryForCoursePossible(courseName) && courses[courseName].studentsByName.ContainsKey(studentUserName))
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
            OutputWriter.PrintStudent(new KeyValuePair<string, double>(username, courses[courseName].studentsByName[username].marksByCourseName[courseName]));
        }
    }

    public void GetAllStudentsFromCourse(string courseName)
    {
        if (IsQueryForCoursePossible(courseName))
        {
            OutputWriter.WriteMessageOnNewLine($"{courseName}:");
            foreach (KeyValuePair<string, Student> studentMarksEntry in courses[courseName].studentsByName)
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
                studentsToTake = courses[courseName].studentsByName.Count;
            }

            Dictionary<string, double> marks = courses[courseName].studentsByName.ToDictionary(x => x.Key, x => x.Value.marksByCourseName[courseName]);

            filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
        }
    }

    public void OrderAndTake(string courseName, string givenFilter, int? studentsToTake = null)
    {
        if (IsQueryForCoursePossible(courseName))
        {
            if (studentsToTake == null)
            {
                studentsToTake = courses[courseName].studentsByName.Count;
            }

            Dictionary<string, double> marks = courses[courseName].studentsByName.ToDictionary(x => x.Key, x => x.Value.marksByCourseName[courseName]);

            sorter.OrderAndTake(marks, givenFilter, studentsToTake.Value);
        }
    }
}

