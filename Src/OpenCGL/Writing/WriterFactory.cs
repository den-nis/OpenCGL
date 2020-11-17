using System;

namespace OpenCGL.Writing
{
    public enum WriterType
    {
        NativeColorWriter,
        NativeWriter,
        Win32Writer,
    }

    static class WriterFactory
    {
        public static IWriter GetRenderer(WriterType rendererType)
        {
            return rendererType switch
            {
                WriterType.NativeColorWriter => new NativeColorWriter(),
                WriterType.NativeWriter => new NativeWriter(),
                WriterType.Win32Writer => new Win32Writer(),
                _ => throw new ArgumentOutOfRangeException($"Unknown renderer type {rendererType}"),
            };
        }
    }
}
