using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenCGL;
using OpenCGL.Drawing;

namespace OpenCGL.Tests;

[TestClass]
public class CanvasTests
{
    [TestMethod]
    public void TestTriangleWinding()
    {
        Vec2f left = new(-5,5);
        Vec2f right = new(5,5);
        Vec2f top = new(0,-5);

        Canvas canvas = new Canvas(20,20);

        canvas.FillTriangle(left,right,top, Color.Blue);
        Assert.IsTrue(CountColor(canvas, Color.Blue) > 0);

        canvas.FillTriangle(right,left,top, Color.Green);
        Assert.IsTrue(CountColor(canvas, Color.Green) > 0);
    }

    [TestMethod]
    public void TestTriangleOverlap()
    {
        Vec2f[] main = new Vec2f[] {
            new Vec2f(10,10),
            new Vec2f(20,15),
            new Vec2f(12,20),
        };
 
        Vec2f[,] tests = {
            {  main[0], new Vec2f(15,0),  main[1] },
            {  main[1], new Vec2f(25,20), main[2] },
            {  main[2], new Vec2f(5, 15),  main[0] },
        };

        Canvas canvas = new Canvas(30,30);

        for (int i = 0; i < tests.GetLength(0); i++) { canvas.FillTriangle(tests[i,0], tests[i,1], tests[i,2], Color.Green); }
        var expectedGreen = CountColor(canvas, Color.Green);
        canvas.Clear();

        canvas.FillTriangle(main[0], main[1], main[2], Color.Blue);
        var expectedBlue = CountColor(canvas, Color.Blue);

        for (int i = 0; i < tests.GetLength(0); i++) { canvas.FillTriangle(tests[i,0], tests[i,1], tests[i,2], Color.Green); }

        Assert.IsTrue(expectedBlue > 10);
        Assert.IsTrue(expectedGreen > 10);
        Assert.AreEqual(expectedBlue, CountColor(canvas, Color.Blue));
        Assert.AreEqual(expectedGreen, CountColor(canvas, Color.Green));
    }

    private int CountColor(Canvas canvas, Color color)
    {
        int count = 0;
        for (int y = 0; y < canvas.Height; y++)
        {
            for (int x = 0; x < canvas.Width; x++)
            {
                if (canvas.GetColor(x,y) == color)
                {
                    count++;
                }
            }
        }
        return count;
    }
}