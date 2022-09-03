using System;
using System.IO;
using System.Text;

namespace OpenCGL.Renderers;

internal class NativeTextRenderer : IRenderer
{
    public void Write(Color[] data, int width, int height)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;

        if (width == 0 || height == 0)
            return;

        try
        {
            Console.SetCursorPosition(0, 0);
        }
        catch (IOException) { }

        StringBuilder sb = new();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var character = data[x + y * width];
                sb.Append(character == default ? ' ' : character.Character);
            }

            if (y < height-1)
            {
                sb.AppendLine();
            }
        }
        Console.Write(sb);
    }
}
