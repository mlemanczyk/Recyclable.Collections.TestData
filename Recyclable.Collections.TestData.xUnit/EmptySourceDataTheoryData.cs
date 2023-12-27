using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class EmptySourceDataTheoryData : TheoryData<string, IEnumerable<long>, long>
	{
		public EmptySourceDataTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.EmptySourceDataVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount);
			}
		}
	}
}
