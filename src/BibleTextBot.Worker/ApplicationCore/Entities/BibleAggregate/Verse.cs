namespace BibleTextBot.Worker.ApplicationCore.Entities.BibleAggregate
{
    public class Verse
    {
        public Verse()
        {
        }

        public Verse(Int64 numeber, string text)
        {
            Number = numeber;
            Text = text;
        }

        public Int64 Number { get; set; }

        public string Text { get; set; }
    }
}