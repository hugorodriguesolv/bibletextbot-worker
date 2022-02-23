namespace BibleTextBot.Worker.Infrastructure.Services;

using HtmlAgilityPack;

public class TextoUrlService
{
    public static async Task<HtmlDocument> ObterHtmlDocumentAsync(string url)
    {
        var htmlDocument = new HtmlDocument();

        using (var httpCliente = new HttpClient())
        {
            var responseHtml = await httpCliente.GetStringAsync(url);
            htmlDocument.LoadHtml(responseHtml);
        }

        return htmlDocument;
    }
}