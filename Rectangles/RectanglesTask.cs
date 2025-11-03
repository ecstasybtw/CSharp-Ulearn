using System;

namespace Rectangles;

public static class RectanglesTask
{
    public static bool AreIntersected(Rectangle r1, Rectangle r2)
    {
        if (r1.Right < r2.Left || r1.Left > r2.Right || r1.Bottom < r2.Top || r1.Top > r2.Bottom)
            return false;
        else
            return true;
    }
    public static int IntersectionSquare(Rectangle r1, Rectangle r2)
    {
        // Тут я знаю, что могу засунуть все это сразу в return, но я не буду этого делать, потому что лучше код будет читаться, чем будет суперкомпактным.
        var leftBorder = Math.Max(r1.Left, r2.Left);
        var rightBoder = Math.Min(r1.Right, r2.Right);
        var bottomBorder = Math.Min(r1.Bottom, r2.Bottom);
        var topBorder = Math.Max(r1.Top, r2.Top);
        var IntersectionWidth = rightBoder - leftBorder;
        var IntersectionHeight = bottomBorder - topBorder;
        if (IntersectionWidth <= 0 || IntersectionHeight <= 0)
            return 0;
        return IntersectionHeight * IntersectionWidth; 
    }
    public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
    {
        if (r1.Left >= r2.Left && r1.Right <= r2.Right && r1.Top >= r2.Top && r1.Bottom <= r2.Bottom)
            return 0;

        else if (r2.Left >= r1.Left && r2.Right <= r1.Right && r2.Top >= r1.Top && r2.Bottom <= r1.Bottom)
            return 1;
        else
            return -1;
    }
}