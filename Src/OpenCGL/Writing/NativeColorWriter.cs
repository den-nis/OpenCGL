using System;
using System.Text;

namespace OpenCGL.Writing
{
    class NativeColorWriter : IWriter
    {
        public void Write(ConsoleChar[] data, int width, int height)
        {
            if (Console.CursorLeft != 0 || Console.CursorTop != 0)
                Console.SetCursorPosition(0, 0);

            var BackgroundColor = Console.BackgroundColor;
            var ForegroundColor = Console.ForegroundColor;

            var OldBackgroundColor = BackgroundColor;
            var OldForegroundColor = ForegroundColor;

            StringBuilder buffer = new StringBuilder();
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
}
