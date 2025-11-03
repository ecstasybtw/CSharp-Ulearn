using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using NUnit.Framework;

namespace Autocomplete;

internal class AutocompleteTask
{
	public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			return phrases[index];
            
		return null;
	}

	public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
	{
		var startIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		if (startIndex >= phrases.Count || !phrases[startIndex].StartsWith(prefix))
			return Array.Empty<string>();
		
		var endIndex = Math.Min(startIndex + count, phrases.Count);
		var result = new List<string>();
		for (int i = startIndex; i < endIndex; i++)
		{
			if (!phrases[i].StartsWith(prefix))
				break;
			result.Add(phrases[i]);
		}

		return result.ToArray();
	}

	public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		var rightIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);

		if (leftIndex >= rightIndex)
			return 0;

		return rightIndex - leftIndex;
	}

}

[TestFixture]
public class AutocompleteTests
{
	[Test]
	public void TopByPrefix_IsEmpty_WhenNoPhrases()
	{
		// ...
		//CollectionAssert.IsEmpty(actualTopWords);
	}

	// ...

	[Test]
	public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
	{
		// ...
		//Assert.AreEqual(expectedCount, actualCount);
	}

	// ...
}