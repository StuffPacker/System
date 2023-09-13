namespace SP.Shared.Common.Feature.PackingList.Model;

public class PackListGroupModel
{
    public string Name { get; set; } = string.Empty;

    public List<PackingListGroupItemModel> Items { get; set; } = null!;

    public string Id { get; set; } = string.Empty;
}