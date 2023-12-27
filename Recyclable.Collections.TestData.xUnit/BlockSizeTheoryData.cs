using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class BlockSizeTheoryData : TheoryData<int>
	{
		public BlockSizeTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.BlockSizeVariants)
			{
				Add(testCase);
			}
		}
	}
}
