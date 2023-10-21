using System.Collections;

namespace Recyclable.Collections.TestData
{
	public class CustomIList<T> : IList, IEnumerable<T>
	{
		protected List<T> _list;

		public CustomIList()
		{
			_list = new List<T>();
		}

		public CustomIList(IEnumerable<T> items)
		{
			_list = new List<T>(items);
		}

		public object? this[int index] { get => ((IList)_list)[index]; set => ((IList)_list)[index] = value; }

		public bool IsFixedSize => ((IList)_list).IsFixedSize;

		public bool IsReadOnly => ((IList)_list).IsReadOnly;

		public int Count => ((ICollection)_list).Count;

		public bool IsSynchronized => ((ICollection)_list).IsSynchronized;

		public object SyncRoot => ((ICollection)_list).SyncRoot;

		public int Add(object? value) => ((IList)_list).Add(value);

		public void Clear()
		{
			((IList)_list).Clear();
		}

		public bool Contains(object? value) => ((IList)_list).Contains(value);

		public void CopyTo(Array array, int index)
		{
			((ICollection)_list).CopyTo(array, index);
		}

		public IEnumerator GetEnumerator() => ((IEnumerable)_list).GetEnumerator();

		public int IndexOf(object? value) => ((IList)_list).IndexOf(value);

		public void Insert(int index, object? value)
		{
			((IList)_list).Insert(index, value);
		}

		public void Remove(object? value)
		{
			((IList)_list).Remove(value);
		}

		public void RemoveAt(int index)
		{
			((IList)_list).RemoveAt(index);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)_list).GetEnumerator();
	}
}