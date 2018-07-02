using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
	public interface IDataFilter
	{
        void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter, int studentsToTake);
    }
}
