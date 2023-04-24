using HtmlAgilityPack;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

public class Scraping
{
    public static async Task<bool> CheckAdValidityAsync(string link)
    {
        var web = new HtmlWeb();
        await web.LoadFromWebAsync(link);
        return web.StatusCode == HttpStatusCode.OK;
    }

    public static int GetPosition(Ad ad, HtmlDocument searchResultsHtml)
    {
        var position = 0;
        var idOfAds = SelectAllIdOfAds(searchResultsHtml);
        if (idOfAds.Any(id => { position++; return id == ad.AdID; })) return position;
        else return 0;
    }

    public static string GetCity(HtmlDocument searchResultsHtml)
    {
        return searchResultsHtml.DocumentNode
            .Descendants("span")
            .FirstOrDefault(node => node.GetAttributeValue("class", "") == "desktop-nev1ty")?
            .InnerText;
    }

    private static IEnumerable<string> SelectAllIdOfAds(HtmlDocument htmlPageWithAds) =>
        htmlPageWithAds.DocumentNode
                    .Descendants("div")
                    .Where(node => node.GetAttributeValue("data-marker", "").Equals("item"))
                    .Select(node => node.GetAttributeValue("data-item-id", ""));
}