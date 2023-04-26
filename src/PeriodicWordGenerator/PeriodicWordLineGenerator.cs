namespace PeriodWordGenerator
{
	public class PeriodicWordLineGenerator
	{

		public IEnumerable<string> GetLines(Int32 upperBound)
		{
			string result;
			bool skip;
			int testVal;
			for (int i = 0; i <= upperBound - 1; i++)
			{
				testVal = i + 1;
				result = "";
				skip = false;
				if (testVal % 3 == 0)
				{
					result += "Ricky";
					skip = true;
				}
				if (testVal % 5 == 0)
				{
					result += "Bobby";
					skip = true;
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