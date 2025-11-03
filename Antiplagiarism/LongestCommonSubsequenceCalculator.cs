using System;
using System.Collections.Generic;

namespace Antiplagiarism;

public static class LongestCommonSubsequenceCalculator
{
	public static List<string> Calculate(List<string> first, List<string> second)
	{
		var opt = CreateOptimizationTable(first, second);
		return RestoreAnswer(opt, first, second);
	}

	private static int[,] CreateOptimizationTable(List<string> first, List<string> second)
	{
		int[,] opt = new int[first.Count + 1, second.Count + 1];
		for (int i = 1; i <= first.Count; i++) {
			for (int j = 1; j <= second.Count; j++) {
				if (first[i - 1] == second[j - 1])
					opt[i, j] = opt[i - 1, j - 1] + 1;
				else
					opt[i, j] = Math.Max(opt[i, j - 1], opt[i - 1, j]);
			}
		}
		return opt;
	}

	private static List<string> RestoreAnswer(int[,] opt, List<string> first, List<string> second)
	{
		var result = new List<string>();
		int firstCount = first.Count;
		int secondCount = second.Count;
		while (firstCount > 0 && secondCount > 0) {
			if (first[firstCount - 1] == second[secondCount - 1]) {
				result.Insert(0, first[firstCount - 1]);
				firstCount--;
				secondCount--;
			}
			else if (opt[firstCount, secondCount - 1] > opt[firstCount - 1, secondCount])
				secondCount--;
			else
				firstCount--;
		}
		return result;
	}
}
