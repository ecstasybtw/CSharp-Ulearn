using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var xLength = original.GetLength(0);
            var yLength = original.GetLength(1);
            var newImage = new double[xLength, yLength];

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                {
                    var neighbors = GetNeighbors(original, x, y, xLength, yLength);
                    newImage[x, y] = CalculateMedian(neighbors);
                }

            return newImage;
        }
	     private static List<double> GetNeighbors(double[,] original, int x, int y, int xLength, int yLength)
        {
            var neighbors = new List<double>();

            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    var nx = x + i;
                    var ny = y + j;
                    if (nx >= 0 && nx < xLength && ny >= 0 && ny < yLength)
                        neighbors.Add(original[nx, ny]);
                }

            return neighbors;
        }

        private static double CalculateMedian(List<double> values)
        {
            values.Sort();
            var count = values.Count;
            if (count % 2 == 1)
                return values[count / 2];
            else
                return (values[(count / 2) - 1] + values[count / 2]) / 2;
        }
    }
}
