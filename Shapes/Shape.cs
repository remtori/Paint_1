using SharpGL;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Paint_1
{
    abstract class Shape
    {
        public static readonly float CPR = 10.0f; // Control Point Radius
        public static readonly int MAX_VERTEX_COUNT = 90; // Số lượng đỉnh tối đa cho 1 hình

        protected Color color;
        protected Color fillColor;
        protected float lineWidth;

        protected bool isInitialDraw;
        protected bool showControl;
        protected bool isFixedScale;

        protected Dictionary<int, Point> controlPoints;
        protected int selectedCP;

        protected Mat2 rotationMat;

        protected Point stored;
        protected Point mouseDown;

        protected Point minCoord;
        protected Point maxCoord;

        public static Shape CreateShape(EShape shape, float x, float y, float lW)
        {
            switch (shape)
            {
                case EShape.LINE:
                    return new Line(x, y, lW);
                case EShape.TRIANGLE:
                    return new Triangle(x, y, lW);
                case EShape.RECTANGLE:
                    return new Rectangle(x, y, lW);
                case EShape.PENTAGON:
                    return new Pentagon(x, y, lW);
                case EShape.HEXAGON:
                    return new Hexagon(x, y, lW);
                case EShape.POLYGON:
                    return new Polygon(x, y, lW);
                case EShape.CIRCLE:
                    return new Circle(x, y, lW);
                case EShape.ELLIPSE:
                    return new Ellipse(x, y, lW);
                default:
                    throw new Exception("Unknown Shape!");
            }
        }

        public Shape(float x, float y, float lW, int controlLevel = 2)
        {
            color = Paint.INSTANCE.GetForegroundColor();
            fillColor = Paint.INSTANCE.GetBackgroundColor();
            lineWidth = lW;

            isInitialDraw = true;
            showControl = false;
            isFixedScale = false;

            selectedCP = (int)EPos.None;

            rotationMat = new Mat2();

            minCoord = new Point(float.MaxValue, float.MaxValue);
            maxCoord = new Point(float.MinValue, float.MinValue);

            controlPoints = new Dictionary<int, Point>();

            controlPoints[(int)EPos.Center] = new Point(x, y);            
            controlPoints[(int)EPos.PositionOffset] = new Point(x, y);

            if (controlLevel == 0) return;

            controlPoints[(int)EPos.Rotation] = new Point(x + 50, y);
            controlPoints[(int)EPos.RotationOffset] = new Point(0, 0);

            controlPoints[(int)EPos.TopLeft] = new Point(0, 0);
            controlPoints[(int)EPos.TopRight] = new Point(0, 0);
            controlPoints[(int)EPos.BottomLeft] = new Point(0, 0);
            controlPoints[(int)EPos.BottomRight] = new Point(0, 0);

            SelectCP((int)EPos.BottomRight);

            if (controlLevel == 1) return;

            controlPoints[(int)EPos.Top] = new Point(0, 0);
            controlPoints[(int)EPos.Left] = new Point(0, 0);
            controlPoints[(int)EPos.Right] = new Point(0, 0);
            controlPoints[(int)EPos.Bottom] = new Point(0, 0);                        
        }

        public void SetColor(Color c)
        {
            color = c;
        }

        public void SetFillColor(Color c)
        {
            fillColor = c;
        }

        public void SetLineWidth(float lw)
        {
            lineWidth = lw;
        }

        public abstract EShape GetShape();

        // Kiểm tra xem điểm (x, y) tọa độ màn hình có thuộc hình đã được vẽ
        // Dùng để "chọn" các hình
        public abstract bool IsCollideWith(float x, float y);

        // Kiểm tra xem điểm (x, y) tọa độ màn hình có thuộc hình chữ nhật minCoord -> maxCoord
        // Dùng để "di chuyển" các hình
        public virtual bool IsInBoundingBox(float x, float y)
        {
            Point p = ScreenToWorld(new Point(x, y));            

            return minCoord.X < p.X && p.X < maxCoord.X &&
                minCoord.Y < p.Y && p.Y < maxCoord.Y;
        }

        public abstract void DrawShape();

        public bool FillShape()
        {
            if (fillColor == Color.White) return false;

            if (Paint.INSTANCE.IsFillModeFlood())
                FloodFillShape();
            else
                ScanlineFillShape();

            return true;
        }

        public virtual void FloodFillShape()
        {
            Point p = WorldToScreen(controlPoints[(int)EPos.Center]);

            Queue<System.Drawing.Point> q = new Queue<System.Drawing.Point>();
            q.Enqueue(new System.Drawing.Point(
                (int)Math.Floor(p.X),
                (int)Math.Floor(p.Y)
            ));

            void TryEnqueue(int x, int y)
            {
                if (Paint.INSTANCE.IsInCanvas(x, y) &&
                    Paint.INSTANCE.GetPixil(x, y).ToArgb() == Color.White.ToArgb()
                )
                    q.Enqueue(new System.Drawing.Point(x, y));
            }

            while (q.Count > 0)
            {
                System.Drawing.Point cur = q.Dequeue();
                Color c = Paint.INSTANCE.GetPixil(cur.X, cur.Y);

                if (Color.White.ToArgb() != c.ToArgb()) continue;

                Paint.INSTANCE.SetPixil(cur.X, cur.Y, fillColor);

                TryEnqueue(cur.X + 1, cur.Y);
                TryEnqueue(cur.X, cur.Y + 1);
                TryEnqueue(cur.X - 1, cur.Y);
                TryEnqueue(cur.X, cur.Y - 1);
            }
        }

        public virtual void ScanlineFillShape()
        {
        }

        public void DrawControlPoint()
        {
            if (!showControl) return;

            OpenGL gl = Paint.INSTANCE.gl;

            gl.PolygonMode(OpenGL.GL_FRONT, OpenGL.GL_FILL);
            gl.Color(0.0f, 1.0f, 0.0f, 1.0f);

            foreach (KeyValuePair<int, Point> kv in controlPoints)
            {
                if (!IsInteractive(kv.Key)) continue;

                Point p = WorldToScreen(kv.Value);

                gl.Begin(OpenGL.GL_QUADS);
                gl.Vertex(p.X - CPR / 2, p.Y - CPR / 2);
                gl.Vertex(p.X + CPR / 2, p.Y - CPR / 2);
                gl.Vertex(p.X + CPR / 2, p.Y + CPR / 2);
                gl.Vertex(p.X - CPR / 2, p.Y + CPR / 2);
                gl.End();
            }

            if (controlPoints.ContainsKey((int)EPos.Rotation))
                DrawLine(controlPoints[(int)EPos.Center], controlPoints[(int)EPos.Rotation], Color.Blue);
        }

        protected void DrawLine(Point a, Point b, Color? c = null)
        {
            Color col = c.GetValueOrDefault(this.color);

            OpenGL gl = Paint.INSTANCE.gl;

            a = WorldToScreen(a);
            b = WorldToScreen(b);

            // Lấy vector đơn vị của a -> b
            double vx = b.X - a.X;
            double vy = b.Y - a.Y;
            double d = Dist(a, b) + float.Epsilon;
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
            gl.Color(col.R / 255.0f, col.G / 255.0f, col.B / 255.0f, 1.0f);

            gl.Begin(OpenGL.GL_QUADS);
            gl.Vertex(a.X - vx, a.Y - vy);
            gl.Vertex(a.X + vx, a.Y + vy);
            gl.Vertex(b.X + vx, b.Y + vy);
            gl.Vertex(b.X - vx, b.Y - vy);
            gl.End();
        }

        public virtual bool OnMouseMove(MouseEventArgs e)
        {
            if (selectedCP == (int)EPos.None) return false;

            Point m = new Point(e.X, e.Y);
            Point p = controlPoints[selectedCP];

            if (selectedCP == (int)EPos.Rotation)
            {
                Point newV = m - WorldToScreen(controlPoints[(int)EPos.Center]);                
                rotationMat.ToRotation(- Math.Atan2(newV.Y, newV.X));               

                return true;
            }

            // Khoảng cách chuột đã di chuyển tính từ vị trí lúc bấm xuống
            // Tọa độ thế giới
            Point o = (m - mouseDown);

            // Nếu đang cầm điểm điều khuyển và hình không thể chỉnh tỷ lệ
            if (selectedCP >= 0 && isFixedScale)
            {
                o = o * rotationMat.GetInverse();
                float t = Math.Min(Math.Abs(o.X), Math.Abs(o.Y));
                int sX = Math.Sign(o.X);
                int sY = Math.Sign(o.Y);

                if (selectedCP == (int)EPos.TopLeft || selectedCP == (int)EPos.BottomRight)
                {
                    if (sX * sY == 1)
                        o.Set(sX * t, sY * t);
                    else
                        o.Set(t, t);
                }
                else if (selectedCP == (int)EPos.TopRight || selectedCP == (int)EPos.BottomLeft)
                {
                    if (sX * sY == -1)
                        o.Set(sX * t, sY * t);
                    else
                        o.Set(t, -t);
                }
            }

            // Cập nhập vị trí của điểm kiểm soát được chọn
            p.X = stored.X + o.X;
            p.Y = stored.Y + o.Y;

            // System.Diagnostics.Debug.WriteLine(String.Format("({0}, {1}) = ({2}, {3}) + ({4}, {5})", p.X, p.Y, stored.X, stored.Y, o.X, o.Y));

            // Những điểm < 0 là những điểm ảo, không ảnh hưởng tới vị trí tương đối của các điểm khác
            if (selectedCP < 0) return true;

            minCoord = new Point(float.MaxValue, float.MaxValue);
            maxCoord = new Point(float.MinValue, float.MinValue);

            // Lấy số hàng và cột của điểm điều khiển hiện tại            
            int cRow = selectedCP / 3;
            int cCol = selectedCP % 3;

            // Cập nhập các điểm điều khiển khác theo vị trí tương đối và theo hàng và cột
            foreach (KeyValuePair<int, Point> kv in controlPoints)
            {
                if (!IsInteractive(kv.Key)) continue;

                int row = kv.Key / 3;
                int col = kv.Key % 3;

                if (row == cRow)
                    kv.Value.Y = p.Y;

                if (col == cCol)
                    kv.Value.X = p.X;

                if (kv.Key == (int)EPos.TopLeft ||
                    kv.Key == (int)EPos.TopRight || 
                    kv.Key == (int)EPos.BottomLeft || 
                    kv.Key == (int)EPos.BottomRight)
                {
                    minCoord.X = Math.Min(kv.Value.X, minCoord.X);
                    minCoord.Y = Math.Min(kv.Value.Y, minCoord.Y);
                    maxCoord.X = Math.Max(kv.Value.X, maxCoord.X);
                    maxCoord.Y = Math.Max(kv.Value.Y, maxCoord.Y);
                }
            }

            controlPoints[(int)EPos.TopLeft].Set(minCoord.X, minCoord.Y);
            controlPoints[(int)EPos.TopRight].Set(maxCoord.X, minCoord.Y);
            controlPoints[(int)EPos.BottomLeft].Set(minCoord.X, maxCoord.Y);
            controlPoints[(int)EPos.BottomRight].Set(maxCoord.X, maxCoord.Y);

            controlPoints[(int)EPos.Center] = (controlPoints[(int)EPos.TopLeft] + controlPoints[(int)EPos.BottomRight]) / 2;

            // Căng giữa 4 điểm nằm ở 4 phương
            if (controlPoints.ContainsKey((int)EPos.Top))
            {
                controlPoints[(int)EPos.Top] = (controlPoints[(int)EPos.TopLeft] + controlPoints[(int)EPos.TopRight]) / 2;
                controlPoints[(int)EPos.Left] = (controlPoints[(int)EPos.TopLeft] + controlPoints[(int)EPos.BottomLeft]) / 2;
                controlPoints[(int)EPos.Right] = (controlPoints[(int)EPos.TopRight] + controlPoints[(int)EPos.BottomRight]) / 2;
                controlPoints[(int)EPos.Bottom] = (controlPoints[(int)EPos.BottomLeft] + controlPoints[(int)EPos.BottomRight]) / 2;
            }

            if (controlPoints.ContainsKey((int)EPos.Rotation))
                controlPoints[(int)EPos.Rotation] = controlPoints[(int)EPos.Center] + controlPoints[(int)EPos.RotationOffset];            

            return true;
        }

        public virtual bool OnMouseUp(MouseEventArgs e)
        {
            selectedCP = -1;
            if (isInitialDraw)
            {
                isInitialDraw = false;
                showControl = true;
            }
            return true;
        }

        public virtual bool OnMouseDown(MouseEventArgs e)
        {
            mouseDown = new Point(e.X, e.Y);
            Point m = ScreenToWorld(mouseDown);

            if (controlPoints.ContainsKey((int)EPos.Rotation))
            {
                controlPoints[(int)EPos.RotationOffset] = controlPoints[(int)EPos.Rotation] - controlPoints[(int)EPos.Center];
            }

            if (selectedCP != (int)EPos.None) return true;

            foreach (KeyValuePair<int, Point> kv in controlPoints)
            {
                if (!IsInteractive(kv.Key)) continue;

                if (Dist(m.X, m.Y, kv.Value.X, kv.Value.Y) < CPR)
                {
                    SelectCP(kv.Key);
                    return true;
                }
            }

            if (IsInBoundingBox(e.X, e.Y))
            {
                SelectCP((int)EPos.PositionOffset);
                return true;
            }

            return false;
        }

        protected void SelectCP(int pos)
        {
            selectedCP = pos;
            stored = controlPoints[pos].Copy();
        }

        protected Point ScreenToWorld(Point p)
        {
            // Dịch p về tâm để xoay
            Point r = p - controlPoints[(int)EPos.Center] - controlPoints[(int)EPos.PositionOffset];
            // Xoay theo hướng ngược lại
            r = r * rotationMat.GetInverse();
            // Dịch chuyển về lại vị trí cũ
            r = r + controlPoints[(int)EPos.Center];

            return r;
        }

        protected Point WorldToScreen(Point p)
        {
            // Dịch p về tâm của hình để xoay
            Point r = p - controlPoints[(int)EPos.Center];
            // Xoay p bằng affin qua ma trận
            r = r * rotationMat;
            // Dịch p về vị trí cũ và áp dụng offset
            r = r + controlPoints[(int)EPos.Center] + controlPoints[(int)EPos.PositionOffset];

            return r;
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

        protected static double Dist(float x1, float y1, float x2, float y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        protected static double Dist(Point a, Point b)
        {
            return Dist(a.X, a.Y, b.X, b.Y);
        }

        protected static bool IsInteractive(int epos)
        {
            switch ((EPos) epos)
            {
                case EPos.None:
                case EPos.Center:
                case EPos.PositionOffset:
                case EPos.RotationOffset:
                    return false;

                default:
                    return true;
            }
        }
    }
}
