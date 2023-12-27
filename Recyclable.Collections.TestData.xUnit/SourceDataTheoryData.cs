using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceDataTheoryData : TheoryData<string, IEnumerable<long>, long>
	{
		public SourceDataTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.SourceDataVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount);
			}
		}
	}
}
