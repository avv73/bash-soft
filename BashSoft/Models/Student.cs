using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    private string userName;
    private Dictionary<string, Course> enrolledCourses;
    private Dictionary<string, double> marksByCourseName;

    public string UserName
    {
        get
        {
            return userName;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(userName), ExceptionMessages.NullOrEmptyValue);
            }

            userName = value;
        }
    }

    public IReadOnlyDictionary<string, Course> EnrolledCourses
    {
        get
        {
            return enrolledCourses;
        }
    }

    public IReadOnlyDictionary<string, double> MarksByCourseName
    {
        get
        {
            return marksByCourseName;
        }
    }

    public Student(string userName)
    {
        UserName = userName;
        enrolledCourses = new Dictionary<string, Course>();
        marksByCourseName = new Dictionary<string, double>();
    }

    public void EnrollInCourse(Course course)
    {
        if (enrolledCourses.ContainsKey(course.Name))
        {
            throw new InvalidOperationException(string.Format(
                ExceptionMessages.StudentAlreadyEnrolledInGivenCourse,
                userName, course.Name));
        }

        enrolledCourses.Add(course.Name, course);
    }

    public void SetMarkOnCourse(string courseName, params int[] scores)
    {
        if (!enrolledCourses.ContainsKey(courseName))
        {
            throw new InvalidOperationException(ExceptionMessages.NotEnrolledInCourse);
        }

        if (scores.Length > Course.NumberOfTasksOnExam)
        {
            throw new InvalidOperationException(ExceptionMessages.InvalidNumberOfScores);
        }

        marksByCourseName.Add(courseName, CalculateMark(scores));
    }

    private double CalculateMark(int[] scores)
    {
        double percentageOfSolvedExam = scores.Sum() /
            (double)(Course.NumberOfTasksOnExam * Course.MaxScoreOnExamTask);
        double mark = percentageOfSolvedExam * 4 + 2;
        return mark;
    }
}

