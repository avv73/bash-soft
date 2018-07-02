using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
	public interface IContentComparer
	{
        void CompareContent(string userOutputPath, string expectedOutputPath);
    }
}
