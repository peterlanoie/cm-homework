using PeriodWordGenerator;
using System.Diagnostics.SymbolStore;

namespace ConsoleApp1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var generator = new PeriodicWordLineGenerator();
			var results = generator.GetLines(15).ToArray();

			for (int i = 0; i < results.Length; i++)
			{
				Console.WriteLine($"{i+1}: {results[i]}");
			}
		}
	}
}