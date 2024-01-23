namespace SP.Shared.Common.Feature.PackingList.Dto;

public class PackingListGroupDto
{
    public List<PackingListGroupItemDto> Items { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;
}