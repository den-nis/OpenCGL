namespace OpenCGL.Renderers;

public interface IRenderer
{
    void Write(Color[] buffer, int width, int height);
}
