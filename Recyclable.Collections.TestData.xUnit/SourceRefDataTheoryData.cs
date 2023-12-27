using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class SourceRefDataTheoryData : TheoryData<string, IEnumerable<object>, long>
	{
		public SourceRefDataTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.SourceRefDataVariants)
			{
				Add(testCase.TestCase, testCase.TestData, testCase.ItemsCount);
			}
		}
	}
}
