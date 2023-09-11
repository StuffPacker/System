namespace SP.Database.Mongo;

public class DataBaseFetchBase
{
    private readonly IDatabaseClient _databaseClient;

    public DataBaseFetchBase(IDatabaseClient databaseClient)
    {
        _databaseClient = databaseClient;
    }

    public async Task Create<T>(string name, T entity)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<T>(name);
        await collection.InsertOneAsync(entity);
    }
}