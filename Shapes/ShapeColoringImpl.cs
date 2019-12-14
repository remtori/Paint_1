using System;
using System.Drawing;
using System.Collections.Generic;

namespace Paint_1
{
    struct EdgeBucket
    {
        public int yMax, yMin;
        public int x;
        public float edge;
    }

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
            // 
            List<EdgeBucket> edges = new List<EdgeBucket>();
            List<EdgeBucket> activeList = new List<EdgeBucket>();

            Func<Point, Point, int> TryStoreEdge = (a, b) =>
            {
                if (a.Y == b.Y) return 0;

                EdgeBucket bucket = new EdgeBucket();

                bucket.edge = 0.0f;
                if (a.X != b.X)
                {
                    bucket.edge = 1.0f / (((float)(b.Y - a.Y)) / ((float)(b.X - a.X)));
                }

                if (a.Y > b.Y)
                {
                    bucket.yMax = (int) Math.Floor(a.Y);
                    bucket.yMin = (int)Math.Floor(b.Y);
                    bucket.x = (int)Math.Floor(b.X);
                }
                else
                {
                    bucket.yMax = (int)Math.Floor(b.Y);
                    bucket.yMin = (int)Math.Floor(a.Y);
                    bucket.x = (int)Math.Floor(a.X);
                }

                edges.Add(bucket);

                return 0;
            };

            TryStoreEdge(WorldToScreen(verticies[0]), WorldToScreen(verticies[verticies.Count - 1]));
            for (int i = 1; i < verticies.Count; i++)
                TryStoreEdge(WorldToScreen(verticies[i - 1]), WorldToScreen(verticies[i]));

            while (edges.Count > 0)
            {

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
