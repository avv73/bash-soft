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
    [Alias("mkdir")]
    public class MakeDirectoryCommand : Command, IExecutable
    {
        [Inject]
        private IDirectoryManager inputOutputManager;

        public MakeDirectoryCommand(string inputI, string[] dataI)
            : base(inputI, dataI)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            string directoryName = Data[1];
            inputOutputManager.CreateDirectoryInCurrentFolder(directoryName);
        }
    }
}

