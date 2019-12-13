using System;
using System.Drawing;

namespace Paint_1
{
    class Ellipse : SimpleShape
    {
        public Ellipse(float x, float y, float lw)
                : base(x, y, lw)
        {            
        }

        public override EShape GetShape()
        {
            return EShape.ELLIPSE;
        }

        protected override Point[] GetVerticies()
        {
            Point[] v = new Point[MAX_VERTEX_COUNT];

            Point center = controlPoints[(int)EPos.Center];
            double rX = Dist(controlPoints[(int)EPos.Left], controlPoints[(int)EPos.Right]) / 2;
            double rY = Dist(controlPoints[(int)EPos.Top], controlPoints[(int)EPos.Bottom]) / 2;

            for (int i = 0; i < MAX_VERTEX_COUNT; i++)
            {
                double angle = 2 * Math.PI * i / MAX_VERTEX_COUNT;
                v[i] = new Point(
                    (float)(rX * Math.Cos(angle) + center.X),
                    (float)(rY * Math.Sin(angle) + center.Y)
                );
            }

            return v;
        }

        public override bool IsCollideWith(float x, float y)
        {
            Point m = ScreenToWorld(new Point(x, y)) - controlPoints[(int) EPos.Center];
            double rX = Dist(controlPoints[(int)EPos.Left], controlPoints[(int)EPos.Right]) / 2;
            double rY = Dist(controlPoints[(int)EPos.Top], controlPoints[(int)EPos.Bottom]) / 2;

            m.Y *= (float) (rX / rY);

            return (m.X * m.X ) + (m.Y * m.Y) < (rX * rX + 16.0);
        }
    }
}
