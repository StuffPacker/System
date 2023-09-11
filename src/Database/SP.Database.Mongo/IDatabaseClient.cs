using MongoDB.Driver;

namespace SP.Database.Mongo;

public interface IDatabaseClient
{
    public IMongoDatabase GetDatabase();
}