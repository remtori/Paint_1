using System.Drawing;
using System.Windows.Forms;

namespace Paint_1
{
    class Line : Shape
    {
        public Line(float x, float y, float lW)
            : base(x, y, lW, 0)
        {
            controlPoints[0] = new Point(0, 0);
            controlPoints[1] = new Point(0, 0);
            ReCalcVerticies();

            SelectCP(1);
        }

        public override EShape GetShape()
        {
            return EShape.LINE;
        }

        protected override void ReCalcVerticies()
        {
            // Control point và verticies là cùng 1 đỉnh nên k cần tính lại
            if (!isInitialDraw) return;

            verticies.Clear();
            verticies.Add(controlPoints[0]);
            verticies.Add(controlPoints[1]);
        }

        public override bool IsInBoundingBox(float x, float y)
        {
            return IsCollideWith(x, y);
        }

        public override bool OnMouseMove(MouseEventArgs e)
        {
            if (selectedCP == (int) EPos.None) return false;

            if (selectedCP < 0) return base.OnMouseMove(e);
            
            controlPoints[selectedCP].X = stored.X + (e.X - mouseDown.X);
            controlPoints[selectedCP].Y = stored.Y + (e.Y - mouseDown.Y);
            return true;
        }
    }
}
