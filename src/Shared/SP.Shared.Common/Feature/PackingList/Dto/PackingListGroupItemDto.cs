namespace SP.Shared.Common.Feature.PackingList.Dto;

public class PackingListGroupItemDto
{
    public string Name { get; set; } = string.Empty;

    public string WeightSufix { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;

    public bool Weight { get; set; }

    public int Quantity { get; set; }
}