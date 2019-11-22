using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_1
{    
    abstract class Shape
    {        
        static readonly float CPR = 5.0f; // Control Point Radius

        protected Point scale;
        protected Point position;        

        protected Color color;
        protected float lineWidth;

        protected bool isInitialDraw;

        protected int selectedCP;
        protected Point storedCP;
        protected bool useDefaultCP;
        protected Dictionary<int, Point> controlPoints;


        protected static int mouseDownX, mouseDownY;

        public Shape(float x, float y, Color c, float lW, bool defaultControlPoint = true)
        {
            scale = new Point(1.0f, 1.0f);
            position = new Point(x, y);

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

        public abstract void DrawShape(OpenGL gl);

        public void DrawControlPoint(OpenGL gl)
        {
            gl.PolygonMode(OpenGL.GL_FRONT, OpenGL.GL_FILL);
            gl.Color(66, 135, 245);

            foreach (KeyValuePair<int, Point> kv in controlPoints)
            {
                Point p = kv.Value;
                gl.Begin(OpenGL.GL_QUADS);
                gl.Vertex(p.X - CPR / 2, p.Y - CPR / 2);
                gl.Vertex(p.X + CPR / 2, p.Y - CPR / 2);
                gl.Vertex(p.X + CPR / 2, p.Y + CPR / 2);
                gl.Vertex(p.X - CPR / 2, p.Y + CPR / 2);
                gl.End();
            }
        }

        protected void DrawLine(OpenGL gl, Point a, Point b)
        {
            gl.PolygonMode(OpenGL.GL_FRONT, OpenGL.GL_FILL);
            color.Apply(gl);
            gl.Begin(OpenGL.GL_QUADS);
            gl.Vertex(a.X - lineWidth / 2, a.Y - lineWidth / 2);
            gl.Vertex(a.X + lineWidth / 2, a.Y - lineWidth / 2);
            gl.Vertex(b.X + lineWidth / 2, b.Y + lineWidth / 2);
            gl.Vertex(b.X - lineWidth / 2, b.Y + lineWidth / 2);
            gl.End();
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
            if (!useDefaultCP || selectedCP == -1) return;

            // Update control point position
            Point p = controlPoints[selectedCP];
            int cRow = selectedCP / 3;
            int cCol = selectedCP % 3;

            if (cRow != 1)
                p.X = storedCP.X + (mouseDownX - e.X);

            if (cCol != 1)
                p.Y = storedCP.Y + (mouseDownY - e.Y);

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
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            selectedCP = -1;
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            mouseDownX = e.X;
            mouseDownY = e.Y;
            for(int i = 0; i < controlPoints.Count; i++)
            {
                if (DistSqr(e.X, e.X, controlPoints[i].X, controlPoints[i].Y) < CPR * CPR)
                {
                    selectedCP = i;
                    storedCP = new Point(
                        controlPoints[i].X,
                        controlPoints[i].Y
                    );

                    return;
                }
            }
        }
        protected float DistSqr(float x1, float y1, float x2, float y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }
    }
}
