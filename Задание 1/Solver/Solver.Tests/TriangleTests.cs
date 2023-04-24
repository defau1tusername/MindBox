[TestFixture]
public class RightCircleTests
{
    [TestCase(3, 4, 5, true)]
    [TestCase(4, 16, 15.491933384829668, true)]
    [TestCase(2, 2, 3, false)]
    public void IsRight(double a, double b, double c, bool isRight)
    {
        Assert.AreEqual(isRight, new Triangle(a, b, c).IsRight());
    }
}
