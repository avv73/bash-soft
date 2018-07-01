using BashSoft.Exceptions;
using System;
using System.Collections.Generic;

namespace BashSoft.Models
{
    public class Course
    {
        private string name;
        private Dictionary<string, Student> studentsByName;

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

        public IReadOnlyDictionary<string, Student> StudentsByName
        {
            get
            {
                return studentsByName;
            }
        }

        public Course(string name)
        {
            this.name = name;
            studentsByName = new Dictionary<string, Student>();
        }

        public void EnrollStudent(Student student)
        {
            if (studentsByName.ContainsKey(student.UserName))
            {
                throw new DuplicateEntryInStructureException(student.UserName, Name);
            }

            studentsByName.Add(student.UserName, student);
        }
    } 
}

