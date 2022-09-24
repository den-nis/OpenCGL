using OpenCGL.Drawing;
using OpenCGL.Renderers;
using System;
using System.Runtime.InteropServices;

namespace OpenCGL;

public class Window
{
    public event Action OnWindowResized;

    public int MaxWidth { get; set; } = int.MaxValue;
    public int MaxHeight { get; set; } = int.MaxValue;

    public int Width { get; private set; } = -1;
    public int Height { get; private set; } = -1;
    public bool ResetContextOnRender { get; set; } = true;


    public Canvas Canvas { get; set; }
    private IRenderer Renderer { get; }

    public Window(RendererType type) : this(RendererFactory.GetRenderer(type)) 
    {
    }

    public Window() : this(GetOptimalRenderer())
    {
    }

    public Window(IRenderer renderer)
    {
        Renderer = renderer;
        UpdateSize();
    }

    public void UpdateSize()
    {
        int oldWidth = Width, oldHeight = Height;
        UpdateWidthAndHeight();

        if (oldWidth != Width || oldHeight != Height || Canvas == null)
        {
            var context = Canvas?.Context;
            Canvas = new Canvas(Width, Height);
            Canvas.Context ??= context;
            OnWindowResized?.Invoke();
        }
	}

    public void Render()
    {
        Renderer.Write(Canvas.Buffer, Canvas.Width, Canvas.Height);

        if (ResetContextOnRender)
        {
            Canvas.Context.Reset();
        }
    }

    private void UpdateWidthAndHeight()
    {
        Width = Math.Max(0, Math.Min(MaxWidth, Console.WindowWidth));
        Height = Math.Max(0, Math.Min(MaxHeight, Console.WindowHeight));
    }

    private static RendererType GetOptimalRenderer()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) 
        {
            return RendererType.NativeRenderer;
        }

        return RendererType.NativeRenderer;
    }
}