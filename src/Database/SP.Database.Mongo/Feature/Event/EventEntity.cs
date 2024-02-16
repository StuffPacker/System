using SP.Database.Mongo.Entity;

namespace SP.Database.Mongo.Feature.Event;

public class EventEntity : BaseEntity
{
    public EventEntity()
    {
        Users = new List<string>();
    }

    public string Name { get; set; }

    public string UserId { get; set; }

    public List<string> Users { get; set; }
}