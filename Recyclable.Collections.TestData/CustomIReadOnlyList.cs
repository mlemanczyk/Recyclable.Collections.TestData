using System.Collections;

namespace Recyclable.Collections.TestData
{
	public class CustomIReadOnlyList<T> : IReadOnlyList<T>
	{
		private readonly T[] _testData;

		public CustomIReadOnlyList(T[] testData)
		{
			_testData = testData;
		}

		public T this[int index] => _testData[index];

		public int Count => _testData.Length;

		public IEnumerator<T> GetEnumerator() => _testData.AsEnumerable().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _testData.GetEnumerator();
	}
}