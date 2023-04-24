using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly DBService dbService;

    public AdminController(DBService dbService)
    {
        this.dbService = dbService;
    }

    [Route("/admin")]
    [HttpGet]
    public IActionResult GetMonitoringPage() => Content(System.IO.File.ReadAllText("./html/admin_index.html"), "text/html");

    [Route("/admin/users")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var response = new List<admin_UsersInfo>();
        var users = await dbService.GetAllUsersAsync();
        foreach (var user in users)
        {
            var adsCount = await dbService.NumberOfAdsPerUser(user.UserId);
            response.Add(new admin_UsersInfo() { Id = user.UserId, Name = user.Name, adsCount = adsCount });
        }
        if (response.Any())
            return Ok(response);

        else return NotFound();
    }

    [Route("/admin/log")]
    [HttpGet]
    public IActionResult GetLogFileAsync()
    {
        var month = DateTime.Now.Month < 10 
            ? "0" + DateTime.Now.Month 
            : DateTime.Now.Month.ToString();

        var logName = $"webapi-{DateTime.Now.Year + "" + month + "" + DateTime.Now.Day}";
        var path = $"../logs/{logName}.log";

        System.IO.File.Copy(path, "../logs/tempLog.log", true);
        var pathToTempLogFile = @"../logs/tempLog.log"; 

        using var fs = new FileStream(pathToTempLogFile, FileMode.Open, FileAccess.Read);
        using var sr = new StreamReader(fs, Encoding.UTF8);
        string content = sr.ReadToEnd();

        return Content(content);
    }



}

