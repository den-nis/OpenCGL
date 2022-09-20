namespace OpenCGL;

public struct Vec2f
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vec2f(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Vec2f operator*(Vec2f p, float magnitude)
	{
        return new Vec2f(p.X * magnitude, p.Y * magnitude);
	}

    public static Vec2f operator+(Vec2f a, Vec2f b)
	{
        return new Vec2f(a.X + b.X, a.Y + b.Y);
	}

    public static Vec2f operator-(Vec2f a, Vec2f b)
	{
        return new Vec2f(a.X - b.X, a.Y - b.Y);
	}

    public static Vec2f operator+(Vec2f a, float b)
	{
        return new Vec2f(a.X * b, a.Y * b);
	}

    public Vec2i ToVec2i() => new((int)X, (int)Y);

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}
