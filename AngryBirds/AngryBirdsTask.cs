using System;

namespace AngryBirds;

public static class AngryBirdsTask
{
    public static double FindSightAngle(double InitVelocity, double distance)
    {
        const double g = 9.8;
        double angle = (Math.Asin((distance * g) / (InitVelocity * InitVelocity))) / 2;
        return angle;
    }
}