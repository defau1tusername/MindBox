[TestFixture]
public class TriangleTests
{
    [TestCase(1, 1, 1, 0.4330127018922193)]
    [TestCase(2, 3, 4, 2.9047375096555625)]
    [TestCase(12, 2, 12, 11.958260743101398)]
    public void AreaCalculation_IsCorrect(double a, double b, double c, double expected)
    {
        Assert.AreEqual(expected, new Triangle(a, b, c).Area);
    }

    [TestCase(0, 0, 0)]
    [TestCase(-12, -2, -12)]
    public void Constructor_WhenRadiusNonPositiveOrZero_ShouldThrowException(double a, double b, double c)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Triangle(a, b, c));
    }
}
