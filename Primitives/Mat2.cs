using System;

namespace Paint_1
{
    class Mat2
    {
        public float[] values;

        // Khởi tạo ma trận là ma trận đơn vị
        public Mat2()
        {
            values = new float[4];
            ToIdentity();
        }

        public void ToIdentity()
        {
            values[0] = 1.0f;
            values[1] = 0.0f;
            values[2] = 0.0f;
            values[3] = 1.0f;
        }

        public void ToRotation(double angle)
        {
            float sinA = (float)Math.Sin(angle);
            float cosA = (float)Math.Cos(angle);

            values[0] = cosA;
            values[1] = -sinA;
            values[2] = sinA;
            values[3] = cosA;
        }

        // Sinh ma trận nghịch đảo của ma trận hiện tại
        public Mat2 GetInverse()
        {
            Mat2 r = new Mat2();
            r.values[0] = values[3];
            r.values[3] = values[0];
            r.values[1] = -values[1];
            r.values[2] = -values[2];

            return r;
        }
    }
}
