using MongoDB.Bson;
using MongoDB.Driver;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Database.Mongo.Feature.PackingList;

public class PackingListRepository : DataBaseFetchBase, IPackingListRepository
{
    private readonly IDatabaseClient _databaseClient;

    public PackingListRepository(IDatabaseClient databaseClient)
        : base(databaseClient)
    {
        _databaseClient = databaseClient;
    }

    public async Task<PackingListModel> Create(PackingListModel model)
    {
        var newid = ObjectId.GenerateNewId();
        model.Id = newid.ToString();
        var entity = GetEntity(model);
        await Create(MongoDbNames.PackingListName, entity);
        model.Id = entity.Id;
        return model;
    }

    public async Task<PackingListModel> GetById(string id)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<PackingListEntity>(MongoDbNames.PackingListName);
        var filter = MongoDB.Driver.Builders<PackingListEntity>.Filter.Eq("Id", id.ToString());
        var results = await (await collection.FindAsync(filter)).FirstOrDefaultAsync();
        if (results == null)
        {
            return null!;
        }

        return GetModel(results);
    }

    public async Task Update(PackingListModel model)
    {
        var entity = GetEntity(model);
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<PackingListEntity>(MongoDbNames.PackingListName);
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task Delete(string id)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<PackingListEntity>(MongoDbNames.PackingListName);
        await collection.DeleteOneAsync(x => x.Id == id.ToString());
    }

    public async Task<IEnumerable<PackingListModel>> GetByUserId(Guid userId)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<PackingListEntity>(MongoDbNames.PackingListName);
        var filter = Builders<PackingListEntity>.Filter.Eq("UserId", userId);
        var results = await (await collection.FindAsync(filter)).ToListAsync();

        var list = new List<PackingListModel>();
        foreach (var item in results)
        {
            var i = item;
            list.Add(GetModel(i));
        }

        return list;
    }

    public async Task<IEnumerable<PackingListModel>> GetPublic()
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<PackingListEntity>(MongoDbNames.PackingListName);
        var filter = Builders<PackingListEntity>.Filter.Eq("IsPublic", true);
        var results = await (await collection.FindAsync(filter)).ToListAsync();

        var list = new List<PackingListModel>();
        foreach (var item in results)
        {
            var i = item;
            list.Add(GetModel(i));
        }

        return list;
    }

    private PackingListEntity GetEntity(PackingListModel model)
    {
        return new PackingListEntity()
        {
            Id = model.Id.ToString(),
            UserId = model.UserId.ToString(),
            Name = model.Name,
            Groups = GetGroups(model.Groups),
            IsPublic = model.IsPublic,
            Language = model.Language,
            Description = model.Description
        };
    }

    private List<PackingListGroupsEntity> GetGroups(List<PackingListGroupModel> modelGroups)
    {
        var list = new List<PackingListGroupsEntity>();
        foreach (var item in modelGroups)
        {
            list.Add(new PackingListGroupsEntity
            {
                Name = item.Name,
                Items = GetItems(item.Items),
                Id = item.Id
            });
        }

        return list;
    }

    private List<PackingListGroupsItemEntity> GetItems(List<PackingListGroupItemModel> itemItems)
    {
        var list = new List<PackingListGroupsItemEntity>();
        foreach (var item in itemItems)
        {
            list.Add(new PackingListGroupsItemEntity
            {
                Name = item.Name,
                Weight = item.Weight,
                WeightSufix = item.WeightSufix,
                RefId = item.RefId,
                Quantity = item.Quantity
            });
        }

        return list;
    }

    private PackingListModel GetModel(PackingListEntity results)
    {
        return new PackingListModel
        {
            Name = results.Name,
            Id = results.Id,
            Groups = GetGroupsModel(results.Groups),
            UserId = Guid.Parse(results.UserId),
            IsPublic = results.IsPublic,
            Language = results.Language,
            Description = results.Description
        };
    }

    private List<PackingListGroupModel> GetGroupsModel(List<PackingListGroupsEntity> resultsGroups)
    {
        var list = new List<PackingListGroupModel>();
        if (resultsGroups == null)
        {
            return list;
        }

        foreach (var item in resultsGroups)
        {
            list.Add(new PackingListGroupModel
            {
                Name = item.Name,
                Id = item.Id,
                Items = GetItemsModel(item.Items)
            });
        }

        return list;
    }

    private List<PackingListGroupItemModel> GetItemsModel(List<PackingListGroupsItemEntity> itemItems)
    {
        var list = new List<PackingListGroupItemModel>();
        foreach (var item in itemItems)
        {
            list.Add(new PackingListGroupItemModel
            {
                Name = item.Name,
                RefId = item.RefId,
                WeightSufix = item.WeightSufix,
                Weight = item.Weight,
                Quantity = item.Quantity
            });
        }

        return list;
    }
}