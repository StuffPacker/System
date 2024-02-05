namespace SP.Shared.Common.Feature.PackingList.Model;

public class PackingListModel
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<PackingListGroupModel> Groups { get; set; } = null!;

    public Guid UserId { get; set; }

    public bool IsPublic { get; set; }

    public string Language { get; set; } = string.Empty;
}