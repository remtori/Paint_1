namespace Paint_1
{
    class Point
    {
        public float X, Y;
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Set(float x, float y)
        {
            X = x;
            Y = y;
        }

        public double Length()
        {
            return System.Math.Sqrt(X * X + Y * Y);
        }

        public Point Copy()
        {
            return new Point(X, Y);
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator *(Point a, Point b)
        {
            return new Point(a.X * b.X, a.Y * b.Y);
        }

        public static Point operator *(Point p, float d)
        {
            return new Point(p.X * d, p.Y * d);
        }

        public static Point operator /(Point p, float d)
        {
            return new Point(p.X / d, p.Y / d);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public static Point operator*(Point a, Mat2 m)
        {
            return new Point(
                a.X * m.values[0] + a.Y * m.values[2],
                a.X * m.values[1] + a.Y * m.values[3]
            );
        }
    }
}
