namespace OpenCGL
{
    public struct Vec2f
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vec2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vec2i ToVec2i() => new Vec2i((int)X, (int)Y);
    }
}
