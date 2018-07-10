using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Judge;
using BashSoft.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.IO.Commands
{
    [Alias("cdabs")]
    public class ChangeAbsolutePathCommand : Command, IExecutable
    {
        [Inject]
        private IDirectoryManager inputOutputManager;

        public ChangeAbsolutePathCommand(string inputI, string[] dataI)
            : base(inputI, dataI)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidOperationException(Input);
            }

            string absolutePath = Data[1];
            inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
        }
    } 
}

