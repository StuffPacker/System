using MongoDB.Bson;
using MongoDB.Driver;
using SP.Shared.Common.Feature.Event.Model;

namespace SP.Database.Mongo.Feature.Event;

public class EventItemRepository : DataBaseFetchBase, IEventItemRepository
{
    private readonly IDatabaseClient _databaseClient;

    public EventItemRepository(IDatabaseClient databaseClient)
        : base(databaseClient)
    {
        _databaseClient = databaseClient;
    }

    public async Task<IEnumerable<EventModel>> Get()
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<EventEntity>(MongoDbNames.EventTableName);
        var filter = Builders<EventEntity>.Filter.Empty;

        // filter &= Builders<UserItemEntity>.Filter.Eq("Id", id);
        var results = (await collection.FindAsync(filter)).ToList();
        if (results == null)
        {
            return null!;
        }

        var list = new List<EventModel>();
        foreach (var item in results)
        {
            list.Add(GetModel(item));
        }

        return list;
    }

    public async Task<EventModel> Create(EventModel model)
    {
        var newid = ObjectId.GenerateNewId();
        model.Id = newid.ToString();
        var entity = GetEntity(model);
        await Create(MongoDbNames.EventTableName, entity);
        model.Id = entity.Id;
        return model;
    }

    private EventEntity GetEntity(EventModel model)
    {
        return new EventEntity
        {
            Id = model.Id,
            Name = model.Name
        };
    }

    private EventModel GetModel(EventEntity entity)
    {
        return new EventModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}