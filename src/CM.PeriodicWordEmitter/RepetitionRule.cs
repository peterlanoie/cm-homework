using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.PeriodicWordEmitter
{
	public class RepetitionRule
	{
		public RepetitionRule(int repeatsAt, string word)
		{
			if (repeatsAt < 1)
			{
				throw new ArgumentOutOfRangeException("Rule repetition factor must be at least 1");
			}
			if (string.IsNullOrEmpty(word))
			{
				throw new ArgumentException("Provided rule repetition word is empty");
			}
			RepeatsAt = repeatsAt;
			Word = word;
		}

		/// <summary>
		/// The word to add to the line at the repeat point.
		/// </summary>
		public string Word { get; set; }

		/// <summary>
		/// The repetition factor. "Repeat ever X lines."
		/// </summary>
		public int RepeatsAt { get; set; }
	}
}
