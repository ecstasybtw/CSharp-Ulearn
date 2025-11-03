using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            // Векторы AB и AP (где P - точка с координатами x, y)
            double ABx = bx - ax;
            double ABy = by - ay;
            double APx = x - ax;
            double APy = y - ay;

            // Квадрат длины отрезка AB
            double AB_squared = ABx * ABx + ABy * ABy;

            // Если A и B совпадают, вернуть расстояние от точки до A (или B, т.к. они равны)
            if (AB_squared == 0)
            {
                return Math.Sqrt(APx * APx + APy * APy);
            }

            // Проекция вектора AP на AB в доле от длины отрезка AB
            double t = (APx * ABx + APy * ABy) / AB_squared;

            // Ограничиваем t диапазоном от 0 до 1, чтобы проекция была на отрезке AB
            t = Math.Max(0, Math.Min(1, t));

            // Проекция точки на отрезок: точка на отрезке P'
            double Px = ax + t * ABx;
            double Py = ay + t * ABy;

            // Расстояние от точки до проекции P'
            double distance = Math.Sqrt((x - Px) * (x - Px) + (y - Py) * (y - Py));

            return distance;
        }
    }
}