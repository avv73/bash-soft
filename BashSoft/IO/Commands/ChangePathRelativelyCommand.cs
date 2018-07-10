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
    [Alias("cdrel")]
    public class ChangePathRelativelyCommand : Command, IExecutable
    {
        [Inject]
        private IDirectoryManager inputOutputManager;

        public ChangePathRelativelyCommand(string inputI, string[] dataI)
            : base(inputI, dataI)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            string relPath = Data[1];
            inputOutputManager.ChangeCurrentDirectoryRelative(relPath);
        }
    } 
}

