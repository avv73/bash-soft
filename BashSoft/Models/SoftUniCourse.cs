using BashSoft.Contracts;
using BashSoft.Exceptions;
using System;
using System.Collections.Generic;

namespace BashSoft.Models
{
    public class SoftUniCourse : ICourse
    {
        private string name;
        private Dictionary<string, IStudent> studentsByName;

        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException(nameof(name));
                }

                name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName
        {
            get
            {
                return studentsByName;
            }
        }

        public SoftUniCourse(string name)
        {
            this.name = name;
            studentsByName = new Dictionary<string, IStudent>();
        }

        public void EnrollStudent(IStudent student)
        {
            if (studentsByName.ContainsKey(student.UserName))
            {
                throw new DuplicateEntryInStructureException(student.UserName, Name);
            }

            studentsByName.Add(student.UserName, student);
        }
    } 
}

