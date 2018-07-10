using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Judge;
using BashSoft.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.IO.Commands
{
    [Alias("show")]
    public class ShowCourseCommand : Command, IExecutable
    {
        [Inject]
        private IDatabase repository;

        public ShowCourseCommand(string inputI, string[] dataI)
            : base(inputI, dataI)
        {
        }

        public override void Execute()
        {
            if (Data.Length == 2)
            {
                string courseName = Data[1];
                repository.GetAllStudentsFromCourse(courseName);
            }
            else if (Data.Length == 3)
            {
                string courseName = Data[1];
                string userName = Data[2];
                repository.GetStudentScoresFromCourse(courseName, userName);
            }
            else
            {
                throw new InvalidOperationException(Input);
            }
        }
    } 
}

