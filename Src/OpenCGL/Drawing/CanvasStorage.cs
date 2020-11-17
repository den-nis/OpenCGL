using System.Collections.Generic;
using System.IO;

namespace OpenCGL.Drawing
{
    public partial class Canvas
    {
        public byte[] ToBinary()
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(Width);
            bw.Write(Height);

            foreach (var c in CanvasData)
            {
                bw.Write(c.Character);
                bw.Write(c.Attributes);
            }

            return ms.ToArray();
        }

        public static Canvas FromBinary(byte[] data)
        {
            using MemoryStream ms = new MemoryStream(data);
            using BinaryReader bw = new BinaryReader(ms);

            int width = bw.ReadInt32();
            int height = bw.ReadInt32();

            List<ConsoleChar> consoleChars = new List<ConsoleChar>();

            for (int i = 0; i < width * height; i++)
            {
                var character = bw.ReadChar();
                var attributes = bw.ReadInt16();

                consoleChars.Add(new ConsoleChar(attributes, character));
            }

            var canvas = new Canvas(width, height, consoleChars.ToArray());

            return canvas;
        }
    }
}
