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
				new Tuple<int, string>(0, "1"),
				new Tuple<int, string>(1, "2"),
				new Tuple<int, string>(2, "Ricky"),
				new Tuple<int, string>(3, "4"),
				new Tuple<int, string>(4, "Bobby"),
				new Tuple<int, string>(5, "Ricky"),
				new Tuple<int, string>(6, "7"),
				new Tuple<int, string>(7, "8"),
				new Tuple<int, string>(8, "Ricky"),
				new Tuple<int, string>(9, "Bobby"),
				new Tuple<int, string>(10, "11"),
				new Tuple<int, string>(11, "Ricky"),
				new Tuple<int, string>(12, "13"),
				new Tuple<int, string>(13, "14"),
				new Tuple<int, string>(14, "RickyBobby"),
			};
		}

		[TestMethod]
		public void BaseIterationPass()
		{
			var generator = new PeriodicWordLineGenerator();
			var results = generator.GetLines(15).ToArray();
			foreach (var pair in BASE_ITERATION_SET)
			{
				Assert.AreEqual(pair.Item2, results[pair.Item1]);
			}
		}

	}
}