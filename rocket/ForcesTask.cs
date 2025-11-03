using System;
using System.Reflection.PortableExecutable;

namespace func_rocket;

public class ForcesTask
{
	public static RocketForce GetThrustForce(double forceValue) => r => new Vector(forceValue * Math.Cos(r.Direction), forceValue * Math.Sin(r.Direction));

	public static RocketForce ConvertGravityToForce(Gravity gravity, Vector spaceSize) => r => gravity(spaceSize, r.Location);

	public static RocketForce Sum(params RocketForce[] forces) => r =>
    {
        double sumX = 0, sumY = 0;
        foreach (var force in forces)
        {
            var result = force(r);
            sumX += result.X;
            sumY += result.Y;
        }
        return new Vector(sumX, sumY);
    };
}