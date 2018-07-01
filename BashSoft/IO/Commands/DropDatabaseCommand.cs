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
    public class DropDatabaseCommand : Command
    {
        public DropDatabaseCommand(string inputI, string[] dataI, Tester judge, StudentsRepository repository, IOManager inputOutputManager)
            : base(inputI, dataI, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 1)
            {
                throw new InvalidCommandException(Input);
            }

            Repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }
    } 
}

