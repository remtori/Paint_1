using System;
using System.Drawing;

namespace Paint_1
{
    class Circle : SimpleShape
    {
        protected int vertexCount;
        protected double angleOffset;

        public Circle(float x, float y, float lw)
                : base(x, y, lw, 1)
        {
            isFixedScale = true;
            angleOffset = 0;
            vertexCount = MAX_VERTEX_COUNT;
        }

        public override EShape GetShape()
        {
            return EShape.CIRCLE;
        }

        protected override Point[] GetVerticies()
        {
            Point[] v = new Point[vertexCount];

            Point center = controlPoints[(int)EPos.Center];
            double R = Dist(controlPoints[(int)EPos.TopLeft], controlPoints[(int)EPos.BottomLeft]) / 2;

            for (int i = 0; i < vertexCount; i++)
            {
                double angle = 2 * Math.PI * i / vertexCount + angleOffset;
                v[i] = new Point(
                    (float) (R * Math.Cos(angle) + center.X),
                    (float) (R * Math.Sin(angle) + center.Y)
                );
            }

            return v;
        }

        public override bool IsCollideWith(float x, float y)
        {
            Point m = ScreenToWorld(new Point(x, y));
            double R = Dist(controlPoints[(int)EPos.TopLeft], controlPoints[(int)EPos.BottomLeft]) / 2;
            return Dist(m, controlPoints[(int)EPos.Center]) < (R + 4);
        }
    }
}
