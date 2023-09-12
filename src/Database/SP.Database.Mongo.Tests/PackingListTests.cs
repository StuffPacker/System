using FluentAssertions;
using Microsoft.Extensions.Options;
using SP.Database.Mongo.Feature.PackingList;
using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Database.Mongo.Tests;

[TestClass]
public class PackingListTests
{
    [TestMethod]
    public async Task CRUD()
    {
        var userId = Guid.NewGuid();
        var model = new PackingListModel()
        {
            Name = "name1",
            Groups = new List<PackListGroupModel>(),
            UserId = userId
        };
        var name2 = "name2";
        var target = GetTarget();
        var m1 = await target.Create(model);
        var m2 = await target.Create(model);
        var result1 = await target.GetById(m1.Id);
        result1.Should().NotBeNull();
        model.Name = name2;
        await target.Update(model);
        var result2 = await target.GetById(m1.Id);
        result2.Name.Should().BeEquivalentTo(name2);

        var list = await target.GetByUserId(userId);
        list.Count().Should().BeGreaterThan(1);
        foreach (var item in list)
        {
            await target.Delete(item.Id);
        }

        var result3 = await target.GetById(m1.Id);
        result3.Should().BeNull();
    }

    private IPackingListRepository GetTarget()
    {
        IOptions<MongoDbDatabaseOptions> databaseOptions = Options.Create<MongoDbDatabaseOptions>(new MongoDbDatabaseOptions
        {
            MongoDbDatabase = "StuffPacker",
            MongoDbConnectionString = "mongodb://localhost:41017"
        });
        var databaseClient = new DatabaseClient(databaseOptions);
        return new PackingListRepository(databaseClient);
    }
}