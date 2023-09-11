using MongoDB.Bson;
using MongoDB.Driver;
using SP.Database.Mongo.Entity;
using SP.Shared.Common.Feature.Database.UserItem;
using SP.Shared.Common.Feature.Item.Model;

namespace SP.Database.Mongo.Feature.UserItem;

public class UserItemRepository : DataBaseFetchBase, IUserItemRepository
{
    private readonly IDatabaseClient _databaseClient;

    public UserItemRepository(IDatabaseClient databaseClient)
        : base(databaseClient)
    {
        _databaseClient = databaseClient;
    }

    public async Task<ItemModel> Create(ItemModel model)
    {
        var newid = ObjectId.GenerateNewId();
        model.Id = newid.ToString();
        var entity = GetEntity(model);
        await Create(MongoDbNames.UserItemTableName, entity);
        model.Id = entity.Id;
        return model;
    }

    public async Task<ItemModel> GetById(string id)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<UserItemEntity>(MongoDbNames.UserItemTableName);
        var filter = Builders<UserItemEntity>.Filter.Eq("Id", id.ToString());

        // filter &= Builders<UserItemEntity>.Filter.Eq("Id", id);
        var results = await (await collection.FindAsync(filter)).FirstOrDefaultAsync();
        if (results == null)
        {
            return null!;
        }

        return GetModel(results);
    }

    public async Task Update(ItemModel model)
    {
        var entity = GetEntity(model);
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<UserItemEntity>(MongoDbNames.UserItemTableName);
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task Delete(string id)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<UserItemEntity>(MongoDbNames.UserItemTableName);
        await collection.DeleteOneAsync(x => x.Id == id.ToString());
    }

    public async Task<IEnumerable<ItemModel>> GetByUserId(Guid userId)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<UserItemEntity>(MongoDbNames.UserItemTableName);
        var filter = Builders<UserItemEntity>.Filter.Eq("UserId", userId);
        var results = await (await collection.FindAsync(filter)).ToListAsync();

        var list = new List<ItemModel>();
        foreach (var item in results)
        {
            var i = item;
            list.Add(GetModel(i));
        }

        return list;
    }

    private UserItemEntity GetEntity(ItemModel model)
    {
        return new UserItemEntity()
        {
            Id = model.Id.ToString(),
            UserId = model.UserId.ToString(),
            Name = model.Name,
            WeightSufix = model.WeightSufix,
            Weight = model.Weight
        };
    }

    private ItemModel GetModel(UserItemEntity results)
    {
        return new ItemModel
        {
            Name = results.Name,
            Id = results.Id,
            WeightSufix = results.WeightSufix,
            UserId = Guid.Parse(results.UserId),
            Weight = results.Weight
        };
    }
}