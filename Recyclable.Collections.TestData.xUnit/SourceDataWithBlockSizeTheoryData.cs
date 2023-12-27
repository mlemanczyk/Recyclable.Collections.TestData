using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceDataWithBlockSizeTheoryData : TheoryData<string, IEnumerable<long>, long, int>
	{
		public SourceDataWithBlockSizeTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.SourceDataWithBlockSizeVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.BlockSize);
			}
		}
	}
}
