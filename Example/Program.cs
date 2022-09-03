using OpenCGL;
using System;

Console.Title = "Hello world";

Window window = new(RendererType.NativeRenderer);

while (true)
{
    Console.CursorVisible = false;

    var canvas = window.GetCanvas();

    canvas.Clear();

    canvas.FillCircle(window.Width / 2, window.Height / 2, Math.Min(window.Width, window.Height) / 2, ConsoleChar.Red);

    window.Render();
}
