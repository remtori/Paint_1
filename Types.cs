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
    }

    class Color
    {
        public int R, G, B;
        public Color(int r, int g, int b)
        {
            R = r; G = g; B = b;
        }

        public void Apply(SharpGL.OpenGL gl)
        {
            gl.Color(R / 255.0f, G / 255.0f, B / 255.0f, 1.0f);
        }
    }

    enum EShape
    {
        LINE, TRIANGLE, RECTANGLE,
        ELLIPSE, CIRCLE,
        PENTAGON, HEXAGON, POLYGON
    }

    enum ETool
    {

    }

    public enum ECursors
    {
        SizeNWSE, SizeNESW, SizeWE, SizeNS, SizeAll,
        RotateN, RotateE, RotateS, RotateW, RotateNW, RotateNE, RotateSW, RotateSE
    }
}
