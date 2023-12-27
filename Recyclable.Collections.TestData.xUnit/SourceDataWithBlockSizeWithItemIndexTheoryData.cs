using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceDataWithBlockSizeWithItemIndexTheoryData : TheoryData<string, IEnumerable<long>, long, int, IEnumerable<long>>
	{
		public SourceDataWithBlockSizeWithItemIndexTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.SourceDataWithBlockSizeWithItemIndexVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.BlockSize, testCase.ItemIndexes);
			}
		}
	}
}
