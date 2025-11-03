namespace ReadOnlyVector
{
    public class ReadOnlyVector
    {
        public readonly double X;
        public readonly double Y;

        public ReadOnlyVector(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public ReadOnlyVector Add(ReadOnlyVector other)
        {
            return new ReadOnlyVector(this.X + other.X, this.Y + other.Y);
        }

        public ReadOnlyVector WithX(double x)
        {
            return new ReadOnlyVector(x, this.Y);
        }

        public ReadOnlyVector WithY(double y)
        {
            return new ReadOnlyVector(this.X, y);
        }
    }
}