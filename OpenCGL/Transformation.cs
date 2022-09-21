using System;

namespace OpenCGL;

public class Transformation
{
    private float[,] matrix = new float[,]
    {
        {1,0,0},
        {0,1,0},
        {0,0,1}
    };

    public void Reset() => matrix = new float[,]
    {
        {1,0,0},
        {0,1,0},
        {0,0,1}
    };

    public Transformation Clone() 
    {
        return new Transformation
        {
            matrix = matrix
        };
    }

    public void Rotate(float radians)
    {
        var c = (float)Math.Cos(radians);
        var s = (float)Math.Sin(radians);
        matrix = MultiplyMatrix3x3(new float[,] {
            {c, -s, 0},
            {s,  c, 0},
            {0,  0, 1},
        }, matrix);
    }

    public void ShearX(float radians)
    {
        var t = (float)Math.Tan(radians);
        matrix = MultiplyMatrix3x3(new float[,] {
            {1, t, 0},
            {0, 1, 0},
            {0, 0, 1},
        }, matrix);
    }

    public void ShearY(float radians)
    {
        var t = (float)Math.Tan(radians);
        matrix = MultiplyMatrix3x3(new float[,] {
            {1, 0, 0},
            {t, 1, 0},
            {0, 0, 1},
        }, matrix);
    }

    public void Translate(Vec2f t) => Translate(t.X, t.Y);
    public void Translate(float x, float y)
    {
        matrix = MultiplyMatrix3x3(new float[,] {
            {1, 0, x},
            {0, 1, y},
            {0, 0, 1},
        }, matrix);
    }

    public void Scale(Vec2f scale) => Scale(scale.X, scale.Y);
    public void Scale(float x, float y)
    {
        matrix = MultiplyMatrix3x3(new float[,] {
            {x, 0, 0},
            {0, y, 0},
            {0, 0, 1},
        }, matrix);
    }

    public Vec2f Apply(float x, float y) => Apply(new Vec2f(x, y));
    public Vec2f Apply(Vec2f v)
    {
        return new Vec2f(
            matrix[0, 0] * v.X + matrix[0, 1] * v.Y + matrix[0, 2],
            matrix[1, 0] * v.X + matrix[1, 1] * v.Y + matrix[1, 2]
        );
    }

    private static float[,] MultiplyMatrix3x3(float[,] a, float[,] b)
    {
        float[,] result = new float[3, 3];

        for (int i = 0; i < 3; i++)
        {
            result[i, 0] = a[i, 0] * b[0, 0] + a[i, 1] * b[1, 0] + a[i, 2] * b[2, 0];
            result[i, 1] = a[i, 0] * b[0, 1] + a[i, 1] * b[1, 1] + a[i, 2] * b[2, 1];
            result[i, 2] = a[i, 0] * b[0, 2] + a[i, 1] * b[1, 2] + a[i, 2] * b[2, 2];
        }

        return result;
    }
}
