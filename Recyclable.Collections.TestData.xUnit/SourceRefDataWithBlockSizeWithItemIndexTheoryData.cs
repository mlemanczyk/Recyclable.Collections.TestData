using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceRefDataWithBlockSizeWithItemIndexTheoryData : TheoryData<string, IEnumerable<object>, long, int, IEnumerable<long>>
	{
		public SourceRefDataWithBlockSizeWithItemIndexTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.SourceRefDataWithBlockSizeWithItemIndexVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.BlockSize, testCase.ItemIndexes);
			}
		}
	}
}
