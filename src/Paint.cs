using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_1
{    
    class PaintState
    {
        public Color[] colors;
        public EShape selectedShape;
        public float lineWidth;
    }
    class Paint
    {
        public OpenGL gl;
        public PaintState ps;

        public Paint(OpenGL glIn)
        {
            gl = glIn;
            ps = new PaintState();
            ps.colors = new Color[2]; // Primary and Secondary Color
            ps.selectedShape = EShape.LINE;
            ps.lineWidth = 1.0f;
        }

        public void SetColor(Color color, int slot = 0)
        {
            if (slot >= ps.colors.Length)
                ps.colors[0] = color;
            else
                ps.colors[slot] = color;

        }

        public void SetShape(EShape shape)
        {
            ps.selectedShape = shape;
        }

        public void SetLineWidth(float width)
        {
            ps.lineWidth = width;
        }

        public void OnMouseMove(MouseEventArgs e)
        {     
        }

        public void OnMouseDown(MouseEventArgs e)
        {
        }

        public void OnMouseUp(MouseEventArgs e)
        {
        }
    }
}
