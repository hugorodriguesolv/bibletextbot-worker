namespace BibleTextBot.Worker.ApplicationCore.Entities.BibleAggregate
{
    public class VersionBible
    {
        public VersionBible(string abbreviation, string name)
        {
            Abbreviation = abbreviation;
            Name = name;
        }

        public string Abbreviation { get; set; }

        public string Name { get; set; }
    }
}