using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public static class ExtensionsTask
{
	public static double Median(this IEnumerable<double> items)
	{
		var sortedItems = items.OrderBy(item => item).ToList();
		var count = sortedItems.Count();
		if (count == 0)
        	return 0;
		if (count % 2 == 1)
			return sortedItems[count / 2];
		else
			return (sortedItems[count / 2 - 1] + sortedItems[count / 2]) / 2;
	}

	public static IEnumerable<(T First, T Second)> Bigrams<T>(this IEnumerable<T> items)
	{
		var enumerator = items.GetEnumerator();
		if (!enumerator.MoveNext())
			yield break;

		var prev = enumerator.Current;
		while (enumerator.MoveNext())
		{
			yield return (prev, enumerator.Current);
			prev = enumerator.Current;
		}
	}
}