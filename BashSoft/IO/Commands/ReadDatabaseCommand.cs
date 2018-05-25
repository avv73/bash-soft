using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ReadDatabaseCommand : Command
{
    public ReadDatabaseCommand(string inputI, string[] dataI, Tester judge, StudentsRepository repository, IOManager inputOutputManager) 
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

