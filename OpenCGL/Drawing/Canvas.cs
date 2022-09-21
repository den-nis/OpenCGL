using System.Collections.Generic;

namespace OpenCGL.Drawing;

public partial class Canvas
{
    internal Stack<Transformation> TransformationStack { get; set; } = new();
    internal Color[] Buffer { get; }

    public Vec2i Size => new Vec2i(Width, Height);

    public int Width { get; }
    public int Height { get; }

    public Transformation Context { get; set; } = new();

    public Canvas(int width, int height)
    {
        Width = width;
        Height = height;
        Buffer = new Color[width * height];
    }

    public Canvas(int width, int height, Color[] data)
    {
        Width = width;
        Height = height;
        Buffer = data;
    }

    public void PushContext() 
    {
        TransformationStack.Push(Context);
        Context = Context.Clone();
    }

    public void PopContext() => Context = TransformationStack.Pop();
    
    public Color this[int x, int y]
    {
        get => Buffer[x + y * Width];
        set => Buffer[x + y * Width] = value;
    }

    public Canvas Copy()
    {
        var copy = new Canvas(Width, Height);
        Buffer.CopyTo(copy.Buffer, 0);
        return copy;
    }

    public void Clear() => Fill(default);
    public void Fill(Color character)
    {
        for (int i = 0; i < Buffer.Length; ++i)
            Buffer[i] = character;
    }

    public void DrawCharacter(Vec2i point, Color character) => DrawCharacter(point.X, point.Y, character);
    public void DrawCharacter(int x, int y, Color character)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
            return;
        Buffer[x + y * Width] = character;
    }

    public void DrawCharacter(int i, Color character)
    {
        if (i < 0 || i >= Buffer.Length)
            return;
        Buffer[i] = character;
    }

    public Color GetColor(Vec2i point) => GetColor(point.X, point.Y);
    public Color GetColor(int x, int y)
    {
        return Buffer[x + y * Width];
    }
}
