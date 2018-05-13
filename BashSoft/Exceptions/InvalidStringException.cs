using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvalidStringException : Exception
{
    private const string NullOrEmptyValue = "The value of the variable CANNOT be null or empty!";

    public InvalidStringException() : base(NullOrEmptyValue) { }

    public InvalidStringException(string variable) : base(variable + " " + NullOrEmptyValue) { }
}

