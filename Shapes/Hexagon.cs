using System;
using System.Drawing;

namespace Paint_1
{
    class Hexagon : SimpleShape
    {
        public Hexagon(float x, float y, float lw)
            : base(x, y, lw, 1)
        {
            isFixedScale = true;
        }

        public override EShape GetShape()
        {
            return EShape.HEXAGON;
        }

        protected override Point[] GetVerticies()
        {
            Point[] v = new Point[6];

            const double ratioY = 0.25;

            float oY = (float)(Dist(controlPoints[(int)EPos.TopLeft], controlPoints[(int)EPos.BottomLeft]) * ratioY);

            v[0] = (controlPoints[(int)EPos.TopLeft] + controlPoints[(int)EPos.TopRight]) / 2;

            v[1] = controlPoints[(int)EPos.TopRight].Copy();
            v[1].Y += oY;

            v[2] = controlPoints[(int)EPos.BottomRight].Copy();
            v[2].Y -= oY;

            v[3] = (controlPoints[(int)EPos.BottomLeft] + controlPoints[(int)EPos.BottomRight]) / 2;

            v[4] = controlPoints[(int)EPos.BottomLeft].Copy();
            v[4].Y -= oY;

            v[5] = controlPoints[(int)EPos.TopLeft].Copy();
            v[5].Y += oY;

            return v;
        }
    }
}
