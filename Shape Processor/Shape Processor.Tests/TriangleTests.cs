[TestFixture]
public class TriangleTests
{
    [TestCase(1, 1, 1, 0.4330127018922193)]
    [TestCase(2, 3, 4, 2.9047375096555625)]
    [TestCase(12, 2, 12, 11.958260743101398)]
    public void AreaCalculation_IsCorrect(double a, double b, double c, double expected)
    {
        var epsilon = 1e-5;
        Assert.That(expected, Is.EqualTo(new Triangle(a, b, c).Area).Within(epsilon));
    }

    [TestCase(0, 0, 0)]
    [TestCase(-12, -2, -12)]
    public void Constructor_WhenLengthNonPositiveOrZero_ShouldThrowException(double a, double b, double c)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Triangle(a, b, c));
    }

    [TestCase(3, 4, 5, true)]
    [TestCase(4, 16, 15.491933384829668, true)]
    [TestCase(2, 2, 3, false)]
    public void IsRight(double a, double b, double c, bool expected)
    {
        Assert.That(new Triangle(a, b, c).IsRight(), Is.EqualTo(expected));
    }
}
