using System;
using System.Collections.Generic;

namespace func_rocket;
public class LevelsTask
{
    static readonly Physics standardPhysics = new();

    public static IEnumerable<Level> CreateLevels()
    {
        yield return new Level("Zero", 
            new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
            new Vector(600, 200), 
            (size, v) => Vector.Zero, standardPhysics);

        yield return new Level("Heavy", 
            new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
            new Vector(600, 200), 
            (size, v) => new Vector(0, 0.9), standardPhysics);
        
        yield return new Level("Up",
            new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
            new Vector(700, 500),
            (size, v) => new Vector(0, -300 / (size.Y - v.Y + 300.0)), standardPhysics);
        
        yield return new Level("WhiteHole",
            new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
            new Vector(600, 200),
            (size, v) =>
            {
                var d = (new Vector(600, 200) - v).Length;
                return (new Vector(600, 200) - v).Normalize() * (-140 * d) / (d * d + 1); 
            }, standardPhysics);

        yield return new Level("BlackHole",
			new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
			new Vector(600, 200),
			(size, v) =>
			{
				var target = new Vector(600, 200);
				var startPosition = new Vector(200, 500);
				var anomaly = (target + startPosition) / 2;

				var d = (anomaly - v).Length;
				return (anomaly - v).Normalize() * (300 * d) / (d * d + 1);
			}, standardPhysics);


        
        yield return new Level("BlackAndWhiteHole",
			new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
			new Vector(600, 200),
			(size, v) =>
			{
				var target = new Vector(600, 200);
				var startPosition = new Vector(200, 500);

				var anomalyBlack = (target + startPosition) / 2;
				var dBlack = (anomalyBlack - v).Length;
				var gravityBlack = (anomalyBlack - v).Normalize() * (300 * dBlack) / (dBlack * dBlack + 1);

				var dWhite = (target - v).Length;
				var gravityWhite = (target - v).Normalize() * (-140 * dWhite) / (dWhite * dWhite + 1);

				return (gravityBlack + gravityWhite) / 2;
			}, standardPhysics);

    }
}
