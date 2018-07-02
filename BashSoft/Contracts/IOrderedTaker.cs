using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
	public interface IOrderedTaker
	{
        void OrderAndTake(string courseName, string givenFilter, int? studentsToTake = null);
    }
}
