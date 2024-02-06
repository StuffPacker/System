using SP.Database.Mongo.Entity;

namespace SP.Database.Mongo.Feature.PackingList;

public class PackingListEntity : BaseEntity
{
    public string UserId { get; set; }

    public string Name { get; set; }

    public List<PackingListGroupsEntity> Groups { get; set; }

    public bool IsPublic { get; set; }

    public string Language { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}