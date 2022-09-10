namespace OpenCGL;

public struct Vec2i
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vec2i(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vec2f operator*(Vec2i p, float magnitude)
    {
        return new Vec2f(p.X * magnitude, p.Y * magnitude);
    }

    public Vec2f ToVec2f() => new(X, Y);

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}
