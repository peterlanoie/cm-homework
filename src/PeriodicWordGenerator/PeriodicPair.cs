using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicWordGenerator
{
	public class PeriodicPair
	{
		public PeriodicPair(int repeatsAt, string word)
		{
			this.RepeatsAt = repeatsAt;
			this.Word = word;
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
