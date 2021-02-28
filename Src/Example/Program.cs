using OpenCGL;
using OpenCGL.Settings;
using System;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 50, height = 50;

            Console.Title = "Hello world";
            Console.CursorVisible = false;
            ConsoleSettings.ApplyDefaultSettings();
            ConsoleSettings.SetSize(width, height);

            Window window = new Window(width, height);

            while (true)
            {
                window.Canvas.Clear();
                window.Canvas.FillCircle(width/2, height/2, 10, ConsoleChar.Red);
                window.Render();
            }
        }
    }
}
