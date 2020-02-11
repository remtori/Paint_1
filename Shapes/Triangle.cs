using System;

namespace Paint_1
{
    class Triangle : Shape
    {
        public Triangle(float x, float y, float lw)
            : base(x, y, lw, 1)
        {
            isFixedScale = true;
            vertexCount = 3;
            angleOffset = -Math.PI / 2;
        }

        public override EShape GetShape()
        {
            return EShape.TRIANGLE;
        }
    }
}
