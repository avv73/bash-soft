using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
	public interface IRequester
	{
        void GetStudentScoresFromCourse(string courseName, string username);

        void GetAllStudentsFromCourse(string courseName);

    }
}
