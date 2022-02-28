namespace BibleTextBot.Worker.ApplicationCore.Entities.BibleAggregate
{
    public class Language
    {
        public Language(string abbreviation, string description)
        {
            Abbreviation = abbreviation;
            Description = description;
        }

        public string Abbreviation { get; set; }

        public string Description { get; set; }
    }
}