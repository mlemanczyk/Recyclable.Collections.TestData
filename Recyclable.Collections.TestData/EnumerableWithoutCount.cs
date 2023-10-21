using System.Collections;

namespace Recyclable.Collections.TestData
{
	public class EnumerableWithoutCount<T> : IEnumerable, IEnumerable<T>
	{
		private readonly IEnumerable<T> _testData;

		public EnumerableWithoutCount(IEnumerable<T> testData)
		{
			_testData = testData;
		}

		public IEnumerator GetEnumerator() => _testData.GetEnumerator();

		IEnumerator<T> IEnumerable<T>.GetEnumerator() => _testData.GetEnumerator();
	}
}