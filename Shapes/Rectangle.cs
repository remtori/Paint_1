using System.Drawing;

namespace Paint_1
{
    class Rectangle : Shape
    {
        public Rectangle(float x, float y, float lw)
            : base(x, y, lw)
        {            
        }

        public override EShape GetShape()
        {
            return EShape.RECTANGLE;
        }

        protected override void ReCalcVerticies()
        {
            // 4 đỉnh của hình chữ nhật là 4 góc của control point nên k cần tính lại
            if (!isInitialDraw) return;            

            verticies.Clear();
            verticies.Add(controlPoints[(int)EPos.TopLeft]);
            verticies.Add(controlPoints[(int)EPos.TopRight]);
            verticies.Add(controlPoints[(int)EPos.BottomRight]);
            verticies.Add(controlPoints[(int)EPos.BottomLeft]);
        }

    }
}
