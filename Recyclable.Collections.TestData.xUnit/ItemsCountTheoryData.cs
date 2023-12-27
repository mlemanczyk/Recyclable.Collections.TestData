using Xunit;

namespace Recyclable.Collections.TestData.xUnit
{
	public class ItemsCountTheoryData : TheoryData<long>
	{
		public ItemsCountTheoryData()
		{
			foreach (var testCase in RecyclableLongListTestData.ItemsCountVariants)
			{
				Add(testCase);
			}
		}
	}
}
