using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceDataWithBlockSizeWithItemIndexWithRangeTheoryData : TheoryData<string, IEnumerable<long>, long, int, IEnumerable<(long, long)>>
	{
		public SourceDataWithBlockSizeWithItemIndexWithRangeTheoryData()
		{
			foreach (var tc in RecyclableLongListTestData.SourceDataWithBlockSizeWithItemIndexWithRangeVariants)
			{
				Add(tc.TestCase, tc.TestData, tc.ItemsCount, tc.BlockSize, tc.ItemsIndexesWithRange);
			}
		}
	}
}
