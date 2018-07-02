using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
	public interface IDataSorter
	{
        void OrderAndTake(Dictionary<string, double> studentsWithMarks, string comparison, int studentsToTake);
    }
}
