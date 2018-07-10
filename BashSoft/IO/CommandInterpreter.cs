using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.IO.Commands;
using BashSoft.Judge;
using BashSoft.Repository;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

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
            object[] parametersForConstruction = { input, data };
            Type typeOfCommand = Assembly.GetExecutingAssembly()
                .GetTypes()
                .First(type => type.GetCustomAttributes(typeof(AliasAttribute))
                                    .Where(atr => atr.Equals(command))
                                    .ToArray().Length > 0);

            Type typeOfInterpreter = typeof(CommandInterpreter);

            Command exe = (Command)Activator.CreateInstance(typeOfCommand, parametersForConstruction);

            FieldInfo[] fieldsOfCommand = typeOfCommand.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo[] fieldsOfInterpreter = typeOfInterpreter.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo fieldOfCommand in fieldsOfCommand)
            {
                Attribute atrAttribute = fieldOfCommand.GetCustomAttribute(typeof(InjectAttribute));
                if (atrAttribute != null)
                {
                    if (fieldsOfInterpreter.Any(x => x.FieldType == fieldOfCommand.FieldType))
                    {
                        fieldOfCommand.SetValue(exe,
                            fieldsOfInterpreter.First(x => x.FieldType == fieldOfCommand.FieldType)
                                .GetValue(this));
                    }
                }
            }

            return exe;
        }
    } 
}

