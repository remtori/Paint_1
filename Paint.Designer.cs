using System;

namespace Paint_1
{
    partial class Paint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_polygon = new System.Windows.Forms.Button();
            this.bt_rectangle = new System.Windows.Forms.Button();
            this.bt_triangle = new System.Windows.Forms.Button();
            this.bt_pentagon = new System.Windows.Forms.Button();
            this.bt_hexagon = new System.Windows.Forms.Button();
            this.bt_circle = new System.Windows.Forms.Button();
            this.bt_ellipse = new System.Windows.Forms.Button();
            this.bt_undo = new System.Windows.Forms.Button();
            this.bt_clear = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.bt_fill = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.bt_line = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_polygon
            // 
            this.bt_polygon.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_polygon.Location = new System.Drawing.Point(3, 43);
            this.bt_polygon.Name = "bt_polygon";
            this.bt_polygon.Size = new System.Drawing.Size(75, 23);
            this.bt_polygon.TabIndex = 2;
            this.bt_polygon.Text = "Polygon";
            this.bt_polygon.UseVisualStyleBackColor = true;
            this.bt_polygon.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // bt_rectangle
            // 
            this.bt_rectangle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_rectangle.Location = new System.Drawing.Point(85, 13);
            this.bt_rectangle.Name = "bt_rectangle";
            this.bt_rectangle.Size = new System.Drawing.Size(75, 23);
            this.bt_rectangle.TabIndex = 3;
            this.bt_rectangle.Text = "Rectangle";
            this.bt_rectangle.UseVisualStyleBackColor = true;
            this.bt_rectangle.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // bt_triangle
            // 
            this.bt_triangle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_triangle.Location = new System.Drawing.Point(85, 43);
            this.bt_triangle.Name = "bt_triangle";
            this.bt_triangle.Size = new System.Drawing.Size(75, 23);
            this.bt_triangle.TabIndex = 4;
            this.bt_triangle.Text = "Triangle";
            this.bt_triangle.UseVisualStyleBackColor = true;
            this.bt_triangle.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // bt_pentagon
            // 
            this.bt_pentagon.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_pentagon.Location = new System.Drawing.Point(167, 13);
            this.bt_pentagon.Name = "bt_pentagon";
            this.bt_pentagon.Size = new System.Drawing.Size(75, 23);
            this.bt_pentagon.TabIndex = 5;
            this.bt_pentagon.Text = "Pentagon";
            this.bt_pentagon.UseVisualStyleBackColor = true;
            this.bt_pentagon.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // bt_hexagon
            // 
            this.bt_hexagon.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_hexagon.Location = new System.Drawing.Point(167, 43);
            this.bt_hexagon.Name = "bt_hexagon";
            this.bt_hexagon.Size = new System.Drawing.Size(75, 23);
            this.bt_hexagon.TabIndex = 6;
            this.bt_hexagon.Text = "Hexagon";
            this.bt_hexagon.UseVisualStyleBackColor = true;
            this.bt_hexagon.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // bt_circle
            // 
            this.bt_circle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_circle.Location = new System.Drawing.Point(249, 12);
            this.bt_circle.Name = "bt_circle";
            this.bt_circle.Size = new System.Drawing.Size(75, 23);
            this.bt_circle.TabIndex = 7;
            this.bt_circle.Text = "Circle";
            this.bt_circle.UseVisualStyleBackColor = true;
            this.bt_circle.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // bt_ellipse
            // 
            this.bt_ellipse.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_ellipse.Location = new System.Drawing.Point(249, 43);
            this.bt_ellipse.Name = "bt_ellipse";
            this.bt_ellipse.Size = new System.Drawing.Size(75, 23);
            this.bt_ellipse.TabIndex = 8;
            this.bt_ellipse.Text = "Ellipse";
            this.bt_ellipse.UseVisualStyleBackColor = true;
            this.bt_ellipse.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // bt_undo
            // 
            this.bt_undo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_undo.Location = new System.Drawing.Point(871, 12);
            this.bt_undo.Name = "bt_undo";
            this.bt_undo.Size = new System.Drawing.Size(75, 23);
            this.bt_undo.TabIndex = 11;
            this.bt_undo.Text = "Undo";
            this.bt_undo.UseVisualStyleBackColor = true;
            this.bt_undo.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // bt_clear
            // 
            this.bt_clear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_clear.Location = new System.Drawing.Point(870, 43);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(75, 23);
            this.bt_clear.TabIndex = 12;
            this.bt_clear.Text = "Clear all";
            this.bt_clear.UseVisualStyleBackColor = true;
            this.bt_clear.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { 1.0f, 3.0f, 5.0f, 8.0f });
            this.comboBox1.SelectedIndex = 0;
            this.comboBox1.Location = new System.Drawing.Point(790, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(62, 21);
            this.comboBox1.TabIndex = 13;
            // 
            // openGLControl
            // 
            this.openGLControl.BackColor = System.Drawing.Color.GhostWhite;
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(3, 84);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(1030, 419);
            this.openGLControl.TabIndex = 14;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this._OnInit);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this._OnDraw);
            this.openGLControl.Resized += new System.EventHandler(this._OnResize);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this._OnMouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this._OnMouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this._OnMouseUp);
            this.openGLControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this._OnKeyUp);
            this.openGLControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this._OnKeyDown);
            // 
            // bt_fill
            // 
            this.bt_fill.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_fill.Location = new System.Drawing.Point(790, 12);
            this.bt_fill.Name = "bt_fill";
            this.bt_fill.Size = new System.Drawing.Size(75, 23);
            this.bt_fill.TabIndex = 15;
            this.bt_fill.Text = "Fill";
            this.bt_fill.UseVisualStyleBackColor = true;
            this.bt_fill.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // bt_line
            // 
            this.bt_line.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_line.Location = new System.Drawing.Point(3, 13);
            this.bt_line.Name = "bt_line";
            this.bt_line.Size = new System.Drawing.Size(75, 23);
            this.bt_line.TabIndex = 1;
            this.bt_line.Text = "Line";
            this.bt_line.UseVisualStyleBackColor = true;
            this.bt_line.Click += new System.EventHandler(this._OnButtonClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 506);
            this.Controls.Add(this.bt_fill);
            this.Controls.Add(this.openGLControl);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.bt_clear);
            this.Controls.Add(this.bt_undo);
            this.Controls.Add(this.bt_ellipse);
            this.Controls.Add(this.bt_circle);
            this.Controls.Add(this.bt_hexagon);
            this.Controls.Add(this.bt_pentagon);
            this.Controls.Add(this.bt_triangle);
            this.Controls.Add(this.bt_rectangle);
            this.Controls.Add(this.bt_polygon);
            this.Controls.Add(this.bt_line);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "Form1";
            this.Text = "Paint - 1712159 - 1712202 - 1712209 - 1712318";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bt_line;
        private System.Windows.Forms.Button bt_polygon;
        private System.Windows.Forms.Button bt_rectangle;
        private System.Windows.Forms.Button bt_triangle;
        private System.Windows.Forms.Button bt_pentagon;
        private System.Windows.Forms.Button bt_hexagon;
        private System.Windows.Forms.Button bt_circle;
        private System.Windows.Forms.Button bt_ellipse;
        private System.Windows.Forms.Button bt_undo;
        private System.Windows.Forms.Button bt_clear;
        private System.Windows.Forms.ComboBox comboBox1;
        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Button bt_fill;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}

