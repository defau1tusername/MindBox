
[TestFixture]
public class CircleTests
{
    private const double epsilon = 1e-5;

    [TestCase(0,0)]
    [TestCase(3.2, 32.169908772759484)]
    [TestCase(39.0, 4778.362426110075)]
    [TestCase(23478324, 1731745452388171.5)]
    public void WhenRadiusIsPositive_AreaIsCorrect(double radius, double expectedArea)
    {
        var circleArea = Figure.ForCircle().WithRadius(radius).GetArea();
        Assert.That(expectedArea, Is.EqualTo(circleArea).Within(epsilon));
    }

    [TestCase(-10)]
    [TestCase(-12391012)]
    public void WhenRadiusNonPositive_ShouldThrowException(double radius)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Figure.ForCircle().WithRadius(radius));
    }
}
