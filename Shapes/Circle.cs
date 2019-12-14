using System;
using System.Drawing;

namespace Paint_1
{
    class Circle : Shape
    {        
        public Circle(float x, float y, float lw)
                : base(x, y, lw, 1)
        {
            isFixedScale = true;            
        }

        public override EShape GetShape()
        {
            return EShape.CIRCLE;
        }

        public override bool IsCollideWith(float x, float y)
        {
            Point m = ScreenToWorld(new Point(x, y));
            double R = Dist(controlPoints[(int)EPos.TopLeft], controlPoints[(int)EPos.BottomLeft]) / 2;
            return Dist(m, controlPoints[(int)EPos.Center]) < (R + 4);
        }
    }
}
