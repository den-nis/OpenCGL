using OpenCGL.Drawing;
using OpenCGL.Renderers;
using System;

namespace OpenCGL;

public class Window
{
    public event Action OnWindowResized;

    public int MaxWidth { get; set; } = int.MaxValue;
    public int MaxHeight { get; set; } = int.MaxValue;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool ResizeToWindow { get; set; }

    private Canvas canvas;
    private IRenderer Renderer { get; }

    public Window(RendererType type = RendererType.WindowsRenderer) : this(RendererFactory.GetRenderer(type)) 
    {
    }

    public Window(IRenderer renderer)
    {
        Renderer = renderer;
        UpdateWidthAndHeight();
    }

    public Canvas GetCanvas()
    {
        int oldWidth = Width, oldHeight = Height;
        UpdateWidthAndHeight();

        if (oldWidth != Width || oldHeight != Height || canvas == null)
        {
            canvas = new Canvas(Width, Height);
            OnWindowResized?.Invoke();
        }

        return canvas;
	}

    private void UpdateWidthAndHeight()
    {
        Width = Math.Max(0, Math.Min(MaxWidth, Console.WindowWidth));
        Height = Math.Max(0, Math.Min(MaxHeight, Console.WindowHeight));
    }

    public void Render()
    {
        Renderer.Write(canvas.Buffer, canvas.Width, canvas.Height);
    }
}