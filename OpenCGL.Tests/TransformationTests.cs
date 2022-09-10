using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenCGL;

namespace OpenCGL.Tests;

[TestClass]
public class TransformationTests
{
    [TestMethod]
    public void TestTranslate()
    {
        Vec2f position = new Vec2f(10,10);

        Transformation transformation = new();
        transformation.Translate(12,15);

        var result = transformation.Apply(position);

        Assert.AreEqual(22, (int)Math.Round(result.X));
        Assert.AreEqual(25, (int)Math.Round(result.Y));
    }

    [TestMethod]
    public void Test90DegreeRotate()
    {
        Vec2f position = new Vec2f(0,10);

        Transformation transformation = new();
        transformation.Rotate(-(float)Math.PI * .5f);

        var result = transformation.Apply(position);

        Assert.AreEqual(10, (int)Math.Round(result.X));
        Assert.AreEqual(0, (int)Math.Round(result.Y));
    }

    [TestMethod]
    public void TestScale()
    {
        Vec2f position = new Vec2f(5,10);

        Transformation transformation = new();
        transformation.Scale(10,5);

        var result = transformation.Apply(position);

        Assert.AreEqual(50, (int)Math.Round(result.X));
        Assert.AreEqual(50, (int)Math.Round(result.Y));
    }

    [TestMethod]
    public void TestNegateTransformations()
    {
        Vec2f position = new Vec2f(12,56);

        Transformation transformation = new();

        transformation.Translate(53f,13f);
        transformation.Scale(3.55f,5.55f);
        transformation.ShearX(5.3f);
        transformation.Rotate(50.3f);
        transformation.Translate(10,5f);

        transformation.Translate(-10f,-5f);
        transformation.Rotate(-50.3f);
        transformation.ShearX(-5.3f);
        transformation.Scale(1/3.55f,1/5.55f);
        transformation.Translate(-53f,-13f);

        transformation.Translate(1f, 1f);

        var result = transformation.Apply(position);

        Assert.AreEqual(13, (int)Math.Round(result.X));
        Assert.AreEqual(57, (int)Math.Round(result.Y));
    }
}