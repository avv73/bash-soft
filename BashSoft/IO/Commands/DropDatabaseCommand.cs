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
    [Alias("dropdb")]
    public class DropDatabaseCommand : Command, IExecutable
    {
        [Inject]
        private IDatabase repository;

        public DropDatabaseCommand(string inputI, string[] dataI)
            : base(inputI, dataI)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 1)
            {
                throw new InvalidCommandException(Input);
            }

            repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }
    } 
}

