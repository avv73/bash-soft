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
    [Alias("readdb")]
    public class ReadDatabaseCommand : Command, IExecutable
    {
        [Inject]
        private IDatabase repository;

        public ReadDatabaseCommand(string inputI, string[] dataI)
            : base(inputI, dataI)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            string fileName = Data[1];
            repository.LoadData(fileName);
        }
    } 
}

