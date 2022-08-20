namespace OpenCGL.Renderers;

public interface IRenderer
{
    void Write(ConsoleChar[] buffer, int width, int height);
}
