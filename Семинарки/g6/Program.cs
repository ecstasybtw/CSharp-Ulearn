using System;
using System.Collections.Generic;

//Найти путь перекатывания кубика в заданную точку так, чтобы он оказался на той же грани, что и в начале.

using System;
using System.Collections.Generic;

class Program
{
    class Cube
    {
        public int Top, Bottom, Front, Back, Left, Right;

        public Cube(int top, int bottom, int front, int back, int left, int right)
        {
            Top = top;
            Bottom = bottom;
            Front = front;
            Back = back;
            Left = left;
            Right = right;
        }

        public Cube(Cube other)
        {
            Top = other.Top;
            Bottom = other.Bottom;
            Front = other.Front;
            Back = other.Back;
            Left = other.Left;
            Right = other.Right;
        }

        public Cube Roll(int deltaX, int deltaY)
        {
            if (deltaX == 1) // вперёд
                return new Cube(Back, Front, Top, Bottom, Left, Right);
            else if (deltaX == -1) // назад
                return new Cube(Front, Back, Bottom, Top, Left, Right);
            else if (deltaY == 1) // вправо
                return new Cube(Left, Right, Front, Back, Bottom, Top);
            else if (deltaY == -1) // влево
                return new Cube(Right, Left, Front, Back, Top, Bottom);

            return new Cube(this);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Cube other) return false;
            return Top == other.Top && Bottom == other.Bottom &&
                   Front == other.Front && Back == other.Back &&
                   Left == other.Left && Right == other.Right;
        }

        public override int GetHashCode()
        {
            return Top * 100000 + Bottom * 10000 + Front * 1000 + Back * 100 + Left * 10 + Right;
        }
    }

    class Node
    {
        public int X, Y;
        public Cube Orientation;
        public string Path;

        public Node(int x, int y, Cube orientation, string path)
        {
            X = x;
            Y = y;
            Orientation = orientation;
            Path = path;
        }
    }

    static int Rows, Columns;
    public static char[,]? Maze;
    static int StartX, StartY, TargetX, TargetY;

    static readonly int[] MoveX = { -1, 1, 0, 0 };             // назад, вперёд, влево, вправо
    static readonly int[] MoveY = { 0, 0, -1, 1 };
    static readonly char[] DirectionLetters = { 'B', 'F', 'L', 'R' };

    static void Main()
    {
        string[] rawMaze = new string[]
        {
            "S...",
            ".#..",
            "..#.",
            "...T"
        };

        Rows = rawMaze.Length;
        Columns = rawMaze[0].Length;
        Maze = new char[Rows, Columns];

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Maze[i, j] = rawMaze[i][j];
                if (Maze[i, j] == 'S')
                {
                    StartX = i;
                    StartY = j;
                }
                else if (Maze[i, j] == 'T')
                {
                    TargetX = i;
                    TargetY = j;
                }
            }
        }

        RunBFS();
    }

    static void RunBFS()
    {
        Cube initialOrientation = new Cube(1, 6, 2, 5, 3, 4);
        var startNode = new Node(StartX, StartY, initialOrientation, "");

        var visited = new HashSet<string>();
        var queue = new Queue<Node>();
        queue.Enqueue(startNode);
        visited.Add($"{StartX},{StartY},{initialOrientation.GetHashCode()}");

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.X == TargetX && current.Y == TargetY &&
                current.Orientation.Top == initialOrientation.Top)
            {
                Console.WriteLine(current.Path.Length);
                Console.WriteLine(current.Path);
                return;
            }

            for (int dir = 0; dir < 4; dir++)
            {
                int nextX = current.X + MoveX[dir];
                int nextY = current.Y + MoveY[dir];

                if (nextX >= 0 && nextX < Rows &&
                    nextY >= 0 && nextY < Columns &&
                    Maze![nextX, nextY] != '#')
                {
                    var newOrientation = current.Orientation.Roll(MoveX[dir], MoveY[dir]);
                    string stateKey = $"{nextX},{nextY},{newOrientation.GetHashCode()}";

                    if (!visited.Contains(stateKey))
                    {
                        visited.Add(stateKey);
                        queue.Enqueue(new Node(nextX, nextY, newOrientation, current.Path + DirectionLetters[dir]));
                    }
                }
            }
        }

        Console.WriteLine("-1");
    }
}
