namespace Paint_1
{
    enum EShape
    {
        LINE, TRIANGLE, RECTANGLE,
        ELLIPSE, CIRCLE,
        PENTAGON, HEXAGON, POLYGON
    }

    enum ETool
    {

    }

    public enum ECursors
    {
        SizeNWSE, SizeNESW, SizeWE, SizeNS, SizeAll,
        RotateN, RotateE, RotateS, RotateW, RotateNW, RotateNE, RotateSW, RotateSE,
        Fill
    }

    public enum EPos
    {
        None = -1,
        PositionOffset = -2, // Độ lệch của hình
        Rotation = -3,       // Cùng với Center điểu khiển góc xoay của hình
        RotationOffset = -4, // Độ lệch của Rotation so với Center

        // 8 điểm điều khiển ở 8 hướng
        // Vì những điểm này sẽ dùng phép "/" và "%" 
        // để lấy hàng và cột nó thuộc về nên những con số này bắt buộc phải như sau
        // vd: BottomLeft = 6, nghĩa là nó nằm ở hàng 6 / 3 = 2, cột 6 % 3 = 0
        TopLeft    = 0, Top    = 1, TopRight    = 2,
        Left       = 3, Center = 4, Right       = 5,
        BottomLeft = 6, Bottom = 7, BottomRight = 8
    }
}
