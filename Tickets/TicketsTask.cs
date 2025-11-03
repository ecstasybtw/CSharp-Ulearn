using System;
using System.Numerics;

namespace Tickets;

public class TicketsTask
{
	public static BigInteger Solve(int halfLen, int totalSum)
	{
		if ((totalSum & 1) != 0)
			return BigInteger.Zero;
		int halfSum = totalSum / 2;
		var opt = BuildTable(halfLen, halfSum);
		var count = opt[halfLen - 1, halfSum];
		return count * count;
	}

	private static BigInteger[,] BuildTable(int len, int sum)
	{
		var opt = new BigInteger[len, sum + 1];
		InitFirstRowAndColumn(opt, len, sum);
		for (int i = 1; i < len; i++)
			for (int j = 1; j <= sum; j++) {
				if (j < 10)
					opt[i, j] = opt[i, j - 1] + opt[i - 1, j];
				else
					opt[i, j] = opt[i, j - 1] + opt[i - 1, j] - opt[i - 1, j - 10];

				if (opt[i, j] == opt[i, j - 1]) {
					CopyBackward(opt, i, j, sum, j - 1);
					break;
				}

				if (opt[i, j] < opt[i, j - 1]) {
					CopyBackward(opt, i, j, sum, j - 2);
					break;
				}
			}
		return opt;
	}

	private static void InitFirstRowAndColumn(BigInteger[,] opt, int rows, int maxSum)
	{
		for (int i = 0; i < rows; i++)
			opt[i, 0] = BigInteger.One;

		for (int j = 0; j <= maxSum && j < 10; j++)
			opt[0, j] = BigInteger.One;
	}

	private static void CopyBackward(BigInteger[,] opt, int row, int from, int maxSum, int source)
	{
		for (int k = 1; source - k >= 0 && from + k <= maxSum; k++)
			opt[row, from + k] = opt[row, source - k];
	}
}
