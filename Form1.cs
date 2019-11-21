using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using SharpGL;

namespace Paint_1
{
    public partial class Form1 : Form
    {
        Stack<shape> st_shape = new Stack<shape>(); // TAO STACK CHUA CAC HINH
        Color colorUserColor_Line;
        int shShape;
        bool mouseDown, mouseUp, fill = false;
        Point pFill;
        public Form1()
        {
            InitializeComponent();
            colorUserColor_Line = Color.Black;
            shShape = 0;
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the clear color.
            gl.ClearColor(255, 255, 255, 0);
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {

            OpenGL gl = openGLControl.OpenGL;

            //set color
            gl.Color(colorUserColor_Line.R / 255.0, colorUserColor_Line.G / 255.0, colorUserColor_Line.B / 255.0, 0);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);// innit
                                                                              //draw

            if (st_shape.Count != 0)
            {
                foreach (shape i in st_shape)
                {       // VE TUNG HINH TRONG STACK
                    switch (i.Get_shape())
                    {
                        case 0:
                            draw_line(i.Get_start(), i.Get_end());
                            break;
                        case 1:
                            draw_circle(i.Get_start(), i.Get_end());
                            break;
                        case 2:
                            draw_rec(i.Get_start(), i.Get_end());
                            break;
                        case 3:
                            draw_esllipe(i.Get_start(), i.Get_end());
                            break;
                        case 4:
                            draw_tri(i.Get_start(), i.Get_end());
                            break;
                        case 5:
                            draw_penta(i.Get_start(), i.Get_end());
                            break;
                        case 6:
                            draw_hexa(i.Get_start(), i.Get_end());
                            break;
                        case 7:
                            draw_poly(i);
                            break;
                    }
                }
            }
            gl.Flush();
        }

        //draw shape 
        private void draw_line(Point pStart, Point pEnd)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pStart.Y);
            gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pEnd.Y);
            gl.End();
            gl.Flush();
        }
        private void draw_circle(Point pStart, Point pEnd)
        {
            Point center = new Point();
            center.X = (pStart.X + pEnd.X) / 2;
            center.Y = (pStart.Y + pEnd.Y) / 2;
            double D = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
            float R = (float)D / 2;

            OpenGL gl = openGLControl.OpenGL;
            gl.Begin(OpenGL.GL_LINE_LOOP);
            for (int i = 0; i < 360; i++)
            {
                gl.Vertex(D * Math.Cos(2 * Math.PI * i / 360) + pStart.X, D * Math.Sin(2 * Math.PI * i / 360) + gl.RenderContextProvider.Height - pStart.Y, 0.0);
            }
            gl.End();
            gl.Flush();
        }
        private void draw_rec(Point topLeft, Point bottomRight)
        {
            Point p1 = topLeft;
            Point p2 = new Point();
            Point p3 = bottomRight;
            Point p4 = new Point();
            p2.X = topLeft.X;
            p2.Y = bottomRight.Y;
            p3.X = p3.X;
            p3.Y = p3.Y;
            p4.X = bottomRight.X;
            p4.Y = topLeft.Y;


            draw_line(p1, p2);
            draw_line(p2, p3);
            draw_line(p3, p4);
            draw_line(p4, p1);
        }
        private void draw_esllipe(Point pStart, Point pEnd)
        {
            Point center = new Point();
            center.X = (pStart.X + pEnd.X) / 2;
            center.Y = (pStart.Y + pEnd.Y) / 2;
            int rX = Math.Abs(pStart.X - pEnd.X);
            int rY = Math.Abs(pStart.Y - pEnd.Y);

            OpenGL gl = openGLControl.OpenGL;
            gl.Begin(OpenGL.GL_LINE_LOOP);
            for (int i = 0; i < 360; i++)
            {
                gl.Vertex(rX * Math.Cos(2 * Math.PI * i / 360) + pStart.X, rY * Math.Sin(2 * Math.PI * i / 360) + gl.RenderContextProvider.Height - pStart.Y, 0.0);
            }
            gl.End();
            gl.Flush();
        }
        private void draw_tri(Point pStart, Point pEnd)
        {
            OpenGL gl = openGLControl.OpenGL;
            Point[] p = new Point[3];
            p[0] = pStart;
            p[1] = pEnd;
            Point p1 = pStart;
            Point p2 = pEnd;
            Point p3 = new Point();

            p1.X = pStart.X;
            p1.Y = pEnd.Y;

            double D = Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
            double high = D * Math.Sin(Math.PI / 3);
            p3.X = (p1.X + p2.X) / 2;
            p3.Y = p1.Y - (int)high;

            draw_line(p1, p2);
            draw_line(p2, p3);
            draw_line(p3, p1);
        }
        private void draw_penta(Point pStart, Point pEnd)
        {
            double D = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
            float R = (float)D / 2;
            Point[] p = new Point[5];
            for (int i = 0; 72 * i < 360; i++)
            {
                p[i].X = (int)(D * Math.Cos(2 * Math.PI * 72 * i / 360) + pStart.X);
                p[i].Y = (int)(D * Math.Sin(2 * Math.PI * 72 * i / 360) + pStart.Y);
            }

            draw_line(p[0], p[1]);
            draw_line(p[1], p[2]);
            draw_line(p[2], p[3]);
            draw_line(p[3], p[4]);
            draw_line(p[4], p[0]);
        }
        private void draw_hexa(Point pStart, Point pEnd)
        {
            double D = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
            float R = (float)D / 2;
            Point[] p = new Point[6];

            for (int i = 0; 60 * i < 360; i++)
            {
                p[i].X = (int)(D * Math.Cos(2 * Math.PI * 60 * i / 360) + pStart.X);
                p[i].Y = (int)(D * Math.Sin(2 * Math.PI * 60 * i / 360) + pStart.Y);
            }

            draw_line(p[0], p[1]);
            draw_line(p[1], p[2]);
            draw_line(p[2], p[3]);
            draw_line(p[3], p[4]);
            draw_line(p[4], p[5]);
            draw_line(p[5], p[0]);
        }
        private void draw_poly(shape s)
        {
            if (s.points.Count == 2)
            {  // trường hợp đa giác mới chỉ là 1 đoạn thẳng
                draw_line(s.points[0], s.points[1]);
            }
            else if (s.points.Count > 2)
            {      // đa giác lớn hơn 3 đỉnh
                for (int i = 0; i < s.points.Count - 1; i++)
                    draw_line(s.points[i], s.points[i + 1]);
                draw_line(s.points[0], s.points[s.points.Count - 1]); // để nối từ điểm đầu đến điểm cuối
            }
        }

        private void bt_LineWidth_Click(object sender, EventArgs e)
        {
        }
        private void back(object sender, EventArgs e)
        {
            if (st_shape.Count() == 0)
                return;
            st_shape.Pop();
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }       // HAM DE XOA 1 HINH VE TRUOC DO, DO K TAO DC BUTTON NEN T DUNG TAM BUTTON FILL
        private void bt_line_Click(object sender, EventArgs e)
        {
            shShape = 0;
        }

        private void bt_circle_Click(object sender, EventArgs e)
        {
            shShape = 1;
        }

        private void bt_rectangle_Click(object sender, EventArgs e)
        {
            shShape = 2;
        }

        private void bt_ellipse_Click(object sender, EventArgs e)
        {
            shShape = 3;
        }


        private void bt_triangle_Click(object sender, EventArgs e)
        {
            shShape = 4;
        }

        private void bt_pentagon_Click(object sender, EventArgs e)
        {
            shShape = 5;
        }

        private void bt_hexagon_Click(object sender, EventArgs e)
        {
            shShape = 6;
        }

        private void bt_polygon_Click(object sender, EventArgs e)
        {
            shShape = 7;
        }

        struct ColorRGB
        {
            public byte r;
            public byte g;
            public byte b;
        }

        private ColorRGB GetColor(int x, int y)
        {
            ColorRGB c;
            byte[] pixelData = new byte[3];
            OpenGL gl = openGLControl.OpenGL;
            gl.ReadPixels(x, gl.RenderContextProvider.Height - y, 1, 1, OpenGL.GL_RGB, OpenGL.GL_BYTE, pixelData);
            c.r = pixelData[0];
            c.g = pixelData[1];
            c.b = pixelData[2];
            return c;
        }

        private void SetColor(int x, int y, ColorRGB c)
        {
            OpenGL gl = openGLControl.OpenGL;
            byte[] pixel = new byte[3];
            pixel[0] = c.r;
            pixel[1] = c.g;
            pixel[2] = c.b;

            gl.Begin(OpenGL.GL_POINTS);
            gl.Color(c.r, c.g, c.b);
            gl.Vertex(x, y);
            gl.End();

            gl.Flush();
        }

        private bool IsColorBackground(int x, int y)
        {
            ColorRGB c, current;
            c.r = colorUserColor_Line.R;
            c.g = colorUserColor_Line.G;
            c.b = colorUserColor_Line.B;

            current = GetColor(x,y);
            if (current.r != colorUserColor_Line.R || current.g != colorUserColor_Line.G || current.b != colorUserColor_Line.B) 
            {
                return true;
            }
            else
                return false;      
        }

        int n = 50;
        private void FloodFill(int x, int y, ColorRGB c)
        { 
            if (IsColorBackground(x,y))
            {
                SetColor(x, y, c);
                FloodFill(x + 1, y, c);
                FloodFill(x - 1, y, c);
                FloodFill(x, y + 1, c);
                FloodFill(x, y - 1, c);
            }
        }

        private void bt_undo_Click(object sender, EventArgs e)
        {
            if (st_shape.Count() == 0)
                return;
            st_shape.Pop();
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
          while(st_shape.Count()!=0)
            {
                st_shape.Pop();
            }
        }

        private void ctrl_openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == true && mouseUp == false)
            {
                OpenGL gl = openGLControl.OpenGL;
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                st_shape.Peek().Set_end(e.Location);   // update vi tri ket thuc cua shape
                st_shape.Peek().Set_shape(shShape);     //update shape type
            }
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                st_shape.Peek().Set_end(e.Location); // update lan cuoi vi tri ket thuc cho shape
                mouseUp = true;
                mouseDown = false;
                // them 1 shape vao stack de viec thay doi shape tren cung stack khong con dien ra nua
                if (shShape != 7)
                {
                    st_shape.Push(new shape());
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            if(fill==true)
            {
                pFill = e.Location;
                ColorRGB c;
                c.r = colorUserColor_Line.R;
                c.g = colorUserColor_Line.G;
                c.b = colorUserColor_Line.B;
                FloodFill(pFill.X, pFill.Y, c);
            }
        }

        private void bt_fill_Click(object sender, EventArgs e)
        {
            fill = true;
        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (st_shape.Count == 0)
                {      // neu stack rong, khoi tao 1 shape moi them vao
                    st_shape.Push(new shape(new Point(), new Point(), shShape));
                }
                st_shape.Peek().Set_start(e.Location);
                st_shape.Peek().Set_end(e.Location);
                mouseDown = true;
                mouseUp = false;
                if (shShape == 7)
                {
                    Point te = e.Location;
                    st_shape.Peek().points.Add(te);
                }
            }

            if (e.Button == MouseButtons.Right && shShape == 7)
            {
                st_shape.Push(new shape()); // click chuot phai de huy ve da giac, nen them 1 shape moi vao no
            }
        }


    }
    public class shape
    {
        public Point pStart, pEnd;
        public List<Point> points;
        public int shape_type;
        public shape()
        {
            pStart = new Point(0, 0);
            pEnd = new Point(0, 0);
            shape_type = 0;
            points = new List<Point>();
        }
        public shape(Point a, Point b, int t, List<Point> c)
        {
            pStart = a;
            pEnd = b;
            shape_type = t;
            points = c;
        }
        public shape(Point a, Point b, int t)
        {
            pStart = a;
            pEnd = b;
            shape_type = t;
            points = new List<Point>();
        }
        public Point Get_start()
        {
            return pStart;
        }
        public Point Get_end()
        {
            return pEnd;
        }
        public int Get_shape()
        {
            return shape_type;
        }
        public void Set_start(Point a)
        {
            pStart = a;
        }
        public void Set_end(Point a)
        {
            pEnd = a;
        }
        public void Set_shape(int a)
        {
            shape_type = a;
        }
    } //////////////////////////////////////////////////////////////////////////////////
    public class polygon
    {
        public Stack<Point> points;
    }
}
