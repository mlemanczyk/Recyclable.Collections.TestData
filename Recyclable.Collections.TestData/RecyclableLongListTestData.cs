using Open.Numeric.Primes;
using Recyclable.Collections;
using System.Numerics;

namespace Recyclable.Collections.TestData
{
	public static class RecyclableLongListTestData
	{
		public static object[] SourceRefData { get; } = Enumerable.Range(1, 100).Cast<object>().ToArray();

		public static IEnumerable<long> CreateTestData(long itemsCount)
		{
			for (long index = 1; index <= itemsCount; index++)
			{
				yield return index;
			}
		}

		public static IEnumerable<object> CreateRefTestData(long itemsCount)
		{
			for (long index = 1; index <= itemsCount; index++)
			{
				yield return index;
			}
		}

		public static readonly IEnumerable<long> ItemsCountVariants = new long[]
		{
			//RecyclableDefaults.MinPooledArrayLength - 5, RecyclableDefaults.MinPooledArrayLength - 1, RecyclableDefaults.MinPooledArrayLength, RecyclableDefaults.MinPooledArrayLength + 1, RecyclableDefaults.MinPooledArrayLength + 5, 127, 128, 129, RecyclableDefaults.MinItemsCountForParallelization
			0, 1, 2, 3, 7, 10, 16, RecyclableDefaults.MinPooledArrayLength - 5, RecyclableDefaults.MinPooledArrayLength - 1, RecyclableDefaults.MinPooledArrayLength, RecyclableDefaults.MinPooledArrayLength + 1, RecyclableDefaults.MinPooledArrayLength + 5, RecyclableDefaults.MinPooledArrayLength * 100
		};

		public static readonly IEnumerable<object[]> ItemsCountTestCases = ItemsCountVariants.Select(x => new object[] { (int)x });

		public static readonly IEnumerable<int> BlockSizeVariants = new int[]
		{
			1, 2, 4, 16, RecyclableDefaults.MinPooledArrayLength - 5, (int)BitOperations.RoundUpToPowerOf2(RecyclableDefaults.MinPooledArrayLength)
		};

		private static bool IsValidBlockSize(long itemsCount, long blockSize) => itemsCount is < 1024 || blockSize is > 8;

		private static IEnumerable<object[]> GetTargetDataVariants()
		{
			foreach (var itemsCount in ItemsCountVariants)
			{
				foreach (var targetBlockSize in BlockSizeVariants)
				{
					if (!IsValidBlockSize(itemsCount, targetBlockSize))
					{
						continue;
					}

					yield return new object[] { itemsCount, targetBlockSize };
				}
			}
		}

		public static IEnumerable<object[]> TargetDataVariants => GetTargetDataVariants();

		private static IEnumerable<object[]> CombineSourceDataWithBlockSize(IEnumerable<object[]> sourceData)
		{
			foreach (var testData in sourceData)
			{
				foreach (var targetBlockSize in BlockSizeVariants)
				{
					if (!IsValidBlockSize((long)testData[2], targetBlockSize))
					{
						continue;
					}

					yield return new object[] { testData[0], testData[1], testData[2], targetBlockSize };
				}
			}
		}

		private static IEnumerable<object[]> CreateSourceDataVariants(long itemsCount, bool refType)
		{
			GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);

			switch (refType)
			{
				case false:
					long[] testData = CreateTestData(itemsCount).ToArray();

					yield return new object[] { $"Array[{itemsCount}]", testData, itemsCount };
					yield return new object[] { $"ICollection[{itemsCount}]", new CustomICollection<long>(testData), itemsCount };
					yield return new object[] { $"ICollection<T>[{itemsCount}]", new CustomICollectionT<long>(testData.ToList()), itemsCount };
					yield return new object[] { $"IEnumerable[{itemsCount}]", new EnumerableWithoutCount<long>(testData), itemsCount };
					yield return new object[] { $"IEnumerable<T>[{itemsCount}] without non-enumerated count", new EnumerableWithoutCount<long>(testData), itemsCount };
					yield return new object[] { $"IList[{itemsCount}]", new CustomIList<long>(testData), itemsCount };
					yield return new object[] { $"IList<T>[{itemsCount}]", new CustomIListT<long>(testData), itemsCount };
					yield return new object[] { $"IReadOnlyList<T>[{itemsCount}])", new CustomIReadOnlyList<long>(testData), itemsCount };
					yield return new object[] { $"List<T>[{itemsCount}]", testData.ToList(), itemsCount };
					yield return new object[] { $"ReadOnlySpan<T>[{itemsCount}]", testData, itemsCount };
					yield return new object[] { $"RecyclableList<T>[{itemsCount}]", testData.ToRecyclableList(), itemsCount };

					foreach (var sourceBlockSize in BlockSizeVariants)
					{
						if (!IsValidBlockSize(itemsCount, sourceBlockSize))
						{
							continue;
						}

						yield return new object[]
						{
							$"RecyclableLongList<T>[itemsCount: {itemsCount}, minBlockSize: {sourceBlockSize}]",
							testData.ToRecyclableLongList(sourceBlockSize),
							itemsCount
						};
					}

					yield return new object[] { $"Span<T>[{itemsCount}]", testData, itemsCount };
					yield return new object[] { $"T[{itemsCount}]", testData, itemsCount };
					break;

				case true:
					object[] testRefData = CreateRefTestData(itemsCount).ToArray();

					yield return new object[] { $"Array[{itemsCount}]", testRefData, itemsCount };
					yield return new object[] { $"ICollection(itemsCount: {itemsCount}", new CustomICollection<object>(testRefData), itemsCount };
					yield return new object[] { $"ICollection<T>(itemsCount: {itemsCount}", new CustomICollectionT<object>(testRefData.ToList()), itemsCount };
					yield return new object[] { $"IEnumerable(itemsCount: {itemsCount})", new EnumerableWithoutCount<object>(testRefData), itemsCount };
					yield return new object[] { $"IEnumerable<T>(itemsCount: {itemsCount}) without non-enumerated count", new EnumerableWithoutCount<object>(testRefData), itemsCount };
					yield return new object[] { $"IList(itemsCount: {itemsCount})", new CustomIList<object>(testRefData), itemsCount };
					yield return new object[] { $"IList<T>(itemsCount: {itemsCount})", new CustomIListT<object>(testRefData), itemsCount };
					yield return new object[] { $"IReadOnlyList<T>(itemsCount: {itemsCount})", new CustomIReadOnlyList<object>(testRefData), itemsCount };
					yield return new object[] { $"List<T>(itemsCount: {itemsCount}", testRefData.ToList(), itemsCount };
					yield return new object[] { $"ReadOnlySpan<T>[{itemsCount}]", testRefData, itemsCount };
					yield return new object[] { $"RecyclableList<T>(itemsCount: {itemsCount})", testRefData.ToRecyclableList(), itemsCount };

					foreach (var sourceBlockSize in BlockSizeVariants)
					{
						if (!IsValidBlockSize(itemsCount, sourceBlockSize))
						{
							continue;
						}

						yield return new object[]
						{
							$"RecyclableLongList<T>(itemsCount: {itemsCount}, minBlockSize: {sourceBlockSize})",
							testRefData.ToRecyclableLongList(sourceBlockSize),
							itemsCount
						};
					}

					yield return new object[] { $"Span<T>[{itemsCount}]", testRefData, itemsCount };
					yield return new object[] { $"T[{itemsCount}]", testRefData, itemsCount };
					break;
			}
		}

		private static IEnumerable<object[]> CreateSourceDataVariants(bool refType)
		{
			foreach (var itemsCount in ItemsCountVariants)
			{
				foreach (var variant in CreateSourceDataVariants(itemsCount, refType))
				{
					yield return variant;
				}
			}
		}

		public static IEnumerable<object[]> EmptySourceDataVariants => CreateSourceDataVariants(0, false);
		public static IEnumerable<object[]> SourceDataVariants => CreateSourceDataVariants(false);
		public static IEnumerable<object[]> SourceDataWithItemIndexVariants
		{
			get
			{
				foreach (var testCase in CreateSourceDataVariants(false).Where(testCase => (long)testCase[2] > 0))
				{
					var itemIndexes = CreateItemIndexVariants((long)testCase[2], RecyclableDefaults.BlockSize).ToArray();
					yield return new object[] { testCase[0], testCase[1], testCase[2], itemIndexes };
				}
			}
		}

		public static IEnumerable<object[]> SourceRefDataWithItemIndexVariants
		{
			get
			{
				IEnumerable<object[]> testCases = CreateSourceDataVariants(true).Where(testCase => (long)testCase[2] > 0);
				foreach (var testCase in testCases)
				{
					var itemIndexes = CreateItemIndexVariants((long)testCase[2], RecyclableDefaults.BlockSize).ToArray();
					
					yield return new object[] { testCase[0], testCase[1], testCase[2], itemIndexes };
				}
			}
		}

		public static IEnumerable<object[]> SourceDataWithBlockSizeVariants => CombineSourceDataWithBlockSize(SourceDataVariants);
		public static IEnumerable<object[]> SourceRefDataVariants => CreateSourceDataVariants(true);
		public static IEnumerable<object[]> SourceRefDataWithBlockSizeVariants { get; } = CombineSourceDataWithBlockSize(SourceRefDataVariants);

		private static IEnumerable<long> InitialItemIndexVariants => new long[] { 0, 1, 2, 3, 4, 5 };
		private static IEnumerable<long> CreateItemIndexVariants(long itemsCount, int blockSize)
		{
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

			primes = primes.Concat(
					primes.Select(index => itemsCount - index - 1)
				)
				.Where(index => itemsCount > 0 && index >= 0 && index < itemsCount).ToArray()
				.Distinct().ToArray();

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

		public static IEnumerable<object[]> CombineSourceRefDataWithBlockSizeWithItemIndex(bool refType)
		{
			IEnumerable<object[]> sourceData = refType switch
			{
				false => SourceDataWithBlockSizeVariants,
				true => SourceRefDataWithBlockSizeVariants
			};

			foreach (var testCase in sourceData)
			{
				long itemsCount = (long)testCase[2];
				if (itemsCount == 0)
				{
					continue;
				}

				long[] itemIndexes = CreateItemIndexVariants(itemsCount, (int)testCase[3])
					.Where(_ => IsValidBlockSize((long)testCase[2], (int)testCase[3]))
					.ToArray();

				yield return new object[] { testCase[0], testCase[1], (long)testCase[2], (int)testCase[3], itemIndexes };
			}
		}

		public static IEnumerable<object[]> CombineSourceRefDataWithBlockSizeWithItemIndexWithRange(bool refType)
		{
			IEnumerable<object[]> sourceData = refType switch
			{
				false => SourceDataWithBlockSizeVariants,
				true => SourceRefDataWithBlockSizeVariants
			};

			foreach (var testCase in sourceData)
			{
				long itemsCount = (long)testCase[2];
				if (itemsCount == 0)
				{
					continue;
				}

				var itemIndexes = CreateItemIndexVariants(itemsCount, (int)testCase[3])
					.Where(_ => IsValidBlockSize((long)testCase[2], (int)testCase[3]));

				var itemIndexesWithRange = itemIndexes.SelectMany(
					   itemIndex => CombineItemIndexWithRange(itemIndex, itemsCount))
					.ToArray();

				yield return new object[] { testCase[0], testCase[1], (long)testCase[2], (int)testCase[3], itemIndexesWithRange };
			}
		}

		public static IEnumerable<(long ItemIndex, long RangeItemsCount)> CombineItemIndexWithRange(long itemIndex, long itemsCount)
		{
			yield return (0L, Math.Max(itemIndex, 1L));
			yield return (itemIndex, Math.Max(itemsCount - itemIndex, 1L));
			yield return (itemIndex, 1L);
			yield return (Math.Max(0L, itemIndex - 1L), Math.Max(Math.Min(itemsCount - itemIndex, 2L), 1L));

			yield return (0L, 0L);
			yield return (itemIndex, 0L);
			yield return (Math.Max(0L, itemIndex - 1L), 0L);

			yield return (0L, 1L);
			yield return (0L, Math.Max(itemIndex, 1L));
			yield return (0L, Math.Max(itemIndex, 1L));
			yield return (0L, Math.Max(itemsCount - itemIndex, 1L));
			yield return (0L, Math.Max(Math.Min(itemsCount - itemIndex, 2L), 1L));
		}

		public static IEnumerable<object[]> SourceDataWithBlockSizeWithItemIndexVariants { get; } = CombineSourceRefDataWithBlockSizeWithItemIndex(false);
		public static IEnumerable<object[]> SourceDataWithBlockSizeWithItemIndexWithRangeVariants { get; } = CombineSourceRefDataWithBlockSizeWithItemIndexWithRange(false);
		public static IEnumerable<object[]> SourceRefDataWithBlockSizeWithItemIndexVariants { get; } = CombineSourceRefDataWithBlockSizeWithItemIndex(true);
		public static IEnumerable<object[]> SourceRefDataWithBlockSizeWithItemIndexWithRangeVariants { get; } = CombineSourceRefDataWithBlockSizeWithItemIndexWithRange(true);
	}
}