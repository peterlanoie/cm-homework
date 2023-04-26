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
		public void BaseIterationPass()
		{
			var generator = new PeriodicWordLineGenerator();
			var results = generator.GetLines(15).ToArray();
			foreach (var pair in BASE_ITERATION_SET)
			{
				Assert.AreEqual(pair.Item2, results[pair.Item1 - 1]);
			}
		}

	}
}