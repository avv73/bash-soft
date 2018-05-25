using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PrintFilteredStudentsCommand : Command
{
    public PrintFilteredStudentsCommand(string inputI, string[] dataI, Tester judge, StudentsRepository repository, IOManager inputOutputManager) 
        : base(inputI, dataI, judge, repository, inputOutputManager)
    {
    }

    public override void Execute()
    {
        if (Data.Length != 5)
        {
            throw new InvalidOperationException(Input);
        }

        string courseName = Data[1];
        string filter = Data[2].ToLower();
        string takeCommand = Data[3].ToLower();
        string takeQuantity = Data[4].ToLower();

        TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
    }

    private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
    {
        if (takeCommand == "take")
        {
            if (takeQuantity == "all")
            {
                Repository.FilterAndTake(courseName, filter);
            }
            else
            {
                int studentsToTake;
                bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                if (hasParsed)
                {
                    Repository.FilterAndTake(courseName, filter, studentsToTake);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                }
            }
        }
        else
        {
            OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        }
    }
}

