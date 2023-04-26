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
		public void BaseIterationAsArray()
		{
			var generator = new PeriodicWordLineGenerator();
			var results = generator.GetLines(15).ToArray();
			foreach (var pair in BASE_ITERATION_SET)
			{
				Assert.AreEqual(pair.Item2, results[pair.Item1 - 1]);
			}
		}

		[TestMethod]
		public void BaseIterationAsEnumerable()
		{
			var generator = new PeriodicWordLineGenerator();
			var list = generator.GetLines(int.MaxValue);

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
		public void OneHundred()
		{
			var generator = new PeriodicWordLineGenerator();
			var lines = generator.GetLines(100).ToArray();

			for (int i = 14; i < 100; i+=15)
			{
				Assert.IsTrue(lines[i] == "RickyBobby");
			}
			for (int i = 2; i < 100; i += 3)
			{
				Assert.IsTrue(lines[i].Contains("Ricky"));
			}
			for (int i = 4; i < 100; i += 5)
			{
				Assert.IsTrue(lines[i].Contains("Bobby"));
			}

		}

	}
}