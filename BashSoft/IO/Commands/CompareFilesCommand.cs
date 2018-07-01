using BashSoft.Exceptions;
using BashSoft.Judge;
using BashSoft.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.IO.Commands
{
    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string inputI, string[] dataI, Tester judge, StudentsRepository repository, IOManager inputOutputManager)
            : base(inputI, dataI, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 3)
            {
                throw new InvalidCommandException(Input);
            }

            string firstPath = Data[1];
            string secondPath = Data[2];

            Judge.CompareContent(firstPath, secondPath);
        }
    } 
}

