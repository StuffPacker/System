namespace SP.Shared.Common.Feature.PackingList.Model;

public class PackingListGroupItemModel
{
    public string Name { get; set; } = string.Empty;

    public decimal Weight { get; set; }

    public string WeightSufix { get; set; } = string.Empty;

    public string RefId { get; set; } = string.Empty;
}