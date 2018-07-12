using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Contracts
{
	public interface ISimpleOrderedBag<T> : IEnumerable<T>
        where T : IComparable<T>
	{
        bool Remove(T element);

        int Capacity { get; }

        void Add(T element);

        void AddAll(ICollection<T> collection);

        int Size { get; }

        string JoinWith(string joiner);
	}
}
