using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Paint_1
{
    class Polygon : Shape
    {
        protected List<Point> verticies;
        protected Point initSize;

        protected Mat2 scaleMat;

        public Polygon(float x, float y, float lw)
               : base(x, y, lw, 0)
        {
            controlPoints[(int)EPos.Center].Set(0, 0);
            controlPoints[(int)EPos.PositionOffset].Set(0, 0);

            scaleMat = new Mat2();
            initSize = new Point(0, 0);

            verticies = new List<Point>();
            verticies.Add(new Point(x, y));
            verticies.Add(new Point(x, y));
        }

        public override EShape GetShape()
        {
            return EShape.POLYGON;
        }

        public override void DrawShape()
        {
            for (int i = 1; i < verticies.Count; i++)
                DrawScaledLine(verticies[i - 1], verticies[i]);

            if (!isInitialDraw)
                DrawScaledLine(verticies[0], verticies[verticies.Count - 1]);
        }

        private void DrawScaledLine(Point a, Point b)
        {
            Point offset = controlPoints[(int)EPos.Center] + controlPoints[(int)EPos.PositionOffset];
            a = a - offset;
            a = a * scaleMat;
            a = a + offset;

            b = b - offset;
            b = b * scaleMat;
            b = b + offset;

            DrawLine(a, b);
        }

        public override bool IsCollideWith(float x, float y)
        {
            Point m = ScreenToWorld(new Point(x, y)) * scaleMat.GetInverse();

            for (int i = 1; i < verticies.Count; i++)
                if (IsCollideWithLine(m, verticies[i - 1], verticies[i]))
                    return true;

            return IsCollideWithLine(m, verticies[0], verticies[verticies.Count - 1]);
        }

        public override bool OnMouseMove(MouseEventArgs e)
        {
            if (!isInitialDraw)
            {
                base.OnMouseMove(e);

                Point curSize = maxCoord - minCoord;
                scaleMat.values[0] = curSize.X / initSize.X;
                scaleMat.values[3] = curSize.Y / initSize.Y;

                return true;
            }

            verticies[verticies.Count - 1].X = e.X;
            verticies[verticies.Count - 1].Y = e.Y;

            return true;
        }

        public override bool OnMouseUp(MouseEventArgs e)
        {
            if (isInitialDraw) return true;

            return base.OnMouseUp(e);
        }

        public override bool OnMouseDown(MouseEventArgs e)
        {
            if (!isInitialDraw) return base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                verticies.Add(new Point(e.X, e.Y));
            }
            else if (e.Button == MouseButtons.Right)
            {
                isInitialDraw = false;
                showControl = true;

                foreach(Point p in verticies)
                {
                    minCoord.X = Math.Min(p.X, minCoord.X);
                    minCoord.Y = Math.Min(p.Y, minCoord.Y);
                    maxCoord.X = Math.Max(p.X, maxCoord.X);
                    maxCoord.Y = Math.Max(p.Y, maxCoord.Y);
                }

                controlPoints[(int)EPos.TopLeft] = new Point(minCoord.X, minCoord.Y);
                controlPoints[(int)EPos.TopRight] = new Point(maxCoord.X, minCoord.Y);
                controlPoints[(int)EPos.BottomLeft] = new Point(minCoord.X, maxCoord.Y);
                controlPoints[(int)EPos.BottomRight] = new Point(maxCoord.X, maxCoord.Y);

                controlPoints[(int)EPos.Top] = (controlPoints[(int)EPos.TopLeft] + controlPoints[(int)EPos.TopRight]) / 2;
                controlPoints[(int)EPos.Left] = (controlPoints[(int)EPos.TopLeft] + controlPoints[(int)EPos.BottomLeft]) / 2;
                controlPoints[(int)EPos.Right] = (controlPoints[(int)EPos.TopRight] + controlPoints[(int)EPos.BottomRight]) / 2;
                controlPoints[(int)EPos.Bottom] = (controlPoints[(int)EPos.BottomLeft] + controlPoints[(int)EPos.BottomRight]) / 2;

                controlPoints[(int)EPos.Center] = (controlPoints[(int)EPos.TopLeft] + controlPoints[(int)EPos.BottomRight]) / 2;

                controlPoints[(int)EPos.RotationOffset] = new Point(50, 0);
                controlPoints[(int)EPos.Rotation] = controlPoints[(int)EPos.Center] + controlPoints[(int)EPos.RotationOffset];

                initSize = maxCoord - minCoord;
            }

            return true;
        }
    }
}
