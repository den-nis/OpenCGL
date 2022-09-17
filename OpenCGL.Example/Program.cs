using OpenCGL;
using System;
using System.Diagnostics;

Console.Title = "Hello world";

Window window = new();
Stopwatch deltaTimer = new();

while (true)
{
    Console.CursorVisible = false;

    float delta = (float)deltaTimer.Elapsed.TotalSeconds;
    deltaTimer.Restart();

    window.Canvas.Clear();
    window.Canvas.DrawText(1, 1, $"FPS : {1/delta}");

    window.Canvas.Context.Reset();
    window.Canvas.Context.Translate(window.Width/2, window.Height/2);
    window.Canvas.FillTriangle(0, -5, 10, 5, -10, 5, Color.Green);

    window.UpdateSize();
    window.Render();
}