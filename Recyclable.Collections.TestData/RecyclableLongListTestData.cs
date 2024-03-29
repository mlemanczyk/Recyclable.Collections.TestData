﻿using Open.Numeric.Primes;
using System.Numerics;

namespace Recyclable.Collections.TestData
{
	public static class RecyclableLongListTestData
	{
		public static IEnumerable<(long ItemIndex, long RangeItemsCount)> CombineItemIndexWithRange(long itemIndex, long itemsCount)
		{
			return GenerateRanges(itemIndex, itemsCount).Distinct();

			static IEnumerable<(long ItemIndex, long RangeItemsCount)> GenerateRanges(long itemIndex, long itemsCount)
			{
				yield return (0L, Math.Max(itemIndex, 1L));
				yield return (itemIndex, itemsCount - itemIndex);
				yield return (itemIndex, 1L);

				yield return (0L, 0L);
				yield return (itemIndex, 0L);

				yield return (0L, 1L);
				yield return (0L, Math.Max(itemsCount - itemIndex, 1L));
				
				var rangeSize = (int)Math.Min(itemsCount - itemIndex, 4);
				yield return ((int)(rangeSize > 2 ? Math.Max(0, itemIndex - 2) : itemIndex), rangeSize);

				rangeSize = (int)Math.Max(Math.Min(itemsCount - itemIndex - 2, 4), 0);
				yield return ((int)(rangeSize > 2 ? Math.Max(0, itemIndex - 2 - 1) : Math.Max(0, itemIndex - 1)), rangeSize);

				if (itemsCount > 1)
				{
					yield return (0L, 2L);
				}

				if (itemIndex > 0)
				{

					if (itemIndex > 1)
					{
						yield return (0L, itemIndex);

					}

					yield return (itemIndex - 1, 0L);
					
					if (itemsCount > 1)
					{
						yield return (itemIndex - 1, 2);
					}

					if (itemsCount > 2)
					{
						yield return (itemIndex - 1, Math.Min(3, itemsCount - itemIndex + 1));
					}

					if (itemsCount > 3)
					{
						yield return ((int)(itemIndex - 1), (int)Math.Min(itemIndex + 1, itemsCount - itemIndex + 1));
					}

					if (itemIndex >= RecyclableDefaults.MinItemsCountForParallelization)
					{
						yield return (itemIndex - RecyclableDefaults.MinItemsCountForParallelization, (int)Math.Min(itemsCount - itemIndex + RecyclableDefaults.MinItemsCountForParallelization, itemIndex + RecyclableDefaults.MinItemsCountForParallelization));
					}
				}
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize)> CombineSourceDataWithBlockSize(IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount)> sourceData)
		{
			foreach (var testData in sourceData)
			{
				foreach (var targetBlockSize in BlockSizeVariants)
				{
					if (!IsValidBlockSize(testData.ItemsCount, targetBlockSize))
					{
						continue;
					}

					yield return (testData.TestCase, testData.TestData, testData.ItemsCount, targetBlockSize);
				}
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize)> CombineSourceDataWithBlockSize(IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount)> sourceData)
		{
			foreach (var testData in sourceData)
			{
				foreach (var targetBlockSize in BlockSizeVariants)
				{
					if (!IsValidBlockSize(testData.ItemsCount, targetBlockSize))
					{
						continue;
					}

					yield return (testData.TestCase, testData.TestData, testData.ItemsCount, targetBlockSize);
				}
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize, IEnumerable<long> ItemIndexes)> CombineSourceDataWithBlockSizeWithItemIndex()
		{
			IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize)> sourceData = CombineSourceDataWithBlockSize(CreateSourceDataVariants());

			foreach (var testCase in sourceData)
			{
				long itemsCount = testCase.ItemsCount;
				if (itemsCount == 0)
				{
					continue;
				}

				long[] itemIndexes = CreateItemIndexVariants(itemsCount, testCase.BlockSize)
					.Where(_ => IsValidBlockSize(testCase.ItemsCount, testCase.BlockSize))
					.ToArray();

				yield return (testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.BlockSize, itemIndexes);
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize, IEnumerable<(long ItemIndex, long RangeItemsCount)> ItemsIndexesWithRange)> CombineSourceDataWithBlockSizeWithItemIndexWithRange()
		{
			IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize)> sourceData = CombineSourceDataWithBlockSize(CreateSourceDataVariants());

			foreach (var testCase in sourceData)
			{
				long itemsCount = testCase.ItemsCount;
				if (itemsCount == 0)
				{
					continue;
				}

				var itemIndexes = CreateItemIndexVariants(itemsCount, (int)testCase.BlockSize)
					.Where(_ => IsValidBlockSize(testCase.ItemsCount, testCase.BlockSize));

				var itemIndexesWithRange = itemIndexes.SelectMany(
					   itemIndex => CombineItemIndexWithRange(itemIndex, itemsCount))
					.ToArray();

				yield return (testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.BlockSize, itemIndexesWithRange);
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize, IEnumerable<long> ItemIndexes)> CombineSourceRefDataWithBlockSizeWithItemIndex()
		{
			IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize)> sourceData = CombineSourceDataWithBlockSize(CreateSourceRefDataVariants());

			foreach (var testCase in sourceData)
			{
				long itemsCount = testCase.ItemsCount;
				if (itemsCount == 0)
				{
					continue;
				}

				long[] itemIndexes = CreateItemIndexVariants(itemsCount, testCase.BlockSize)
					.Where(_ => IsValidBlockSize(testCase.ItemsCount, testCase.BlockSize))
					.ToArray();

				yield return (testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.BlockSize, itemIndexes);
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize, IEnumerable<(long ItemIndex, long RangeItemsCount)> ItemsIndexesWithRange)> CombineSourceRefDataWithBlockSizeWithItemIndexWithRange()
		{
			IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize)> sourceData = CombineSourceDataWithBlockSize(CreateSourceRefDataVariants());

			foreach (var testCase in sourceData)
			{
				long itemsCount = testCase.ItemsCount;
				if (itemsCount == 0)
				{
					continue;
				}

				var itemIndexes = CreateItemIndexVariants(itemsCount, testCase.BlockSize)
					.Where(_ => IsValidBlockSize(testCase.ItemsCount, testCase.BlockSize));

				var itemIndexesWithRange = itemIndexes.SelectMany(
					   itemIndex => CombineItemIndexWithRange(itemIndex, itemsCount))
					.ToArray();

				yield return (testCase.TestCase, testCase.TestData, testCase.ItemsCount, testCase.BlockSize, itemIndexesWithRange);
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize)> CreateBigSourceDataWithBlockSize()
		{
			long itemsCount = Array.MaxLength;
			using var testData = CreateTestData(itemsCount).ToRecyclableLongList();
			foreach (var blockSize in BlockSizeVariants)
			{
				if (IsValidBlockSize(itemsCount, blockSize))
				{
					yield return ($"long[{itemsCount}]", testData, itemsCount, blockSize);					
				}
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize, IEnumerable<(long ItemIndex, long RangeStartItemIndex, long RangeItemsCount)> ItemIndexesWithRange)> CreateBigSourceDataWithOutOfRangeItemIndexesWithRange()
		{
			foreach (var tc in CreateBigSourceDataWithBlockSize())
			{
				var outOfRangeItemIndexesWithRanges = CreateOutOfRangeItemIndexesWithRanges(tc.ItemsCount, tc.BlockSize).ToArray();
				yield return (tc.TestCase, tc.TestData, tc.ItemsCount, tc.BlockSize, outOfRangeItemIndexesWithRanges);
			}
		}

		public static IEnumerable<long> CreateItemIndexVariants(long itemsCount, int blockSize)
		{
			if (itemsCount == 0)
			{
				return Enumerable.Empty<long>();
			}

			IEnumerable<long>  primes = InitialItemIndexVariants.Concat(
				InitialItemIndexVariants.SelectMany(index => new long[] { blockSize - index, blockSize + index })
				//new long[] { blockSize - 3, blockSize - 2, blockSize - 1, blockSize, blockSize + 1, blockSize + 2, blockSize + 3 }
			).ToArray();

			IEnumerable<long> second = Prime.Numbers.StartingAt(7)
										 .TakeWhile(prime => 
											prime <= ((blockSize * 2) - 1) &&
											prime < itemsCount
										);

			primes = primes.Concat(second).ToArray();

			primes = primes
				.Concat(primes
					.Select(index => itemsCount - index - 1))
				.Concat(primes
					.Where(index => index >= RecyclableDefaults.MinItemsCountForParallelization)
					.Select(index => index - RecyclableDefaults.MinItemsCountForParallelization))
				.Where(index => index >= 0 && index < itemsCount)
				.ToArray()
				.Distinct()
				.ToArray();

			return primes;

			//IEnumerable<long> CreateItemIndexVariants()
			//{
				//if (itemsCount > 0)
				//{
				//	yield return 0;
				//}

				//foreach (long prime in primes)
				//{
				//	yield return prime;

				//	long index = itemsCount - prime;
				//	if (index >= 0)
				//	{
				//		yield return index;
				//	}
				//}

				//for (long index = itemsCount - (blockSize * 2) - 1; index < itemsCount; index++)
				//{
				//	if (index >= 0)
				//	{
				//		yield return index;
				//	}
				//}

				//if ((blockSize / 16 * 16) + 1 < itemsCount)
				//{
				//	yield return (blockSize / 16 * 16) + 1;
				//}
			//}

			//return CreateItemIndexVariants();
		}

		public static IEnumerable<(long ItemIndex, long RangeStartItemIndex, long RangeItemsCount)> CreateOutOfRangeItemIndexesWithRanges(long itemsCount, int blockSize)
		{
			var itemIndexes = CreateItemIndexVariants(itemsCount, blockSize);
			foreach (var itemIndexForGenerator in itemIndexes)
			{
				var itemRanges = CombineItemIndexWithRange(itemIndexForGenerator, itemsCount);
				foreach (var (itemIndex, rangedItemsCount) in itemRanges)
				{
					if (itemIndex > 0)
					{
						yield return (itemIndex - 1, itemIndex, rangedItemsCount);
					}

					if (itemIndex + rangedItemsCount < itemsCount)
					{
						yield return (itemIndex + rangedItemsCount, itemIndex, rangedItemsCount);
					}
				}
			}
		}

		public static IEnumerable<object> CreateRefTestData(long itemsCount)
		{
			for (long index = 1; index <= itemsCount; index++)
			{
				yield return index;
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount, IEnumerable<long> ItemIndexes)> CreateSourceRefDataWithItemIndexVariants()
		{
			IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount)> testCases = CreateSourceRefDataVariants().Where(testCase => testCase.ItemsCount > 0);
			foreach (var testCase in testCases)
			{
				var itemIndexes = CreateItemIndexVariants(testCase.ItemsCount, RecyclableDefaults.BlockSize).ToArray();
					
				yield return (testCase.TestCase, testCase.TestData, testCase.ItemsCount, itemIndexes);
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount)> CreateSourceDataVariants()
		{
			foreach (var itemsCount in ItemsCountVariants)
			{
				foreach (var variant in CreateSourceDataVariants(itemsCount))
				{
					yield return variant;
				}
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount)> CreateSourceDataVariants(long itemsCount)
		{
			GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);

			long[] testData = CreateTestData(itemsCount).ToArray();

			yield return ( $"Array[{itemsCount}]", testData, itemsCount );
			yield return ( $"ICollection[{itemsCount}]", new CustomICollection<long>(testData), itemsCount );
			yield return ( $"ICollection<T>[{itemsCount}]", new CustomICollectionT<long>(testData.ToList()), itemsCount );
			yield return ( $"IEnumerable[{itemsCount}]", new EnumerableWithoutCount<long>(testData), itemsCount );
			yield return ( $"IEnumerable<T>[{itemsCount}] without non-enumerated count", new EnumerableWithoutCount<long>(testData), itemsCount );
			yield return ( $"IList[{itemsCount}]", new CustomIList<long>(testData), itemsCount );
			yield return ( $"IList<T>[{itemsCount}]", new CustomIListT<long>(testData), itemsCount );
			yield return ( $"IReadOnlyList<T>[{itemsCount}])", new CustomIReadOnlyList<long>(testData), itemsCount );
			yield return ( $"List<T>[{itemsCount}]", testData.ToList(), itemsCount );
			yield return ( $"ReadOnlySpan<T>[{itemsCount}]", testData, itemsCount );
			yield return ( $"RecyclableList<T>[{itemsCount}]", testData.ToRecyclableList(), itemsCount );

			foreach (var sourceBlockSize in BlockSizeVariants)
			{
				if (!IsValidBlockSize(itemsCount, sourceBlockSize))
				{
					continue;
				}

				yield return
				(
					$"RecyclableLongList<T>[itemsCount: {itemsCount}, minBlockSize: {sourceBlockSize}]",
					testData.ToRecyclableLongList(sourceBlockSize),
					itemsCount
				);
			}

			yield return ($"Span<T>[{itemsCount}]", testData, itemsCount);
			yield return ($"T[{itemsCount}]", testData, itemsCount);
		}

		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, IEnumerable<long> ItemIndexes)> CreateSourceDataWithItemIndexVariants()
		{
			foreach (var testCase in CreateSourceDataVariants().Where(testCase => testCase.ItemsCount > 0))
			{
				var itemIndexes = CreateItemIndexVariants(testCase.ItemsCount, RecyclableDefaults.BlockSize).ToArray();
				yield return (testCase.TestCase, testCase.TestData, testCase.ItemsCount, itemIndexes);
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount)> CreateSourceRefDataVariants()
		{
			foreach (var itemsCount in ItemsCountVariants)
			{
				foreach (var variant in CreateSourceRefDataVariants(itemsCount))
				{
					yield return variant;
				}
			}
		}

		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount)> CreateSourceRefDataVariants(long itemsCount)
		{
			GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);

			object[] testRefData = CreateRefTestData(itemsCount).ToArray();

			yield return ( $"Array[{itemsCount}]", testRefData, itemsCount );
			yield return ( $"ICollection(itemsCount: {itemsCount}", new CustomICollection<object>(testRefData), itemsCount );
			yield return ( $"ICollection<T>(itemsCount: {itemsCount}", new CustomICollectionT<object>(testRefData.ToList()), itemsCount );
			yield return ( $"IEnumerable(itemsCount: {itemsCount})", new EnumerableWithoutCount<object>(testRefData), itemsCount );
			yield return ( $"IEnumerable<T>(itemsCount: {itemsCount}) without non-enumerated count", new EnumerableWithoutCount<object>(testRefData), itemsCount );
			yield return ( $"IList(itemsCount: {itemsCount})", new CustomIList<object>(testRefData), itemsCount );
			yield return ( $"IList<T>(itemsCount: {itemsCount})", new CustomIListT<object>(testRefData), itemsCount );
			yield return ( $"IReadOnlyList<T>(itemsCount: {itemsCount})", new CustomIReadOnlyList<object>(testRefData), itemsCount );
			yield return ( $"List<T>(itemsCount: {itemsCount}", testRefData.ToList(), itemsCount );
			yield return ( $"ReadOnlySpan<T>[{itemsCount}]", testRefData, itemsCount );
			yield return ( $"RecyclableList<T>(itemsCount: {itemsCount})", testRefData.ToRecyclableList(), itemsCount );

			foreach (var sourceBlockSize in BlockSizeVariants)
			{
				if (!IsValidBlockSize(itemsCount, sourceBlockSize))
				{
					continue;
				}

				yield return
				(
					$"RecyclableLongList<T>(itemsCount: {itemsCount}, minBlockSize: {sourceBlockSize})",
					testRefData.ToRecyclableLongList(sourceBlockSize),
					itemsCount
				);
			}

			yield return ($"Span<T>[{itemsCount}]", testRefData, itemsCount);
			yield return ($"T[{itemsCount}]", testRefData, itemsCount);
		}

		public static IEnumerable<(long ItemsCount, int BlockSize)> CreateItemsCountWithBlockSizeVariants()
		{
			foreach (var itemsCount in ItemsCountVariants)
			{
				foreach (var targetBlockSize in BlockSizeVariants)
				{
					if (!IsValidBlockSize(itemsCount, targetBlockSize))
					{
						continue;
					}

					yield return (itemsCount, targetBlockSize);
				}
			}
		}

		public static IEnumerable<long> CreateTestData(long itemsCount)
		{
			for (long index = 1; index <= itemsCount; index++)
			{
				yield return index;
			}
		}
		public static bool IsValidBlockSize(long itemsCount, long blockSize) => itemsCount is < 1024 || (itemsCount / blockSize + (itemsCount % blockSize > 0 ? 1 : 0) <= Array.MaxLength);

		public static IEnumerable<int> BlockSizeVariants { get; }= new int[]
		{
			1, 2, 4, 16, RecyclableDefaults.MinPooledArrayLength - 5, (int)BitOperations.RoundUpToPowerOf2(RecyclableDefaults.MinPooledArrayLength)
		};

		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize)> BigSourceDataWithBlockSizeVariants { get; } = CreateBigSourceDataWithBlockSize();
		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize, IEnumerable<(long ItemIndex, long RangeStartItemIndex, long RangeItemsCount)> ItemIndexesWithRange)> BigSourceDataWithOutOfRangeItemIndexesWithRangeVariants { get; } = CreateBigSourceDataWithOutOfRangeItemIndexesWithRange();
		public static IEnumerable<object[]> BlockSizeTestCases { get; } = BlockSizeVariants.Select(x => new object[] { x });

		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount)> EmptySourceDataVariants { get; } = CreateSourceDataVariants(0);
		public static IEnumerable<object[]> EmptySourceDataTestCases { get; } = EmptySourceDataVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount });
		public static IEnumerable<long> InitialItemIndexVariants { get; } = new long[] { 0, 1, 2, 3, 4, 5 };

		public static IEnumerable<long> ItemsCountVariants { get; } = new long[]
		{
			//RecyclableDefaults.MinPooledArrayLength - 5, RecyclableDefaults.MinPooledArrayLength - 1, RecyclableDefaults.MinPooledArrayLength, RecyclableDefaults.MinPooledArrayLength + 1, RecyclableDefaults.MinPooledArrayLength + 5, 127, 128, 129, RecyclableDefaults.MinItemsCountForParallelization
			0, 1, 2, 3, 7, 10, 16, RecyclableDefaults.MinPooledArrayLength - 5, RecyclableDefaults.MinPooledArrayLength - 1, RecyclableDefaults.MinPooledArrayLength,
			RecyclableDefaults.MinPooledArrayLength + 1, RecyclableDefaults.MinPooledArrayLength + 5, RecyclableDefaults.MinPooledArrayLength * 100
		};

		public static IEnumerable<object[]> ItemsCountTestCases { get; } = ItemsCountVariants.Select(x => new object[] { (int)x });
		public static IEnumerable<(long ItemsCount, int BlockSize)> ItemsCountWithBlockSizeVariants { get; } = CreateItemsCountWithBlockSizeVariants();
		public static IEnumerable<object[]> ItemsCountWithBlockSizeTestCases { get; } = ItemsCountWithBlockSizeVariants.Select(x => new object[] { x.ItemsCount, x.BlockSize });
		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount)> SourceDataVariants { get; } = CreateSourceDataVariants();
		public static IEnumerable<object[]> SourceDataTestCases { get; } = SourceDataVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount });
		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize)> SourceDataWithBlockSizeVariants { get; } = CombineSourceDataWithBlockSize(CreateSourceDataVariants());
		public static IEnumerable<object[]> SourceDataWithBlockSizeTestCases { get; } = SourceDataWithBlockSizeVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount, x.BlockSize });
		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize, IEnumerable<long> ItemIndexes)> SourceDataWithBlockSizeWithItemIndexVariants { get; } = CombineSourceDataWithBlockSizeWithItemIndex();
		public static IEnumerable<object[]> SourceDataWithBlockSizeWithItemIndexTestCases { get; } = SourceDataWithBlockSizeWithItemIndexVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount, x.BlockSize, x.ItemIndexes });
		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, int BlockSize, IEnumerable<(long ItemIndex, long RangeItemsCount)> ItemsIndexesWithRange)> SourceDataWithBlockSizeWithItemIndexWithRangeVariants { get; } = CombineSourceDataWithBlockSizeWithItemIndexWithRange();
		public static IEnumerable<object[]> SourceDataWithBlockSizeWithItemIndexWithRangeTestCases { get; } = SourceDataWithBlockSizeWithItemIndexWithRangeVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount, x.BlockSize, x.ItemsIndexesWithRange });
		public static IEnumerable<(string TestCase, IEnumerable<long> TestData, long ItemsCount, IEnumerable<long> ItemIndexes)> SourceDataWithItemIndexVariants { get; } = CreateSourceDataWithItemIndexVariants();
		public static IEnumerable<object[]> SourceDataWithItemIndexTestCases { get; } = SourceDataWithItemIndexVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount, x.ItemIndexes });
		public static object[] SourceRefData { get; } = Enumerable.Range(1, 100).Cast<object>().ToArray();
		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount)> SourceRefDataVariants { get; } = CreateSourceRefDataVariants();
		public static IEnumerable<object[]> SourceRefDataTestCases { get; } = SourceRefDataVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount });
		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize)> SourceRefDataWithBlockSizeVariants { get; } = CombineSourceDataWithBlockSize(CreateSourceRefDataVariants());
		public static IEnumerable<object[]> SourceRefDataWithBlockSizeTestCases { get; } = SourceRefDataWithBlockSizeVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount, x.BlockSize });
		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize, IEnumerable<long> ItemIndexes)> SourceRefDataWithBlockSizeWithItemIndexVariants { get; } = CombineSourceRefDataWithBlockSizeWithItemIndex();
		public static IEnumerable<object[]> SourceRefDataWithBlockSizeWithItemIndexTestCases { get; } = SourceRefDataWithBlockSizeWithItemIndexVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount, x.BlockSize, x.ItemIndexes });
		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount, int BlockSize, IEnumerable<(long ItemIndex, long RangeItemsCount)> ItemsIndexesWithRange)> SourceRefDataWithBlockSizeWithItemIndexWithRangeVariants { get; } = CombineSourceRefDataWithBlockSizeWithItemIndexWithRange();
		public static IEnumerable<object[]> SourceRefDataWithBlockSizeWithItemIndexWithRangeTestCases { get; } = SourceRefDataWithBlockSizeWithItemIndexWithRangeVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount, x.BlockSize, x.ItemsIndexesWithRange });
		public static IEnumerable<(string TestCase, IEnumerable<object> TestData, long ItemsCount, IEnumerable<long> ItemIndexes)> SourceRefDataWithItemIndexVariants { get; } = CreateSourceRefDataWithItemIndexVariants();
		public static IEnumerable<object[]> SourceRefDataWithItemIndexTestCases { get; } = SourceRefDataWithItemIndexVariants.Select(x => new object[] { x.TestCase, x.TestData, x.ItemsCount, x.ItemIndexes });
	}
}