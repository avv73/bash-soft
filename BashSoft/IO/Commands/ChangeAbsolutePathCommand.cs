using BashSoft.Judge;
using BashSoft.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.IO.Commands
{
    public class ChangeAbsolutePathCommand : Command
    {
        public ChangeAbsolutePathCommand(string inputI, string[] dataI, Tester judge, StudentsRepository repository, IOManager inputOutputManager)
            : base(inputI, dataI, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidOperationException(Input);
            }

            string absolutePath = Data[1];
            InputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
        }
    } 
}

