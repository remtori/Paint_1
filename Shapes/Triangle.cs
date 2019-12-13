using System;

namespace Paint_1
{
    class Triangle : Circle
    {
        public Triangle(float x, float y, float lw)
            : base(x, y, lw)
        {
            vertexCount = 3;
            angleOffset = -Math.PI / 2;
        }

        public override EShape GetShape()
        {
            return EShape.TRIANGLE;
        }
    }
}
