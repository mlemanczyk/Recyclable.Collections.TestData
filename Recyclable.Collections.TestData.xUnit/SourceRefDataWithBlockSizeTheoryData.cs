using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceRefDataWithBlockSizeTheoryData : TheoryData<string, IEnumerable<object>, long, int>
	{
		public SourceRefDataWithBlockSizeTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.SourceRefDataWithBlockSizeVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.BlockSize);
			}
		}
	}
}
