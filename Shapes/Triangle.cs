namespace Paint_1
{
    class Triangle : Shape
    {
        public Triangle(float x, float y, Color c, float lw)
            : base(x, y, c, lw)
        {
        }

        public override void DrawShape()
        {
            DrawLine(controlPoints[2 * 3 + 2], controlPoints[2 * 3 + 0]);
            DrawLine(controlPoints[0 * 3 + 1], controlPoints[2 * 3 + 2]);
            DrawLine(controlPoints[2 * 3 + 0], controlPoints[0 * 3 + 1]);
        }

        public override EShape GetShape()
        {
            return EShape.TRIANGLE;
        }

        public override bool IsCollideWith(float x, float y)
        {
            Point p = new Point(x, y);
            return IsCollideWithLine(p, controlPoints[2 * 3 + 2], controlPoints[2 * 3 + 0]) ||
                IsCollideWithLine(p, controlPoints[0 * 3 + 1], controlPoints[2 * 3 + 2]) ||
                IsCollideWithLine(p, controlPoints[2 * 3 + 0], controlPoints[0 * 3 + 1]);
        }
    }
}
