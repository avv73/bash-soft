using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.Judge;
using BashSoft.Repository;
using System;

namespace BashSoft.IO.Commands
{
    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;

        private IContentComparer judge;
        private IDatabase repository;
        private IDirectoryManager inputOutputManager;

        public Command(string inputI, string[] dataI, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
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

        protected IContentComparer Judge
        {
            get { return judge; }
        }

        protected IDatabase Repository
        {
            get { return repository; }
        }

        protected IDirectoryManager InputOutputManager
        {
            get { return inputOutputManager; }
        }

        public abstract void Execute();
    } 
}

