using System;
using System.Windows.Forms;

namespace Paint_1
{
    class Line : Shape
    {
        public Line(float x, float y, Color c, float lW)
            : base(x, y, c, lW, false)
        {
            controlPoints[0] = new Point(x, y);
            controlPoints[1] = new Point(x, y);

            SelectCP(1);
        }

        public override void DrawShape()
        {
            DrawLine(controlPoints[0], controlPoints[1]);
        }

        public override EShape GetShape()
        {
            return EShape.LINE;
        }

        public override bool IsCollideWith(float x, float y)
        {
            return IsCollideWithLine(new Point(x, y), controlPoints[0], controlPoints[1]);
        }

        public override bool IsInBoundingBox(float x, float y)
        {
            return IsCollideWith(x, y);
        }

        public override bool OnMouseMove(MouseEventArgs e)
        {
            if (selectedCP == -1) return false;

            if (selectedCP == -2) return base.OnMouseMove(e);
            
            controlPoints[selectedCP].X = storedX + (e.X - mouseDownX);
            controlPoints[selectedCP].Y = storedY + (e.Y - mouseDownY);
            return true;
        }
    }
}
