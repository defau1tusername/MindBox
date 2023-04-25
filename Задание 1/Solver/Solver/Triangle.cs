using System;

public class Triangle : IFigureWithArea, IFigureWithPerimeter
{
    private readonly double a;
    private readonly double b;
    private readonly double c;
    private double? area;
    private double? perimeter;

    public Triangle(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentOutOfRangeException("Длины сторон не могут быть отрицательными или равными нулю");
        else if (a + b < c || b + c < a || a + c < b)
            throw new ArgumentException("Сумма двух сторон не может быть меньше третьей стороны");
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public double Area
    {
        get
        {
            var semiperimeter = Perimeter / 2;
            return area 
                ??= Math.Sqrt(semiperimeter
                    * (semiperimeter - a)
                    * (semiperimeter - b) 
                    * (semiperimeter - c));
        }
    }

    public double Perimeter => perimeter ??= a + b + c;

    public bool IsRight()
    {
        var epsilon = 1e-5;
        return EqualTo(c * c, a * a + b * b, epsilon)
            || EqualTo(a * a, c * c + b * b, epsilon)
            || EqualTo(b * b, c * c + a * a, epsilon);
    }

    public static bool EqualTo(double value1, double value2, double epsilon) =>
        Math.Abs(value1 - value2) < epsilon;
}

