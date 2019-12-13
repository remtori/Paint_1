using System;
using System.Drawing;

namespace Paint_1
{
    class Hexagon : Circle
    {
        public Hexagon(float x, float y, float lw)
            : base(x, y, lw)
        {
            vertexCount = 6;
            angleOffset = -Math.PI / 2;
        }

        public override EShape GetShape()
        {
            return EShape.HEXAGON;
        }
    }
}
