using SP.Database.Mongo.Entity;

namespace SP.Database.Mongo.Feature.PackingList;

public class PackingListEntity : BaseEntity
{
    public string UserId { get; set; }

    public string Name { get; set; }

    public List<PackingListGroupsEntity> Groups { get; set; }
}