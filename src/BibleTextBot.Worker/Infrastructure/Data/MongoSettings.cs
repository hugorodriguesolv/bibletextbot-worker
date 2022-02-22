namespace BibleTextBot.Worker.Infrastructure.Data
{
    public class MongoSettings
    {
        public string Connection { get; set; }

        public string DatabaseName { get; set; }

        public bool IsSSL { get; set; }
    }
}