using System.Collections.Generic;
using System.Linq;

namespace Dungeon;

public class BfsTask
{
	public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Chest[] chests)
	{
		var track = RunBfs(map, start);
		return GetPathsToChests(track, chests);
	}

	private static Dictionary<Point, SinglyLinkedList<Point>> RunBfs(Map map, Point start)
	{
		var track = new Dictionary<Point, SinglyLinkedList<Point>> { [start] = new(start) };
		var queue = new Queue<SinglyLinkedList<Point>>();
		queue.Enqueue(track[start]);

		while (queue.Count > 0)
		{
			var point = queue.Dequeue();
			ExploreNeighbors(map, track, queue, point);
		}

		return track;
	}

	private static void ExploreNeighbors(Map map, Dictionary<Point, SinglyLinkedList<Point>> track, Queue<SinglyLinkedList<Point>> queue, SinglyLinkedList<Point> point)
	{
		foreach (var direction in Walker.PossibleDirections)
		{
			var newPoint = point.Value + direction;
			if (map.InBounds(newPoint) && map.Dungeon[newPoint.X, newPoint.Y] != MapCell.Wall && !track.ContainsKey(newPoint))
			{
				track[newPoint] = new SinglyLinkedList<Point>(newPoint, point);
				queue.Enqueue(track[newPoint]);
			}
		}
	}

	private static IEnumerable<SinglyLinkedList<Point>> GetPathsToChests(Dictionary<Point, SinglyLinkedList<Point>> track, Chest[] chests)
	{
		return chests.Where(chest => track.ContainsKey(chest.Location)).Select(chest => track[chest.Location]);
	}
}
