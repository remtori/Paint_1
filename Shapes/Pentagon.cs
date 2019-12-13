using System;
using System.Drawing;

namespace Paint_1
{
    class Pentagon : SimpleShape
    {
        public Pentagon(float x, float y, float lw)
            : base(x, y, lw, 1)
        {
            isFixedScale = true;
        }

        public override EShape GetShape()
        {
            return EShape.PENTAGON;
        }

        protected override Point[] GetVerticies()
        {
            Point[] v = new Point[5];

            const double ratioX = 0.20610737385376343; // (1 - sin(36)) / 2
            const double ratioY = 0.34549150281252633; // sin(72) / tan(54) / 2

            float oX = (float) (Dist(controlPoints[(int)EPos.TopLeft], controlPoints[(int)EPos.TopRight]) * ratioX);
            float oY = (float) (Dist(controlPoints[(int)EPos.TopLeft], controlPoints[(int)EPos.BottomLeft]) * ratioY);

            v[0] = (controlPoints[(int)EPos.TopLeft] + controlPoints[(int)EPos.TopRight]) / 2;

            v[1] = controlPoints[(int)EPos.TopRight].Copy();
            v[1].Y += oY;

            v[2] = controlPoints[(int)EPos.BottomRight].Copy();
            v[2].X -= oX;

            v[3] = controlPoints[(int)EPos.BottomLeft].Copy();
            v[3].X += oX;

            v[4] = controlPoints[(int)EPos.TopLeft].Copy();
            v[4].Y += oY;

            return v;
        }

    }
}