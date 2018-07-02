using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
	public interface IFilteredTaker
	{
        void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null);
    }
}
