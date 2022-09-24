using OpenCGL;
using System;
using System.Diagnostics;

Console.Title = "Hello world";
Console.CursorVisible = false;

Window window = new();
Stopwatch deltaTimer = new();

float angle = 0;

while (true)
{
    float delta = (float)deltaTimer.Elapsed.TotalSeconds;
    deltaTimer.Restart();

    angle += delta;

    window.Canvas.Clear();
    window.Canvas.Context.Reset();
    window.UpdateSize();

    window.Canvas.Context.Rotate(angle);
    window.Canvas.Context.Translate(window.Canvas.Size / 2);
    window.Canvas.DrawRectanlge(-7, -7, 7, 7, Color.Red);
    
    var center = window.Canvas.Size / 2;

    window.Render();
}