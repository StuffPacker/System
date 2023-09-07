namespace SP.Web.Site.Features.Item;

public class ItemModel
{
    public string Name { get; set; } = string.Empty;

    public Guid Id { get; set; }

    public decimal Weight { get; set; }

    public string WeightSufix { get; set; } = string.Empty;
}