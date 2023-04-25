using HtmlAgilityPack;
using System.Threading.Tasks;

public interface IAvitoClient
{
    public Task<HtmlDocument> GetSearchResultsPageAsync(Ad ad);
}