# Recyclable.Collections.TestData.xUnit
`Recyclable.Collections.TestData.xUnit` project is a basic open source package for working with repeatable xUnit test data useful for testing collections like `RecyclableList&lt;T&gt;`, `RecyclableLongList&lt;T&gt;`, `List&lt;T&gt;`, `SortableList&lt;T&gt;`, `PriorityQueue&lt;T&gt;` &amp; similar.

## Included
* `BigSourceDataWithOutOfRangeItemIndexesWithRangeTheoryData: TheoryData<string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize, IEnumerable<(long ItemIndex, long RangeStartItemIndex, long RangeItemsCount)> ItemIndexesWithRange>`
* `BlockSizeTheoryData: TheoryData<int BlockSize>`
* `EmptySourceDataTheoryData: TheoryData<string TestCase, IEnumerable<long> TestData, long ItemsCount>`
* `ItemsCountTheoryData: TheoryData<long ItemsCount>`
* `ItemsCountWithBlockSizeTheoryData: TheoryData<long ItemsCount, int BlockSize>`
* `SourceDataTheoryData: TheoryData<string TestCase, IEnumerable<long> TestData, long ItemsCount>`
* `SourceDataWithBlockSizeTheoryData: TheoryData<string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize>`
* `SourceDataWithBlockSizeWithItemIndexTheoryData: TheoryData<string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize, IEnumerable<long> ItemIndexes>`
* `SourceDataWithBlockSizeWithItemIndexWithRangeTheoryData: TheoryData<string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize, IEnumerable<(long ItemIndex, long RangeItemsCount)> ItemRanges>`
* `SourceDataWithItemIndexTheoryData: TheoryData<string TestCase, IEnumerable<long> TestData, long ItemsCount, IEnumerable<long> ItemIndexes>`
* `SourceRefDataTheoryData: TheoryData<string TestCase, IEnumerable<object> TestData, long ItemsCount>`
* `SourceRefDataWithBlockSizeTheoryData: TheoryData<string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize>`
* `SourceRefDataWithBlockSizeWithItemIndexTheoryData: TheoryData<string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize, IEnumerable<long> ItemIndexes>`
* `SourceRefDataWithBlockSizeWithItemIndexWithRangeTheoryData: TheoryData<string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize, IEnumerable<(long ItemIndex, long RangeItemsCount)> ItemRanges>`
* `SourceRefDataWithItemIndexTheoryData: TheoryData<string TestCase, IEnumerable<object> TestData, long ItemsCount, IEnumerable<long> ItemIndexes>`
