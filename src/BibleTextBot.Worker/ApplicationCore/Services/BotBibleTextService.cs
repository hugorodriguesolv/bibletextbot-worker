namespace BibleTextBot.Worker.ApplicationCore.Services;

using Entities.BibleAggregate;
using Infrastructure.Services;
using Interfaces;
using System.Text.RegularExpressions;

public class BotBibleTextService : IBotBibleTextService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<BotBibleTextService> _logger;
    private readonly IContext _context;
    private readonly SettingBibleOnline _settingBibleOnline;

    public BotBibleTextService(
        IConfiguration configuration,
        ILogger<BotBibleTextService> logger,
        IContext context)
    {
        _configuration = configuration;
        _logger = logger;
        _context = context;

        _settingBibleOnline = _configuration
            .GetSection("SettingBibleOnline")
            .Get<SettingBibleOnline>();
    }

    public async void GetBibleTextAsync()
    {
        var bibleTextFilter = new BibleTextFilter();

        foreach (var version in bibleTextFilter.VersoesFilter)
        {
            var bible = new Bible();
            var bibleVersion = new VersionBible(version.Abbreviation, version.Description);

            foreach (var testament in version.TestamentFilters)
            {
                var bibleTestament = new Testament(testament.Abbreviation, testament.Description);

                foreach (var book in testament.booksFilter)
                {
                    var url = $"{_settingBibleOnline.UrlBible}{bibleVersion.Abbreviation.Trim()}/{book.Abbreviation.Trim()}".ToLower();
                    var htmlDocument = await TextoUrlService.ObterHtmlDocumentAsync(url);
                    var chapterCount = htmlDocument
                        .DocumentNode
                        .SelectNodes("//ul[@class='jss1']")
                        .Descendants("li")
                        .Count();

                    var bibleChapters = new List<Chapter>();

                    for (int chapterNumber = 1; chapterNumber <= chapterCount; chapterNumber++)
                    {
                        htmlDocument = await TextoUrlService.ObterHtmlDocumentAsync($"{url}/{chapterNumber}");
                        var verses = htmlDocument
                            .DocumentNode
                            .Descendants("p")
                            .Select(vrs => new Verse()
                            {
                                Number = Int64.Parse(Regex.Replace(vrs.InnerText, @"[^\d]", "")),
                                Text = System.Web.HttpUtility.HtmlDecode(vrs.InnerText.Substring(vrs.InnerText.IndexOf(" ") + 1))
                            })
                            .ToList();

                        bibleChapters.Add(new Chapter(chapterNumber, verses));
                    }

                    bible.AddBook(new Book(book.Abbreviation, book.Description, bibleVersion, bibleTestament, bibleChapters));
                }
            }

            RegisterBiblicalTextAsync(bible);
        }
    }

    private async void RegisterBiblicalTextAsync(Bible bible)
    {
        await _context
            .GetCollection<Book>()
            .InsertManyAsync(bible.BookItems);
    }
}