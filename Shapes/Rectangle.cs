using System.Drawing;

namespace Paint_1
{
    class Rectangle : SimpleShape
    {
        public Rectangle(float x, float y, float lw)
            : base(x, y, lw)
        {            
        }

        public override EShape GetShape()
        {
            return EShape.RECTANGLE;
        }

        protected override Point[] GetVerticies()
        {            
            return new Point[] {
                controlPoints[(int)EPos.TopLeft],
                controlPoints[(int)EPos.TopRight],
                controlPoints[(int)EPos.BottomRight],
                controlPoints[(int)EPos.BottomLeft]
            };
        }

    }
}
