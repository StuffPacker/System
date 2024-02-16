namespace SP.Shared.Common.Feature.Event.Dto;

public class EventDto
{
    public EventDto()
    {
        Users = new List<string>();
    }

    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public List<string> Users { get; set; }
}