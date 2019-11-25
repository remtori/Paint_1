using SharpGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Paint_1
{
    public partial class Paint : Form
    {
        public static Paint INSTANCE;

        public OpenGL gl;

        private Dictionary<ECursors, Cursor> cursors;
        private Dictionary<Keys, bool> keys;

        private Color[] colors;
        private EShape selectedEShape;
        private Shape selectedShape;

        private List<Shape> shapes;

        public Paint()
        {
            INSTANCE = this;

            colors = new Color[2]; // Primary and Secondary Color
            colors[0] = new Color(0, 0, 0); // Black
            colors[1] = new Color(255, 255, 255); // White

            selectedEShape = EShape.LINE;
            selectedShape = null;

            shapes = new List<Shape>();

            cursors = new Dictionary<ECursors, Cursor>();
            cursors[ECursors.RotateN] = new Cursor(new MemoryStream(Properties.Resources.cursorRotateN));
            cursors[ECursors.RotateE] = new Cursor(new MemoryStream(Properties.Resources.cursorRotateE));
            cursors[ECursors.RotateS] = new Cursor(new MemoryStream(Properties.Resources.cursorRotateS));
            cursors[ECursors.RotateW] = new Cursor(new MemoryStream(Properties.Resources.cursorRotateW));
            cursors[ECursors.RotateNW] = new Cursor(new MemoryStream(Properties.Resources.cursorRotateNW));
            cursors[ECursors.RotateNE] = new Cursor(new MemoryStream(Properties.Resources.cursorRotateNE));
            cursors[ECursors.RotateSW] = new Cursor(new MemoryStream(Properties.Resources.cursorRotateSW));
            cursors[ECursors.RotateSE] = new Cursor(new MemoryStream(Properties.Resources.cursorRotateSE));

            cursors[ECursors.SizeAll] = Cursors.SizeAll;
            cursors[ECursors.SizeNS] = Cursors.SizeNS;
            cursors[ECursors.SizeWE] = Cursors.SizeWE;
            cursors[ECursors.SizeNESW] = Cursors.SizeNESW;
            cursors[ECursors.SizeNWSE] = Cursors.SizeNWSE;

            InitializeComponent();
        }

        private void _OnInit(object sender, EventArgs e)
        {
            gl = openGLControl.OpenGL;

            gl.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            gl.Disable(OpenGL.GL_DEPTH_TEST);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
        }

        private void _OnResize(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, openGLControl.Height, 0);
        }

        private void _OnButtonClicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b == null) return;

            switch (b.Name)
            {
                case "bt_line":
                    selectedEShape = EShape.LINE;
                    break;
                case "bt_triangle":
                    selectedEShape = EShape.TRIANGLE;
                    break;
                case "bt_rectangle":
                    selectedEShape = EShape.RECTANGLE;
                    break;
                case "bt_pentagon":
                    selectedEShape = EShape.PENTAGON;
                    break;
                case "bt_hexagon":
                    selectedEShape = EShape.HEXAGON;
                    break;
                case "bt_polygon":
                    selectedEShape = EShape.POLYGON;
                    break;
                case "bt_circle":
                    selectedEShape = EShape.CIRCLE;
                    break;
                case "bt_ellipse":
                    selectedEShape = EShape.ELLIPSE;
                    break;
            }
        }

        private void _OnDraw(object sender, RenderEventArgs args)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);

            foreach (Shape s in shapes)
            {
                s.DrawShape();
            }

            if (selectedShape != null)
                selectedShape.DrawControlPoint();

            gl.Flush();
        }

        private void _OnMouseMove(object sender, MouseEventArgs e)
        {
            if (selectedShape != null)
                selectedShape.OnMouseMove(e);
        }

        private void _OnMouseUp(object sender, MouseEventArgs e)
        {
            if (selectedShape != null)
                selectedShape.OnMouseUp(e);
        }

        private void _OnMouseDown(object sender, MouseEventArgs e)
        {
            if (selectedShape != null)
            {
                if (selectedShape.OnMouseDown(e)) return;

                foreach (Shape s in shapes)
                {
                    if (s.IsCollideWith(e.X, e.Y))
                    {
                        selectedShape = s;
                        selectedShape.OnMouseDown(e);
                        return;
                    }
                }
            }

            selectedShape = Shape.CreateShape(
                selectedEShape,
                e.X, e.Y,
                colors[e.Button == MouseButtons.Left ? 0 : 1],
                (float) comboBox1.SelectedItem
            );

            shapes.Add(selectedShape);
            selectedShape.OnMouseDown(e);
        }

        private void _OnKeyUp(object sender, KeyEventArgs e)
        {
            keys[e.KeyCode] = false;
            keys[Keys.Control] = e.Control;
            keys[Keys.Shift] = e.Shift;
            keys[Keys.Alt] = e.Alt;
        }

        private void _OnKeyDown(object sender, KeyEventArgs e)
        {
            keys[e.KeyCode] = true;
            keys[Keys.Control] = e.Control;
            keys[Keys.Shift] = e.Shift;
            keys[Keys.Alt] = e.Alt;
        }

        public void SetCursor(ECursors cur)
        {
            openGLControl.Cursor = cursors[cur];
        }

        public bool GetKeyState(Keys k)
        {
            bool FALSE = false;
            return keys.TryGetValue(k, out FALSE);
        }
    }
}
