using System.Drawing;

namespace Paint_1
{
    abstract class SimpleShape : Shape
    {
        public SimpleShape(float x, float y, float lw, int ctrlLvl = 2)
            : base(x, y, lw, ctrlLvl)
        {
        }

        protected abstract Point[] GetVerticies();

        public override void DrawShape()
        {
            Point[] verticies = GetVerticies();
            for (int i = 1; i < verticies.Length; i++)
                DrawLine(verticies[i - 1], verticies[i]);

            DrawLine(verticies[0], verticies[verticies.Length - 1]);
        }

        public override bool IsCollideWith(float x, float y)
        {
            Point m = ScreenToWorld(new Point(x, y));

            Point[] verticies = GetVerticies();
            for (int i = 1; i < verticies.Length; i++)
                if (IsCollideWithLine(m, verticies[i - 1], verticies[i]))
                    return true;            

            return IsCollideWithLine(m, verticies[0], verticies[verticies.Length - 1]);
        }
    }
}
