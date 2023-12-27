using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceDataWithBlockSizeWithItemIndexWithRangeTheoryData : TheoryData<string, IEnumerable<long>, long, int, IEnumerable<(long, long)>>
	{
		public SourceDataWithBlockSizeWithItemIndexWithRangeTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.SourceDataWithBlockSizeWithItemIndexWithRangeVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.BlockSize, testCase.ItemsIndexesWithRange);
			}
		}
	}
}
