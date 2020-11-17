namespace OpenCGL
{
    public struct Vec2i
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vec2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vec2f ToVec2f() => new Vec2f(X, Y);
    }
}
