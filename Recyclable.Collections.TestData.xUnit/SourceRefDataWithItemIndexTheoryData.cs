using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceRefDataWithItemIndexTheoryData : TheoryData<string, IEnumerable<object>, long, IEnumerable<long>>
	{
		public SourceRefDataWithItemIndexTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.SourceRefDataWithItemIndexVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.ItemIndexes);
			}
		}
	}
}
