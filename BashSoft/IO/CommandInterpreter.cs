using System;
using System.Diagnostics;
using System.IO;

public class CommandInterpreter
{
    private Tester judge;
    private StudentsRepository repository;
    private IOManager inputOutputManager;

    public CommandInterpreter(Tester judge, StudentsRepository repository, IOManager inputOutputManager)
    {
        this.judge = judge;
        this.repository = repository;
        this.inputOutputManager = inputOutputManager;
    }

    public void InterpretCommand(string input)
    {
        string[] data = input.Split(' ');
        string command = data[0];
        command = command.ToLower();

        try
        {
            ParseCommand(input, command, data);
        }
        catch (InvalidOperationException ioex)
        {
            OutputWriter.DisplayException(ioex.Message);
        }
        catch (DirectoryNotFoundException dnfex)
        {
            OutputWriter.DisplayException(dnfex.Message);
        }
        catch (ArgumentOutOfRangeException aoorex)
        {
            OutputWriter.DisplayException(aoorex.Message);
        }
        catch (ArgumentException aex)
        {
            OutputWriter.DisplayException(aex.Message);
        }
        catch (Exception ex)
        {
            OutputWriter.DisplayException(ex.Message);
        }
    }

    private void ParseCommand(string input, string command, string[] data)
    {
        switch (command)
        {
            case "open":
                TryOpenFile(input, data);
                break;
            case "mkdir":
                TryCreateDirectory(input, data);
                break;
            case "ls":
                TryTraverseFolders(input, data);
                break;
            case "cmp":
                TryCompareFiles(input, data);
                break;
            case "cdrel":
                TryChangePathRelatively(input, data);
                break;
            case "cdabs":
                TryChangePathAbsolute(input, data);
                break;
            case "readdb":
                TryReadDatabaseFromFile(input, data);
                break;
            case "show":
                TryShowWantedData(input, data);
                break;
            case "help":
                TryGetHelp();
                break;
            case "filter":
                TryFilterAndTake(input, data);
                break;
            case "order":
                TryOrderAndTake(input, data);
                break;
            case "dropdb":
                TryDropDb(input, data);
                break;
            case "decorder":
                // TODO: implement soon
                break;
            case "download":
                // TODO: implement soon
                break;
            case "downloadasynch":
                // TODO: implement soon
                break;
            default:
                DisplayInvalidCommandMessage(input);
                break;
        }
    }

    private void TryDropDb(string input, string[] data)
    {
        if (data.Length != 1)
        {
            DisplayInvalidCommandMessage(input);
            return;
        }

        repository.UnloadData();
        OutputWriter.WriteMessageOnNewLine("Database dropped!");
    }

    private void TryShowWantedData(string input, string[] data)
    {
        if (data.Length == 2)
        {
            string courseName = data[1];
            repository.GetAllStudentsFromCourse(courseName);
        }
        else if (data.Length == 3)
        {
            string courseName = data[1];
            string userName = data[2];
            repository.GetStudentScoresFromCourse(courseName, userName);
        }
        else
        {
            DisplayInvalidCommandMessage(input);
        }
    }

    private void TryGetHelp()
    {
        OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path "));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth "));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDirREl:relative path"));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDir:absolute path"));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "show information about given course or a given username for a course from data base - show: courseName (username) \\ username may be omitted"));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
        OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "get help – help"));
        OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
        OutputWriter.WriteEmptyLine();
    }

    private void TryReadDatabaseFromFile(string input, string[] data)
    {
        if (data.Length == 2)
        {
            string fileName = data[1];
            repository.LoadData(fileName);
        }
        else
        {
            DisplayInvalidCommandMessage(input);
        }
    }

    private void TryChangePathAbsolute(string input, string[] data)
    {
        if (data.Length == 2)
        {
            string absolutePath = data[1];
            inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
        }
        else
        {
            DisplayInvalidCommandMessage(input);
        }
        
    }

    private void TryChangePathRelatively(string input, string[] data)
    {
        if (data.Length == 2)
        {
            string relPath = data[1];
            inputOutputManager.ChangeCurrentDirectoryRelative(relPath); 
        }
        else
        {
            DisplayInvalidCommandMessage(input);
        }
    }

    private void TryCompareFiles(string input, string[] data)
    {
        if (data.Length == 3)
        {
            string firstPath = data[1];
            string secondPath = data[2];

            judge.CompareContent(firstPath, secondPath);
        }
        else
        {
            DisplayInvalidCommandMessage(input);
        }
    }

    private void TryTraverseFolders(string input, string[] data)
    {
        if (data.Length == 1)
        {
            inputOutputManager.TraverseDirectory(0);
        }
        else if (data.Length == 2)
        {
            int depth;
            bool hasParsed = int.TryParse(data[1], out depth);
            if (hasParsed)
            {
                inputOutputManager.TraverseDirectory(depth);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
            }
        }

    }

    private void TryCreateDirectory(string input, string[] data)
    {
        if (data.Length == 2)
        {
            string directoryName = data[1];
            inputOutputManager.CreateDirectoryInCurrentFolder(directoryName);
        }
        else
        {
            DisplayInvalidCommandMessage(input);
        }
    }

    private void TryOpenFile(string input, string[] data)
    {
        if (data.Length == 2)
        {
            string fileName = data[1];
            Process.Start(SessionData.currentPath + "\\" + fileName);
        }
        else
        {
            DisplayInvalidCommandMessage(input);
        }
    }

    private void TryFilterAndTake(string input, string[] data)
    {
        if (data.Length == 5)
        {
            string courseName = data[1];
            string filter = data[2].ToLower();
            string takeCommand = data[3].ToLower();
            string takeQuantity = data[4].ToLower();

            TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
        }
        else
        {
            DisplayInvalidCommandMessage(input);
        }
    }

    private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
    {
        if (takeCommand == "take")
        {
            if (takeQuantity == "all")
            {
                repository.FilterAndTake(courseName, filter);
            }
            else
            {
                int studentsToTake;
                bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                if (hasParsed)
                {
                    repository.FilterAndTake(courseName, filter, studentsToTake);
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

    private void TryOrderAndTake(string input, string[] data)
    {
        if (data.Length == 5)
        {
            string courseName = data[1];
            string filter = data[2].ToLower();
            string orderCommand = data[3].ToLower();
            string takeQuantity = data[4].ToLower();

            TryParseParametersForOrderAndTake(orderCommand, takeQuantity, courseName, filter);
        }
        else
        {
            DisplayInvalidCommandMessage(input);
        }
    }

    private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
    {
        if (takeCommand == "take")
        {
            if (takeQuantity == "all")
            {
                repository.OrderAndTake(courseName, filter);
            }
            else
            {
                int studentsToTake;
                bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                if (hasParsed)
                {
                    repository.OrderAndTake(courseName, filter, studentsToTake);
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

    private void DisplayInvalidCommandMessage(string input)
    {
        OutputWriter.DisplayException($"The command {input} is invalid");
    }


}

