using PeriodicWordGenerator;

namespace PeriodWordGenerator
{
	public class PeriodicWordLineGenerator
	{

		public IEnumerable<string> GetLines(Int32 upperBound, params PeriodicPair[] periodicPairs)
		{
			string result;
			bool skip;
			int testVal;
			for (int i = 0; i <= upperBound - 1; i++)
			{
				testVal = i + 1;
				result = "";
				skip = false;
				foreach (var pair in periodicPairs)
				{
					if (testVal % pair.RepeatsAt == 0)
					{
						result += pair.Word;
						skip = true;
					}
				}
				if (!skip)
				{
					result += testVal.ToString();
				}
				yield return result;
			}
		}

	}
}