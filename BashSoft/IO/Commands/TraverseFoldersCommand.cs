using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TraverseFoldersCommand : Command
{
    public TraverseFoldersCommand(string inputI, string[] dataI, Tester judge, StudentsRepository repository, IOManager inputOutputManager) 
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

