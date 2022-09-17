using OpenCGL;
using OpenCGL.Drawing;
using BenchmarkDotNet.Attributes;

public class CanvasPerformance
{
    private Canvas Window { get; set; } = new Canvas(100,100); 

    [Benchmark]
    public void FillTrianglePerformance()
    {
        Window.FillTriangle(52,2,90,85,4,83, Color.Red);
    }
}