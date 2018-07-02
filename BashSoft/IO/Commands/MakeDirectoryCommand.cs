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
    public class MakeDirectoryCommand : Command, IExecutable
    {
        public MakeDirectoryCommand(string inputI, string[] dataI, IContentComparer  judge, IDatabase repository, IDirectoryManager inputOutputManager)
            : base(inputI, dataI, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            string directoryName = Data[1];
            InputOutputManager.CreateDirectoryInCurrentFolder(directoryName);
        }
    }
}

