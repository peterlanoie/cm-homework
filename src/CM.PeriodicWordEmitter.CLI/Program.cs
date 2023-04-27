using CM.PeriodicWordEmitter;
using System.Diagnostics.SymbolStore;

namespace CM.PeriodicWordEmitter.CLI
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var generator = new PeriodicWordEmitter();
			var rules = new RepetitionRule[]
			{
				new RepetitionRule(3, "Foo"),
				new RepetitionRule(5, "Bar")
			};
			var results = generator.GetLines(15, rules);
			results.ToList().ForEach(line => Console.WriteLine(line));
		}
	}
}