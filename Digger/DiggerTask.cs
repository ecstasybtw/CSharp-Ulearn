using System;
using Digger.Architecture;
using Avalonia.Input;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName() => "Terrain.png";
        public CreatureCommand Act(int x, int y) => new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        public bool DeadInConflict(ICreature conflictedObject) => true;
        public int GetDrawingPriority() => 1;
    }

    public class Player : ICreature
    {
        public string GetImageFileName() => "Digger.png";
        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand next = new CreatureCommand() { DeltaX = 0, DeltaY = 0 };

            if (Game.KeyPressed == Key.Up && y - 1 >= 0 && !(Game.Map[x, y - 1] is Sack))
                next = new CreatureCommand() { DeltaX = 0, DeltaY = -1 };
            else if (Game.KeyPressed == Key.Right && x + 1 < Game.MapWidth && !(Game.Map[x + 1, y] is Sack))
                next = new CreatureCommand() { DeltaX = 1, DeltaY = 0 };
            else if (Game.KeyPressed == Key.Down && y + 1 < Game.MapHeight && !(Game.Map[x, y + 1] is Sack))
                next = new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
            else if (Game.KeyPressed == Key.Left && x - 1 >= 0 && !(Game.Map[x - 1, y] is Sack))
                next = new CreatureCommand() { DeltaX = -1, DeltaY = 0 };

            if (Game.Map[x + next.DeltaX, y + next.DeltaY] is Gold)
            {
                Game.Map[x + next.DeltaX, y + next.DeltaY] = null;
                Game.Scores += 10;
            }

            return next;
        }

        public bool DeadInConflict(ICreature conflictedObject) => conflictedObject is Sack || conflictedObject is Monster;
        public int GetDrawingPriority() => 0;
    }

    public class Sack : ICreature
    {
        public string GetImageFileName() => "Sack.png";
        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                var map = Game.Map[x, y + 1];
                if (map == null || map.ToString() == "Digger.Player")
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
            }
            if (Game.Map[x, y + 1] is Sack && Game.Map[x, y + 2] == null)
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject) => false;
        public int GetDrawingPriority() => 1;
    }

    public class Gold : ICreature
    {
        public string GetImageFileName() => "Gold.png";
        public CreatureCommand Act(int x, int y) => new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        public bool DeadInConflict(ICreature conflictedObject) => true;
        public int GetDrawingPriority() => 1;
    }

    public class Point
    {
        public int X, Y;
        public Point(int x, int y) => (X, Y) = (x, y);
    }

    public class Monster : ICreature
    {
        public string GetImageFileName() => "Monster.png";
        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand next = new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
            var posPlayer = GetPlayerPos();
            if (posPlayer.X > -1)
            {
                if (posPlayer.X == x)
                    next.DeltaY = posPlayer.Y < y ? -1 : 1;
                else if (posPlayer.Y == y)
                    next.DeltaX = posPlayer.X < x ? -1 : 1;
                else
                    next.DeltaX = posPlayer.X < x ? -1 : 1;
            }
            if (x + next.DeltaX >= 0 && x + next.DeltaX < Game.MapWidth && y + next.DeltaY >= 0 && y + next.DeltaY < Game.MapHeight)
            {
                var nextCell = Game.Map[x + next.DeltaX, y + next.DeltaY];
                if (nextCell is Terrain || nextCell is Monster || nextCell is Sack)
                {
                    next.DeltaX = 0;
                    next.DeltaY = 0;
                }
            }
            return next;
        }
        public bool DeadInConflict(ICreature conflictedObject) => conflictedObject is Monster || conflictedObject is Sack;
        public int GetDrawingPriority() => 1;
        public Point GetPlayerPos()
        {
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                {
                    var map = Game.Map[i, j];
                    if (map != null && map.ToString() == "Digger.Player")
                        return new Point(i, j);
                }
            return new Point(-1, -1);
        }
    }
}
