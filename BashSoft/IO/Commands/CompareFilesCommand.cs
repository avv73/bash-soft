using BashSoft.Attributes;
using BashSoft.Contracts;
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
    [Alias("cmp")]
    public class CompareFilesCommand : Command, IExecutable
    {
        [Inject]
        private IContentComparer judge;

        public CompareFilesCommand(string inputI, string[] dataI)
            : base(inputI, dataI)
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

            judge.CompareContent(firstPath, secondPath);
        }
    } 
}

