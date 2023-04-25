using HtmlAgilityPack;
using System.Diagnostics;

public class GetPositionTests
{
    private class AvitoClientTest : IAvitoClient
    {
        private readonly string searchResultPath =
            @"C:\Users\Daniil\Desktop\FirstWebApplication\Gitlab\avitoHelper\AvitoAnalytics.UnitTests\SetupFiles\Поисковая выдача.html";
        public Task<HtmlDocument> GetSearchResultsPageAsync(Ad ad)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(searchResultPath);
            return Task.FromResult(htmlDoc);
        }

    }

    [Test]
    public async Task GetPosition_IsCorrect()
    {
        var adId = "2399770647";
        var expectedPosition = 22;
        var avitoInformationProvider = new AvitoInformationProvider(new AvitoClientTest());
        var testAd = new Ad() { AdID = adId };
        Assert.That(await avitoInformationProvider.GetPositionAsync(testAd), Is.EqualTo(expectedPosition));
    }

}
