using System;
using System.Drawing;

namespace Paint_1
{
    class Pentagon : Circle
    {
        public Pentagon(float x, float y, float lw)
            : base(x, y, lw)
        {
            vertexCount = 5;
            angleOffset = -Math.PI / 10;
        }

        public override EShape GetShape()
        {
            return EShape.PENTAGON;
        }
    }
}