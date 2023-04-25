using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;


public class PositionsUpdateBgService : BackgroundService
{
    private readonly DBService dbService;
    private readonly AvitoInformationProvider avitoInformationProvider;

    public PositionsUpdateBgService(DBService dbService, AvitoInformationProvider avitoInformationProvider)
    {
        this.dbService = dbService;
        this.avitoInformationProvider = avitoInformationProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            await Task.Delay((60 - DateTime.Now.Minute) * 60000);
            await Task.Run(() => PositionsUpdateAsync());
        }
    }

    private async Task PositionsUpdateAsync()
    { 
        var randomDelay = new Random();
        await foreach (var currentAd in dbService.ToAsyncEnumerable())
        {
            await Task.Delay(randomDelay.Next(3000, 5000));
            var newPosition = await avitoInformationProvider.GetPositionAsync(currentAd);
            await dbService.UpdateCurrentPositionAsync(currentAd.PostID, newPosition);
            await dbService.AddNewPositionToStatistics(currentAd.PostID, newPosition);
        }
    }
}

 