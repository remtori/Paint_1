using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static Point operator+ (Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator/ (Point p, float d)
        {
            return new Point(p.X / d, p.Y / d);
        }
    }

    class Color
    {
        public int R, G, B;
        public void Apply(SharpGL.OpenGL gl)
        {
            gl.Color(R, G, B);
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
}
