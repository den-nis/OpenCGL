using static OpenCGL.WindowsSystemCalls;

namespace OpenCGL.Renderers;

internal class WindowsRenderer : IRenderer
{
    public void Write(ConsoleChar[] consoleChars, int width, int height)
    {
        SMALL_RECT region = new()
		{
            Left = 0,
            Top = 0,
            Right = (short)width,
            Bottom = (short)height,
        };

        WriteConsoleOutput(ConsoleHandle, consoleChars, new COORD { X = (short)width, Y = (short)height }, new COORD { X = 0, Y = 0 }, ref region);
    }
}