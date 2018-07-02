using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.IO.Commands;
using BashSoft.Judge;
using BashSoft.Repository;
using System;
using System.Diagnostics;
using System.IO;

namespace BashSoft.IO
{
    public class CommandInterpreter : IInterpreter
    {
        private IContentComparer judge;
        private IDatabase repository;
        private IDirectoryManager inputOutputManager;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpretCommand(string input)
        {
            string[] data = input.Split(' ');
            string commandName = data[0].ToLower();

            try
            {
                IExecutable command = ParseCommand(input, commandName, data);
                command.Execute();
            }
            catch (Exception ex)
            {
                OutputWriter.DisplayException(ex.Message);
            }
        } 

        private IExecutable ParseCommand(string input, string command, string[] data)
        {
            switch (command)
            {
                case "open":
                    return new OpenFileCommand(input, data, judge, repository, inputOutputManager);
                case "mkdir":
                    return new MakeDirectoryCommand(input, data, judge, repository, inputOutputManager);
                case "ls":
                    return new TraverseFoldersCommand(input, data, judge, repository, inputOutputManager);
                case "cmp":
                    return new CompareFilesCommand(input, data, judge, repository, inputOutputManager);
                case "cdrel":
                    return new ChangePathRelativelyCommand(input, data, judge, repository, inputOutputManager);
                case "cdabs":
                    return new ChangeAbsolutePathCommand(input, data, judge, repository, inputOutputManager);
                case "readdb":
                    return new ReadDatabaseCommand(input, data, judge, repository, inputOutputManager);
                case "show":
                    return new ShowCourseCommand(input, data, judge, repository, inputOutputManager);
                case "help":
                    return new GetHelpCommand(input, data, judge, repository, inputOutputManager);
                case "filter":
                    return new PrintFilteredStudentsCommand(input, data, judge, repository, inputOutputManager);
                case "order":
                    return new PrintOrderedStudentsCommand(input, data, judge, repository, inputOutputManager);
                case "dropdb":
                    return new DropDatabaseCommand(input, data, judge, repository, inputOutputManager);
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
                    throw new InvalidCommandException(input);
            }

            throw new InvalidCommandException(input);
        }
    } 
}

