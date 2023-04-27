
namespace CM.PeriodicWordEmitter
{
	public class PeriodicWordEmitter
	{

		public IEnumerable<string> GetLines(Int32 upperBound, params RepetitionRule[] periodicPairs)
		{
			if (upperBound < 0)
			{
				throw new ArgumentException("`upperBound` must be 0 or greater");
			}
			string result;
			bool skip;
			int testVal;
			var ruleList = periodicPairs.ToList();
			for (int i = 0; i <= upperBound - 1; i++)
			{
				testVal = i + 1;
				result = "";
				skip = false;
				ruleList.ForEach(rule =>
				{
					if (testVal % rule.RepeatsAt == 0)
					{
						result += rule.Word;
						skip = true;
					}
				});
				if (!skip)
				{
					result += testVal.ToString();
				}
				yield return result;
			}
		}

	}
}