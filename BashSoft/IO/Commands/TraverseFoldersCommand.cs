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
    public class TraverseFoldersCommand : Command, IExecutable
    {
        public TraverseFoldersCommand(string inputI, string[] dataI, IContentComparer  judge, IDatabase repository, IDirectoryManager inputOutputManager)
            : base(inputI, dataI, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length == 1)
            {
                InputOutputManager.TraverseDirectory(0);
            }
            else if (Data.Length == 2)
            {
                int depth;
                bool hasParsed = int.TryParse(Data[1], out depth);
                if (hasParsed)
                {
                    InputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }

        }
    } 
}

