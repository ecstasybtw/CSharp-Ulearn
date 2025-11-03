using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy;

public class GreedyPathFinder : IPathFinder
{
	public List<Point> FindPathToCompleteGoal(State state)
	{
		var chests = new HashSet<Point>(state.Chests);
        var result = new List<Point>();
        var pathFinder = new DijkstraPathFinder();

        var startPosition = state.Position;
        var cost = 0;

        if (state.Goal == 0)
            return new List<Point>();

        for (int i = 0; i < state.Goal; i++) {
            if (!chests.Any())
                return new List<Point>();
            var pathToChest = pathFinder.GetPathsByDijkstra(state, startPosition, chests).FirstOrDefault();
            if (pathToChest == null)
                return new List<Point>();
            startPosition = pathToChest.End;
            cost += pathToChest.Cost;
            chests.Remove(pathToChest.End);
            if (cost > state.Energy)
					return new List<Point>();
            for (int j = 1; j < pathToChest.Path.Count; j++)
					result.Add(pathToChest.Path[j]);
        }
        return result;
	}
}
