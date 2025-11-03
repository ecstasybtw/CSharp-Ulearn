using System;
using NUnit.Framework;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class ManipulatorTask
{
	public static double[] MoveManipulatorTo(double x, double y, double alpha)
	{
		var wristX = x - Palm * Math.Cos(alpha);
		var wristY = y - Palm * Math.Sin(alpha);
		var thirdSide = Math.Sqrt(wristX * wristX + wristY * wristY);

		if (thirdSide > UpperArm + Forearm || thirdSide < Math.Abs(UpperArm - Forearm))
			return new[] { double.NaN, double.NaN, double.NaN };

		var elbowAngle = TriangleTask.GetABAngle(UpperArm, Forearm, thirdSide);
		var shoulderLeftHalf = TriangleTask.GetABAngle(UpperArm, thirdSide, Forearm);
		var anotherSide = Math.Sqrt(Palm * Palm + y * y - 2 * Palm * y * Math.Cos(alpha + Math.PI / 2));
		var shoulderRightHalf = TriangleTask.GetABAngle(x, thirdSide, anotherSide);
		var shoulderAngle = shoulderLeftHalf + shoulderRightHalf;
		var wristAngle = -alpha - shoulderAngle - elbowAngle;

		if (double.IsNaN(elbowAngle) || double.IsNaN(shoulderAngle) || double.IsNaN(wristAngle))
			return new[] { double.NaN, double.NaN, double.NaN };

		return new[] { shoulderAngle, elbowAngle, wristAngle };
	}
}

[TestFixture]
public class ManipulatorTask_Tests
{
    private const double Epsilon = 1e-5;

    [Test]
    public void TestMoveManipulatorTo()
    {
        var random = new Random();

        for (int i = 0; i < 1000; i++)
        {
            var x = random.NextDouble() * (UpperArm + Forearm + Palm) * 2 - (UpperArm + Forearm + Palm);
            var y = random.NextDouble() * (UpperArm + Forearm + Palm) * 2 - (UpperArm + Forearm + Palm);
            var alpha = random.NextDouble() * 2 * Math.PI - Math.PI;
            var result = ManipulatorTask.MoveManipulatorTo(x, y, alpha);
            var wristX = x - Palm * Math.Cos(alpha);
            var wristY = y - Palm * Math.Sin(alpha);
            var distanceToWrist = Math.Sqrt(wristX * wristX + wristY * wristY);

            if (distanceToWrist > UpperArm + Forearm || distanceToWrist < Math.Abs(UpperArm - Forearm))
            {
                Assert.That(result, Is.All.NaN);
            }
            else
            {
                var shoulder = result[0];
                var elbow = result[1];
                var wrist = result[2];

                var shoulderAngle = shoulder;
                var elbowAngle = elbow - (Math.PI - shoulderAngle);
                var wristAngle = wrist - (Math.PI - elbowAngle);

                var elbowX = Math.Cos(shoulderAngle) * UpperArm;
                var elbowY = Math.Sin(shoulderAngle) * UpperArm;

                var wristXGeneral = Math.Cos(elbowAngle) * Forearm + elbowX;
                var wristYGeneral = Math.Sin(elbowAngle) * Forearm + elbowY;

                var palmEndPosX = Math.Cos(wristAngle) * Palm + wristXGeneral;
                var palmEndPosY = Math.Sin(wristAngle) * Palm + wristYGeneral;

                Assert.That(palmEndPosX, Is.EqualTo(x).Within(Epsilon), "X");
                Assert.That(palmEndPosY, Is.EqualTo(y).Within(Epsilon), "Y");
                Assert.That(-(shoulder + elbow + wrist), Is.EqualTo(alpha).Within(Epsilon), "Alpha");
            }
        }
    }
}
