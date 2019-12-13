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
            this.bt_clear = new System.Windows.Forms.Button();
            this.lineWidth = new System.Windows.Forms.ComboBox();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.bt_line = new System.Windows.Forms.Button();
            this.bt_color = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_fillColor = new System.Windows.Forms.Button();
            this.fillMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.avgFillTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_polygon
            // 
            this.bt_polygon.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_polygon.Image = global::Paint_1.Properties.Resources.polygon;
            this.bt_polygon.Location = new System.Drawing.Point(56, 48);
            this.bt_polygon.Name = "bt_polygon";
            this.bt_polygon.Size = new System.Drawing.Size(38, 38);
            this.bt_polygon.TabIndex = 2;
            this.bt_polygon.UseVisualStyleBackColor = true;
            this.bt_polygon.Click += new System.EventHandler(this._OnBtnClicked);
            // 
            // bt_rectangle
            // 
            this.bt_rectangle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_rectangle.Image = global::Paint_1.Properties.Resources.rectangle;
            this.bt_rectangle.Location = new System.Drawing.Point(100, 4);
            this.bt_rectangle.Name = "bt_rectangle";
            this.bt_rectangle.Size = new System.Drawing.Size(38, 38);
            this.bt_rectangle.TabIndex = 3;
            this.bt_rectangle.UseVisualStyleBackColor = true;
            this.bt_rectangle.Click += new System.EventHandler(this._OnBtnClicked);
            // 
            // bt_triangle
            // 
            this.bt_triangle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_triangle.Image = global::Paint_1.Properties.Resources.triangle;
            this.bt_triangle.Location = new System.Drawing.Point(56, 4);
            this.bt_triangle.Name = "bt_triangle";
            this.bt_triangle.Size = new System.Drawing.Size(38, 38);
            this.bt_triangle.TabIndex = 4;
            this.bt_triangle.UseVisualStyleBackColor = true;
            this.bt_triangle.Click += new System.EventHandler(this._OnBtnClicked);
            // 
            // bt_pentagon
            // 
            this.bt_pentagon.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_pentagon.Image = global::Paint_1.Properties.Resources.pentagon;
            this.bt_pentagon.Location = new System.Drawing.Point(144, 3);
            this.bt_pentagon.Name = "bt_pentagon";
            this.bt_pentagon.Size = new System.Drawing.Size(38, 38);
            this.bt_pentagon.TabIndex = 5;
            this.bt_pentagon.UseVisualStyleBackColor = true;
            this.bt_pentagon.Click += new System.EventHandler(this._OnBtnClicked);
            // 
            // bt_hexagon
            // 
            this.bt_hexagon.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_hexagon.Image = global::Paint_1.Properties.Resources.hexagon;
            this.bt_hexagon.Location = new System.Drawing.Point(12, 48);
            this.bt_hexagon.Name = "bt_hexagon";
            this.bt_hexagon.Size = new System.Drawing.Size(38, 38);
            this.bt_hexagon.TabIndex = 6;
            this.bt_hexagon.UseVisualStyleBackColor = true;
            this.bt_hexagon.Click += new System.EventHandler(this._OnBtnClicked);
            // 
            // bt_circle
            // 
            this.bt_circle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_circle.Image = global::Paint_1.Properties.Resources.circle;
            this.bt_circle.Location = new System.Drawing.Point(144, 48);
            this.bt_circle.Name = "bt_circle";
            this.bt_circle.Size = new System.Drawing.Size(38, 38);
            this.bt_circle.TabIndex = 7;
            this.bt_circle.UseVisualStyleBackColor = true;
            this.bt_circle.Click += new System.EventHandler(this._OnBtnClicked);
            // 
            // bt_ellipse
            // 
            this.bt_ellipse.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_ellipse.Image = global::Paint_1.Properties.Resources.ellipse;
            this.bt_ellipse.Location = new System.Drawing.Point(100, 48);
            this.bt_ellipse.Name = "bt_ellipse";
            this.bt_ellipse.Size = new System.Drawing.Size(38, 38);
            this.bt_ellipse.TabIndex = 8;
            this.bt_ellipse.UseVisualStyleBackColor = true;
            this.bt_ellipse.Click += new System.EventHandler(this._OnBtnClicked);
            // 
            // bt_clear
            // 
            this.bt_clear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_clear.Location = new System.Drawing.Point(198, 59);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(104, 23);
            this.bt_clear.TabIndex = 12;
            this.bt_clear.Text = "Clear Screen";
            this.bt_clear.UseVisualStyleBackColor = true;
            this.bt_clear.Click += new System.EventHandler(this._OnBtnClicked);
            // 
            // lineWidth
            // 
            this.lineWidth.FormattingEnabled = true;
            this.lineWidth.Items.AddRange(new object[] {
            1F,
            3F,
            5F,
            8F});
            this.lineWidth.Location = new System.Drawing.Point(259, 32);
            this.lineWidth.Name = "lineWidth";
            this.lineWidth.Size = new System.Drawing.Size(43, 21);
            this.lineWidth.TabIndex = 13;
            // 
            // openGLControl
            // 
            this.openGLControl.BackColor = System.Drawing.Color.GhostWhite;
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(2, 92);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(1031, 411);
            this.openGLControl.TabIndex = 14;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this._OnInit);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this._OnDraw);
            this.openGLControl.Resized += new System.EventHandler(this._OnGLResize);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this._OnMouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this._OnMouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this._OnMouseUp);
            // 
            // bt_line
            // 
            this.bt_line.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_line.Image = global::Paint_1.Properties.Resources.line;
            this.bt_line.Location = new System.Drawing.Point(12, 4);
            this.bt_line.Name = "bt_line";
            this.bt_line.Size = new System.Drawing.Size(38, 38);
            this.bt_line.TabIndex = 1;
            this.bt_line.UseVisualStyleBackColor = true;
            this.bt_line.Click += new System.EventHandler(this._OnBtnClicked);
            // 
            // bt_color
            // 
            this.bt_color.BackColor = System.Drawing.Color.Black;
            this.bt_color.ForeColor = System.Drawing.Color.White;
            this.bt_color.Location = new System.Drawing.Point(259, 4);
            this.bt_color.Name = "bt_color";
            this.bt_color.Size = new System.Drawing.Size(43, 23);
            this.bt_color.TabIndex = 16;
            this.bt_color.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bt_color.UseVisualStyleBackColor = false;
            this.bt_color.Click += new System.EventHandler(this._OnToolClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(195, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Line Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(195, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Color";
            // 
            // bt_fillColor
            // 
            this.bt_fillColor.BackColor = System.Drawing.Color.White;
            this.bt_fillColor.ForeColor = System.Drawing.Color.White;
            this.bt_fillColor.Location = new System.Drawing.Point(411, 3);
            this.bt_fillColor.Name = "bt_fillColor";
            this.bt_fillColor.Size = new System.Drawing.Size(43, 23);
            this.bt_fillColor.TabIndex = 19;
            this.bt_fillColor.UseVisualStyleBackColor = false;
            this.bt_fillColor.Click += new System.EventHandler(this._OnToolClicked);
            // 
            // fillMode
            // 
            this.fillMode.FormattingEnabled = true;
            this.fillMode.Items.AddRange(new object[] {
            "Flood Fill",
            "Scanline"});
            this.fillMode.Location = new System.Drawing.Point(387, 35);
            this.fillMode.Name = "fillMode";
            this.fillMode.Size = new System.Drawing.Size(67, 21);
            this.fillMode.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(329, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Fill Color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(329, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Fill Mode";
            // 
            // avgFillTime
            // 
            this.avgFillTime.AutoSize = true;
            this.avgFillTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.avgFillTime.Location = new System.Drawing.Point(332, 68);
            this.avgFillTime.Name = "avgFillTime";
            this.avgFillTime.Size = new System.Drawing.Size(67, 13);
            this.avgFillTime.TabIndex = 23;
            this.avgFillTime.Text = "Avg Fill Time";
            // 
            // Paint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 506);
            this.Controls.Add(this.avgFillTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fillMode);
            this.Controls.Add(this.bt_fillColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_color);
            this.Controls.Add(this.openGLControl);
            this.Controls.Add(this.lineWidth);
            this.Controls.Add(this.bt_clear);
            this.Controls.Add(this.bt_ellipse);
            this.Controls.Add(this.bt_circle);
            this.Controls.Add(this.bt_hexagon);
            this.Controls.Add(this.bt_pentagon);
            this.Controls.Add(this.bt_triangle);
            this.Controls.Add(this.bt_rectangle);
            this.Controls.Add(this.bt_polygon);
            this.Controls.Add(this.bt_line);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "Paint";
            this.Text = "Paint - 1712159 - 1712202 - 1712209 - 1712318";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button bt_clear;
        private System.Windows.Forms.ComboBox lineWidth;
        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Button bt_color;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_fillColor;
        private System.Windows.Forms.ComboBox fillMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label avgFillTime;
    }
}

