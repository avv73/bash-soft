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
    public class ReadDatabaseCommand : Command, IExecutable
    {
        public ReadDatabaseCommand(string inputI, string[] dataI, IContentComparer  judge, IDatabase repository, IDirectoryManager inputOutputManager)
            : base(inputI, dataI, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            string fileName = Data[1];
            Repository.LoadData(fileName);
        }
    } 
}

