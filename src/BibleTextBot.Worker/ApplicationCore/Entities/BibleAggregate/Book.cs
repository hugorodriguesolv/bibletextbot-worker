namespace BibleTextBot.Worker.ApplicationCore.Entities.BibleAggregate
{
    public class Book
    {
        public Book(string abbreviation, string name, VersionBible version, Testament testament, IEnumerable<Chapter> chapters)
        {
            Abbreviation = abbreviation;
            Name = name;
            Version = version;
            Testament = testament;
            Chapters = chapters;
        }

        [MongoDB.Bson.Serialization.Attributes.BsonId]
        [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string Abbreviation { get; set; }

        public string Name { get; set; }

        public VersionBible Version { get; set; }

        public Testament Testament { get; set; }

        public IEnumerable<Chapter> Chapters { get; set; }



    }
}