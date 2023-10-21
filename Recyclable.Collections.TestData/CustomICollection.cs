using System.Collections;

namespace Recyclable.Collections.TestData
{
	public class CustomICollection<T> : ICollection, IEnumerable<T>
	{
		private readonly T[] _testData;

		public CustomICollection(in T[] testData)
		{
			_testData = testData;
		}

		public int Count => _testData.Length;
		public bool IsSynchronized => true;
		public object SyncRoot => _testData;

		public void CopyTo(Array array, int index) => _testData.CopyTo((T[])array, index);

		public IEnumerator GetEnumerator() => _testData.GetEnumerator();

		IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)_testData).GetEnumerator();
	}
}
