using System;
using System.Collections.Generic;
using System.Linq;

namespace yield;

public static class MovingMaxTask
{
	public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
	{
		var list = new LinkedList<(double y, int index)>();
		int index = 0;

		foreach (DataPoint point in data)
		{
			while (list.Count > 0 && list.Last.Value.y < point.OriginalY)
				list.RemoveLast();

			list.AddLast((point.OriginalY, index));

			if (list.First.Value.index <= index - windowWidth)
				list.RemoveFirst();

			var smoothedY = list.First.Value.y;
			yield return point.WithMaxY(smoothedY);

			index++;
		}
	}
}
