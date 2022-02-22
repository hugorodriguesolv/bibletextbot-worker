namespace BibleTextBot.Worker.ApplicationCore.Entities.BibleAggregate
{
    public class Testament
    {
        public Testament(string abbreviation, string name)
        {
            Abbreviation = abbreviation;
            Name = name;
        }

        public string Abbreviation { get; set; }

        public string Name { get; set; }
    }
}