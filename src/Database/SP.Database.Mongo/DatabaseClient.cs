using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace SP.Database.Mongo;

public class DatabaseClient : IDatabaseClient
{
    private readonly MongoDbDatabaseOptions _databaseOptions;

    public DatabaseClient(IOptions<MongoDbDatabaseOptions> databaseOptions)
    {
        _databaseOptions = databaseOptions.Value;
    }

    public IMongoDatabase GetDatabase()
    {
        var client = GetClient();
        var db = client.GetDatabase(_databaseOptions.MongoDbDatabase);
        return db;
    }

    public MongoClient GetClient()
    {
        return new MongoClient(_databaseOptions.MongoDbConnectionString);
    }
}