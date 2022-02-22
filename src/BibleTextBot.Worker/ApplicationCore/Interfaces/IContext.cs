using MongoDB.Driver;

namespace BibleTextBot.Worker.ApplicationCore.Interfaces
{
    public interface IContext
    {
        IMongoCollection<T> GetCollection<T>();
    }
}