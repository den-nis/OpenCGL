namespace OpenCGL.Drawing;

public partial class Canvas
{
    internal ConsoleChar[] Buffer { get; }

    public int Width { get; }
    public int Height { get; }

    public Canvas(int width, int height)
    {
        Width = width;
        Height = height;
        Buffer = new ConsoleChar[width * height];
    }

    public Canvas(int width, int height, ConsoleChar[] data)
    {
        Width = width;
        Height = height;
        Buffer = data;
    }

    public ConsoleChar this[int x, int y]
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
    public void Fill(ConsoleChar character)
    {
        for (int i = 0; i < Buffer.Length; ++i)
            Buffer[i] = character;
    }

    public void DrawCharacter(Vec2i point, ConsoleChar character) => DrawCharacter(point.X, point.Y, character);
    public void DrawCharacter(int x, int y, ConsoleChar character)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
            return;
        Buffer[x + y * Width] = character;
    }

    public void DrawCharacter(int i, ConsoleChar character)
    {
        if (i < 0 || i >= Buffer.Length)
            return;
        Buffer[i] = character;
    }
}
