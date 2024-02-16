namespace SP.Shared.Common.Feature.Event.Model;

public class EventModel
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public List<Guid> Users { get; set; } = null!;
}