namespace SP.Web.Site.Features.PackingList;

public class PackingListGroupItemModel
{
    public string Name { get; set; } = string.Empty;

    public decimal Weight { get; set; }

    public string WeightSufix { get; set; } = string.Empty;
}