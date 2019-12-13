using System.Drawing;

namespace Paint_1
{
    class Triangle : SimpleShape
    {
        public Triangle(float x, float y, float lw)
            : base(x, y, lw, 1)
        {
            isFixedScale = true;
        }

        public override EShape GetShape()
        {
            return EShape.TRIANGLE;
        }

        protected override Point[] GetVerticies()
        {
            Point p = (controlPoints[(int)EPos.TopLeft] + controlPoints[(int)EPos.TopRight]) / 2;

            return new Point[] {
                p,
                controlPoints[(int)EPos.BottomLeft],
                controlPoints[(int)EPos.BottomRight]
            };
        }        
    }
}
