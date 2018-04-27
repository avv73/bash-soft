using System.Collections.Generic;
using System.Linq;

public class Student
{
    public string userName;
    public Dictionary<string, Course> enrolledCourses;
    public Dictionary<string, double> marksByCourseName;

    public Student(string userName)
    {
        this.userName = userName;
        enrolledCourses = new Dictionary<string, Course>();
        marksByCourseName = new Dictionary<string, double>();
    }

    public void EnrollInCourse(Course course)
    {
        if (enrolledCourses.ContainsKey(course.name))
        {
            OutputWriter.DisplayException(string.Format(
                ExceptionMessages.StudentAlreadyEnrolledInGivenCourse,
                userName, course.name));
            return;
        }

        enrolledCourses.Add(course.name, course);
    }

    public void SetMarkOnCourse(string courseName, params int[] scores)
    {
        if (!enrolledCourses.ContainsKey(courseName))
        {
            OutputWriter.DisplayException(ExceptionMessages.NotEnrolledInCourse);
            return;
        }

        if (scores.Length > Course.NumberOfTasksOnExam)
        {
            OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
            return;
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

