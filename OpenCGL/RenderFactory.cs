using OpenCGL.Renderers;
using System;

namespace OpenCGL;

public enum RendererType
{
	NativeRenderer,
	NativeTextRenderer,
	WindowsRenderer,
}

internal static class RendererFactory
{
	public static IRenderer GetRenderer(RendererType rendererType)
	{
		return rendererType switch
		{
			RendererType.NativeRenderer     => new NativeRenderer(),
			RendererType.NativeTextRenderer => new NativeTextRenderer(),
			RendererType.WindowsRenderer    => new WindowsRenderer(),
			_ => throw new ArgumentOutOfRangeException($"Unknown renderer type {rendererType}"),
		};
	}
}
