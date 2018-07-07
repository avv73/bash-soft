using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
    public interface ICourse : IComparable<ICourse>
    {
        string Name { get; }

        IReadOnlyDictionary<string, IStudent> StudentsByName { get; }

        void EnrollStudent(IStudent student);
    }
}
