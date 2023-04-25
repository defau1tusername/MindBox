using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

public class AvitoClient : IAvitoClient
{
    public static readonly HtmlWeb web = new HtmlWeb();

    public async Task<HtmlDocument> GetSearchResultsPageAsync(Ad ad)
    {
        try
        {
            return await web.LoadFromWebAsync(GetUrl(ad));
        }
        catch
        {
            return null;
        }
    }

    public string GetUrl(Ad ad)
    {
        var cityQuery = ad.Link.Split('/')[3];
        var keyWordsQuery = ad.KeyWords.Replace(' ', '+');
        return $"https://www.avito.ru/{cityQuery}?localPriority=1&q={keyWordsQuery}";
    }
}

