using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Database.Mongo.Feature.PackingList;

public class PackingListGroupsEntity
{
    public string Name { get; set; } = string.Empty;

    public List<PackingListGroupsItemEntity> Items { get; set; }
}