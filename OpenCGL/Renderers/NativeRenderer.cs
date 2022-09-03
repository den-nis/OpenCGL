using System;
using System.IO;
using System.Text;

namespace OpenCGL.Renderers;

internal class NativeRenderer : IRenderer
{
    public void Write(Color[] data, int width, int height)
    {
        if (width == 0 || height == 0)
            return;

        try
        {
            Console.SetCursorPosition(0, 0);
	    } catch (IOException) { }

        var BackgroundColor = Console.BackgroundColor;
        var ForegroundColor = Console.ForegroundColor;

        var OldBackgroundColor = BackgroundColor;
        var OldForegroundColor = ForegroundColor;

        StringBuilder buffer = new();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var write = data[x + y * width];

                if (ForegroundColor != write.ForegroundColor || BackgroundColor != write.BackgroundColor)
                {
                    Console.Write(buffer);
                    buffer.Clear();

                    if (ForegroundColor != write.ForegroundColor)
                    {
                        Console.ForegroundColor = write.ForegroundColor;
                        ForegroundColor = write.ForegroundColor;
                    }

                    if (BackgroundColor != write.BackgroundColor)
                    {
                        Console.BackgroundColor = write.BackgroundColor;
                        BackgroundColor = write.BackgroundColor;
                    }
                }

                buffer.Append(write == default ? ' ' : write.Character);
            }

            if (y < height - 1)
            {
                buffer.AppendLine();
            }
		}

        Console.Write(buffer);
        Console.BackgroundColor = OldBackgroundColor;
        Console.ForegroundColor = OldForegroundColor;
    }
}
