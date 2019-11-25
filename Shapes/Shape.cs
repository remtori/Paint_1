using SharpGL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Paint_1
{
    abstract class Shape
    {
        static readonly float CPR = 10.0f; // Control Point Radius

        protected Point offset;
        protected float rotation;

        protected Color color;
        protected float lineWidth;

        protected bool isInitialDraw;

        protected int selectedCP;
        protected bool useDefaultCP;
        protected Dictionary<int, Point> controlPoints;

        protected float storedX, storedY;
        protected static float mouseDownX, mouseDownY;

        public static Shape CreateShape(EShape shape, float x, float y, Color c, float lW)
        {
            switch (shape)
            {
                case EShape.LINE:
                    return new Line(x, y, c, lW);
                case EShape.TRIANGLE:
                    return new Triangle(x, y, c, lW);

                default:
                    throw new Exception("Unknown Shape!");
            }
        }

        public Shape(float x, float y, Color c, float lW, bool defaultControlPoint = true)
        {
            offset = new Point(0.0f, 0.0f);
            rotation = 0.0f;

            color = c;
            lineWidth = lW;

            isInitialDraw = true;

            selectedCP = -1;
            controlPoints = new Dictionary<int, Point>();
            useDefaultCP = defaultControlPoint;
            if (!defaultControlPoint) return;

            controlPoints[0 * 3 + 0] = new Point(x, y);
            controlPoints[0 * 3 + 1] = new Point(x, y);
            controlPoints[0 * 3 + 2] = new Point(x, y);
            controlPoints[1 * 3 + 0] = new Point(x, y);
            controlPoints[1 * 3 + 2] = new Point(x, y);
            controlPoints[2 * 3 + 0] = new Point(x, y);
            controlPoints[2 * 3 + 1] = new Point(x, y);
            controlPoints[2 * 3 + 2] = new Point(x, y);

            // Center of rotation
            controlPoints[-1] = new Point(x, y);

            SelectCP(8);
        }

        public void SetColor(Color c)
        {
            color = c;
        }

        public void SetLineWidth(float lw)
        {
            lineWidth = lw;
        }

        public abstract EShape GetShape();

        // Kiểm tra xem điểm (x, y) có thuộc hình đã được vẽ
        // Dùng để "chọn" các hình
        public abstract bool IsCollideWith(float x, float y);

        // Kiểm tra xem điểm (x, y) có thuộc hình chữ nhật được tạo nên bởi các điểm điều khiển.
        // Dùng để "di chuyển" các hình
        public virtual bool IsInBoundingBox(float x, float y)
        {
            if (!useDefaultCP) throw new Exception("Custom Control Point must overide this method.");

            Point m = new Point(x, y);
            Point topLeftCP = controlPoints[0];
            Point bottomRightCP = controlPoints[0];

            foreach (KeyValuePair<int, Point> kv in controlPoints)
            {
                if (topLeftCP.X > kv.Value.X || topLeftCP.Y > kv.Value.Y)
                    topLeftCP = kv.Value;

                if (bottomRightCP.X < kv.Value.X || bottomRightCP.Y < kv.Value.Y)
                    bottomRightCP = kv.Value;
            }

            return topLeftCP.X < m.X && m.X < bottomRightCP.X &&
                topLeftCP.Y < m.Y && m.Y < bottomRightCP.Y;
        }

        public abstract void DrawShape();

        public void DrawControlPoint()
        {
            OpenGL gl = Paint.INSTANCE.gl;

            gl.PolygonMode(OpenGL.GL_FRONT, OpenGL.GL_FILL);
            gl.Color(0.0f, 1.0f, 0.0f, 1.0f);

            foreach (KeyValuePair<int, Point> kv in controlPoints)
            {
                Point p = kv.Value + offset;
                gl.Begin(OpenGL.GL_QUADS);
                gl.Vertex(p.X - CPR / 2, p.Y - CPR / 2);
                gl.Vertex(p.X + CPR / 2, p.Y - CPR / 2);
                gl.Vertex(p.X + CPR / 2, p.Y + CPR / 2);
                gl.Vertex(p.X - CPR / 2, p.Y + CPR / 2);
                gl.End();
            }
        }

        protected bool IsCollideWithLine(Point m, Point pS, Point pE)
        {
            double a = Dist(m, pS);
            double b = Dist(m, pE);
            double c = Dist(pS, pE);
            double p = (a + b + c) / 2;

            double h = (2.0f / c) * Math.Sqrt(p * (p - a) * (p - b) * (p - c));

            return h < (lineWidth / 2.0f) + 4.0f;
        }

        protected void DrawLine(Point a, Point b)
        {
            OpenGL gl = Paint.INSTANCE.gl;

            a = a + offset;
            b = b + offset;

            // Lấy vector đơn vị của a -> b
            double vx = b.X - a.X;
            double vy = b.Y - a.Y;
            double d = Dist(a, b);
            vx /= d;
            vy /= d;

            // Xoay 90
            double t = vx;
            vx = -vy;
            vy = t;

            // Tăng độ lớn theo độ dày của đường
            vx *= lineWidth / 2.0;
            vy *= lineWidth / 2.0;

            gl.PolygonMode(OpenGL.GL_FRONT, OpenGL.GL_FILL);
            color.Apply(gl);

            gl.Begin(OpenGL.GL_QUADS);
            gl.Vertex(a.X - vx, a.Y - vy);
            gl.Vertex(a.X + vx, a.Y + vy);
            gl.Vertex(b.X + vx, b.Y + vy);
            gl.Vertex(b.X - vx, b.Y - vy);
            gl.End();
        }

        public virtual bool OnMouseMove(MouseEventArgs e)
        {
            if (selectedCP == -1) return false;

            if (selectedCP == -2)
            {
                offset.X = storedX + (e.X - mouseDownX);
                offset.Y = storedY + (e.Y - mouseDownY);
                return true;
            }

            // Update control point position
            Point p = controlPoints[selectedCP];
            int cRow = selectedCP / 3;
            int cCol = selectedCP % 3;

            p.X = storedX + (e.X - mouseDownX);
            p.Y = storedY + (e.Y - mouseDownY);

            // Update all other control point
            foreach (KeyValuePair<int, Point> kv in controlPoints)
            {
                if (kv.Value == p) continue;
                int row = kv.Key / 3;
                int col = kv.Key % 3;

                if (row == cRow)
                    kv.Value.Y = p.Y;

                if (col == cCol)
                    kv.Value.X = p.X;
            }

            controlPoints[0 * 3 + 1] = (controlPoints[0 * 3 + 0] + controlPoints[0 * 3 + 2]) / 2;
            controlPoints[2 * 3 + 1] = (controlPoints[2 * 3 + 0] + controlPoints[2 * 3 + 2]) / 2;
            controlPoints[1 * 3 + 0] = (controlPoints[0 * 3 + 0] + controlPoints[2 * 3 + 0]) / 2;
            controlPoints[1 * 3 + 2] = (controlPoints[0 * 3 + 2] + controlPoints[2 * 3 + 2]) / 2;

            return true;
        }

        public virtual bool OnMouseUp(MouseEventArgs e)
        {
            selectedCP = -1;
            isInitialDraw = false;

            foreach (KeyValuePair<int, Point> kv in controlPoints)
            {
                kv.Value.X = kv.Value.X + offset.X;
                kv.Value.Y = kv.Value.Y + offset.Y;
            }

            offset.X = 0;
            offset.Y = 0;

            return true;
        }

        public virtual bool OnMouseDown(MouseEventArgs e)
        {
            mouseDownX = e.X;
            mouseDownY = e.Y;

            if (selectedCP >= 0) return true;

            foreach (KeyValuePair<int, Point> kv in controlPoints)
            {
                if (Dist(e.X, e.Y, kv.Value.X, kv.Value.Y) < CPR)
                {
                    SelectCP(kv.Key);
                    return true;
                }
            }

            if (IsCollideWith(e.X, e.Y))
            {
                selectedCP = -2;
                storedX = offset.X;
                storedY = offset.Y;
                return true;
            }

            return false;
        }

        protected void SelectCP(int index)
        {
            selectedCP = index;
            storedX = controlPoints[index].X;
            storedY = controlPoints[index].Y;
        }

        protected double Dist(float x1, float y1, float x2, float y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        protected double Dist(Point a, Point b)
        {
            return Dist(a.X, a.Y, b.X, b.Y);
        }
    }
}
