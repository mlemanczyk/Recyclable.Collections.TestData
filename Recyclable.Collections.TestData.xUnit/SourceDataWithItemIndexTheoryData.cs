using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceDataWithItemIndexTheoryData : TheoryData<string, IEnumerable<long>, long, IEnumerable<long>>
	{
		public SourceDataWithItemIndexTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.SourceDataWithItemIndexVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.ItemIndexes);
			}
		}
	}
}
