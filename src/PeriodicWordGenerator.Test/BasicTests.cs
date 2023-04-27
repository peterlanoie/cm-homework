using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeriodWordGenerator;

namespace PeriodicWordGenerator.Test
{
	[TestClass]
	public class BasicTests
	{
		private static Tuple<int, string>[] BASE_ITERATION_SET;

		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			BASE_ITERATION_SET = new Tuple<int, string>[]{
				new Tuple<int, string>(1, "1"),
				new Tuple<int, string>(2, "2"),
				new Tuple<int, string>(3, "Ricky"),
				new Tuple<int, string>(4, "4"),
				new Tuple<int, string>(5, "Bobby"),
				new Tuple<int, string>(6, "Ricky"),
				new Tuple<int, string>(7, "7"),
				new Tuple<int, string>(8, "8"),
				new Tuple<int, string>(9, "Ricky"),
				new Tuple<int, string>(10, "Bobby"),
				new Tuple<int, string>(11, "11"),
				new Tuple<int, string>(12, "Ricky"),
				new Tuple<int, string>(13, "13"),
				new Tuple<int, string>(14, "14"),
				new Tuple<int, string>(15, "RickyBobby"),
			};
		}

		[TestMethod]
		public void CreatedPeriodicPair()
		{
			var pair = new PeriodicPair(9, "Test");
			Assert.IsNotNull(pair);
			Assert.AreEqual(9, pair.RepeatsAt);
			Assert.AreEqual("Test", pair.Word);
		}

		[TestMethod]
		public void NoPairs()
		{
			var generator = new PeriodicWordLineGenerator();
			var results = generator.GetLines(100).ToArray();
			for (int i = 0; i < 100; i++)
			{
				Assert.AreEqual((i + 1).ToString(), results[i]);
			}
		}

		[TestMethod]
		public void BaseIterationAsArray()
		{
			var generator = new PeriodicWordLineGenerator();
			PeriodicPair[] pairs = new PeriodicPair[2]
			{
				new PeriodicPair(3, "Ricky"),
				new PeriodicPair(5, "Bobby")
			};
			var results = generator.GetLines(15, pairs).ToArray();
			foreach (var pair in BASE_ITERATION_SET)
			{
				Assert.AreEqual(pair.Item2, results[pair.Item1 - 1]);
			}
		}

		[TestMethod]
		public void BaseIterationAsEnumerable()
		{
			var generator = new PeriodicWordLineGenerator();
			PeriodicPair[] pairs = new PeriodicPair[2]
			{
				new PeriodicPair(3, "Ricky"),
				new PeriodicPair(5, "Bobby")
			};
			var list = generator.GetLines(int.MaxValue, pairs);

			int i = 0, max = 15;
			foreach (var line in list)
			{
				var pair = BASE_ITERATION_SET[i];
				Assert.AreEqual(pair.Item2, line);
				i++;
				if (i == max)
				{
					break;
				}
			}
		}

		[TestMethod]
		public void LargeTestSet()
		{
			var testSetSize = 10000000;
			var generator = new PeriodicWordLineGenerator();
			PeriodicPair[] pairs = new PeriodicPair[2]
			{
				new PeriodicPair(3, "Ricky"),
				new PeriodicPair(5, "Bobby")
			};
			var lines = generator.GetLines(testSetSize, pairs).ToArray();

			for (int i = 14; i < testSetSize; i += 15)
			{
				Assert.IsTrue(lines[i] == "RickyBobby");
			}
			for (int i = 2; i < testSetSize; i += 3)
			{
				Assert.IsTrue(lines[i].Contains("Ricky"));
			}
			for (int i = 4; i < testSetSize; i += 5)
			{
				Assert.IsTrue(lines[i].Contains("Bobby"));
			}
		}

	}
}