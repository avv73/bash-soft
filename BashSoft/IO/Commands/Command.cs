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

        public Command(string inputI, string[] dataI)
        {
            Input = inputI;
            Data = dataI;
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

        public abstract void Execute();
    } 
}

