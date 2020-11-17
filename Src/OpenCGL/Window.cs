using OpenCGL.Drawing;
using OpenCGL.Writing;

namespace OpenCGL
{
    public class Window
    {
        public Canvas Canvas { get; set; }
        public int RendererWidth { get; set; }
        public int RendererHeight { get; set; }

        private IWriter Writer { get; }

        public Window(int width, int height, WriterType type = WriterType.Win32Writer) : this(width, height, WriterFactory.GetRenderer(type)) { }

        public Window(int width, int height, IWriter writer)
        {
            Canvas = new Canvas(width, height);

            Writer = writer;

            RendererWidth = width;
            RendererHeight = height;
        }

        public void Render()
        {
            Writer.Write(Canvas.CanvasData, Canvas.Width, Canvas.Height);
        }
    }
}