
using HtmlAgilityPack;
using Serilog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public class AvitoInformationProvider
{
    private readonly IAvitoClient avitoClient;
    private readonly int numberOfAttempts;
    private static ILogger logger;
    public AvitoInformationProvider(IAvitoClient avitoClient)
    {
        this.avitoClient  = avitoClient;
        numberOfAttempts = 3;
        logger = Log.Logger.ForContext<AvitoInformationProvider>();
    }

    public async Task<int> GetPositionAsync(Ad ad)
    {
        var currentNumberOfAttempts = numberOfAttempts;
        var position = 0;
        var timer = new Stopwatch();
        timer.Start();
        while (currentNumberOfAttempts-- > 0)
        {
            var searchResultsHtml = await avitoClient.GetSearchResultsPageAsync(ad);
            if (searchResultsHtml == null)
            {
                await RandomDelayAsync();
                continue;
            }

            position = Scraping.GetPosition(ad, searchResultsHtml);
            if (position != 0 && position != 50) break;
            else position = 0;

            await RandomDelayAsync();
        }
        timer.Stop();
        LogInformation(position, timer, ad, currentNumberOfAttempts);
        return position;
    }

    public async Task<string> GetCityAsync(Ad ad)
    {
        var currentNumberOfAttempts = numberOfAttempts;
        var searchResultsHtml = new HtmlDocument();
        while (currentNumberOfAttempts-- > 0)
        {
            searchResultsHtml = await avitoClient.GetSearchResultsPageAsync(ad);
            if (searchResultsHtml == null)
            {
                await RandomDelayAsync();
                continue;
            }
        }
        if (searchResultsHtml == null) return null;
        return Scraping.GetCity(searchResultsHtml);
    }

    private void LogInformation(int position, Stopwatch timer, Ad ad, int remainingNumberOfAttempts)
    {
        if (position == 0 || position == 50) logger.Information($"[GetPosition] Bad request " +
            $"| PostID: {ad.PostID} " +
            $"| Time: {timer.Elapsed.ToString(@"m\:ss\.fff")} " +
            $"| Number of attempts: {numberOfAttempts - remainingNumberOfAttempts}");
        else
            logger.Information($"[GetPosition] Successful request " +
                $"| PostID: {ad.PostID} " +
                $"| Time: {timer.Elapsed.ToString(@"m\:ss\.fff")} " +
                $"| Number of attempts: {numberOfAttempts - remainingNumberOfAttempts}");
    }

    private async Task RandomDelayAsync() => await Task.Delay(new Random().Next(3000, 5000));

}

