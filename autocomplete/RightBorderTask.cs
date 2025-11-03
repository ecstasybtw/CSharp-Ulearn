using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete;

public class RightBorderTask
{
	public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
	{
		if (phrases.Count() != 0)
		{
			while (left < right)
			{
				var middle = (left + right) / 2;
				if (phrases[middle].CompareTo(prefix) > 0 && !phrases[middle].StartsWith(prefix))
					right = middle;
				else 
					left = middle + 1;
			}

			return left;
		}

		else
			return 0;
	}
}