using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvalidCommandException : Exception
{
    private const string InvalidCommandMessage = "The command {0} is invalid";

    public InvalidCommandException(string variable) : base(string.Format(InvalidCommandMessage, variable))
    { }
}

