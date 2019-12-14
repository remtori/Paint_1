using System;
using System.Drawing;
using System.Collections.Generic;

namespace Paint_1
{
    abstract partial class Shape
    {        
        public bool FillShape()
        {
            if (fillColor == Color.White) return false;

            Point size = maxCoord - minCoord;
            if (size.X < 2 && size.Y < 2) return false;

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

            Func<int, int, bool> TryEnqueue = (x, y) =>
            {
                if (IsInCanvas(x, y) &&
                    GetPixel(x, y).ToArgb() == Color.White.ToArgb()
                )
                    q.Enqueue(new System.Drawing.Point(x, y));

                return true;
            };

            while (q.Count > 0)
            {
                System.Drawing.Point cur = q.Dequeue();

                if (Color.White.ToArgb() != GetPixel(cur.X, cur.Y).ToArgb()) continue;

                SetPixel(cur.X, cur.Y, fillColor);

                TryEnqueue(cur.X + 1, cur.Y);
                TryEnqueue(cur.X, cur.Y + 1);
                TryEnqueue(cur.X - 1, cur.Y);
                TryEnqueue(cur.X, cur.Y - 1);
            }
        }

        public virtual void ScanlineFillShape()
        {
            List<float> GiaoDiem = new List<float>();

            float xMin, xMax, yMin, yMax;

            xMin = xMax = verticies[0].X;
            yMin = yMax = verticies[0].Y;

            foreach (Point p in verticies)
            {
                if (xMin > p.X) xMin = p.X;
                if (xMax < p.X) xMax = p.X;
                if (yMin > p.Y) yMin = p.Y;
                if (yMax < p.Y) yMax = p.Y;
            }

            int i;
            float x, y, x1, x2, y1, y2, temp;
            y = yMin;

            while (y <= yMax)
            {
                GiaoDiem.Clear();
                // với y tăng dần từ ymin > ymax,tìm các giao điểm của từng y với các cặp cạnh☻
                for (i = 0; i < verticies.Count; i++)
                {
                    x1 = verticies[i].X;
                    y1 = verticies[i].Y;
                    x2 = verticies[(i + 1) % verticies.Count].X;
                    y2 = verticies[(i + 1) % verticies.Count].Y;

                    if (y2 < y1)
                    {
                        temp = x1; x1 = x2; x2 = temp;
                        temp = y1; y1 = y2; y2 = temp;
                    }

                    if (y1 <= y && y <= y2)
                    {
                        if (y1 == y2) // nếu y của 2 đỉnh liên tiếp trùng nhau => bỏ qua
                        {
                            x = x1;
                        }
                        else
                        {
                            x = ((y - y1) * (x2 - x1)) / (y2 - y1); //hệ số góc
                            x += x1;
                        }

                        if (xMin <= x && x <= xMax) GiaoDiem.Add(x);
                    }
                }

                // với từng y tăng dần ta vẽ luôn đường thằng nối 2 giao điểm
                for (i = 0; i < GiaoDiem.Count - 1; i += 2)
                {
                    Point A = new Point(GiaoDiem[i], y);
                    Point B = new Point(GiaoDiem[i + 1], y);
                    DrawLine(A, B, fillColor);
                }

                y++;
            }
        }

        // Utility method
        private Color GetPixel(int x, int y)
        {
            return Paint.INSTANCE.GetPixel(x, y);
        }

        private void SetPixel(int x, int y, Color c)
        {
            Paint.INSTANCE.SetPixel(x, y, c);
        }

        private bool IsInCanvas(int x, int y)
        {
            return Paint.INSTANCE.IsInCanvas(x, y);
        }
    }
}
