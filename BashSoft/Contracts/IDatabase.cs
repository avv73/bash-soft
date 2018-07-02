using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
	public interface IDatabase : IRequester, IFilteredTaker, IOrderedTaker
	{
        void LoadData(string fileName);

        void UnloadData();
	}
}
