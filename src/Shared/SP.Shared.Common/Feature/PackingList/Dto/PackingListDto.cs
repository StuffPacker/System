namespace SP.Shared.Common.Feature.PackingList.Dto;

public class PackingListDto
{
    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<PackingListGroupDto> Groups { get; set; } = null!;

    public string Id { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public string Language { get; set; } = string.Empty;
}