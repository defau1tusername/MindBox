[TestFixture]
public class TriangleTests
{
    private const double epsilon = 1e-5;

    [TestCase(1, 1, 1, 0.4330127018922193)]
    [TestCase(2, 3, 4, 2.9047375096555625)]
    [TestCase(12, 2, 12, 11.958260743101398)]
    public void WhenSidesAreValid_AreaIsCorrect(double a, double b, double c, double expectedArea)
    {
        var triangleArea = Figure.ForTriangle().WithSides(a, b, c).GetArea();
        Assert.That(expectedArea, Is.EqualTo(triangleArea).Within(epsilon));
    }

    [TestCase(0, 0, 0)]
    [TestCase(-12, -2, -12)]
    public void WhenLengthNonPositiveOrZero_ShouldThrowException(double a, double b, double c)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Figure.ForTriangle().WithSides(a, b, c));
    }

    [TestCase(3, 4, 5, true)]
    [TestCase(4, 16, 15.491933384829668, true)]
    [TestCase(2, 2, 3, false)]
    public void WhenSidesAreValid_RightCheckIsCorrect(double a, double b, double c, bool expected)
    {
        var isRightActual = Figure.ForTriangle().WithSides(a, b, c).CheckIsRight();
        Assert.That(isRightActual, Is.EqualTo(expected));
    }
}
