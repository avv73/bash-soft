using BashSoft.Exceptions;
using BashSoft.Judge;
using BashSoft.Repository;
using System;

namespace BashSoft.IO.Commands
{
    public abstract class Command
    {
        private string input;
        private string[] data;

        private Tester judge;
        private StudentsRepository repository;
        private IOManager inputOutputManager;

        public Command(string inputI, string[] dataI, Tester judge, StudentsRepository repository, IOManager inputOutputManager)
        {
            Input = inputI;
            Data = dataI;
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        protected string Input
        {
            get { return input; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                input = value;
            }
        }

        protected string[] Data
        {
            get { return data; }
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }

                data = value;
            }
        }

        protected Tester Judge
        {
            get { return judge; }
        }

        protected StudentsRepository Repository
        {
            get { return repository; }
        }

        protected IOManager InputOutputManager
        {
            get { return inputOutputManager; }
        }

        public abstract void Execute();
    } 
}

