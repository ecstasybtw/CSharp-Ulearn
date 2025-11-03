using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot;

public partial class Bot
{
	public Rocket GetNextMove(Rocket rocket)
	{
		int taskCount = Environment.ProcessorCount;

		var bestScore = double.NegativeInfinity;
		Turn bestTurn = default;

		var locker = new object();

		Parallel.For(0, taskCount, i =>
		{
			var localRandom = new Random(random.Next());
			var (turn, score) = SearchBestMove(rocket, localRandom, iterationsCount);

			lock (locker)
			{
				if (score > bestScore)
				{
					bestScore = score;
					bestTurn = turn;
				}
			}
		});

		return rocket.Move(bestTurn, level);
	}


	public List<Task<(Turn Turn, double Score)>> CreateTasks(Rocket rocket)
	{
		return new() { Task.Run(() => SearchBestMove(rocket, new Random(random.Next()), iterationsCount)) };
	}
}
