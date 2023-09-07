namespace SP.Web.Site.Features.PackingList;

public class PackListGroupModel
{
    public string Name { get; set; } = string.Empty;

    public List<PackingListGroupItemModel> Items { get; set; } = null!;
}