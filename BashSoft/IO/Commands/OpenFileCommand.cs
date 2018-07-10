using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.Judge;
using BashSoft.Repository;
using BashSoft.StaticData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.IO.Commands
{
    [Alias("open")]
    public class OpenFileCommand : Command, IExecutable
    {
        public OpenFileCommand(string input, string[] data)
            : base(input, data)
        { }

        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            string fileName = Data[1];
            Process.Start(SessionData.currentPath + "\\" + fileName);
        }
    } 
}

