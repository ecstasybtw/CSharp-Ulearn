using Avalonia.FreeDesktop.DBusIme;

namespace Recognizer;

public static class GrayscaleTask
{
	public static double[,] ToGrayscale(Pixel[,] original)
	{
		var xLength = original.GetLength(0);
		var yLength = original.GetLength(1);
		var grayScaled = new double[xLength, yLength];
		for (int x = 0; x < xLength; x++)
			for (int y = 0; y < yLength; y++)
				grayScaled[x,y] = (0.299*original[x, y].R + 0.587*original[x, y].G + 0.114*original[x, y].B) / 255;
		return grayScaled;
	}
}

