namespace BibleTextBot.Worker.ApplicationCore;

public class BibleTextFilter
{
    private TestamentFilter oldTestament;

    private TestamentFilter newTestament;

    private LanguageFilter portugueseBrazil;

    private LanguageFilter englishUnitedStates;

    private LanguageFilter spanish;

    private IList<BookFilter> booksOT;

    private IList<BookFilter> booksNT;

    public BibleTextFilter()
    {
        portugueseBrazil = new LanguageFilter("pt-BR", "Portuguese (Brazil)");
        englishUnitedStates = new LanguageFilter("en-US", "English (United States)");
        spanish = new LanguageFilter("es-ES", "Spanish");

        booksOT = new List<BookFilter>()
        {
            new BookFilter("gn", "Gênesis"),
            new BookFilter("ex", "Êxodo"),
            new BookFilter("lv", "Levítico"),
            new BookFilter("nm", "Números"),
            new BookFilter("dt", "Deuteronômio"),
            new BookFilter("js", "Josué"),
            new BookFilter("jz", "Juízes"),
            new BookFilter("rt", "Rute"),
            new BookFilter("1sm", "1 Samuel"),
            new BookFilter("2sm", "2 Samuel"),
            new BookFilter("1rs", "1 Reis"),
            new BookFilter("2rs", "2 Reis"),
            new BookFilter("1cr", "1 Crônicas"),
            new BookFilter("2cr", "2 Crônicas"),
            new BookFilter("ed", "Esdras"),
            new BookFilter("ne", "Neemias"),
            new BookFilter("et", "Ester"),
            new BookFilter("jó", "Jó"),
            new BookFilter("sl", "Salmos"),
            new BookFilter("pv", "Provérbios"),
            new BookFilter("ec", "Eclesiastes"),
            new BookFilter("ct", "Cânticos"),
            new BookFilter("is", "Isaías"),
            new BookFilter("jr", "Jeremias"),
            new BookFilter("lm", "Lamentações"),
            new BookFilter("ez", "Ezequiel"),
            new BookFilter("dn", "Daniel"),
            new BookFilter("os", "Oséias"),
            new BookFilter("jl", "Joel"),
            new BookFilter("am", "Amós"),
            new BookFilter("ob", "Obadias"),
            new BookFilter("jn", "Jonas"),
            new BookFilter("mq", "Miquéias"),
            new BookFilter("na", "Naum"),
            new BookFilter("hc", "Habacuque"),
            new BookFilter("sf", "Sofonias"),
            new BookFilter("ag", "Ageu"),
            new BookFilter("zc", "Zacarias"),
            new BookFilter("ml", "Malaquias"),
        };

        booksNT = new List<BookFilter>()
        {
            new BookFilter("mt", "Mateus"),
            new BookFilter("mc", "Marcos"),
            new BookFilter("lc", "Lucas"),
            new BookFilter("jo", "João"),
            new BookFilter("atos", "Atos"),
            new BookFilter("rm", "Romanos"),
            new BookFilter("1co", "1 Coríntios"),
            new BookFilter("2co", "2 Coríntios"),
            new BookFilter("gl", "Gálatas"),
            new BookFilter("ef", "Efésios"),
            new BookFilter("fp", "Filipenses"),
            new BookFilter("cl", "Colossenses"),
            new BookFilter("1ts", "1 Tessalonicenses"),
            new BookFilter("2ts", "2 Tessalonicenses"),
            new BookFilter("1tm", "1 Timóteo"),
            new BookFilter("2tm", "2 Timóteo"),
            new BookFilter("tt", "Tito"),
            new BookFilter("fm", "Filemom"),
            new BookFilter("hb", "Hebreus"),
            new BookFilter("tg", "Tiago"),
            new BookFilter("1pe", "1 Pedro"),
            new BookFilter("2pe", "2 Pedro"),
            new BookFilter("1jo", "1 João"),
            new BookFilter("2jo", "2 João"),
            new BookFilter("3jo", "3 João"),
            new BookFilter("jd", "Judas"),
            new BookFilter("ap", "Apocalipse")
        };

        oldTestament = new TestamentFilter("AT", "Antigo Testamento", booksOT);
        newTestament = new TestamentFilter("NT", "Novo Testamento", booksNT);
    }

    public IList<VersionFilter> VersoesFilter => new List<VersionFilter>()
    {
        new VersionFilter("NVI ", "Nova Versão Internacional", portugueseBrazil)
        {
            TestamentFilters = new List<TestamentFilter>()
            {
                oldTestament,
                newTestament
            }
        },
        new VersionFilter("ARA ", "Almeida Revista e Atualizada ", portugueseBrazil)
        {
            TestamentFilters = new List<TestamentFilter>()
            {
                oldTestament,
                newTestament
            }
        },
        new VersionFilter("ARC ", "Almeida Revista e Corrigida", portugueseBrazil)
        {
            TestamentFilters = new List<TestamentFilter>()
            {
                oldTestament,
                newTestament
            }
        },
        new VersionFilter("NVT ", "Nova Versão Transformadora", portugueseBrazil)
        {
            TestamentFilters = new List<TestamentFilter>()
            {
                oldTestament,
                newTestament
            }
        },
        new VersionFilter("NAA ", "Nova Almeida Atualizada", portugueseBrazil)
                {
            TestamentFilters = new List<TestamentFilter>()
            {
                oldTestament,
                newTestament
            }
        },
        new VersionFilter("ACF ", "Almeida Corrigida Fiel", portugueseBrazil)
                {
            TestamentFilters = new List<TestamentFilter>()
            {
                oldTestament,
                newTestament
            }
        },
        new VersionFilter("AKJV", "American King James Version", englishUnitedStates)
        {
            TestamentFilters = new List<TestamentFilter>()
            {
                oldTestament,
                newTestament
            }
        },
        new VersionFilter("NIV", "New International Version", englishUnitedStates)
        {
            TestamentFilters = new List<TestamentFilter>()
            {
                oldTestament,
                newTestament
            }
        },
    };
}

public class VersionFilter
{
    public VersionFilter(string abbreviation, string description, LanguageFilter languageFilter)
    {
        Abbreviation = abbreviation;
        Description = description;
        LanguageFilter = languageFilter;
    }

    public string Abbreviation { get; set; }

    public string Description { get; set; }

    public LanguageFilter LanguageFilter { get; set; }

    public IList<TestamentFilter> TestamentFilters { get; set; }
}

public class LanguageFilter
{
    public LanguageFilter(string abbreviation, string description)
    {
        Abbreviation = abbreviation;
        Description = description;
    }

    public string Abbreviation { get; set; }

    public string Description { get; set; }
}

public class TestamentFilter
{
    public TestamentFilter(string abbreviation, string description, IList<BookFilter> booksFilter)
    {
        Abbreviation = abbreviation;
        Description = description;
        this.booksFilter = booksFilter;
    }

    public string Abbreviation { get; set; }

    public string Description { get; set; }

    public IList<BookFilter> booksFilter { get; set; }
}

public class BookFilter
{
    public BookFilter(string abbreviation, string description)
    {
        Abbreviation = abbreviation;
        Description = description;
    }

    public string Abbreviation { get; set; }

    public string Description { get; set; }
}