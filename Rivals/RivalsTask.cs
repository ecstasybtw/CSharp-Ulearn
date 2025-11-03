using System.Collections.Generic;
using System.Linq;

namespace Rivals
{
    public class RivalsTask
    {
        private static readonly Point[] Directions =
        {
            new Point(0, 1),
            new Point(1, 0),
            new Point(0, -1),
            new Point(-1, 0)
        };

        public static IEnumerable<OwnedLocation> AssignOwners(Map territory)
        {
            var visited = new Dictionary<Point, OwnedLocation>();
            var queue = SetupInitialQueue(territory);
            return TraverseMap(territory, queue, visited);
        }

        private static Queue<OwnedLocation> SetupInitialQueue(Map territory)
        {
            var resultQueue = new Queue<OwnedLocation>();
            for (int index = 0; index < territory.Players.Count(); index++)
            {
                var startPoint = territory.Players[index];
                resultQueue.Enqueue(new OwnedLocation(index, startPoint, 0));
            }
            return resultQueue;
        }
		
		private static void AddAdjacentPoints(OwnedLocation basePoint, Queue<OwnedLocation> locationsToVisit, Dictionary<Point, OwnedLocation> visitedAreas)
		{
			foreach (var dir in Directions) {
				var neighbor = basePoint.Location + dir;
				if (!visitedAreas.ContainsKey(neighbor))
				{
					var newArea = new OwnedLocation(basePoint.Owner, neighbor, basePoint.Distance + 1);
					locationsToVisit.Enqueue(newArea);
				}
			}
		}

        private static IEnumerable<OwnedLocation> TraverseMap(Map territory, Queue<OwnedLocation> locationsToVisit, Dictionary<Point, OwnedLocation> visitedAreas)
        {
            while (locationsToVisit.Any()) {
                var current = locationsToVisit.Dequeue();
                if (IsInvalid(territory, current.Location, visitedAreas))
                    continue;
                visitedAreas[current.Location] = current;
                yield return current;
                if (!territory.Chests.Contains(current.Location))
                    AddAdjacentPoints(current, locationsToVisit, visitedAreas);
            }
        }

        private static bool IsInvalid(Map territory, Point coordinate, Dictionary<Point, OwnedLocation> visitedAreas)
        {
            return !territory.InBounds(coordinate) || visitedAreas.ContainsKey(coordinate) || territory.Maze[coordinate.X, coordinate.Y] == MapCell.Wall;
        }
    }
}
