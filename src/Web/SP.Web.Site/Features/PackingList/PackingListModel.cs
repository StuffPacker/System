namespace SP.Web.Site.Features.PackingList;

public class PackingListModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<PackListGroupModel> Groups { get; set; } = null!;

    public Guid UserId { get; set; }
}