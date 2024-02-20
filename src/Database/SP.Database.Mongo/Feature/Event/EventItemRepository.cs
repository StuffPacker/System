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

    public async Task<IEnumerable<EventModel>> GetByUserId(Guid currentUserId)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<EventEntity>(MongoDbNames.EventTableName);
        var builder = Builders<EventEntity>.Filter;
        var filter = builder.Eq(f => f.UserId, currentUserId.ToString()) | builder.AnyEq(f => f.Users, currentUserId.ToString());

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

    public async Task Delete(string id)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<EventEntity>(MongoDbNames.EventTableName);
        await collection.DeleteOneAsync(x => x.Id == id.ToString());
    }

    public async Task<EventModel> GetById(string id)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<EventEntity>(MongoDbNames.EventTableName);
        var filter = Builders<EventEntity>.Filter.Eq("Id", id.ToString());

        var results = await (await collection.FindAsync(filter)).FirstOrDefaultAsync();
        if (results == null)
        {
            return null!;
        }

        return GetModel(results);
    }

    private EventEntity GetEntity(EventModel model)
    {
        return new EventEntity
        {
            Id = model.Id,
            Name = model.Name,
            UserId = model.UserId.ToString()
        };
    }

    private EventModel GetModel(EventEntity entity)
    {
        var users = new List<Guid>();
        if (entity.Users != null)
        {
            users = entity.Users.Select(user => Guid.Parse(user)).ToList();
        }

        return new EventModel
        {
            Id = entity.Id,
            Name = entity.Name,
            UserId = Guid.Parse(entity.UserId),
            Users = users
        };
    }
}