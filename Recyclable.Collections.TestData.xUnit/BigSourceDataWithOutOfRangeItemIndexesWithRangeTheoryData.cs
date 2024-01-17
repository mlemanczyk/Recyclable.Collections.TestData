using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class BigSourceDataWithOutOfRangeItemIndexesWithRangeTheoryData : TheoryData<string, IEnumerable<long>, long, int, IEnumerable<(long, long, long)>>
	{
		public BigSourceDataWithOutOfRangeItemIndexesWithRangeTheoryData()
		{
			foreach (var tc in RecyclableLongListTestData.BigSourceDataWithOutOfRangeItemIndexesWithRangeVariants)
			{
				Add(tc.TestCase, tc.TestData, tc.ItemsCount, tc.BlockSize, tc.ItemIndexesWithRange);
			}
		}
	}
}
