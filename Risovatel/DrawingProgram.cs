using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    class Painter
    {
        private static float x, y;
        private static IGraphics graphics;

        public static void Initialize(IGraphics newGraphics)
        {
            graphics = newGraphics;
            graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void DrawLine(Pen pen, double length, double angle)
        {
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void ChangePosition(double length, double angle)
        {
            x += (float)(length * Math.Cos(angle));
            y += (float)(length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double angleTurn, IGraphics graphics)
        {
            Painter.Initialize(graphics);

            var size = Math.Min(width, height);
            var (x0, y0) = CalculateStartPosition(size, width, height);

            Painter.SetPosition(x0, y0);

            Pen yellowPen = new Pen(Brushes.Yellow);

            DrawFirstSide(size, yellowPen);
            DrawSecondSide(size, yellowPen);
            DrawThirdSide(size, yellowPen);
            DrawFourthSide(size, yellowPen);
        }

        private static (float, float) CalculateStartPosition(int size, int width, int height)
        {
            var diagonalLength = Math.Sqrt(2) * (size * 0.375f + size * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            return (x0, y0);
        }

        private static void DrawFirstSide(float size, Pen pen)
        {
            DrawSide(size, 0, pen);
            Painter.ChangePosition(size * 0.04f, -Math.PI);
            Painter.ChangePosition(size * 0.04f * Math.Sqrt(2), 3 * Math.PI / 4);
        }

        private static void DrawSecondSide(float size, Pen pen)
        {
            DrawSide(size, -Math.PI / 2, pen);
            Painter.ChangePosition(size * 0.04f, -Math.PI / 2 - Math.PI);
            Painter.ChangePosition(size * 0.04f * Math.Sqrt(2), -Math.PI / 2 + 3 * Math.PI / 4);
        }

        private static void DrawThirdSide(float size, Pen pen)
        {
            DrawSide(size, Math.PI, pen);
            Painter.ChangePosition(size * 0.04f, Math.PI - Math.PI);
            Painter.ChangePosition(size * 0.04f * Math.Sqrt(2), Math.PI + 3 * Math.PI / 4);
        }

        private static void DrawFourthSide(float size, Pen pen)
        {
            DrawSide(size, Math.PI / 2, pen);
            Painter.ChangePosition(size * 0.04f, Math.PI / 2 - Math.PI);
            Painter.ChangePosition(size * 0.04f * Math.Sqrt(2), Math.PI / 2 + 3 * Math.PI / 4);
        }

        private static void DrawSide(float size, double startAngle, Pen pen)
        {
            Painter.DrawLine(pen, size * 0.375f, startAngle);
            Painter.DrawLine(pen, size * 0.04f * Math.Sqrt(2), startAngle + Math.PI / 4);
            Painter.DrawLine(pen, size * 0.375f, startAngle + Math.PI);
            Painter.DrawLine(pen, size * 0.375f - size * 0.04f, startAngle + Math.PI / 2);
        }
    }
}