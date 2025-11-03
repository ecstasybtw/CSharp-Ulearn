using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
	public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
	{
		var queue = new Queue<DataPoint>();
		double sumOfY = 0;
		foreach (DataPoint point in data)
		{
			queue.Enqueue(point);
			sumOfY += point.OriginalY;
			if (queue.Count > windowWidth)
				sumOfY -= queue.Dequeue().OriginalY;
			var smoothedY = sumOfY / queue.Count;
			yield return point.WithAvgSmoothedY(smoothedY);
		}
	}
}