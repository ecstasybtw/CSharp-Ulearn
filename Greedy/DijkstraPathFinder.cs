using System;
using System.Collections.Generic;
using Greedy.Architecture;
using System.Linq;

namespace Greedy;

public class DijkstraData {
    public Point? Previous {get; set;}
    public int Value {get; set;}
}

public class DijkstraPathFinder
{
	public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
		IEnumerable<Point> targets)
	{
		var toOpen = new PriorityQueue<Point, int>();
        toOpen.Enqueue(start, 0);
        var chests = new HashSet<Point>(targets);
        var visitedPoints = new HashSet<Point>();
        var track = new Dictionary<Point, DijkstraData>();
        track[start] = new DijkstraData {Previous = null, Value = 0};
        while (toOpen.Count > 0)
        {
            var pointToOpen = toOpen.Dequeue();
            if (visitedPoints.Contains(pointToOpen))
                continue;
            if (chests.Contains((Point)pointToOpen))
                yield return RestorePath(track, (Point)pointToOpen);
            foreach (var neighbour in GetNeighbors((Point)pointToOpen, state)) {
                var currentValue = track[pointToOpen].Value + state.CellCost[neighbour.X, neighbour.Y];
                if (!track.ContainsKey(neighbour) || track[neighbour].Value > currentValue) {
                    track[neighbour] = new DijkstraData {Previous = pointToOpen, Value = currentValue};
                    toOpen.Enqueue(neighbour, currentValue);
                }
            }
            visitedPoints.Add((Point)pointToOpen);
        }
    }

public List<Point> GetNeighbors(Point p, State state)
{
    return new List<Point>
    {
        new Point(p.X + 1, p.Y),
        new Point(p.X - 1, p.Y),
        new Point(p.X, p.Y + 1),
        new Point(p.X, p.Y - 1),
    }
    .Where(neighbor => state.InsideMap(neighbor) && !state.IsWallAt(neighbor)).ToList();
}

public static PathWithCost RestorePath(Dictionary<Point, DijkstraData> track, Point end)
{
	var result = new List<Point>();
	Point? currentPoint = end;
	while (currentPoint != null)
    {
		result.Add(currentPoint.Value);
		currentPoint = track[(Point)currentPoint].Previous;
	}
	result.Reverse();
	PathWithCost pathResult = new PathWithCost(track[end].Value, result.ToArray());
	return pathResult;
}
}
