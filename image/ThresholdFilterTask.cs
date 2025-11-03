using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Recognizer;

public static class ThresholdFilterTask
{
	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
	{
		var xLength = original.GetLength(0);
		var yLength = original.GetLength(1);
		var pixels = new List<double>();
		for (int x = 0; x < xLength; x++)
			for (int y = 0; y < yLength; y++)
				pixels.Add(original[x, y]);

		pixels.Sort();
		pixels.Reverse();
		var whitePixels = (int)(xLength * yLength * whitePixelsFraction);
		pixels.RemoveRange(whitePixels, pixels.Count - whitePixels);

		for (int x = 0; x < xLength; x++)
			for (int y = 0; y < yLength; y++)
			{
				if (pixels.Contains(original[x,y]))
					original[x, y] = 1;
				else
					original[x, y] = 0;
			}
		
		return original;
	}
}
