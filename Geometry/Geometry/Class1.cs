namespace Geometry
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector other)
        {
            return Geometry.Add(this, other);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }

    public static class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector firstVector, Vector secondVector)
        {
            return new Vector
            {
                X = firstVector.X + secondVector.X,
                Y = firstVector.Y + secondVector.Y
            };
        }

        public static double GetLength(Segment segment)
        {
            return Math.Sqrt(
                Math.Pow(segment.End.X - segment.Begin.X, 2) +
                Math.Pow(segment.End.Y - segment.Begin.Y, 2));
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var segmentLength = GetLength(segment);
            var beginToVectorLength = Math.Sqrt(
                Math.Pow(vector.X - segment.Begin.X, 2) +
                Math.Pow(vector.Y - segment.Begin.Y, 2));
            var vectorToEndLength = Math.Sqrt(
                Math.Pow(segment.End.X - vector.X, 2) +
                Math.Pow(segment.End.Y - vector.Y, 2));

            return beginToVectorLength + vectorToEndLength == segmentLength;
        }
    }
}