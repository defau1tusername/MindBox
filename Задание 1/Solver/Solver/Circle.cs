using System;

public class Circle : IFigureWithArea
{
    private readonly double radius;
    private double? area;

    public Circle(double radius)
    {
        if (radius < 0)
            throw new ArgumentOutOfRangeException("Радиус треугольника не может быть меньше нуля");
        this.radius = radius;
    }
    public double Area => area ??= Math.PI * radius * radius;
}