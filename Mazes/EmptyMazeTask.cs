namespace Mazes;

public class EmptyMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        int TargetX = width - 2;
		int TargetY = height - 2;

		MoveInDirection(robot, TargetX, Direction.Right);
		MoveInDirection(robot, TargetY, Direction.Down);
    }

	public static void MoveInDirection(Robot robot, int steps, Direction direction)
	{
		while(!robot.Finished && (direction == Direction.Right ? robot.X < steps : robot.Y < steps))
			robot.MoveTo(direction);
	}
}