namespace Mazes;

public static class SnakeMazeTask
{
    public static void MoveOut(Robot robot, int width, int height)
    {
        while(!robot.Finished)
        {
            MoveInDirection(robot, Direction.Right, width - 2);
            MoveInDirection(robot, Direction.Down, 2);
            MoveInDirection(robot, Direction.Left, width - 2);
            if (!robot.Finished)
                MoveInDirection(robot, Direction.Down, 2);
        }
    }
        public static void MoveInDirection(Robot robot, Direction direction, int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            robot.MoveTo(direction);
            if (robot.Finished) return; 
        }
    }
}