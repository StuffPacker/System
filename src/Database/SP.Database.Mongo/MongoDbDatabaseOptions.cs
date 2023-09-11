namespace SP.Database.Mongo;

public class MongoDbDatabaseOptions
{
    public string MongoDbConnectionString { get; set; } = string.Empty;

    public string MongoDbDatabase { get; set; } = string.Empty;
}