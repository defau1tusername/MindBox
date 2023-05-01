using System;

public static class Figure
{
    public static ICircleBuilder ForCircle() => new CircleBuilder();
    public static ITriangleBuilder ForTriangle() => new TriangleBuilder();
}


internal class CircleBuilder : ICircleBuilder
{
    public ICircleInfo WithRadius(double radius)
    {
        if (radius < 0)
            throw new ArgumentOutOfRangeException("Радиус окружности не может быть меньше нуля");
        return new CircleInfo(radius);
    }
}

internal class TriangleBuilder : ITriangleBuilder
{
    public ITriangleInfo WithSides(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentOutOfRangeException("Длины сторон не могут быть отрицательными или равными нулю");
        else if (a + b < c || b + c < a || a + c < b)
            throw new ArgumentException("Сумма двух сторон не может быть меньше третьей стороны");
        return new TriangleInfo(a, b, c);
    }
}

internal class CircleInfo : ICircleInfo
{
    private readonly double radius;
    internal CircleInfo(double radius) => this.radius = radius;

    public double GetArea() =>
        Math.PI * radius * radius;
}

internal class TriangleInfo : ITriangleInfo
{
    private const double epsilon = 1e-5;
    private readonly double a;
    private readonly double b;
    private readonly double c;

    public TriangleInfo(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public double GetArea()
    {
        var semiperimeter = GetPerimeter() / 2;
        return Math.Sqrt(semiperimeter
                * (semiperimeter - a)
                * (semiperimeter - b)
                * (semiperimeter - c));
    }

    public bool CheckIsRight()
    {
        return Equals(c * c, a * a + b * b, epsilon)
            || Equals(a * a, c * c + b * b, epsilon)
            || Equals(b * b, c * c + a * a, epsilon);
    }

    private double GetPerimeter() => a + b + c;

    private static bool Equals(double value1, double value2, double epsilon) =>
        Math.Abs(value1 - value2) < epsilon;
}

