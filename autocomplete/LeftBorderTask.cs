using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete;
public class LeftBorderTask
{
	public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
	{
		if (left == right - 1) return left;

		var middle = (left + right) / 2;
		if (phrases[middle].CompareTo(prefix) < 0 && !phrases[middle].StartsWith(prefix))
			return GetLeftBorderIndex(phrases, prefix, middle, right);
		return GetLeftBorderIndex(phrases, prefix, left, middle);
		
	}
}