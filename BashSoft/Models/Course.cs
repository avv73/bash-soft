using System.Collections.Generic;

public class Course
{
    public string name;
    public Dictionary<string, Student> studentsByName;

    public const int NumberOfTasksOnExam = 5;
    public const int MaxScoreOnExamTask = 100;

    public Course(string name)
    {
        this.name = name;
        studentsByName = new Dictionary<string, Student>();
    }

    public void EnrollStudent(Student student)
    {
        if (studentsByName.ContainsKey(student.userName))
        {
            OutputWriter.DisplayException(string.Format(
                ExceptionMessages.StudentAlreadyEnrolledInGivenCourse,
                student.userName, name));
            return;
        }

        studentsByName.Add(student.userName, student);
    }
}

