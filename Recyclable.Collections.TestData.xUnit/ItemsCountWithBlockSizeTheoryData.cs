using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class ItemsCountWithBlockSizeTheoryData : TheoryData<long, int>
	{
		public ItemsCountWithBlockSizeTheoryData()
		{
			foreach (var (ItemsCount, BlockSize) in RecyclableLongListTestData.TargetDataVariants)
			{
				Add(ItemsCount, BlockSize);
			}
		}
	}
}
