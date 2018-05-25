using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ShowCourseCommand : Command
{
    public ShowCourseCommand(string inputI, string[] dataI, Tester judge, StudentsRepository repository, IOManager inputOutputManager) 
        : base(inputI, dataI, judge, repository, inputOutputManager)
    {
    }

    public override void Execute()
    {
        if (Data.Length == 2)
        {
            string courseName = Data[1];
            Repository.GetAllStudentsFromCourse(courseName);
        }
        else if (Data.Length == 3)
        {
            string courseName = Data[1];
            string userName = Data[2];
            Repository.GetStudentScoresFromCourse(courseName, userName);
        }
        else
        {
            throw new InvalidOperationException(Input);
        }
    }
}

