namespace SP.Shared.Common.Feature.PackingList.Dto;

public class ItemDto
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public decimal Weight { get; set; }

    public string WeightSufix { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
}