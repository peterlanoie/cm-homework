using Microsoft.VisualStudio.TestTools.UnitTesting;
using CM.PeriodicWordEmitter;
using System.Reflection.Emit;

namespace CM.PeriodicWordEmitter.Test
{
	[TestClass]
	public class Tests
	{
		private static Tuple<int, string>[] BASE_TEST_SET;

		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			BASE_TEST_SET = new Tuple<int, string>[]{
				new Tuple<int, string>(1, "1"),
				new Tuple<int, string>(2, "2"),
				new Tuple<int, string>(3, "Threes"),
				new Tuple<int, string>(4, "4"),
				new Tuple<int, string>(5, "Fives"),
				new Tuple<int, string>(6, "Threes"),
				new Tuple<int, string>(7, "7"),
				new Tuple<int, string>(8, "8"),
				new Tuple<int, string>(9, "Threes"),
				new Tuple<int, string>(10, "Fives"),
				new Tuple<int, string>(11, "11"),
				new Tuple<int, string>(12, "Threes"),
				new Tuple<int, string>(13, "13"),
				new Tuple<int, string>(14, "14"),
				new Tuple<int, string>(15, "ThreesFives"),
			};
		}

		[TestMethod]
		public void CreatedPatternRule()
		{
			var rule = new RepetitionRule(9, "Test");
			Assert.IsNotNull(rule);
			Assert.AreEqual(9, rule.RepeatsAt);
			Assert.AreEqual("Test", rule.Word);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ErrorOnRuleFactorZero()
		{
			new RepetitionRule(0, "Test");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ErrorOnRuleWordEmpty()
		{
			new RepetitionRule(1, "");
		}

		[TestMethod]
		public void NoRules()
		{
			var generator = new PeriodicWordEmitter();
			var result = generator.GetLines(100).ToArray();
			var expected = Enumerable.Range(1, 100).Select(x => x.ToString()).ToArray();
			CollectionAssert.AreEqual(expected, result);
		}

		private static RepetitionRule[] ThreeFiveRuleSet
		{
			get
			{
				return new RepetitionRule[2]
				{
					new RepetitionRule(3, "Threes"),
					new RepetitionRule(5, "Fives")
				};
			}
		}

		[TestMethod]
		public void BaseIterationAsArray()
		{
			var generator = new PeriodicWordEmitter();
			var results = generator.GetLines(15, ThreeFiveRuleSet).ToArray();
			CollectionAssert.AreEqual(results, BASE_TEST_SET.Select(x => x.Item2).ToArray());
		}

		[TestMethod]
		public void BaseIterationAsEnumerable()
		{
			var generator = new PeriodicWordEmitter();
			var list = generator.GetLines(int.MaxValue, ThreeFiveRuleSet);

			int i = 0, max = 15;
			foreach (var line in list)
			{
				var pair = BASE_TEST_SET[i];
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
			var generator = new PeriodicWordEmitter();
			var lines = generator.GetLines(testSetSize, ThreeFiveRuleSet).ToArray();

			for (int i = 14; i < testSetSize; i += 15)
			{
				Assert.IsTrue(lines[i] == "ThreesFives");
			}
			for (int i = 2; i < testSetSize; i += 3)
			{
				Assert.IsTrue(lines[i].Contains("Threes"));
			}
			for (int i = 4; i < testSetSize; i += 5)
			{
				Assert.IsTrue(lines[i].Contains("Fives"));
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NegativeCountInvalid()
		{
			var generator = new PeriodicWordEmitter();
			var lines = generator.GetLines(-1);
			lines.First();
		}

	}
}