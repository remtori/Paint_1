using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Paint_1
{
    public partial class Paint : Form
    {
        public static Paint INSTANCE;
        public static readonly int MAX_CANVAS_SIZE = 1920 * 1080;

        public OpenGL gl;

        private Color color;
        private Color fillColor;
        private EShape selectedEShape;
        private Shape selectedShape;

        private List<Shape> shapes;
        private byte[] pixels;

        private List<Button> shapeSelectButtons;

        public Paint()
        {
            INSTANCE = this;
            color = Color.Black;
            fillColor = Color.White;
            selectedShape = null;            

            shapes = new List<Shape>();
            shapeSelectButtons = new List<Button>();

            InitializeComponent();

            shapeSelectButtons.Add(bt_line);
            shapeSelectButtons.Add(bt_triangle);
            shapeSelectButtons.Add(bt_rectangle);
            shapeSelectButtons.Add(bt_pentagon);
            shapeSelectButtons.Add(bt_hexagon);
            shapeSelectButtons.Add(bt_polygon);
            shapeSelectButtons.Add(bt_ellipse);
            shapeSelectButtons.Add(bt_circle);

            pixels = new byte[MAX_CANVAS_SIZE * 3];
        }

        public Color GetPixil(int x, int y)
        {
            int o = x + (openGLControl.Size.Height - y) * openGLControl.Size.Width;
            o *= 3;

            return Color.FromArgb(pixels[o], pixels[o + 1], pixels[o + 2]);
        }

        public void SetPixil(int x, int y, Color c)
        {
            int o = x + (openGLControl.Size.Height - y) * openGLControl.Size.Width;
            o *= 3;

            pixels[o + 0] = c.R;
            pixels[o + 1] = c.G;
            pixels[o + 2] = c.B;
        }

        public bool IsInCanvas(int x, int y)
        {
            return 0 < x && x < openGLControl.Size.Width && 0 < y && y < openGLControl.Size.Height;
        }

        public bool IsFillModeFlood()
        {
            return fillMode.SelectedIndex == 0;
        }

        public Color GetForegroundColor()
        {
            return color;
        }

        public Color GetBackgroundColor()
        {
            return fillColor;
        }

        private void _OnInit(object sender, EventArgs e)
        {
            selectedEShape = EShape.LINE;
            bt_line.Enabled = false;
            lineWidth.SelectedIndex = 0;
            fillMode.SelectedIndex = 0;

            gl = openGLControl.OpenGL;

            gl.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            gl.Disable(OpenGL.GL_DEPTH_TEST);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
        }

        private void _OnGLResize(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, openGLControl.Height, 0);            
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            this.openGLControl.Size = new Size(
                this.ClientSize.Width - 2,
                this.ClientSize.Height - 95
            );
        }

        private void _OnDraw(object sender, RenderEventArgs args)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);

            foreach (Shape s in shapes)
            {
                s.DrawShape();
            }

            gl.Flush();
            gl.ReadPixels(0, 0, openGLControl.Size.Width, openGLControl.Size.Height, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, pixels);

            int filledShape = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            foreach (Shape s in shapes)
            {
                if (s.FillShape()) filledShape++;
            }
            watch.Stop();

            long avgTime = 0;
            if (filledShape != 0) avgTime = watch.ElapsedMilliseconds / filledShape;

            avgFillTime.Text = String.Format("Avg Fill Time: {0,4} ms", avgTime);

            gl.DrawPixels(openGLControl.Size.Width, openGLControl.Size.Height, OpenGL.GL_RGB, pixels);

            if (selectedShape != null)
                selectedShape.DrawControlPoint();

            gl.Flush();
        }


        private void _OnBtnClicked(object sender, EventArgs e)
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
                case "bt_clear":
                    shapes.Clear();
                    selectedShape = null;
                    break;
            }

            if (b.Name == "bt_clear") return;
            
            foreach (Button c in shapeSelectButtons)
            {
                c.Enabled = true;
            }

            b.Enabled = false;
        }

        private void _OnToolClicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b == null) return;

            switch (b.Name)
            {
                case "bt_color":
                case "bt_fillColor":
                    {
                        ColorDialog colorPicker = new ColorDialog();
                        colorPicker.AllowFullOpen = false;
                        colorPicker.ShowHelp = true;
                        colorPicker.Color = b.Name == "bt_color" ? this.color : this.fillColor;

                        if (colorPicker.ShowDialog() == DialogResult.OK)
                        {                            
                            if (b.Name == "bt_color")
                            {
                                this.color = colorPicker.Color;
                                this.bt_color.BackColor = colorPicker.Color;
                                selectedShape?.SetColor(this.color);
                            }
                            else
                            {
                                this.fillColor = colorPicker.Color;
                                this.bt_fillColor.BackColor = colorPicker.Color;
                                selectedShape?.SetFillColor(this.fillColor);
                            }
                        }
                    }
                    break;

            }
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
                (float) lineWidth.SelectedItem
            );

            shapes.Add(selectedShape);
            selectedShape.OnMouseDown(e);
            

        }
    }
}
