namespace BibleTextBot.Worker.ApplicationCore.Entities.BibleAggregate
{
    public class Chapter
    {
        public Chapter(int number, IEnumerable<Verse> verses)
        {
            Number = number;
            Verses = verses;
        }

        public int Number { get; set; }

        public IEnumerable<Verse> Verses { get; set; }
    }
}