using System;
using System.Text;

namespace OpenCGL.Writing
{
    class NativeWriter : IWriter
    {
        public void Write(ConsoleChar[] data, int width, int height)
        {
            if (Console.CursorLeft != 0 || Console.CursorTop != 0)
                Console.SetCursorPosition(0, 0);

            StringBuilder sb = new StringBuilder();
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
}
