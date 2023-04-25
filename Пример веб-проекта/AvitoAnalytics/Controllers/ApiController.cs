using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
public class ApiController : ControllerBase
{
    private readonly DBService dbService;
    private readonly AvitoInformationProvider avitoInformationProvider;
    public ApiController(DBService dbService, AvitoInformationProvider avitoInformationProvider)
    { 
        this.dbService = dbService;
        this.avitoInformationProvider = avitoInformationProvider;
    }
    
    [Route("/monitoring")]
    [HttpGet]
    public IActionResult GetMonitoringPage() => Content(System.IO.File.ReadAllText("./html/monitoring.html"), "text/html");

    [Route("/api/ads")]
    [HttpGet]
    public async Task<IActionResult> GetAdsAsync()
    {
        var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return Ok(await dbService.GetAllAdsAsync(userId));
    }

    [Route("/api/ads")]
    [HttpPost]
    public async Task<IActionResult> CreateAdAsync([FromBody] AddingAddForm addingAddForm)
    {
        try
        {
            if (addingAddForm != null && await Scraping.CheckAdValidityAsync(addingAddForm.Link))
            {
                var newAd = new Ad()
                {
                    UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    AdID = addingAddForm.Link.Split('_')[^1],
                    KeyWords = addingAddForm.KeyWords,
                    Link = addingAddForm.Link,
                    Name = addingAddForm.Name
                };

                newAd.Position = await avitoInformationProvider.GetPositionAsync(newAd);
                newAd.City = await avitoInformationProvider.GetCityAsync(newAd);

                await dbService.CreateAdAsync(newAd);
                await dbService.AddNewPositionToStatistics(newAd.PostID, newAd.Position);

                return Ok(newAd);
            }
            else return NotFound();
        }
        catch
        {
            return NotFound();
        }
    }

    [Route("/api/ads/{guid:Guid}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAdAsync([FromRoute] Guid guid)
    {
        var foundAd = await dbService.GetAdAsync(guid);
        if (foundAd != null)
        {
            await dbService.RemoveAdAsync(guid);
            await dbService.RemovePositionStatisticsAsync(guid);
            return Ok(foundAd);
        }
        else
            return NotFound("Ошибка при удалении");
    }

    [Route("/statistics/{guid:Guid}")]
    [HttpGet]
    public IActionResult GetStatisticsPage() => Content(System.IO.File.ReadAllText("./html/statistics.html"), "text/html");

    [Route("/statistics/{guid:Guid}/positions")]
    [HttpGet]
    public async Task<IActionResult> GetAdNameAndPositionsAsync([FromRoute] Guid guid)
    {
        var positions = await dbService.GetAllPositionsAsync(guid);
        var adName = await dbService.GetAdNameAsync(guid);
        if (positions.Any() && adName != null)
            return Ok(new NameAndPositionStatistics(adName, positions));
        else
            return NotFound("Статистика позиций не найдена");
    }
}

