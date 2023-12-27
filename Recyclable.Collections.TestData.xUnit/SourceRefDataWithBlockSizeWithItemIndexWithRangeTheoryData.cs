using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceRefDataWithBlockSizeWithItemIndexWithRangeTheoryData : TheoryData<string, IEnumerable<object>, long, int, IEnumerable<(long, long)>>
	{
		public SourceRefDataWithBlockSizeWithItemIndexWithRangeTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.SourceRefDataWithBlockSizeWithItemIndexWithRangeVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.BlockSize, testCase.ItemsIndexesWithRange);
			}
		}
	}
}
