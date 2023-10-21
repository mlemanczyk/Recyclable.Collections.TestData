using System.Collections;

namespace Recyclable.Collections.TestData
{
	public class CustomICollectionT<T> : ICollection<T>, IEnumerable<T>
	{
		private readonly List<T> _testData;

		public CustomICollectionT(in List<T> testData)
		{
			_testData = testData;
		}

		public int Count => _testData.Count;
		public bool IsReadOnly => false;

		public void Add(T item) => _testData.Add(item);

		public void Clear() => _testData.Clear();

		public bool Contains(T item) => _testData.Contains(item);

		public void CopyTo(T[] array, int arrayIndex) => _testData.CopyTo(array, arrayIndex);

		public IEnumerator<T> GetEnumerator() => _testData.GetEnumerator();

		public bool Remove(T item) => _testData.Remove(item);

		IEnumerator IEnumerable.GetEnumerator() => _testData.GetEnumerator();
	}
}
