
using System;
namespace Billiards;

public static class BilliardsTask
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directionRadians">Угол направления движения шара</param>
    /// <param name="wallInclinationRadians">Угол</param>
    /// <returns></returns>
    public static double BounceWall(double directionRadians, double wallInclinationRadians)
    {
        //TODO
        double aPartOfFallAngleRadians = (Math.PI / 2) - wallInclinationRadians;
        double fallAngle = aPartOfFallAngleRadians + directionRadians;
        double resultAngle = Math.PI - aPartOfFallAngleRadians - fallAngle;
        return resultAngle;
    }
}