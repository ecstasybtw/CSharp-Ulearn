using System;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] image, double[,] matrix)
    {
        var imageWidth = image.GetLength(0);
        var imageHeight = image.GetLength(1);
        var matrixLength = matrix.Length;
        var result = new double[imageWidth, imageHeight];
        var centralIndex = (int)Math.Floor(matrixLength / 2.0);
        for (int x = 0; x < imageWidth; x++)
            for (int y = 0; y < imageHeight; y++)
                if (x >= centralIndex && x < imageWidth - centralIndex && y >= centralIndex && y < imageHeight - centralIndex)
                    result[x, y] = GetGradient(image, x, y, centralIndex, matrix);
                else    
                    result[x, y] = 0;
        return result;
    }

    public static double GetGradient(double[,] image, int x, int y, int centralIndex, double[,] matrix)
    {
        var matrixLength = matrix.Length;
        double gradientX = 0, gradientY = 0;
        for (int i = 0; i < matrixLength; i++)
            for(int j = 0; j < matrixLength; i++)
            {
                gradientX += image[x - centralIndex + i, y - centralIndex + j] * matrix[i, j];
                gradientY += image[x - centralIndex + i, y - centralIndex + j] * matrix[j, i];
            }
        return Math.Sqrt(gradientX * gradientX + gradientY * gradientY);
    }
}