using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Judge;
using BashSoft.Repository;
using BashSoft.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.IO.Commands
{
    [Alias("ls")]
    public class TraverseFoldersCommand : Command, IExecutable
    {
        [Inject]
        private IDirectoryManager inputOutputManager;

        public TraverseFoldersCommand(string inputI, string[] dataI)
            : base(inputI, dataI)
        {
        }

        public override void Execute()
        {
            if (Data.Length == 1)
            {
                inputOutputManager.TraverseDirectory(0);
            }
            else if (Data.Length == 2)
            {
                int depth;
                bool hasParsed = int.TryParse(Data[1], out depth);
                if (hasParsed)
                {
                    inputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }

        }
    } 
}

