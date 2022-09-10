using OpenCGL;
using BenchmarkDotNet.Attributes;

public class TransformationPerformance
{
    Transformation transformation = new();
    Vec2f vector = new Vec2f(1,1);

    [Benchmark]
    public void ApplyPerformance()
    {
        transformation.Apply(vector);
    }

    [Benchmark]
    public void RotationPerformance()
    {
        transformation.Rotate(90);
    }

    [Benchmark]
    public void TranslatePerformance()
    {
        transformation.Translate(5,5);
    }
}